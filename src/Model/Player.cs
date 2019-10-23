using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class Player {
        //fields
        private TileV _tile;
        private Inventory _quests;
        private Inventory _arts;
        private Inventory _cquests;
        //constructors
        public Player() {
            _quests = new Inventory();
            _cquests = new Inventory();
            _arts = new Inventory();
        }
        //properties
        public string Info
        {
            get
            {
                //assume console so use \n and \t
                string text = "";
                text += "Current at tile " + Tile.X + " - " + Tile.Y + "\n";
                text += "Doing quest(s):\n";
                foreach (string q in _quests.NameList)
                {
                    text += "\t" + q + "\n";
                }
                text += "Inventory:\n";
                foreach (string a in _arts.NameList)
                {
                    text += "\t" + a + "\n";
                }
                text += "Completed quest(s):\n";
                foreach (string q in _cquests.NameList)
                {
                    text += "\t" + q + "\n";
                }
                return text;
            }
        }
        public TileV Tile { get;set; }
        //methods
        public void FlipTile() {
            if (Tile.Trigger != null) {
                Tile.Trigger.FlippedBy(this);
            }
        }
        public void Move(TDir dir) {
            if (Tile.CanMoveTo(dir)) {
                Tile = Tile.GetNextTile(dir);
            }
        }
        public void Complete(string questname) {
            if (Has(questname,"Q")) {
                _cquests.Add(Remove(questname,"Q"));
            }
        }
        public void Add(VirtualObject obj) {
            if (obj is Artifact) { _arts.Add(obj); }
            else if (obj is Quest) { _quests.Add(obj); } 
        }
        public bool Has(string name, string key="A") {
            if (key == "A") { return _arts.Has(name); }
            else if (key == "Q") { return _quests.Has(name); }
            else if (key == "CQ") { return _cquests.Has(name); }
            return false;
        }
        public VirtualObject Find(string name, string key="A") {
            if (key == "A") { return _arts.Find(name); }
            else if (key == "Q") { return _quests.Find(name); }
            return null;
        }
        public VirtualObject Remove(string name, string key="A") {
            if (key == "A") { return _arts.Remove(name); }
            else if (key == "Q") { return _quests.Remove(name); }
            return null;
        }
    }
}
