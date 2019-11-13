using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEVirtual {
    public class PlayerCP {
        //fields
        private List<TileV> _tiles;
        private List<PlayerAction> _pacts;
        private TileVBuilder _build;
        private List<string> _wins;
        private string _map;
        private List<string> _failed;
        //constructors
        public PlayerCP() 
        {
            Player = new Player();
        }
        //properties
        public string Map { get => _map; }
        public string WinBy { get; set; }
        public RRLine Running { get; set; }
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
        public bool CheckLose()
        {
            if (Player.Diff.Name == "EASY") return false;
            List<string> qnames = new List<string>();
            if (_wins != null) { qnames = _wins; }
            else
            {
                foreach (Quest q in GetQuests())
                {
                    qnames.Add(q.Name);
                }
            }
            foreach (string win in qnames)
            {
                if (!_failed.Contains(win))
                    return false;
            }
            return true;
        }
        public void AddFail(string q)
        {
            if (!_failed.Contains(q))
                _failed.Add(q);
        }
        public bool CheckWin()
        {
            if (_wins == null) return CheckWinD(GetQuests());
            else return CheckWinD(_wins);
        }
        //player wins if any of win quests is completed
        private bool CheckWinD(List<string> qs)
        {
            bool result = false;
            foreach (string q in qs)
            {
                if (Player.Has(q, "CQ"))
                {
                    WinBy = q;
                    result = true;
                    break;
                }
            }
            return result;
        }
        //player wins if all quests are completed (default)
        private bool CheckWinD(List<Quest> qs)
        {
            bool result = true;
            foreach (Quest q in qs)
            {
                if (!Player.Has(q.Name, "CQ"))
                {
                    WinBy = "ALLQUESTS";
                    result = false;
                    break;
                }
            }
            return result;
        }
        public void SetWin(string qnames)
        {
            if (qnames != null)
                _wins = qnames.Split("|").ToList();
        }
        public List<Quest> GetQuests()
        {
            List<Quest> qs = new List<Quest>();
            foreach (TileV tile in _tiles)
            {
                if (tile.Trigger == null)
                    continue;
                if (tile.Trigger is TriggerQ)
                {
                    foreach (Quest q in (tile.Trigger as TriggerQ).GetQuests())
                    {
                        qs.Add(q);
                    }
                }
            }
            return qs;
        }
        public void SetInput(RRBuilder rbuilder)
        {
            rbuilder.SetInput(_tiles);
        }
        private int ConsoleKeyToInt(ConsoleKeyInfo cki)
        {
            if (char.IsDigit(cki.KeyChar))
            {
                return int.Parse(cki.KeyChar.ToString());
            }
            return -1;
        }
        private bool ValidIndex(ConsoleKeyInfo cki, string key)
        {
            int index = ConsoleKeyToInt(cki);
            if (key == "M")
                return index >= 0 && index < GameMaps.GetAvailMapCount();
            else
                return index >= 0 && index < Player.GetCount(key);
        }
        private PlayerInput GetInputForUseAction(string key)
        {
            if (key != "M")
                Viewer.Display(Player.GetList(key));
            else 
                Viewer.Display(GameMaps.GetAvailMaplist());
            Viewer.Display("\n>>>>>Press index to choose: ");
            ConsoleKeyInfo cki = Console.ReadKey();
            PlayerInput input;
            if (ValidIndex(cki, key))
            {
                input = new PlayerInput(cki);
            }
            else
            {
                Viewer.Display("\n>>>>>Invalid input.");
                input = null;
            }
            return input;
        }
        public string GetDialogue()
        {
            if (Player.Tile != null)
            {
                if (Player.Tile.CanBeFlippedBy(Player) && Player.Tile.Storybook != null)
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
        private void PerformAction_Con(ConLine line)
        {
            line.IsAttemptedBy(Player);
            if (line.ActionType == "place")
            {
                line.Run();
                Player.UpdateToken();
            }
            else
            {
                //use an artifact on an AO
                PlayerInput input = GetInputForUseAction("A");
                if (input != null) 
                {
                    line.Run(Player.Find(ConsoleKeyToInt(input.CKI),"A").Name);
                    Player.UpdateToken();
                    Player.RemoveUsed();
                }
            }
        }
        private void PerformAction_Use(RRLine line, GameMode mode)
        {
            //drop a/q
            if (mode == GameMode.GAME)
            {
                string key = null;
                if (line.PlayerInput.CKI.Key == ConsoleKey.K) { key = "A"; }
                else if (line.PlayerInput.CKI.Key == ConsoleKey.M) { key = "Q"; }
                if (key != null)
                {
                    PlayerInput input = GetInputForUseAction(key);
                    if (input != null)
                    {
                        line.Run(Player.Find(ConsoleKeyToInt(input.CKI), key).Name);
                    }
                }
            }
            //switch map
            else if (mode == GameMode.MAP)
            {
                PlayerInput input = GetInputForUseAction("M");
                if (input != null)
                {
                    line.Run(GameMaps.GetMapName(ConsoleKeyToInt(input.CKI)));
                }
            }
            //choose dialogue
            else if (mode == GameMode.DIAL)
            {
                PlayerInput input = GetInputForUseAction("C");
                if (input != null)
                {
                    string choice = Player.Tile.Storybook.Choices[ConsoleKeyToInt(input.CKI)];
                    line.Run(choice);
                }
            }
        }
        public void PerformAction(PlayerInput input)
        {
            PlayerAction pact = PlayerAction;
            if (pact != null)
            {
                foreach (RRLine posline in pact.GetValidLines(Player))
                {
                    if (posline.IsFlagged(input))
                    {
                        if (Mode != GameMode.ALERT && Mode != GameMode.DIAL)
                            Running = posline;
                        if (posline is ConLine)
                        {
                            PerformAction_Con(posline as ConLine);
                        }
                        else if (posline.IsOfType("U")) { PerformAction_Use(posline, Mode); }
                        else if (posline.IsOfType("M")) { PerformAction_Move(posline, input); }
                        else if (posline.IsOfType("V")) { PerformAction_Void(posline); }
                    }
                }
            }
        }
        public void InitializeGame(string map)
        {
            _failed = new List<string>(); 
            //initialize player and tiles n such here
            TileVBuilder tbuilder = new TileVBuilder();
            tbuilder.SetMap(map);
            _tiles = tbuilder.LoadTileVsFromFile();
            //get random first tile - or fixed in maps.txt?
            Random rand = new Random();
            TileV ftile = tbuilder.FindTileAt(_tiles, rand.Next(0, tbuilder.MaxCol), rand.Next(0, tbuilder.MaxRow));
            while (ftile.Blocked)
            {
                ftile = tbuilder.FindTileAt(_tiles, rand.Next(0, tbuilder.MaxCol), rand.Next(0, tbuilder.MaxRow));
            }
            Player.Tile = ftile;
            _build = tbuilder;
            //initialize dialogue
            StoryBuilder sbuilder = new StoryBuilder();
            sbuilder.SetMap(map);
            sbuilder.LoadBranches();
            _tiles = sbuilder.LoadBooks(_tiles, _build);
            //initialize objects
            ObjectBuilder obuilder = new ObjectBuilder(Player);
            obuilder.SetMap(map);
            _tiles = obuilder.AddObjectFromFile(_tiles);
            _map = map;
        }
        public void Initialize(List<PlayerAction> pacts) {
            //initialize pacts
            _pacts = new List<PlayerAction>();
            foreach (PlayerAction pact in pacts)
            {
                _pacts.Add(pact);
            }
        }
    }
}
