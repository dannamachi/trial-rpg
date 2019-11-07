using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public enum GameMode
    {
        MENU,
        GAME,
        DIAL
    }
    public class GameCP : ViewLens {
        //fields
        private GameMode _mode;
        private PlayerCP _playCP;
        //constructors
        public GameCP() {
            //for game to keep track of quest finished -- a better way to do this?

            IsPlay = false;
            IsQuit = false;
            IsWin = false;
            _playCP = CreatePlayCP();
            Mode = GameMode.MENU;
            Dialogue = "";
        }
        //properties
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
                        text += "\n>>>Press f for action\n";
                        text += "\n>>>Press q to quit\n";
                        return text;
                    case GameMode.MENU:
                        text += "\nMAIN MENU";
                        text += "\n>>>Press z to play\n";
                        text += "\n>>>Press q to quit\n";
                        return text;
                    case GameMode.DIAL:
                        text += "\n>>>Press z to continue reading\n";
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
                return text;
            }
        }
        public bool IsPlay { get;set; }
        public bool IsQuit { get;set; }
        public bool IsWin { get;set; }
        //methods
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
            else { Mode = GameMode.GAME; }
        }
        public void PerformAction(PlayerInput input)
        {
            _playCP.PerformAction(input);
        }
        public PlayerCP CreatePlayCP() {
            //rrbuilder and such
            PlayerCP playCP = new PlayerCP();
            RRBuilder builder = new RRBuilder(this,playCP.Player);
            playCP.Initialize(builder.BuildPActs());
            return playCP;
        }
        public void QuitTheGame() {
            IsQuit = true;
        }
        public void PlayTheGame() {
            IsPlay = true;
            Mode = GameMode.GAME;
        }
    }
}
