using System;
using System.Collections.Generic;
using SEGraphic;
using System.Text;

namespace SEVirtual {
    public class PlayerAction {
        //fields
        private GameMode _mode;
        private List<RRLine> _actionLines;
        //constructors
        public PlayerAction(List<RRLine> rrlines, GameMode mode) {
            _actionLines = new List<RRLine>();
            foreach (RRLine rrline in rrlines) {
            _actionLines.Add(rrline);
            }
            _mode = mode;
        }
        //properties
        public GameMode Mode { get => _mode; }
        public List<PlayerInput> ValidInputs
        {
            get
            {
                List<PlayerInput> inputs = new List<PlayerInput>();
                foreach (RRLine line in _actionLines)
                {
                    if (line.Activated)
                        inputs.Add(line.PlayerInput);
                }
                return inputs;
            }
        }
        //methods
        public List<RRLine> GetValidLines(PlayerCP playCP)
        {
            Player p = playCP.Player;
            List<RRLine> lines = new List<RRLine>();
            foreach (RRLine line in _actionLines)
            {
                lines.Add(line);
            }
            //real time variable actions
            if (Mode == GameMode.CHOOSE)
            {

            }
            else if (Mode == GameMode.GAME)
            {
                if (p.Tile != null)
                {
                    if (p.Tile.Object is ActionObject)
                    {
                        ActionObject ao = p.Tile.Object as ActionObject;
                        if (!ao.IsSolvedBy(p))
                        {
                            if ((ao.ActionLine as ConLine).IsDoableBy(p))
                                lines.Add(p.Tile.Object.ActionLine);
                        }
                    }
                    else if (p.Tile.Object is GameObject && p.Holding == null)
                    {
                        lines.Add(p.Tile.Object.ActionLine);
                        //pickplace
                    }
                }
                if (p.Holding != null)
                {
                    lines.Add(p.Holding.ActionLine);
                    //pickplace
                }
                if (p.EComm)
                {
                    //see complete
                    List<Button> bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.P), new ClickButton("game_but|buttonsys_complete") };
                    RRLine seeline = new RRLine(new ActionVoid(playCP.DisplayComplete));
                    seeline.Add(playCP.SwitchInfo);
                    seeline.PlayerInput = PlayerInput.GetInput2(bus);
                    //helpline
                    bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.H), new ClickButton("game_but|buttonsys_help") };
                    RRLine helpline = new RRLine(new ActionVoid(playCP.DisplayHelp));
                    helpline.Add(playCP.SwitchInfo);
                    helpline.PlayerInput = PlayerInput.GetInput2(bus);
                    //dconvo
                    bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.J), new ClickButton("game_but|buttonsys_journal") };
                    RRLine jline = new RRLine(new ActionVoid(playCP.DisplayConvo));
                    jline.Add(playCP.SwitchInfo);
                    jline.PlayerInput = PlayerInput.GetInput2(bus);
                    //quitline
                    bus = new List<Button> { new KbButton(Microsoft.Xna.Framework.Input.Keys.Q), new ClickButton("game_but|buttonsys_savequit") };
                    RRLine quitline = new RRLine(new ActionVoid(playCP.SaveQuit));
                    quitline.PlayerInput = PlayerInput.GetInput2(bus);

                    lines.Add(seeline);
                    lines.Add(helpline);
                    lines.Add(jline);
                    lines.Add(quitline);
                }
            }
            else if (Mode == GameMode.DIAL)
            {
                if (p.Tile.Storybook != null)
                {
                    if (p.Tile.Storybook.NeedChoice)
                    {
                        lines.Add(p.ChooseDialogue);
                    }
                }
            }
            else if (Mode == GameMode.MENU)
            {
                lines.Add(p.ResetGame);
            }
            return lines;
        }
        public void ActivateLines() {
            foreach (RRLine line in _actionLines) {
            line.Activated = true;
            }
        }
        public void DeactivateLines() {
            foreach (RRLine line in _actionLines) {
            line.Activated = false;
            }
        }
        public bool IsUsedIn(GameMode mode) {
            return mode == _mode;
        }
    }
}
