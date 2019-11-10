using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class TriggerF : Trigger {
        //fields
        private List<string> _questNames;
        //constructors
        public TriggerF(List<string> questNames) : base() {
            _questNames = new List<string>();
            foreach (string name in questNames) {
                _questNames.Add(name);
            }
        }
        //properties
        public override int Count { get => _questNames.Count; }
        public override List<string> Namelist { get => _questNames; }
        public override string Info
        {
            get
            {
                string text = "";
                text += "\nFinish Trigger:";
                foreach (string qname in _questNames)
                {
                    text += "\n\t" + qname;
                }
                return text;
            }
        }
        //methods
        protected override void PerformFlip(Player p) {
            foreach (string questName in _questNames) {
                p.Attempt(questName);
            }
        }
    }
}
