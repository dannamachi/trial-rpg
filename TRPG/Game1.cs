using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TRPG.src;

namespace TRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private Item _foodChicken;

        private StaticSprite _inventoryWindow;
        private StaticSprite _inventorySlots;
        private Button _button;
        private float _slotX, _slotY;

        private StaticSprite _guibackground;
        private TiledBackground _background;

        private bool _showingInventory;
        private MouseState _currentMS, _lastMS;
        //properties
        public int ScreenWidth { get => _graphics.PreferredBackBufferWidth; set { _graphics.PreferredBackBufferWidth = value; _graphics.ApplyChanges(); } }
        public int ScreenHeight { get => _graphics.PreferredBackBufferHeight; set { _graphics.PreferredBackBufferHeight = value; _graphics.ApplyChanges(); } }
        //constructors
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _showingInventory = false;
        }
        //methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player();
            _foodChicken = new Item("Chicken");
            _button = new Button();
            IsMouseVisible = true;
            _currentMS = Mouse.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Texture2D texture = Content.Load<Texture2D>("WalkingWithFlip");
            _player.SetSprite(texture,4,9);
            Texture2D texture2 = Content.Load<Texture2D>("FoodChicken");
            _foodChicken.Sprite = new StaticSprite(texture2, 0, 0, texture2.Width, texture2.Height);
            _foodChicken.Location = new Vector2(100, 100);

            Texture2D button = Content.Load<Texture2D>("ButtonInventory");
            _button.SetSprite(button, 0, 0, button.Width, button.Height);
            _button.Resize(100, 100);
            _button.Location = new Vector2(ScreenWidth - _button.WidthDrawn, 0);

            Texture2D inventoryWin = Content.Load<Texture2D>("InventoryWindow");
            Texture2D inventorySlot = Content.Load<Texture2D>("InventorySlots");
            _inventoryWindow = new StaticSprite(inventoryWin);
            _inventoryWindow.WidthDrawn = ScreenWidth - _button.WidthDrawn;
            _inventoryWindow.HeightDrawn = ScreenHeight;
            _inventorySlots = new StaticSprite(inventorySlot);
            _inventorySlots.HeightDrawn = _inventoryWindow.HeightDrawn - 150;
            _slotX = 38;
            _slotY = 110;

            Texture2D texture1 = Content.Load<Texture2D>("Background");
            _background = new TiledBackground(texture1, 7, 8, ScreenWidth, ScreenHeight);
            Texture2D guibg = Content.Load<Texture2D>("gui_background");
            _guibackground = new StaticSprite(guibg);
            _guibackground.WidthDrawn = ScreenWidth;
            _guibackground.HeightDrawn = ScreenHeight;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //getState
            _lastMS = _currentMS;
            var kstate = Keyboard.GetState();
            _currentMS = Mouse.GetState();
            float distance = _player.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_currentMS.LeftButton == ButtonState.Released && _lastMS.LeftButton == ButtonState.Pressed)
            {
                if (_button.IsPressed(_lastMS.Position)) { _showingInventory = !_showingInventory; }
            }

            if (_showingInventory)
            {
                if (kstate.IsKeyDown(Keys.A)) { if (_slotX + _inventorySlots.WidthDrawn > 676) { _slotX -= 10; } }
                if (kstate.IsKeyDown(Keys.D)) { if (_slotX < 38) { _slotX += 10; } }
            }

            if (kstate.IsKeyDown(Keys.X)) { _player.Resize(215, 165); }
            if (kstate.IsKeyDown(Keys.Z)) { _player.Resize(107, 83); }

            if (kstate.IsKeyDown(Keys.Space)) { _player.Take(_foodChicken); }

            if (kstate.IsKeyDown(Keys.Up))
            {
                if (!_background.Move(distance, Direction.South))
                {
                    if (_player.WillBeWithinRect(distance, Direction.North, new Rectangle(0, 0, ScreenWidth, ScreenHeight)))
                        _player.Move(Direction.North, distance);
                }
                else
                {
                    if (_player.WillBeWithinRect(distance, Direction.North, new Rectangle(0, ScreenHeight / 3, ScreenWidth, ScreenHeight * 2 / 3)))
                        _player.Move(Direction.North, distance);
                }
                _player.UpdateAnimation();
            }

            else if (kstate.IsKeyDown(Keys.Down)) 
            {
                if (!_background.Move(distance, Direction.North))
                {
                    if (_player.WillBeWithinRect(distance, Direction.South, new Rectangle(0, 0, ScreenWidth, ScreenHeight)))
                        _player.Move(Direction.South, distance); 
                }
                else
                {
                    if (_player.WillBeWithinRect(distance, Direction.South, new Rectangle(0, 0, ScreenWidth, ScreenHeight * 2 / 3)))
                        _player.Move(Direction.South, distance);
                }
                _player.UpdateAnimation();
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                _player.FaceTo(FacingWhichSide.Left);
                if (!_background.Move(distance, Direction.East))
                {
                    if (_player.WillBeWithinRect(distance, Direction.West, new Rectangle(0, 0, ScreenWidth, ScreenHeight)))
                        _player.Move(Direction.West, distance);
                }
                else
                {
                    if (_player.WillBeWithinRect(distance, Direction.West, new Rectangle(ScreenWidth / 3, 0, ScreenWidth * 2 / 3, ScreenHeight / 3)))
                        _player.Move(Direction.West, distance);
                }
                _player.UpdateAnimation();
            }

            else if (kstate.IsKeyDown(Keys.Right))
            {
                _player.FaceTo(FacingWhichSide.Right);
                if (!_background.Move(distance, Direction.West))
                {
                    if (_player.WillBeWithinRect(distance, Direction.East, new Rectangle(0, 0, ScreenWidth, ScreenHeight)))
                        _player.Move(Direction.East, distance);
                }
                else
                {
                    if (_player.WillBeWithinRect(distance, Direction.East, new Rectangle(0, 0, ScreenWidth * 2 / 3, ScreenHeight)))
                        _player.Move(Direction.East, distance);
                }
                _player.UpdateAnimation();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            if (!_showingInventory)
            {
                _background.Draw(_spriteBatch);
                _button.Draw(_spriteBatch);
                if (!_player.Have("Chicken"))
                    _foodChicken.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
            }
            else
            {
                _guibackground.Draw(_spriteBatch, new Vector2(0,0));
                _button.Draw(_spriteBatch);
                _inventorySlots.Draw(_spriteBatch, new Vector2(_slotX,_slotY));
                _inventoryWindow.Draw(_spriteBatch, new Vector2(0, 0));

            }

            base.Draw(gameTime);
        }
    }
}
