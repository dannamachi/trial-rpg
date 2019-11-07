using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
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
            List<PlayerAction> pacts = new List<PlayerAction>();
            pacts.Add(BuildPActMENU());
            pacts.Add(BuildPActGAME());
            pacts.Add(BuildPActDIAL());
            return pacts;
        }

        private PlayerAction BuildPActDIAL()
        {
            List<RRLine> diallist = new List<RRLine>();

            RRLine readline = new RRLine(new ActionVoid(_game.ContinueDialogue));
            readline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('z', ConsoleKey.Z, false, false, false));

            diallist.Add(readline);

            return new PlayerAction(diallist, GameMode.DIAL);
        }
        private PlayerAction BuildPActMENU() {
            List<RRLine> menulist = new List<RRLine>();

            RRLine quitline = new RRLine(new ActionVoid(_game.QuitTheGame));
            quitline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false));
            RRLine playline = new RRLine(new ActionVoid(_game.PlayTheGame));
            playline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('z', ConsoleKey.Z, false, false, false));

            menulist.Add(quitline);
            menulist.Add(playline);

            return new PlayerAction(menulist, GameMode.MENU);
        }
        private PlayerAction BuildPActGAME() {
            List<RRLine> gamelist = new List<RRLine>();

            //quitting
            RRLine quitline = new RRLine(new ActionVoid(_game.QuitTheGame));
            quitline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false));
            //moving
            RRLine movelineW = new RRLine(new ActionMove(_player.Move));
            movelineW.PlayerInput = new PlayerInput(new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false));
            RRLine movelineD = new RRLine(new ActionMove(_player.Move));
            movelineD.PlayerInput = new PlayerInput(new ConsoleKeyInfo('d', ConsoleKey.D, false, false, false));
            RRLine movelineS = new RRLine(new ActionMove(_player.Move));
            movelineS.PlayerInput = new PlayerInput(new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false));
            RRLine movelineA = new RRLine(new ActionMove(_player.Move));
            movelineA.PlayerInput = new PlayerInput(new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false));
            //flipline
            RRLine flipline = new RRLine(new ActionVoid(_player.FlipTile));
            flipline.Add(new ActionVoid(_game.StartDialogue));
            flipline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('f', ConsoleKey.F, false, false, false));

            gamelist.Add(quitline);
            gamelist.Add(movelineW);
            gamelist.Add(movelineD);
            gamelist.Add(movelineS);
            gamelist.Add(movelineA);
            gamelist.Add(flipline);

            return new PlayerAction(gamelist, GameMode.GAME);
        }
    }
}
