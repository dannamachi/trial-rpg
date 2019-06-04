using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using TRPG.src;
using TRPG.src.modules;

namespace TRPG
{
    public enum GameState
    {
        Exploring,
        Pausing,
        Talking,
        Warning
    }
    public enum Showing
    {
        Inventory,
        Status,
        Quest,
        Map,
        Settings,
        Nothing
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //fields
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static SpriteFont _font20;

        private Player _player;

        private Button _button;
        private Button _button1;
        private Button _button2;
        private Button _button3;
        private Button _button4;

        private AlertRect _alertBox;

        private StaticSprite _guibackground;
        private TiledBackground _background;

        private bool _playingMusic;
        private GameState _gameState;
        private MouseState _currentMS, _lastMS;

        private Song _bgm;
        //properties
        private Showing Showing { get; set; }
        public bool IsLeftClicked
        {
            get { return _currentMS.LeftButton == ButtonState.Released && _lastMS.LeftButton == ButtonState.Pressed; }
        }
        public TiledBackground GameBackground { get => _background; }
        public Player Player { get => _player; }
        public static SpriteFont Font20 { get => _font20; }
        public static int ScreenWidth { get => _graphics.PreferredBackBufferWidth; set { _graphics.PreferredBackBufferWidth = value; _graphics.ApplyChanges(); } }
        public static int ScreenHeight { get => _graphics.PreferredBackBufferHeight; set { _graphics.PreferredBackBufferHeight = value; _graphics.ApplyChanges(); } }
        //constructors
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _playingMusic = false;
            MediaPlayer.IsRepeating = true;
            ScreenHeight = 500;
        }
        //methods
        public void CloseAlert()
        {
            _gameState = GameState.Exploring;
        }

        public void PlayMusic()
        {
            _playingMusic = true;
            MediaPlayer.Play(_bgm);
            CloseAlert();
        }
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
            _button = new Button();
            _button1 = new Button();
            _button2 = new Button();
            _button3 = new Button();
            _button4 = new Button();


            IsMouseVisible = true;
            _currentMS = Mouse.GetState();
            _gameState = GameState.Exploring;
            Showing = Showing.Nothing;

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

            Texture2D texture = Content.Load<Texture2D>("sprites/WalkingWithFlip");
            _player.SetSprite(texture,4,9);

            _font20 = Content.Load<SpriteFont>("fonts/Basic20");


            Texture2D button = Content.Load<Texture2D>("sprites/ButtonInventory");
            _button.SetSprite(button, 0, 0, button.Width, button.Height);
            _button.Resize(100, 100);
            _button.Location = new Vector2(ScreenWidth - _button.WidthDrawn, 0);

            Texture2D button1 = Content.Load<Texture2D>("sprites/ButtonMusic");
            _button1.SetSprite(button1, 0, 0, button1.Width, button1.Height);
            _button1.Resize(100, 100);
            _button1.Location = new Vector2(ScreenWidth - _button1.WidthDrawn, 100);

            Texture2D button2 = Content.Load<Texture2D>("sprites/ButtonTake");
            _button2.SetSprite(button2, 0, 0, button2.Width, button2.Height);
            _button2.Resize(100, 100);
            _button2.Location = new Vector2(ScreenWidth - _button2.WidthDrawn, 200);

            Texture2D button3 = Content.Load<Texture2D>("sprites/ButtonUse");
            _button3.SetSprite(button3, 0, 0, button3.Width, button3.Height);
            _button3.Resize(100, 100);
            _button3.Location = new Vector2(ScreenWidth - _button3.WidthDrawn, 200);

            Texture2D button4 = Content.Load<Texture2D>("sprites/ButtonQuit");
            _button4.SetSprite(button4, 0, 0, button4.Width, button4.Height);
            _button4.Resize(100, 100);
            _button4.Location = new Vector2(ScreenWidth - _button3.WidthDrawn, 400);

            Texture2D inventoryWin = Content.Load<Texture2D>("sprites/InventoryWindow");
            Texture2D inventorySlot = Content.Load<Texture2D>("sprites/InventorySlots");
            Texture2D alert = Content.Load<Texture2D>("sprites/Popup");
            _player.Inventory.SetSprite(inventoryWin, inventorySlot, alert);

            Texture2D texture1 = Content.Load<Texture2D>("sprites/Background");
            _background = new TiledBackground(texture1, 7, 8, ScreenWidth, ScreenHeight);
            Texture2D guibg = Content.Load<Texture2D>("sprites/gui_background");
            _guibackground = new StaticSprite(guibg);
            _guibackground.WidthDrawn = ScreenWidth;
            _guibackground.HeightDrawn = ScreenHeight;

            _bgm = Content.Load<Song>("tracks/BGM");

            Texture2D yesno = Content.Load<Texture2D>("sprites/buttonYesNo");
            _alertBox = new AlertRect(alert, yesno, 300, 200, new Vector2(100,100));
            _alertBox.ClickYes += new InteractiveRect.ClickEvent(CloseAlert);
            _alertBox.ClickNo += new InteractiveRect.ClickEvent(PlayMusic);

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

            if (IsLeftClicked)
            {
                if (_gameState == GameState.Warning)
                {
                    _alertBox.CheckEvent(_lastMS.Position);
                }

                if (_button.IsPressed(_lastMS.Position))
                {
                    if (Showing != Showing.Inventory) { _player.Inventory.ResetScroll(); }
                    _gameState = GameState.Pausing;
                    Showing = Showing.Inventory; 
                }
                
                if (_button1.IsPressed(_lastMS.Position)) { _playingMusic = !_playingMusic; if (_playingMusic) { MediaPlayer.Play(_bgm); } else { MediaPlayer.Stop(); } }

                if (_button4.IsPressed(_lastMS.Position)) { _gameState = GameState.Warning; }

            }

            if (Showing == Showing.Inventory)
            {
                UserInput.ProcessScroll(_player, kstate);
                _player.Inventory.CheckShowDetail(_currentMS.Position);
            }

            if (_gameState == GameState.Exploring)
                UserInput.ProcessMovement(distance, _player, _background, kstate);

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //Exit();

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
            if (_gameState == GameState.Exploring)
            {
                _background.Draw(_spriteBatch);
                _button.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                _button2.Draw(_spriteBatch);
            }
            else if (_gameState == GameState.Pausing)
            {
                if (Showing == Showing.Inventory) {
                    _guibackground.Draw(_spriteBatch, new Vector2(0, 0));
                    _player.Inventory.Draw(_spriteBatch, new Rectangle(0, 0, ScreenWidth - _button.WidthDrawn, ScreenHeight));
                    _button3.Draw(_spriteBatch);
                    _spriteBatch.Begin();
                    _spriteBatch.DrawString(_font20, "Inventory", new Vector2(150, 50), Color.Red);
                    _spriteBatch.End();
                }
            }
            _button.Draw(_spriteBatch);
            _button1.Draw(_spriteBatch);
            _button4.Draw(_spriteBatch);

            if (_gameState == GameState.Warning) {
                _alertBox.Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
