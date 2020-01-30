using System;
using System.Collections.Generic;
using System.Linq;
using SEGraphic;
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
        private string _info;
        private int _infopage;
        //constructors
        public PlayerCP() 
        {
            Player = new Player();
            _info = "";
            _infopage = 0;
        }
        //properties
        public List<TileV> Tiles { get => _tiles; }
        public ActionUse SwitchChoice { get; set; }
        public RRLine ChooseAction { get; set; }
        public bool ExtraComm { get => Player.EComm; }
        public ActionVoid SwitchInfo { get; set; }
        public ActionVoid SaveQuit { get; set; }
        public string Map { get => _map; }
        public string WinBy { get; set; }
        public RRLine Running { get; set; }
        public GameMode Mode { get; set; }
        public int MaxCol { get => _build.MaxCol; }
        public int MaxRow { get => _build.MaxRow; }

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
        public void ResetPage()
        {
            _infopage = 0;
        }
        public List<string> DisplayChooseList(Player p, string key)
        {
            if (key != "M")
                return p.GetList(key);
            else
                return GameMaps.GetAvailMaplist();
        }
        public int GetChoiceCount(string key)
        { 
            int count;
            if (key != "M")
                count = Player.GetCount(key);
            else
                count = GameMaps.GetAvailMapCount();
            return count;
        }
        public void DisplayCredits()
        {
            _info = "";
            _info += "Made by Mochi|";
            _info += "With the support of friends";
        }
        public void DisplayComplete()
        {
            _info = "";
            _info += "Finished quests:|";
            _info += Player.DisplayQuests();
        }
        public void DecrementPage()
        {
            if (_infopage > 0) _infopage -= 1;
        }
        public void IncrementPage()
        {
            if ((_infopage + 1) * 10 < _info.Split('|').ToList().Count) _infopage += 1;
        }
        public List<string> GetInfo()
        {
            List<string> lines = _info.Split('|').ToList();
            List<string> sent = new List<string>();
            int entry;
            for (int i = 0; i < 10; i++)
            {
                entry = _infopage * 10 + i;
                if (entry >= lines.Count)
                    break;
                sent.Add(lines[entry]);
            }
            return sent;
        }
        public void DisplayConvo()
        {
            _info = "";
            _info += "JOURNAL:|";
            _info += Player.DisplayConvo();
        }
        public void DisplayHelp()
        {
            _info = "";
            _info += "Shortcuts:|";
            _info += "wasd - Move|";
            _info += "f - Find => check tile|";
            _info += "e - Pick/Place => pick up objects|";
            _info += "c - Use => use artifacts|";
            _info += "m - Drop artifact/quest|";
            _info += "k - Toggle artifacts/quests|";
            _info += "l - Menu||";
            _info += "There are secrets to be uncovered.|";
            _info += "Check the tiles, pick up artifacts and quests, read conversations,|";
            _info += "use certain artifacts on special tiles and fulfill quests.";
            _info += "When in doubt, check conversation history for clues, or|";
            _info += "reset the game via map selection. Good luck!";
        }
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
                _wins = qnames.Split('|').ToList();
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
        private void GetInputForUseAction(string key, RRLine line)
        {
            ChooseAction = line;
            SwitchChoice(key);
        }
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
        private void PerformAction_Void(RRLine line)
        {
            line.Run();
        }
        private void PerformAction_Move(RRLine line, PlayerInput input)
        {
            if (line.PlayerInput.HasButton("W")) { line.Run(TDir.TOP); }
            else if (line.PlayerInput.HasButton("D")) { line.Run(TDir.RIGHT); }
            else if (line.PlayerInput.HasButton("S")) { line.Run(TDir.BOTTOM); }
            else if (line.PlayerInput.HasButton("A")) { line.Run(TDir.LEFT); }
        }
        private void PerformAction_Con(ConLine line)
        {
            line.IsAttemptedBy(Player);
            if (line.ActionType == "pickplace")
            {
                line.Run();
                Player.UpdateToken();
            }
            else
            {
                //use an artifact on an AO
                GetInputForUseAction("A", line);
            }
        }
        private void PerformAction_Use(RRLine line, GameMode mode)
        {
            //drop a/q
            if (mode == GameMode.GAME)
            {
                string key = null;
                if (Player.SeeingArtifact) { key = "A"; }
                else { key = "Q"; }
                if (key != null)
                {
                    GetInputForUseAction(key, line);
                }
            }
            //switch map
            else if (mode == GameMode.MAP)
            {
                GetInputForUseAction("M", line);
            }
            //choose dialogue
            else if (mode == GameMode.DIAL)
            {
                GetInputForUseAction("C", line);
            }
        }
        public void PerformAction(PlayerInput input)
        {
            PlayerAction pact = PlayerAction;
            if (pact != null)
            {
                foreach (RRLine posline in pact.GetValidLines(this))
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
            //new player
            Player.Reset();
            _info = "";
            _infopage = 0;
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
