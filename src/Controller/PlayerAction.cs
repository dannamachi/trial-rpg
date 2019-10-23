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
        public List<RRLine> ValidLines { get => _actionLines; }
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
