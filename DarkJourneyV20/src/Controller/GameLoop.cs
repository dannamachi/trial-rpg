using SEGraphic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SEVirtual {
    public class GameLoop {
        //fields
        private static GameCP _gameCP = new GameCP();
        private static UserAgent _user;
        //constructors
        public GameLoop(SpriteFont font, SpriteFont fontsmall)
        {
            InitializeGraphic(font, fontsmall);
            _user = _gameCP.InitializeUser();
        }
        //properties
        public bool IsQuit { get => _gameCP.IsQuit; }
        public PlayerInput Input { get => _user.Input; }
        public GameTexture GameTexture { get => GraphicManager.GameTexture; }
        public static ActionUse LoadTexture { get; set; }
        public GraphicManager GraphicManager { get => _gameCP.GraphicManager; }
        //methods
        public void LoadMapTexture(Texture2D arts, Texture2D chas, Texture2D objs, Texture2D tils, Texture2D bgs)
        {
            _gameCP.GraphicManager.LoadMapTexture(arts, chas, objs, tils, bgs);
        }
        public static bool InitializeContent(Texture2D common, Texture2D gui, Texture2D titlet, Texture2D defaultt)
        {
            return _gameCP.GraphicManager.InitializeCommon(common, gui , titlet, defaultt);
        }
        private static void InitializeGraphic(SpriteFont font, SpriteFont fontsmall)
        {
            _gameCP.GraphicManager = new GraphicManager();
            _gameCP.GraphicManager.Initialize(font, fontsmall);
            _gameCP.LoadTexture = LoadTexture;
        }
        public static void Reset()
        {
            if (_gameCP.OpName == "TOTALRESET")
            {
                //transfer preloaded common resources
                SpriteFont sfont = _gameCP.GraphicManager.Font;
                SpriteFont smfont = _gameCP.GraphicManager.FontSmall;
                Dictionary<string, Texture2D> found = _gameCP.GraphicManager.GetLoadedTexture();
                //clear game path
                GamePath.Renew();
                //make new gameCP
                _gameCP = new GameCP();
                //re initialize evt
                InitializeGraphic(sfont, smfont);
                _user = _gameCP.InitializeUser();
                GameMaps.Initialize();
                InitializeContent(found["common"], found["gui"], found["titleBG"], found["defaultBG"]);
            }
            else
            {
                _gameCP.OpName = "TOTALRESET";
                _gameCP.SwitchAlert();
                _gameCP.Alert = "You are about to reset the game.";
            }
        }
        public void ProcessInput()
        {
            _user.CheckInput(_gameCP);
            _user.PassInput(_gameCP);
        }
        public void Display(SpriteBatch sb) {
            _gameCP.GraphicManager.Draw(_gameCP, _gameCP.GetPlayer(), sb);
            _gameCP.GraphicManager.DrawActive(_gameCP, _gameCP.GetPlayer(), sb);
        }
    }
}
