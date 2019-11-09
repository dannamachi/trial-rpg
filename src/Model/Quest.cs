using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class Quest : VirtualObject{
        //fields
        private List<Request> _reqs;
        //constructors
        public Quest(string name, List<Request> requests) {
            Name = name;
            _reqs = new List<Request>();
            foreach (Request req in requests) {
                _reqs.Add(req);
            }
            Description = "A quest, who knows?";
        }
        //properties
        public List<Request> QuestRequests
        {
            get
            {
                List<Request> reqs = new List<Request>();
                foreach (Request req in _reqs)
                {
                    if (req.GetNeededQuest != null)
                        reqs.Add(req);
                }
                return reqs;
            }
        }
        public List<Request> ArtifactRequests
        {
            get
            {
                List<Request> reqs = new List<Request>();
                foreach (Request req in _reqs)
                {
                    if (req.GetNeededArtifact != null)
                        reqs.Add(req);
                }
                return reqs;
            }
        }
        public string Description { get; set; }
        //methods
        public string Info()
        {
            string text = "\n" + Description;
            foreach (Request req in _reqs)
            {
                text += "\n" + req.Info();
            }
            return text;
        }
        public bool IsFulfilledBy(Player p) {
            foreach (Request req in _reqs) {
                if (!req.IsFulfilledBy(p)) {
                    return false;
                }
            }
            return true;
        }
    }
}
