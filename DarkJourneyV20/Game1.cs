using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using SEVirtual;
using SEGraphic;

namespace DarkJourneyV20
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _sb;
        SpriteFont _font, _fontsmall;
        GameLoop _loop;
        Texture2D _common, _gui;
        Texture2D _title, _default;

        //TEST
        static SpriteFont _fontest;
        static SpriteBatch _sbtest;
        static bool test1 = true;
        static string alert = "HELLO";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameLoop.LoadTexture = LoadMapTexture;
        }

        //test draw
        public static void Debug(string name = "HELLO")
        {
            test1 = false;
            alert = name;
        }
        //load map-spec texture
        private void LoadMapTexture(string mapname)
        {
            Texture2D artifacts = Content.Load<Texture2D>(mapname + "/artifacts");
            Texture2D characters = Content.Load<Texture2D>(mapname + "/characters");
            Texture2D objects = Content.Load<Texture2D>(mapname + "/objects");
            Texture2D tiles = Content.Load<Texture2D>(mapname + "/tiles");
            Texture2D bgs = Content.Load<Texture2D>(mapname + "/bgs");

            _loop.LoadMapTexture(artifacts, characters, objects, tiles, bgs);
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
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(20);
            base.Initialize();
            _graphics.PreferredBackBufferWidth = 800;  // WIDTH
            _graphics.PreferredBackBufferHeight = 600;   // HEIGHT
            _graphics.ApplyChanges();
            MonoButton.PrevState = Keyboard.GetState();
            UserAgent.PrevMouse = Mouse.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _sb = new SpriteBatch(_graphics.GraphicsDevice);
            //TEST
            _sbtest = new SpriteBatch(_graphics.GraphicsDevice);
            _fontest = Content.Load<SpriteFont>("Arial");
            // TODO: use this.Content to load your game content here
            _font = Content.Load<SpriteFont>("Arial");
            _fontsmall = Content.Load<SpriteFont>("ArialS");

            _common = Content.Load<Texture2D>("common/common");
            _gui = Content.Load<Texture2D>("common/guis");
            _title = Content.Load<Texture2D>("common/bg");
            _default = Content.Load<Texture2D>("common/default");
            _loop = new GameLoop(_font, _fontsmall);

            //TEST

            if (!GameMaps.Initialize())
                Exit();
            if (!GameLoop.InitializeContent(_common, _gui, _title, _default))
                Exit();
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            MonoButton.CurrState = Keyboard.GetState();
            UserAgent.CurrMouse = Mouse.GetState();
            _loop.ProcessInput();
            if (_loop.IsQuit) Exit();
            base.Update(gameTime);
            MonoButton.PrevState = MonoButton.CurrState;
            UserAgent.PrevMouse = UserAgent.CurrMouse;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            // TODO: Add your drawing code here
            _loop.Display(_sb);

            //_sb.Begin();
            //Rectangle rect = _loop.GameTexture.GetRect("menu_but|button_start");
            //_sb.Draw(_loop.GameTexture.GetTexture("menu_but|button_start"), Viewer.GetRect("button_start"), rect, Color.White);
            //_sb.DrawString(_font, "hello" + rect.X + "-" + rect.Y + "-" + rect.Width + "-" + rect.Height, new Vector2(0, 0), Color.Red);
            //_sb.End();

            //TEST
            if (!test1)
            {
                _sbtest.Begin();
                _sbtest.DrawString(_fontest, alert, new Vector2(100, 100), Color.Red);
                _sbtest.End();
            }

            base.Draw(gameTime);
        }
    }
}
