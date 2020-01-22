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
        public override bool IsFlipped { get => _isf; }
        public override List<string> Namelist
        {
            get
            {
                List<string> alist = new List<string>();
                foreach (Quest q in _quests)
                {
                    alist.Add(q.Name + " - " + q.Description);
                }
                return alist;
            }
        }
        public override int Count { get => _quests.Count; }
        public List<Quest> Quests
        {
            get
            {
                if (!_isf)
                    return _quests;
                return new List<Quest>();
            }
        }
        public override string Info
        {
            get
            {
                string text = "";
                text += "\nQuest Trigger:";
                foreach (Quest q in _quests)
                {
                    text += "\n\t" + q.Info();
                }
                return text;
            }
        }
        //methods
        public List<Quest> GetQuests()
        {
            return _quests;
        }
        protected override void PerformFlip(Player p) {
            if (!_isf)
            {
                foreach (Quest q in _quests)
                {
                    p.Add(q);
                }
                if (p.Diff.Name == "HARD")
                    _isf = true;
            }
        }
    }
}
