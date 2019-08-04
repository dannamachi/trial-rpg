using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class QuestTrigger : Trigger
    {
        //fields
        private Quest _quest;
        //properties
        //constructors
        public QuestTrigger(Quest quest)
        {
            _quest = quest;
        }
        //methods
        public override string PrintInfo()
        {
            string requests = "";
            foreach (string line in _quest.RequestList)
            {
                requests += line + "&";
            }
            requests = requests.Substring(0, requests.Length - 1); 
            return requests;
        }
        public override void FlippedBy(Player p)
        {
            p.TakeQuest(_quest);
        }
    }
}
