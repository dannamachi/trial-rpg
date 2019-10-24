using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class GameLoop {
    //fields
        private GameCP _gameCP;
        private Viewer _view;
        //constructors
        public GameLoop() {
            _gameCP = new GameCP();
            _view = new Viewer();
        }
        //properties
        //methods
        public void Run()
        {
            while (!_gameCP.IsQuit)
            {
                ProcessInput();
                Display();
            }
        }
        private void ProcessInput() {
            ConsoleKeyInfo cki = Console.ReadKey();
            PlayerInput input = new PlayerInput(cki);
            _gameCP.PerformAction(input);
        }
        private void Display() {
            _view.Display(_gameCP);
        }
    }
}
