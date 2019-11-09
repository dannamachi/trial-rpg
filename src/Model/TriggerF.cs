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
        public bool CanBeFulfilledBy(Player p)
        {
            foreach (string questName in _questNames)
            {
                if (p.Has(questName, "Q"))
                {
                    if ((p.Find(questName, "Q") as Quest).IsFulfilledBy(p))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected override void PerformFlip(Player p) {
            foreach (string questName in _questNames) {
                if (p.Has(questName,"Q")) {
                    if ((p.Find(questName,"Q") as Quest).IsFulfilledBy(p)) {
                        p.Complete(questName);
                    }
                    else
                    {
                        p.Remove(questName, "Q");
                    }
                }
            }
        }
    }
}
