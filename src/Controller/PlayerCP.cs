using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public enum GameMode {
    MENU,
    GAME
    }

    public class PlayerCP : ViewLens {
        //fields
        private List<TileV> _tiles;
        private List<PlayerAction> _pacts;
        //constructors
        public PlayerCP() 
        {
            Player = new Player();
        }
        //properties
        private string ModeInfo
        {
            get
            {
                string text = "";
                switch (Mode)
                {
                    case GameMode.GAME:
                        text += "GAME IN PROGRESS";
                        text += Player.Info;
                        text += ">>>Press wasd for movement\n";
                        text += ">>>Press q to quit\n";
                        return text;
                    case GameMode.MENU:
                        text += "MAIN MENU";
                        text += ">>>Press z to play\n";
                        text += ">>>Press q to quit\n";
                        return text;
                }
                return "Error\n";
            }
        }
        public string DisplayString
        {
            get
            {
                //assume console so using \n 
                string text = "";
                text += "----------\n";
                text += ModeInfo;
                text += "----------\n";
                return text;
            }
        }
        public PlayerAction PlayerAction
        {
            get
            {
                foreach (PlayerAction pact in _pacts)
                {
                    if (pact.Mode == Mode)
                        return pact;
                }
                return null;
            }
        }
        public Player Player { get; set; }
        public GameMode Mode { get; set; }
        //methods
        private void PerformAction_Void(RRLine line)
        {
            line.Run();
        }
        private void PerformAction_Move(RRLine line, PlayerInput input)
        {
            if (line.PlayerInput.CKI.Key == ConsoleKey.W) { line.Run(TDir.TOP); }
            else if (line.PlayerInput.CKI.Key == ConsoleKey.D) { line.Run(TDir.RIGHT); }
            else if (line.PlayerInput.CKI.Key == ConsoleKey.S) { line.Run(TDir.BOTTOM); }
            else if (line.PlayerInput.CKI.Key == ConsoleKey.A) { line.Run(TDir.LEFT); }
        }
        public void PerformAction(PlayerInput input)
        {
            PlayerAction pact = PlayerAction;
            if (pact != null)
            {
                foreach (RRLine posline in pact.ValidLines)
                {
                    if (posline.IsFlagged(input))
                    {
                        if (posline.IsOfType("M")) { PerformAction_Move(posline, input); }
                        else if (posline.IsOfType("V")) { PerformAction_Void(posline); }
                    }
                }
            }
        }
        public void Initialize(List<PlayerAction> pacts) {
            //initialize pacts
            _pacts = new List<PlayerAction>();
            foreach (PlayerAction pact in pacts)
            {
                _pacts.Add(pact);
            }
            //initialize player and tiles n such here
            Mode = GameMode.MENU;
            TileVBuilder tbuilder= new TileVBuilder();
            _tiles = tbuilder.LoadTileVsFromFile();
            Random rand = new Random();
            Player.Tile = tbuilder.FindTileAt(_tiles, rand.Next(0, tbuilder.MaxCol), rand.Next(0, tbuilder.MaxRow));
        }
    }
}
