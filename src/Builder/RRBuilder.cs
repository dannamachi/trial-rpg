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
        public List<TileV> SetInput(List<TileV> tiles)
        {
            foreach (TileV tile in tiles)
            {
                if (tile.Object != null)
                {
                    if (tile.Object is ActionObject)
                    {
                        (tile.Object as ActionObject).ActionLine.PlayerInput = new PlayerInput(new ConsoleKeyInfo('c', ConsoleKey.C, false, false, false));
                    }
                    else if (tile.Object is GameObject)
                    {
                        RRLine hline = new RRLine(new ActionVoid(_player.HoldObject));
                        hline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('e', ConsoleKey.E, false, false, false));
                        ConLine cline = new ConLine(new ActionVoid(_player.PlaceObject), new ConToken(tile.Object.Name + "|place|000"));
                        cline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('r', ConsoleKey.R, false, false, false));
                        tile.Object.SetMovePlace(hline, cline);
                    }
                }
            }
            return tiles;
        }
        public List<PlayerAction> BuildPActs() {
            List<PlayerAction> pacts = new List<PlayerAction>();
            pacts.Add(BuildPActMENU());
            pacts.Add(BuildPActGAME());
            pacts.Add(BuildPActDIAL());
            pacts.Add(BuildPActALERT());
            pacts.Add(BuildPActSET());
            return pacts;
        }
        private PlayerAction BuildPActSET()
        {
            List<RRLine> setlist = new List<RRLine>();

            RRLine easyline = new RRLine(new ActionVoid(_game.SetEasy));
            easyline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false));
            RRLine hardline = new RRLine(new ActionVoid(_game.SetHard));
            hardline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false));

            setlist.Add(easyline);
            setlist.Add(hardline);

            return new PlayerAction(setlist, GameMode.SET);
        }
        private PlayerAction BuildPActALERT()
        {
            List<RRLine> alertlist = new List<RRLine>();

            RRLine confirmline = new RRLine(new ActionVoid(_game.RunOp));
            confirmline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('z', ConsoleKey.Z, false, false, false));
            RRLine cancelline = new RRLine(new ActionVoid(_game.CancelOp));
            cancelline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('y', ConsoleKey.Y, false, false, false));

            alertlist.Add(confirmline);
            alertlist.Add(cancelline);

            return new PlayerAction(alertlist, GameMode.ALERT);
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
            RRLine setline = new RRLine(new ActionVoid(_game.SwitchSet));
            setline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false));
            RRLine resetline = new RRLine(new ActionVoid(_game.Reset));
            resetline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false));

            menulist.Add(quitline);
            menulist.Add(playline);
            menulist.Add(setline);
            menulist.Add(resetline);

            return new PlayerAction(menulist, GameMode.MENU);
        }
        private PlayerAction BuildPActGAME() {
            List<RRLine> gamelist = new List<RRLine>();

            //quitting
            RRLine quitline = new RRLine(new ActionVoid(_game.SaveQuit));
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
            flipline.PlayerInput = new PlayerInput(new ConsoleKeyInfo('f', ConsoleKey.F, false, false, false));
            //checklose
            RRLine checklose = new RRLine(new ActionVoid(_game.CheckLose));
            checklose.PlayerInput = new PlayerInput(new ConsoleKeyInfo('v', ConsoleKey.V, false, false, false));

            gamelist.Add(quitline);
            gamelist.Add(movelineW);
            gamelist.Add(movelineD);
            gamelist.Add(movelineS);
            gamelist.Add(movelineA);
            gamelist.Add(flipline);
            gamelist.Add(checklose);

            return new PlayerAction(gamelist, GameMode.GAME);
        }
    }
}
