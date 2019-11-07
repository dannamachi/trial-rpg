using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class PlayerCP {
        //fields
        private List<TileV> _tiles;
        private List<PlayerAction> _pacts;
        private TileVBuilder _build;
        //constructors
        public PlayerCP() 
        {
            Player = new Player();
        }
        //properties
        public GameMode Mode { get; set; }
        public int MaxCol { get => _build.MaxCol; }
        public int MaxRow { get => _build.MaxRow; }
        public string DisplayMap
        {
            get
            {
                string text = "\n";
                for (int i = 0; i < MaxRow; i++)
                {
                    for (int j = 0; j < MaxCol; j++)
                    {
                        TileV tile = FindTileAt(j, i);
                        if (tile == null) { text += "  "; }
                        else if (tile.Blocked)
                        {
                            text += "o ";
                        }
                        else if (Player.Tile.IsAt(j,i))
                        {
                            text += "x ";
                        }
                        else 
                        {
                            text += "w ";
                        }
                    }
                    text += "\n";
                }
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
        //methods
        public string GetDialogue()
        {
            if (Player.Tile != null)
            {
                if (Player.Tile.Storybook != null)
                {
                    if (!Player.Tile.Storybook.IsRead)
                    {
                        return Player.Tile.Storybook.DisplayPage;
                    }
                }
            }
            return null;
        }
        public TileV FindTileAt(int x, int y)
        {
            if (x > MaxCol || x < 0 || y > MaxRow || y < 0) { return null; }
            return _build.FindTileAt(_tiles, x, y);
        }
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
            TileVBuilder tbuilder= new TileVBuilder();
            _tiles = tbuilder.LoadTileVsFromFile();
            Random rand = new Random();
            TileV ftile = tbuilder.FindTileAt(_tiles, rand.Next(0, tbuilder.MaxCol), rand.Next(0, tbuilder.MaxRow));
            while (ftile.Blocked)
            { 
                ftile = tbuilder.FindTileAt(_tiles, rand.Next(0, tbuilder.MaxCol), rand.Next(0, tbuilder.MaxRow));
            }
            Player.Tile = ftile;
            _build = tbuilder;
        }
    }
}
