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
    public void ProcessInput() {

    }
    public void Display() {

    }
  }
}
