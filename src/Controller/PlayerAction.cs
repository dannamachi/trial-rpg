using System;
using System.Collections.Generic;
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
        public List<RRLine> GetValidLines(Player p)
        {
            List<RRLine> lines = new List<RRLine>();
            foreach (RRLine line in _actionLines)
            {
                lines.Add(line);
            }
            //real time variable actions
            if (Mode == GameMode.GAME)
            {
                if (p.Tile != null)
                {
                    if (p.Tile.Object is ActionObject)
                    {
                        if ((p.Tile.Object.ActionLine as ConLine).IsDoableBy(p))
                            lines.Add(p.Tile.Object.ActionLine);
                    }
                    else if (p.Tile.Object is GameObject && p.Holding == null)
                    {
                        lines.Add(p.Tile.Object.ActionLine);
                    }
                }
                if (p.Holding != null) { lines.Add(p.Holding.ActionLine); }
            }
            else if (Mode == GameMode.DIAL)
            {
                if (p.Tile.Storybook != null)
                {
                    if (p.Tile.Storybook.NeedChoice)
                    {
                        lines.Add(p.ChooseDialogue);
                        p.Tile.Storybook.NeedChoice = false;
                    }
                }
            }
            else if (Mode == GameMode.MENU)
            {
                if (p.Diff.Name == "EASY")
                {
                    lines.Add(p.ResetGame);
                }
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
