using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class GameLoop {
    //fields
        private GameCP _gameCP;
        private PlayerCP _playerCP;
        //constructors
        public GameLoop() {
            _gameCP = new GameCP();
            Player player = new Player();
            _playerCP = _gameCP.CreatePlayCP(player);
        }
        //properties
        //methods
        private bool IsValidInput(ConsoleKeyInfo cki, List<ConsoleKey> validCKs) {
            bool result = false;
            foreach (ConsoleKey CK in validCKs) {
            if (cki.Key == CK) {
                result = true;
            }
            }
            return result;
        }
        public void ProcessInput() {
            //look into linking ConsoleKey and PlayerAction 
        }
        public void Display() {

        }
    }
}
