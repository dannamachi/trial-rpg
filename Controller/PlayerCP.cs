using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public enum GameMode {
    MENU,
    GAME
  }

  public class PlayerCP {
    //fields
    private List<PlayerAction> _pacts;
    //constructors
    public PlayerCP(List<PlayerAction> pacts, Player p) {
      _pacts = new List<PlayerAction>();
      foreach (PlayerAction pact in pacts) {
        _pacts.Add(pact);
      }
      Player = p;
    }
    //properties
    public Player Player { get; set; }
    public GameMode Mode { get;set; }
    //methods
    public void Initialize() {
      Mode = GameMode.MENU;
      RRBuilder builder = new RRBuilder();
    }
  }
}
