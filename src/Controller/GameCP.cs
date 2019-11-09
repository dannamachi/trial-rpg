using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public enum GameMode
    {
        MENU,
        GAME,
        DIAL,
        ALERT
    }
    public class GameCP : ViewLens {
        //fields
        private GameMode _pmode;
        private GameMode _mode;
        private PlayerCP _playCP;
        private string _info;
        private VirtualObject _opname;
        //constructors
        public GameCP() {
            //for game to keep track of quest finished -- a better way to do this?

            IsPlay = false;
            IsQuit = false;
            IsWin = false;
            _opname = new VirtualObject();
            _playCP = CreatePlayCP();
            _playCP.Player.OpName = _opname;
            _playCP.Player.SwitchAlert = new ActionVoid(SwitchAlert);
            Mode = GameMode.MENU;
            Dialogue = "";
            Alert = "";
            _info = "";
        }
        //properties
        public RRLine SecondRun { get; set; }
        public RRLine Running { get => _playCP.Running; set => _playCP.Running = value; }
        public string OpName { get => _opname.Name; set => _opname.Name = value; }
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
                        text += "\n>>>Press x to reset\n";
                        text += "\n>>>Press q to quit\n";
                        return text;
                    case GameMode.DIAL:
                        text += "\n>>>Press z to continue reading\n";
                        return text;
                    case GameMode.ALERT:
                        text += "\n>>>>>You are about to: " + OpName;
                        text += "\n>>>>>Press z to confirm: ";
                        text += "\n>>>>>Press y to cancel: ";
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
        public bool IsPlay { get;set; }
        public bool IsQuit { get;set; }
        public bool IsWin { get;set; }
        //methods
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
            if (Dialogue == null)
            {
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
            RRBuilder builder = new RRBuilder(this,playCP.Player);
            playCP.Initialize(builder.BuildPActs());
            playCP.SetInput(builder);
            playCP.Player.RunDialogue = new ActionVoid(RunDialogue);
            return playCP;
        }
        public void Reset()
        {
            if (OpName == "RESET")
            {
                IsPlay = false;
                IsQuit = false;
                IsWin = false;
                _playCP = CreatePlayCP();
                Mode = GameMode.MENU;
                Dialogue = "";
                _info = "\nGame has been reset.";
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
            IsPlay = true;
            Mode = GameMode.GAME;
        }
    }
}
