using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class GameCP {
        //fields
        //constructors
        public GameCP() {
            //for game to keep track of quest finished -- a better way to do this?

            IsPlay = false;
            IsQuit = false;
            IsWin = false;
        }
        //properties
        private bool IsPlay { get;set; }
        private bool IsQuit { get;set; }
        private bool IsWin { get;set; }
        //methods
        public PlayerCP CreatePlayCP(Player p) {
            //rrbuilder and such
            RRBuilder builder = new RRBuilder(this,p);
            PlayerCP playCP = new PlayerCP(builder.BuildPActs(),p);
            return playCP;
        }
        public void QuitTheGame() {
            IsQuit = true;
        }
        public void PlayTheGame() {
            IsPlay = true;
        }
    }
}
