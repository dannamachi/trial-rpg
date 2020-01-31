using System;
using System.Collections.Generic;
using System.Text;
using SEGraphic;

namespace SEVirtual {
    public enum GameMode
    {
        MENU,
        GAME,
        DIAL,
        ALERT,
        SET,
        MAP,
        INFO,
        CHOOSE
    }
    public class GameCP {
        //general fields
        private RRBuilder _builder;
        private GameMode _pmode;
        private GameMode _mode;
        private VirtualObject _opname;
        private bool _lose;
        private string _key;
        private int _count;
        //map specific fields
        private VirtualObject _diff;
        private string _map;
        private PlayerCP _playCP;
        private string _alert;
        //constructors
        public GameCP() {
            _playCP = CreatePlayCP();
            IsQuit = false;
            IsEnd = false;
            _opname = new VirtualObject();
            Mode = GameMode.MAP;
            Dialogue = "";
            _alert = "";
        }
        //properties
        public ActionUse LoadTexture { get; set; }
        public GraphicManager GraphicManager { get; set; }
        public List<TileV> Tiles { get => _playCP.Tiles; }
        public bool IsPlay { get => Mode == GameMode.GAME; }
        public string Diff 
        { 
            get => _diff.Name;
            set
            {
                _diff.Name = value;
                _playCP.Player.ToggleDiff();
            }
        }
        public RRLine SecondRun { get; set; }
        public RRLine Running { get => _playCP.Running; set => _playCP.Running = value; }
        public string OpName
        {
            get => _opname.Name;
            set
            {
                _opname.Name = value;
                _playCP.Player.OpName.Name = value;
            }
        }
        public string Alert {
            get {
                if (_alert == "") return _playCP.Player.AlertContent;
                else return _alert;
            }
            set {
                _alert = value;
            }
        }
        public string Dialogue { get; set; }
        public GameMode Mode {
            get => _mode;
            set
            {
                _mode = value;
                _playCP.Mode = value;
            }
        }
        public bool IsQuit { get;set; }
        public bool IsWin { get;set; }
        public bool IsLose
        {
            get => _lose;
            set
            {
                _lose = value;
                _playCP.Player.IsLose = value;
            }
        }
        public bool IsEnd { get; set; }
        //methods
        public Player GetPlayer()
        {
            return _playCP.Player;
        }
        public UserAgent InitializeUser()
        {
            return new UserAgent(_playCP.Player, GraphicManager);
        }
        public List<Button> GetButtons()
        {
            List<Button> bus = new List<Button>();
            foreach (RRLine line in _playCP.PlayerAction.GetValidLines(_playCP))
            {
                foreach (Button bu in line.PlayerInput.GetButtons())
                {
                    bus.Add(bu);
                }
            }
            return bus;
        }
        public string GetChosen()
        {
            return _playCP.DisplayChooseList(_playCP.Player, _key)[_count];
        }
        public List<string> GetInfo()
        {
            return _playCP.GetInfo();
        }
        public void PerformChoice()
        {
            string name;
            if (_key == "M")
            {
                name = GameMaps.GetMapName(_count);
            }
            else if (_key == "C")
            {
                name = _playCP.Player.Tile.Storybook.Choices[_count];
            }
            else
            {
                name = _playCP.Player.Find(_count, _key).Name;
            }
            if (_playCP.ChooseAction != null)
            {
                _playCP.ChooseAction.Run(name);
                if (_playCP.ChooseAction is ConLine)
                {
                    _playCP.Player.UpdateToken();
                    _playCP.Player.RemoveUsed();
                }
                if (_key == "M")
                    Mode = GameMode.SET;
                else
                    Mode = _pmode;
                _playCP.ChooseAction = null;
                _key = null;
                _count = 0;
            }
        }
        public void CancelChoice()
        {
            _playCP.ChooseAction = null;
            _key = null;
            Mode = _pmode;
            _count = 0;
        }
        public void SwitchChoice(string key)
        {
            _count = 0;
            _key = key;
            _pmode = Mode;
            Mode = GameMode.CHOOSE;
            if (_playCP.DisplayChooseList(_playCP.Player, _key).Count < 1) CancelChoice();
        }
        public void IncrementChoice()
        {
            if (_count < _playCP.GetChoiceCount(_key) - 1) _count += 1;
        }
        public void DecrementChoice()
        {
            if (_count > 0) _count -= 1;
        }
        public void DisplayCredits()
        {
            _playCP.DisplayCredits();
        }
        public void DecrementPage()
        {
            _playCP.DecrementPage();
        }
        public void IncrementPage()
        {
            _playCP.IncrementPage();
        }
        public void SwitchInfo()
        {
            Mode = GameMode.INFO;
            _playCP.ResetPage();
        }
        private bool CheckEnd()
        {
            return GameMaps.GetAvailMapCount() == 0;
        }
        private void CheckLose()
        {
            bool result = _playCP.CheckLose();
            if (result)
            {
                //display loss???
            }
            IsLose = result;
        }
        public void SetMap(string name)
        {
            _map = name;
            Reload();
            _playCP.SetWin(GameMaps.GetWin(_map));
        }
        public void SwitchMap()
        {
            Mode = GameMode.MAP;
        }
        public void SwitchMenu()
        {
            if (_map != null && Diff != null)
            {
                Mode = GameMode.MENU;
            }
        }
        public void SetEasy() 
        {
            if (OpName == "SET") { Diff = "EASY"; Mode = GameMode.MENU; _alert = ""; }
            else { OpName = "SET"; SwitchAlert(); _alert = "You are about to set the difficulty to Easy."; }
        }
        public void SetHard() 
        {
            if (OpName == "SET") { Diff = "HARD"; Mode = GameMode.MENU; _alert  = ""; }
            else { OpName = "SET"; SwitchAlert(); _alert = "You are about to set the difficulty to Hard."; }
        }
        public void CheckWin()
        {
            bool result = _playCP.CheckWin();
            if (result)
            {
                //display win???
                GamePath.GetPath().AddChapter(_playCP);
            }
            IsWin = result;
        }
        public void RunDialogue()
        {
            StartDialogue();
            _playCP.Player.AddStory();
            SecondRun = Running;
        }
        public void StartDialogue()
        {
            Dialogue = _playCP.GetDialogue();
            if (Dialogue != null)
                ToggleDialogue();
        }
        public void ContinueDialogue()
        {
            Dialogue = _playCP.GetDialogue();
            if (_playCP.Player.Tile.Storybook.NeedChoice)
            {
                _playCP.PerformAction(PlayerInput.GetInput2(new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.C) }));
            }      
            if (_playCP.Player.HasRead())
            {
                _playCP.Player.AddStory();
                ToggleDialogue();
            }
        }
        public void ToggleDialogue()
        {
            if (Mode != GameMode.DIAL) { Mode = GameMode.DIAL; }
            else 
            { 
                Mode = GameMode.GAME;
                if (SecondRun != null)
                {
                    SecondRun.Run();
                    SecondRun = null; 
                }
            }
        }
        public void PerformAction(PlayerInput input)
        {
            _playCP.PerformAction(input);
            if (IsPlay)
            {
                CheckWin();
                CheckLose();
                if (IsWin || IsLose)
                {
                    Mode = GameMode.MAP;
                    _map = "";
                }
                if (CheckEnd())
                {
                    IsEnd = true;
                    //display end game???
                    _playCP.Running = new RRLine(new ActionVoid(GameLoop.Reset));
                    GameLoop.Reset();
                }
            }
        }
        public void RunOp()
        {
            Mode = _pmode;
            _playCP.Running.Run();
            _alert = "";
            OpName = "";
            _playCP.Running = null;
        }
        public void CancelOp()
        {
            Mode = _pmode;
            _alert = "";
            OpName = "";
            _playCP.Running = null;
        }
        public void SwitchAlert()
        {
            _pmode = Mode;
            Mode = GameMode.ALERT;
        }
        public PlayerCP CreatePlayCP() {
            //rrbuilder and such
            PlayerCP playCP = new PlayerCP();
            _builder = new RRBuilder(this, playCP.Player);
            playCP.Initialize(_builder.BuildPActs());
            playCP.SaveQuit = new ActionVoid(SaveQuit);
            playCP.SwitchInfo = new ActionVoid(SwitchInfo);
            playCP.SwitchChoice = new ActionUse(SwitchChoice);
            //fail quest
            playCP.Player.FailQuest = new ActionUse(playCP.AddFail);
            //reset
            List<Button> bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.X) };
            RRLine resetline = new RRLine(new ActionVoid(Reset));
            resetline.PlayerInput = PlayerInput.GetInput2(bus);
            playCP.Player.ResetGame = resetline;
            //dialogue
            playCP.Player.RunDialogue = new ActionVoid(RunDialogue);
            //initial setting
            _diff = new VirtualObject();
            playCP.Player.Diff = _diff;
            playCP.Player.OpName = new VirtualObject();
            playCP.Player.SwitchAlert = new ActionVoid(SwitchAlert);
            return playCP;
        }
        private void ReloadPlayerCP()
        {
            //tiles
            _playCP.InitializeGame(_map);
            _playCP.SetInput(_builder);
        }
        private void Reload()
        {
            IsWin = false;
            IsLose = false;
            ReloadPlayerCP();
            LoadTexture(_map);
            GraphicManager.InitializeMap(_map);
            Mode = GameMode.SET;
            Dialogue = "";
            _alert = "";
            OpName = "";
        }
        public void Reset()
        {
            if (OpName == "RESET")
            {
                Reload();
                _alert = "";
            }
            else { OpName = "RESET"; SwitchAlert(); _alert = "You are about to reset the map.";}
        }
        public void SaveQuit()
        {
            if (OpName == "SAVEQUIT") { Mode = GameMode.MENU; _alert = "";}
            else { OpName = "SAVEQUIT"; SwitchAlert(); _alert = "You are about to return to the menu.";}
        }
        public void QuitTheGame() {
            if (OpName == "QUIT") { IsQuit = true; _alert = ""; }
            else { OpName = "QUIT"; SwitchAlert(); _alert = "You are about to quit the game."; }
        }
        public void PlayTheGame() {
            if (_map != null && !IsWin && !IsLose)
            {
                _alert = "";
                Mode = GameMode.GAME;
                _playCP.Player.EComm = false;
            }
        }
    }
}
