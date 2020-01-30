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
        private string _info;
        private ActionVoid _switchalert;
        private RRLine _choose;
        private int _speed;
        //constructors
        public Player() {
            _quests = new Inventory();
            _cquests = new Inventory();
            _arts = new Inventory();
            _convos = new Inventory();
            _ctokens = new Inventory();
            _alert = "";
            _choose = new RRLine(new ActionUse(ChooseDial));
            List<SEGraphic.Button> bus = new List<SEGraphic.Button> { new SEGraphic.KbButton(Microsoft.Xna.Framework.Input.Keys.C) };
            _choose.PlayerInput = SEGraphic.PlayerInput.GetInput2(bus);
            SeeingArtifact = true;
            EComm = false;
            _speed = 10;
        }
        //properties
        public string AlertContent { get => _alert; }
        public string InfoContent { get => _info; }
        public int Capacity { get => _arts.Capacity; }
        public bool EComm { get; set; }
        public bool SeeingArtifact { get; set; }
        public bool IsLose { get; set; }
        public List<string> ConvoList { get => _convos.NameList; }
        public RRLine ChooseDialogue { get => _choose; }
        public VirtualObject Diff { get; set; }
        public ActionUse FailQuest { get; set; }
        public RRLine ResetGame { get; set; }
        public ActionVoid RunDialogue { get; set; }
        public ActionVoid SwitchAlert { set => _switchalert = value; }
        public VirtualObject OpName { get; set; }
        public Artifact Using { get; set; }
        public GameObject Holding { get; set; }
        public TileV Tile { get;set; }
        //methods
        public Storybook GetCurrBook() {
            if (Tile.Storybook != null) {
                return Tile.Storybook;
            }
            else return null;
        }
        public void Reset()
        {
            _quests = new Inventory();
            _cquests = new Inventory();
            _arts = new Inventory();
            _convos = new Inventory();
            _ctokens = new Inventory();
            _alert = "";
            _info = "";
            SeeingArtifact = true;
            EComm = false;
        }
        public void ToggleComm()
        {
            EComm = !EComm;
        }
        public void ToggleStuff()
        {
            SeeingArtifact = !SeeingArtifact;
        }
        //GRAPHIC
        public List<string> GetStuffList()
        {
            if (SeeingArtifact) return GetList("A");
            else return GetList("Q");
        }
        //GRAPHIC
        public string DisplayConvo()
        {
            string text = "";
            for (int i = 0; i < _convos.Count; i++)
            {
                Storybook q = _convos.Find(i) as Storybook;
                text += "Conversation: " + q.Name + "|";
                foreach (Storypage page in q.Pages)
                {
                    text += page.PersonSpeaking + ":" + page.SpeechContent + "|";
                }
            }
            return text;
        }
        //GRAPHIC
        public string DisplayQuests()
        {
            string text = "";
            for (int i = 0; i < _cquests.Count; i++)
            {
                Quest q = _cquests.Find(i) as Quest;
                text += "Completed: " + q.Name + "|";
                text += q.Info();
            }
            return text;
        }
        public bool HasRead()
        {
            if (Tile != null)
            {
                if (Tile.Storybook != null)
                {
                    return Tile.Storybook.IsRead;
                }
            }
            return true;
        }
        public int GetCount(string key)
        {
            switch (key)
            {
                case "A": return _arts.Count;
                case "Q": return _quests.Count;
                case "CQ": return _cquests.Count;
                case "C":
                    if (Tile == null) return 0;
                    if (Tile.Storybook == null) return 0;
                    return Tile.Storybook.Choices.Count;
                default: return 0;
            }
        }
        private void ChooseDial(string name)
        {
            Tile.Storybook.Chosen = name;
        }
        public void ToggleDiff()
        {
            int num;
            if (Diff.Name == "HARD")
            {
                num = 5;
            }
            else
            {
                num = 10;
            }
            _arts.Capacity = num;
            _quests.Capacity = num;
        }
        private bool CanAdd(string key, int num)
        {
            if (key == "A") { return _arts.Count + num <= _arts.Capacity; }
            if (key == "Q") { return _quests.Count + num <= _quests.Capacity; }
            return true;
        }
        public List<string> GetList(string key)
        {
            switch (key)
            {
                case "A":
                    return _arts.NameList;
                case "Q":
                    return _quests.NameList;
                case "CQ":
                    return _cquests.NameList;
                case "C":
                    if (Tile == null) break;
                    if (Tile.Storybook == null) break;
                    List<string> choices = new List<string>();
                    for (int i = 0; i < Tile.Storybook.Choices.Count; i++)
                    {
                        choices.Add(Tile.Storybook.Choices[i]);
                    }
                    return choices;
            }
            return new List<string>();
        }
        private bool CanFulfill(string questname)
        {
            if (Has(questname, "Q"))
            {
                return (Find(questname, "Q") as Quest).IsFulfilledBy(this);
            }
            return false;
        }
        private bool CanFulfill(TriggerF trig)
        {
            foreach (string questname in trig.Namelist)
            {
                if (!CanFulfill(questname))
                {
                    return false;
                }
            }
            return true;
        }
        public void Drop(string name)
        {
            string key;
            if (SeeingArtifact) { key = "A"; }
            else { key = "Q"; }
            if (Has(name,key))
            {
                Remove(name, key);
                _info = "You have removed " + name;
            }
        }
        public void UpdateToken()
        {
            ConToken token = _ctokens.GetLast as ConToken;
            if (token.ActionType == "pickplace")
            {
                if (Holding == null)
                {
                    token.SetKey(Tile.ID);
                }
                else
                    token.SetKey(Holding.Name);
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
                _info = "You used " + Using.Name + " on " + Tile.Object.Name;
            }
        }
        public void PickPlaceObject()
        {
            if (Holding == null) HoldObject();
            else PlaceObject();

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
                        _info = "You picked up a " + Holding.Name;
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
                        _info = "You placed " + Tile.Object.Name + " at Tile " + Tile.X + " - " + Tile.Y;
                    }
                    else
                    {
                        _info = "There is already something there.";
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
        private bool IsDoingQuestFrom(TriggerF trig)
        {
            foreach (string qname in trig.Namelist)
            {
                if (Has(qname, "Q")) return true;
            }
            return false;
        }
        public bool CanRead(Trigger trig)
        {
            if (trig == null) return true;
            if (!(trig is TriggerF)) return true;
            if (IsDoingQuestFrom(trig as TriggerF)) return true;
            return false;
        }
        public void FlipTile() {
            _alert = "";
            _info = "";
            if (!HasRead())
            {
                if (CanRead(Tile.Trigger))
                    RunDialogue();
                else
                    _info = "There's nothing to do here.";
            }
            else if (Tile.Trigger != null) {
                if (Tile.CanBeFlippedBy(this))
                {
                    if (OpName.Name == "GET") 
                    {
                        if (Tile.Trigger is TriggerF)
                        {
                            if (!CanFulfill(Tile.Trigger as TriggerF))
                                _info = "You failed one of the quest(s)!";
                            Tile.Trigger.FlippedBy(this);
                        }
                        else
                        {
                            string key = "";
                            if (Tile.Trigger is TriggerA) { key = "A"; }
                            else { key = "Q"; }
                            if (CanAdd(key, Tile.Trigger.Count))
                            {
                                Tile.Trigger.FlippedBy(this);
                                _info = "You got something!";
                            }
                            else
                            {
                                _info = "You cannot get this due to limited space.";
                            }
                        }
                    }
                    else 
                    {
                        OpName.Name = "GET";
                        string item = "";
                        if (Tile.Trigger is TriggerA) { item = "artifact(s)"; }
                        else if (Tile.Trigger is TriggerQ) { item = "quest(s)"; }
                        if (item != "")
                        {
                            _alert = "Gaining " + Tile.Trigger.Count + " " + item ;
                        }
                        else
                        {
                            _alert = "Attempting " + Tile.Trigger.Count + " quest(s)";
                        } 
                        foreach (string qname in Tile.Trigger.Namelist)
                        {
                            _alert += qname + ",";
                        }                       
                        _switchalert(); 
                    }
                }
                else
                {
                    if (Tile.Object != null)
                    {
                        _info = "You observed that there is a " + Tile.Object.Name + " at the site.";
                    }
                    else {
                        _info = "There might be something that you can do here.";
                    }
                }
            }
            else
            {
                _info = "There's nothing to get here.";
            }
        }
        public void Move(TDir dir) {
            _speed -= 1;
            if (Tile.CanMoveTo(dir)) {
                if (_speed % 10 == 0)
                {
                    Tile = Tile.GetNextTile(dir);
                    _speed = 10;
                }
            }
            if (_speed == 0) _speed = 10;
        }
        public void Attempt(string questname) {
            if (Has(questname,"Q")) {
                if (CanFulfill(questname))
                    _cquests.Add(Remove(questname, "Q"));
                else
                {
                    Remove(questname, "Q");
                    FailQuest(questname);
                }
            }
        }
        public void Add(VirtualObject obj) {
            if (obj is Artifact) { _arts.Add(obj); }
            else if (obj is Quest) { if (!Has(obj.Name,"CQ")) _quests.Add(obj); }
            else if (obj is ConToken) { _ctokens.Add(obj); if (_ctokens.Count > 50) { _ctokens.CutDownTo(50); } }
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
            else if (key == "Q") { return _quests.Find(index); }
            return null;
        }
        private VirtualObject Remove(string name, string key="A") {
            if (key == "A") { return _arts.Remove(name); }
            else if (key == "Q") { return _quests.Remove(name); }
            return null;
        }
    }
}
