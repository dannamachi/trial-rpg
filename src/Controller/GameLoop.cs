using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class GameLoop {
    //fields
        private GameCP _gameCP;
        //constructors
        public GameLoop() {
            _gameCP = new GameCP();
        }
        //properties
        //methods
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
            _gameCP.CheckWin();
            if (_gameCP.IsWin)
            {
                _gameCP.Reset();
                _gameCP.Running = new RRLine(new ActionVoid(_gameCP.Reset));
            }
        }
        private void Display() {
            Viewer.Display(_gameCP);
        }
    }
}
