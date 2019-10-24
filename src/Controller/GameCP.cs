using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class GameCP : ViewLens {
        //fields
        private PlayerCP _playCP;
        //constructors
        public GameCP() {
            //for game to keep track of quest finished -- a better way to do this?

            IsPlay = false;
            IsQuit = false;
            IsWin = false;
            _playCP = CreatePlayCP();
        }
        //properties
        public string DisplayString
        {
            get
            {
                string text = "";
                text += "\n==========";
                text += "\n==========";
                text += _playCP.DisplayString;
                text += "\n==========";
                text += "\n==========";
                return text;
            }
        }
        public bool IsPlay { get;set; }
        public bool IsQuit { get;set; }
        public bool IsWin { get;set; }
        //methods
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
            _playCP.Mode = GameMode.GAME;
        }
    }
}
