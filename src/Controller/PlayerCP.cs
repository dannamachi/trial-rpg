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
        private bool FindQuest(string name)
        {
            foreach (TileV tile in _tiles)
            {
                if (tile.Trigger != null)
                {
                    if (tile.Trigger is TriggerQ)
                    {
                        foreach (Quest q in (tile.Trigger as TriggerQ).Quests)
                        {
                            if (q.IsCalled(name))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool FindArtifact(string name)
        {
            foreach (TileV tile in _tiles)
            {
                if (tile.Trigger != null)
                {
                    if (tile.Trigger is TriggerA)
                    {
                        foreach (Artifact art in (tile.Trigger as TriggerA).Artifacts)
                        {
                            if (art.IsCalled(name))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public void CheckLose()
        {
            bool result = true;
            //get all requests for an artifact
            List<Request> reqs = new List<Request>();
            foreach (Quest q in GetQuests())
            {
                foreach (Request req in q.ArtifactRequests)
                {
                    reqs.Add(req);
                }
            }
            //find in map if artifact still there
            foreach (Request req in reqs)
            {
                if (!FindArtifact(req.GetNeededArtifact))
                {
                    result = false;
                }
            }
            //get all requests that need another quest
            reqs = new List<Request>();
            foreach (Quest q in GetQuests())
            {
                foreach (Request req in q.QuestRequests)
                {
                    reqs.Add(req);
                }
            }
            //find in map if quest still avail
            foreach (Request req in reqs)
            {
                if (!FindQuest(req.GetNeededQuest))
                {
                    result = false;
                }
            }
            if (!result)
            {
                Viewer.Display("\nYou've lost the game, please proceed to reset.");
            }
            else
            {
                Viewer.Display("\nYou haven't lost the game yet!");
            }
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
            if (key == "A")
                return index >= 0 && index < Player.ArtifactCount;
            else if (key == "Q")
                return index >= 0 && index < Player.QuestCount;
            else return false;
        }
        private PlayerInput GetInputForUseAction(string key)
        {
            Viewer.Display(Player.GetList(key));
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
        private void PerformAction_Use(RRLine line)
        {
            string key = null;
            if (line.PlayerInput.CKI.Key == ConsoleKey.K) { key = "A"; }
            else if (line.PlayerInput.CKI.Key == ConsoleKey.M) { key = "Q"; }
            if (key != null)
            {
                PlayerInput input = GetInputForUseAction(key);
                if (input != null)
                {
                    line.Run(Player.Find(ConsoleKeyToInt(input.CKI),key).Name);
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
                        else if (posline.IsOfType("U")) { PerformAction_Use(posline); }
                        else if (posline.IsOfType("M")) { PerformAction_Move(posline, input); }
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
            //initialize objects
            ObjectBuilder obuilder = new ObjectBuilder(Player);
            _tiles = obuilder.AddObjectFromFile(_tiles);
        }
    }
}
