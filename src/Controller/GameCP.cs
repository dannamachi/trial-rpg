using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public enum GameMode
    {
        MENU,
        GAME,
        DIAL,
        ALERT,
        SET,
        MAP
    }
    public class GameCP : ViewLens {
        //general fields
        private RRBuilder _builder;
        private GameMode _pmode;
        private GameMode _mode;
        private string _info;
        private VirtualObject _opname;
        private bool _lose;
        //map specific fields
        private VirtualObject _diff;
        private string _map;
        private PlayerCP _playCP;
        //constructors
        public GameCP() {
            _playCP = CreatePlayCP();
            IsQuit = false;
            IsEnd = false;
            _opname = new VirtualObject();
            Mode = GameMode.MAP;
            Dialogue = "";
            Alert = "";
            _info = "";
        }
        //properties
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
        public string Alert { get; set; }
        private string Dialogue { get; set; }
        private string ModeInfo
        {
            get
            {
                string text = "";
                switch (Mode)
                {
                    case GameMode.GAME:
                        text += "\nGAME IN PROGRESS";
                        text += _playCP.Player.Info;
                        text += "\n>>>Press wasd for movement\n";
                        text += "\n>>>Press f to find\n";
                        text += "\n>>>Press q to return to menu\n";
                        return text;
                    case GameMode.MENU:
                        text += "\nMAIN MENU";
                        text += "\n>>>Press z to play\n";
                        text += "\n>>>Press m to choose another chapter\n";
                        if (Diff == "EASY")
                            text += "\n>>>Press x to reset chapter\n";
                        text += "\n>>>Press o to reset game\n";
                        text += "\n>>>Press q to quit\n";
                        return text;
                    case GameMode.DIAL:
                        text += "\n>>>Press z to continue reading\n";
                        return text;
                    case GameMode.ALERT:
                        text += "\n>>>>>You are about to: " + OpName;
                        text += "\n>>>>>Press z to confirm";
                        text += "\n>>>>>Press y to cancel";
                        return text;
                    case GameMode.SET:
                        text += "\nSETTING GAME DIFFICULTY";
                        text += "\n>>>>>Press q for EASY";
                        text += "\n>>>>>Press w for HARD";
                        if (Diff == null)
                            text += "\nCannot return unless difficulty has been selected\n";
                        return text;
                    case GameMode.MAP:
                        text += "\nCHAPTER SELECTION";
                        text += "\n>>>>>Press z to select chapter";
                        text += "\n>>>>>Press q to return to menu";
                        if (_map == null)
                            text += "\nCannot return unless a chapter has been selected\n";
                        return text;
                }
                return "\nError";
            }
        }
        public GameMode Mode {
            get => _mode;
            set
            {
                _mode = value;
                _playCP.Mode = value;
            }
        }
        public string DisplayString
        {
            get
            {
                string text = "";
                text += "\n==========";
                text += "\n==========";
                if (Mode == GameMode.GAME)
                {
                    text += "\n----------";
                    text += "\n" + _playCP.DisplayMap;
                    text += "\n----------";
                }
                else if (Mode == GameMode.DIAL)
                {
                    text += Dialogue;
                }
                text += ModeInfo;
                text += "\n==========";
                text += "\n==========";
                if (_info != "")
                {
                    text += "\n----------";
                    text += "\n" + _info;
                    text += "\n----------";
                    _info = "";
                }
                return text;
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
        private bool CheckEnd()
        {
            return GameMaps.GetAvailMapCount() == 0;
        }
        private void CheckLose()
        {
            bool result = _playCP.CheckLose();
            if (result)
            {
                Viewer.Display("\nYou have lost! Please reset and try again.");
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
                Mode = GameMode.MENU;
        }
        public void SetEasy() 
        {
            if (OpName == "SET") { Diff = "EASY"; _info = "\n Difficulty set as EASY."; Mode = GameMode.MENU; }
            else { OpName = "SET"; SwitchAlert(); }
        }
        public void SetHard() 
        {
            if (OpName == "SET") { Diff = "HARD"; _info = "\n Difficulty set as HARD."; Mode = GameMode.MENU; }
            else { OpName = "SET"; SwitchAlert(); }
        }
        public void CheckWin()
        {
            bool result = _playCP.CheckWin();
            if (result)
            {
                Viewer.Display("\nYou 've finished the chapter!");
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
                    Viewer.Display("\nGame has ended!");
                    _playCP.Running = new RRLine(new ActionVoid(GameLoop.Reset));
                    GameLoop.Reset();
                }
            }
        }
        public void RunOp()
        {
            Mode = _pmode;
            _playCP.Running.Run();
            OpName = "";
            _playCP.Running = null;
        }
        public void CancelOp()
        {
            Mode = _pmode;
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
            //fail quest
            playCP.Player.FailQuest = new ActionUse(playCP.AddFail);
            //reset
            RRLine resetline = new RRLine(new ActionVoid(Reset));
            resetline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false));
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
            Mode = GameMode.SET;
            Dialogue = "";
            Alert = "";
            _info = "\nA new game has been loaded.";
            OpName = "";
        }
        public void Reset()
        {
            if (OpName == "RESET")
            {
                Reload();
            }
            else { OpName = "RESET"; SwitchAlert(); }
        }
        public void SaveQuit()
        {
            if (OpName == "SAVEQUIT") { Mode = GameMode.MENU; }
            else { OpName = "SAVEQUIT"; SwitchAlert(); }
        }
        public void QuitTheGame() {
            if (OpName == "QUIT") { IsQuit = true; }
            else { OpName = "QUIT"; SwitchAlert(); }
        }
        public void PlayTheGame() {
            if (_map != null && !IsWin && !IsLose)
                Mode = GameMode.GAME;
        }
    }
}
