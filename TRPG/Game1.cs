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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private AnimatedSprite walking;
        private FacingWhichSide side;

        private TiledBackground background;

        private Vector2 playerPos;
        private float playerSpeed;

        //properties
        public int ScreenWidth { get => graphics.PreferredBackBufferWidth; set { graphics.PreferredBackBufferWidth = value; graphics.ApplyChanges(); } }
        public int ScreenHeight { get => graphics.PreferredBackBufferHeight; set { graphics.PreferredBackBufferHeight = value; graphics.ApplyChanges(); } }
        //constructors
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            playerPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            playerSpeed = 100f;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Texture2D texture = Content.Load<Texture2D>("WalkingWithFlip");
            walking = new AnimatedSprite(texture, 4, 9);
            side = FacingWhichSide.Right;

            Texture2D texture1 = Content.Load<Texture2D>("Background");
            background = new TiledBackground(texture1, 7, 8, ScreenWidth, ScreenHeight);
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
            var kstate = Keyboard.GetState();
            float distance = playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Up))
            {
                if (background.Move(distance, Direction.South))
                {
                    if (playerPos.Y - distance >= walking.Height) { playerPos.Y -= distance; }
                }
                else
                {
                    if (playerPos.Y - distance >= 0) { playerPos.Y -= distance; }
                }
                walking.Update(side);
            }

            else if (kstate.IsKeyDown(Keys.Down)) 
            {
                if (background.Move(distance, Direction.North))
                {
                    if (playerPos.Y + distance < ScreenHeight - 2 * walking.Height) { playerPos.Y += distance; }
                }
                else
                {
                    if (playerPos.Y + distance < ScreenHeight - walking.Height) { playerPos.Y += distance; }
                }
                walking.Update(side);
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                if (background.Move(distance, Direction.East))
                {
                    if (playerPos.X - distance > walking.Width) { playerPos.X -= distance; }
                }
                else
                {
                    if (playerPos.X - distance > 0) { playerPos.X -= distance; }
                }
                side = FacingWhichSide.Left;
                walking.Update(side);
            }

            else if (kstate.IsKeyDown(Keys.Right))
            {
                if (background.Move(distance, Direction.West))
                {
                    if (playerPos.X + distance < ScreenWidth - 2 * walking.Width) { playerPos.X += distance; }
                }
                else
                {
                    if (playerPos.X + distance < ScreenWidth - walking.Width) { playerPos.X += distance; }
                }
                side = FacingWhichSide.Right;
                walking.Update(side);
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
            background.Draw(spriteBatch);
            walking.Draw(spriteBatch, playerPos);

            base.Draw(gameTime);
        }
    }
}
