using System;
using System.Collections.Generic;
using System.Text;
using SEGraphic;

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
            List<Button> bus;
            foreach (TileV tile in tiles)
            {
                if (tile.Object != null)
                {
                    if (tile.Object is ActionObject)
                    {
                        bus = new List<Button> { new ClickButton("game_but|button_use"), new KbButton(Microsoft.Xna.Framework.Input.Keys.C) };
                        (tile.Object as ActionObject).ActionLine.PlayerInput = PlayerInput.GetInput2(bus);
                    }
                    else if (tile.Object is GameObject)
                    {
                        bus = new List<Button> { new ClickButton("game_but|button_pickplace"), new KbButton(Microsoft.Xna.Framework.Input.Keys.E) };
                        ConLine hline = new ConLine(new ActionVoid(_player.HoldObject), new ConToken(tile.Object.Name + "|pickplace|000"));
                        hline.PlayerInput = PlayerInput.GetInput2(bus);
                        bus = new List<Button> { new ClickButton("game_but|button_pickplace"), new KbButton(Microsoft.Xna.Framework.Input.Keys.R) };
                        ConLine cline = new ConLine(new ActionVoid(_player.PlaceObject), new ConToken(tile.Object.Name + "|pickplace|000"));
                        cline.PlayerInput = PlayerInput.GetInput2(bus);
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
            pacts.Add(BuildPActMAP());
            pacts.Add(BuildPActINFO());
            pacts.Add(BuildPActCHOOSE());
            return pacts;
        }
        private PlayerAction BuildPActCHOOSE()
        {
            List<RRLine> chooselist = new List<RRLine>();
            List<Button> bus;

            //quitline
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Q), new ClickButton("choose_but|option_no") };
            RRLine quitline = new RRLine(new ActionVoid(_game.CancelChoice));
            quitline.PlayerInput = PlayerInput.GetInput2(bus);
            //updown
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.W), new ClickButton("choose_but|option_up") };
            RRLine upline = new RRLine(new ActionVoid(_game.IncrementChoice));
            upline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.S), new ClickButton("choose_but|option_down") };
            RRLine downline = new RRLine(new ActionVoid(_game.DecrementChoice));
            downline.PlayerInput = PlayerInput.GetInput2(bus);
            //pickline
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Z), new ClickButton("choose_but|option_yes") };
            RRLine pickline = new RRLine(new ActionVoid(_game.PerformChoice));
            pickline.PlayerInput = PlayerInput.GetInput2(bus);

            chooselist.Add(quitline);
            chooselist.Add(upline);
            chooselist.Add(downline);
            chooselist.Add(pickline);

            return new PlayerAction(chooselist, GameMode.CHOOSE);
        }
        private PlayerAction BuildPActINFO()
        {
            List<RRLine> infolist = new List<RRLine>();
            List<Button> bus;

            //updown
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.W), new ClickButton("info_but|info_up") };
            RRLine upline = new RRLine(new ActionVoid(_game.DecrementPage));
            upline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.S), new ClickButton("info_but|info_down") };
            RRLine downline = new RRLine(new ActionVoid(_game.IncrementPage));
            downline.PlayerInput = PlayerInput.GetInput2(bus);
            //return
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Q), new ClickButton("info_but|info_menu") };
            RRLine menuline = new RRLine(new ActionVoid(_game.SaveQuit));
            menuline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.E), new ClickButton("info_but|info_game") };
            RRLine gameline = new RRLine(new ActionVoid(_game.PlayTheGame));
            gameline.PlayerInput = PlayerInput.GetInput2(bus);

            infolist.Add(upline);
            infolist.Add(downline);
            infolist.Add(menuline);
            infolist.Add(gameline);

            return new PlayerAction(infolist, GameMode.INFO);
        }
        private PlayerAction BuildPActMAP()
        {
            List<RRLine> maplist = new List<RRLine>();
            List<Button> bus;

            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Z), new ClickButton("map_but|button_choosemap") };
            RRLine cmap = new RRLine(new ActionUse(_game.SetMap));
            cmap.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Q), new ClickButton("map_but|button_menu") };
            RRLine quitline = new RRLine(new ActionVoid(_game.SwitchMenu));
            quitline.PlayerInput = PlayerInput.GetInput2(bus);

            maplist.Add(cmap);
            maplist.Add(quitline);

            return new PlayerAction(maplist, GameMode.MAP);
        }
        private PlayerAction BuildPActSET()
        {
            List<RRLine> setlist = new List<RRLine>();
            List<Button> bus;

            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Q), new ClickButton("set_but|button_easy") };
            RRLine easyline = new RRLine(new ActionVoid(_game.SetEasy));
            easyline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.W), new ClickButton("set_but|button_hard") };
            RRLine hardline = new RRLine(new ActionVoid(_game.SetHard));
            hardline.PlayerInput = PlayerInput.GetInput2(bus);

            setlist.Add(easyline);
            setlist.Add(hardline);

            return new PlayerAction(setlist, GameMode.SET);
        }
        private PlayerAction BuildPActALERT()
        {
            List<RRLine> alertlist = new List<RRLine>();
            List<Button> bus;

            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Z), new ClickButton("alert_but|button_yes") };
            RRLine confirmline = new RRLine(new ActionVoid(_game.RunOp));
            confirmline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Y), new ClickButton("alert_but|button_no") };
            RRLine cancelline = new RRLine(new ActionVoid(_game.CancelOp));
            cancelline.PlayerInput = PlayerInput.GetInput2(bus);

            alertlist.Add(confirmline);
            alertlist.Add(cancelline);

            return new PlayerAction(alertlist, GameMode.ALERT);
        }
        private PlayerAction BuildPActDIAL()
        {
            List<RRLine> diallist = new List<RRLine>();
            List<Button> bus;

            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Z), new ClickButton("zone_overall") };
            RRLine readline = new RRLine(new ActionVoid(_game.ContinueDialogue));
            readline.PlayerInput = PlayerInput.GetInput2(bus);

            diallist.Add(readline);

            return new PlayerAction(diallist, GameMode.DIAL);
        }
        private PlayerAction BuildPActMENU() {
            List<RRLine> menulist = new List<RRLine>();
            List<Button> bus;

            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Q), new ClickButton("menu_but|button_exit") };
            RRLine quitline = new RRLine(new ActionVoid(_game.QuitTheGame));
            quitline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Z), new ClickButton("menu_but|button_start") };
            RRLine playline = new RRLine(new ActionVoid(_game.PlayTheGame));
            playline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.M), new ClickButton("menu_but|button_map") };
            RRLine mapline = new RRLine(new ActionVoid(_game.SwitchMap));
            mapline.PlayerInput = PlayerInput.GetInput2(bus);
            //RRLine setline = new RRLine(new ActionVoid(_game.SwitchSet));
            //setline.PlayerInput = PlayerInput.GetInput(new ConsoleKeyInfo('s', ConsoleKey.S, false, false, false));
            //RRLine resetline = new RRLine(new ActionVoid(_game.Reset));
            //resetline.PlayerInput = PlayerInput.GetInput(new ConsoleKeyInfo('x', ConsoleKey.X, false, false, false));
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.O) };
            RRLine resetline = new RRLine(new ActionVoid(GameLoop.Reset));
            resetline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.C), new ClickButton("menu_but|button_credits") };
            RRLine creditline = new RRLine(new ActionVoid(_game.DisplayCredits));
            creditline.Add(_game.SwitchInfo);
            creditline.PlayerInput = PlayerInput.GetInput2(bus);

            menulist.Add(quitline);
            menulist.Add(playline);
            menulist.Add(mapline);
            //menulist.Add(setline);
            //menulist.Add(resetline);
            menulist.Add(resetline);
            menulist.Add(creditline);

            return new PlayerAction(menulist, GameMode.MENU);
        }
        private PlayerAction BuildPActGAME() {
            List<RRLine> gamelist = new List<RRLine>();
            List<Button> bus;
            Button spec;

            //moving
            spec = new KbButton(Microsoft.Xna.Framework.Input.Keys.W);
            spec.IsSlow = false;
            bus = new List<Button> { spec };
            RRLine movelineW = new RRLine(new ActionMove(_player.Move));
            movelineW.PlayerInput = PlayerInput.GetInput2(bus);
            spec = new KbButton(Microsoft.Xna.Framework.Input.Keys.D);
            spec.IsSlow = false;
            bus = new List<Button> { spec };
            RRLine movelineD = new RRLine(new ActionMove(_player.Move));
            movelineD.PlayerInput = PlayerInput.GetInput2(bus);
            spec = new KbButton(Microsoft.Xna.Framework.Input.Keys.S);
            spec.IsSlow = false;
            bus = new List<Button> { spec };
            RRLine movelineS = new RRLine(new ActionMove(_player.Move));
            movelineS.PlayerInput = PlayerInput.GetInput2(bus);
            spec = new KbButton(Microsoft.Xna.Framework.Input.Keys.A);
            spec.IsSlow = false;
            bus = new List<Button> { spec };
            RRLine movelineA = new RRLine(new ActionMove(_player.Move));
            movelineA.PlayerInput = PlayerInput.GetInput2(bus);
            //flipline
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.F), new ClickButton("game_but|button_find") };
            RRLine flipline = new RRLine(new ActionVoid(_player.FlipTile));
            flipline.PlayerInput = PlayerInput.GetInput2(bus);
            //drops
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.M) };
            RRLine dropline = new RRLine(new ActionUse(_player.Drop));
            dropline.PlayerInput = PlayerInput.GetInput2(bus);
            //toggle
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.K), new ClickButton("game_but|button_toggleinv") };
            RRLine toggline = new RRLine(new ActionVoid(_player.ToggleStuff));
            toggline.PlayerInput = PlayerInput.GetInput2(bus);
            bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.L), new ClickButton("game_but|button_togglesys") };
            RRLine toggline2 = new RRLine(new ActionVoid(_player.ToggleComm));
            toggline2.PlayerInput = PlayerInput.GetInput2(bus);

            gamelist.Add(movelineW);
            gamelist.Add(movelineD);
            gamelist.Add(movelineS);
            gamelist.Add(movelineA);
            gamelist.Add(flipline);
            gamelist.Add(dropline);
            gamelist.Add(toggline);
            gamelist.Add(toggline2);

            return new PlayerAction(gamelist, GameMode.GAME);
        }
    }
}
