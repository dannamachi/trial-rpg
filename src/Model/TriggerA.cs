using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class TriggerA : Trigger {
        //fields
        private bool _isf;
        private List<Artifact> _arts;
        //constructors
        public TriggerA(List<Artifact> arts) : base() {
            _arts = new List<Artifact>();
            foreach (Artifact art in arts) {
                _arts.Add(art);
            }
            _isf = false;
        }
        //properties
        public List<Artifact> Artifacts
        {
            get
            {
                if (!_isf) return _arts;
                else return new List<Artifact>();
            }
        }
        public override string Info
        {
            get
            {
                string text = "";
                text += "\nArtifact Trigger:";
                foreach (Artifact art in _arts)
                {
                    text += "\n\t" + art.Description;
                }
                return text;
            }
        }
        //methods
        protected override void PerformFlip(Player p) {
            if (!_isf)
            {
                foreach (Artifact art in _arts)
                {
                    p.Add(art);
                }
                if (GameCP.GetDiff == "HARD")
                    _isf = true;
            }
        }
    }
}
