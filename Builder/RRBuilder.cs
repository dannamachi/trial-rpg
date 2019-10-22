using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public enum ActionNode {
    MENU_QUIT,
    MENU_PLAY,
    GAME_MOVE,
    GAME_FLIP,
    GAME_QUIT
  }
  public class RRBuilder {
    //fields
    private GameCP _game;
    private Player _player;
    //constructors
    public RRBuilder(GameCP game, Player p) {
      _game = game;
      _player = p;
    }
    //properties
    //methods
    public List<PlayerAction> BuildPActs() {
      pacts = new List<PlayerAction>();
      pacts.Add(BuildPActMENU());
      pacts.Add(BuildPActGAME());
      return pacts;
    }

    private PlayerAction BuildPActMENU() {
      menulist = new List<RRLine>();

      RRLine quitline = new RRLine(new ActionVoid(_game.QuitTheGame));
      RRLine playline = new RRLine(new ActionVoid(_game.PlayTheGame));

      menulist.Add(quitline);
      menulist.Add(playline);

      return new PlayerAction(menulist, GameMode.MENU);
    }
    private PlayerAction BuildPActGAME() {
      gamelist = new List<RRLine>();

      RRLine quitline = new RRLine(new ActionVoid(_game.QuitTheGame));
      RRLine playline = new RRLine(new ActionVoid(_game.PlayTheGame));
      RRLine moveline = new RRLine(new ActionMove(_player.Move));
      RRLine flipline = new RRLine(new ActionVoid(_player.FlipTile));

      gamelist.Add(quitline);
      gamelist.Add(playline);
      gamelist.Add(moveline);
      gamelist.Add(flipline);

      return new PlayerAction(gamelist, GameMode.GAME);
    }
  }
}
