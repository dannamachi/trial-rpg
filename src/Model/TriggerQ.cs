using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class TriggerQ : Trigger {
        //fields
        private bool _isf;
        private List<Quest> _quests;
        //constructors
        public TriggerQ(List<Quest> quests) : base() {
            _quests = new List<Quest>();
            foreach (Quest q in quests) {
            _quests.Add(q);
            }
            _isf = false;
        }
        //properties
        //methods
        protected override void PerformFlip(Player p) {
            if (!_isf)
            {
                foreach (Quest q in _quests)
                {
                    p.Add(q);
                }
                _isf = true;
            }
        }
    }
}
