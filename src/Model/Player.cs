using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class Player {
        //fields
        private Inventory _quests;
        private Inventory _arts;
        private Inventory _cquests;
        private Inventory _ctokens;
        private Inventory _convos;
        private string _alert;
        private ActionVoid _switchalert;
        //constructors
        public Player() {
            _quests = new Inventory();
            _cquests = new Inventory();
            _arts = new Inventory();
            _convos = new Inventory();
            _ctokens = new Inventory();
            _alert = "";
        }
        //properties
        public ActionVoid RunDialogue { get; set; }
        public ActionVoid SwitchAlert { set => _switchalert = value; }
        public VirtualObject OpName { get; set; }
        public int ArtifactCount { get => _arts.Count; }
        public string ArtifactList
        {
            get
            {
                string text = "";
                for (int i = 0; i < _arts.NameList.Count; i++)
                {
                    text += "\n" + i + " " + _arts.NameList[i];
                }
                return text;
            }
        }
        public Artifact Using { get; set; }
        public GameObject Holding { get; set; }
        public string Info
        {
            get
            {
                //assume console so use \n and \t
                string text = "";
                text += "\nCurrently at tile " + Tile.X + " - " + Tile.Y;
                if (Holding != null)
                {
                    text += "\nHolding: " + Holding.Name;
                }
                text += "\nDoing quest(s):";
                foreach (string q in _quests.NameList)
                {
                    text += "\n\t" + q;
                }
                text += "\nInventory:";
                foreach (string a in _arts.NameList)
                {
                    text += "\n\t" + a;
                }
                text += "\nCompleted quest(s):";
                foreach (string q in _cquests.NameList)
                {
                    text += "\n\t" + q;
                }
                text += _alert;
                _alert = "";
                if (Holding != null)
                {
                    text += "\n>>>Press r to place object\n";
                }
                if (Tile.Object != null)
                {
                    if (Tile.Object is ActionObject)
                        text += "\n>>>Press c to use item on object\n";
                    else if (Holding == null)
                        text += "\n>>>Press e to pick up object\n";
                }
                return text;
            }
        }
        public TileV Tile { get;set; }
        //methods 
        public void UpdateToken()
        {
            ConToken token = _ctokens.GetLast as ConToken;
            if (token.ActionType == "place")
            {
                token.SetKey(Tile.ID);
            }
            else if (token.ActionType == "use")
            {
                token.SetKey(Using.Name);
            }
        }
        public void UseArtifact(string name)
        {
            if (_arts.Has(name))
            { 
                Using = Remove(name, "A") as Artifact;
                _alert = "\nYou used " + Using.Name + " on " + Tile.Object.Name + ".";
            }
        }
        public void RemoveUsed()
        {
            Using = null;
        }
        public void HoldObject()
        {
            if (Tile != null)
            {
                if (Tile.Object != null)
                {
                    if (Tile.Object is GameObject)
                    {
                        Tile.Object.ToggleHold();
                        Holding = Tile.Object as GameObject;
                        Tile.Object = null;
                        _alert = "\nYou picked up a " + Holding.Name;
                    }
                }
            }
        }
        public void PlaceObject()
        {
            if (Holding != null)
            { 
                if (Tile != null)
                {
                    if (Tile.Object == null)
                    {
                        Holding.ToggleHold();
                        Tile.Object = Holding;
                        Holding = null;
                        _alert = "\nYou placed " + Tile.Object.Name + " at Tile " + Tile.X + " - " + Tile.Y;
                    }
                }
            }
        }
        public void AddStory()
        {
            if (Tile.Storybook != null)
            {
                if (!_convos.Has(Tile.Storybook.Name))
                    _convos.Add(Tile.Storybook);
            }
        }
        private bool HasRead()
        {
            if (Tile.Storybook != null)
            {
                return Tile.Storybook.IsRead;
            }
            return true;
        }
        private bool CanRead()
        {
            if (Tile.Trigger == null) return true;
            if (!(Tile.Trigger is TriggerF)) return true;
            //if ((Tile.Trigger as TriggerF).CanBeFulfilledBy(this)) return true;
            return false;
        }
        public void FlipTile() {
            if (Tile.Object != null)
            {
                _alert = "\nYou observed that there is a " + Tile.Object.Name + " at the site.";
            }
            else
            {
                _alert = "";
            }
            if (!HasRead())
            {
                if (CanRead())
                    RunDialogue();
                else
                    _alert += "\nThere's nothing to do here.";
            }
            else if (Tile.Trigger != null) {
                if (Tile.CanBeFlippedBy(this))
                {
                    _alert += "\nThere's something that you can get here.";
                    if (OpName.Name == "GET") 
                    {
                        if (Tile.Trigger is TriggerF)
                        {
                            if (!(Tile.Trigger as TriggerF).CanBeFulfilledBy(this))
                                _alert += "\nYou failed the quest!";
                            Tile.Trigger.FlippedBy(this);
                        }
                        else
                        {
                            Tile.Trigger.FlippedBy(this);
                            _alert += "\nYou got something!";
                        }
                    }
                    else { OpName.Name = "GET"; _switchalert(); }
                }
                else
                {
                    _alert += "\nThere might be something that you can get here.";
                }
            }
            else
            {
                _alert += "\nThere's nothing to get here.";
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
            else if (obj is ConToken) { _ctokens.Add(obj); if (_ctokens.Count > 5) { _ctokens.CutDownTo(5); } }
        }
        public bool Has(string name, string key="A") {
            if (key == "A") { return _arts.Has(name); }
            else if (key == "Q") { return _quests.Has(name); }
            else if (key == "CQ") { return _cquests.Has(name); }
            else if (key == "C") { return _convos.Has(name); }
            else if (key == "CT") { return _ctokens.Has(name); }
            return false;
        }
        public VirtualObject Find(string name, string key="A") {
            if (key == "A") { return _arts.Find(name); }
            else if (key == "Q") { return _quests.Find(name); }
            return null;
        }
        public VirtualObject Find(int index,string key="A")
        {
            if (key == "A") { return _arts.Find(index); }
            return null;
        }
        public VirtualObject Remove(string name, string key="A") {
            if (key == "A") { return _arts.Remove(name); }
            else if (key == "Q") { return _quests.Remove(name); }
            return null;
        }
    }
}
