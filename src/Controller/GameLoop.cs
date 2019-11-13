using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class GameLoop {
    //fields
        private static GameCP _gameCP = new GameCP();
        //constructors
        public GameLoop() { }
        //properties
        //methods
        public static void Reset()
        {
            if (_gameCP.OpName == "TOTALRESET")
            {
                GamePath.Renew();
                _gameCP = new GameCP();
            }
            else
            {
                _gameCP.OpName = "TOTALRESET";
                _gameCP.SwitchAlert();
            }
        }
        public void Run()
        {
            while (!_gameCP.IsQuit)
            {
                Display();
                ProcessInput();
            }
        }
        private void ProcessInput() {
            ConsoleKeyInfo cki = Console.ReadKey();
            PlayerInput input = new PlayerInput(cki);
            _gameCP.PerformAction(input);
        }
        private void Display() {
            Viewer.Display(_gameCP);
        }
    }
}
