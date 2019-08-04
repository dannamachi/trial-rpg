using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class Quest
    {
        //fields
        private List<Request> _reqs;
        //properties
        public List<string> RequestList
        {
            get
            {
                List<string> lines = new List<string>();
                for (int i = 0; i < RequestCount; i++)
                {
                    lines.Add(_reqs[i].PrintInfo());
                }
                return lines;
            }
        }
        public int RequestCount { get => _reqs.Count; }
        //constructors
        public Quest()
        {
            _reqs = new List<Request>();
        }
        //methods
        public void AddRequest(Request req)
        {
            _reqs.Add(req);
        }
        public bool IsFulfilledBy(Player p)
        {
            foreach (Request req in _reqs)
            {
                if (!req.IsFulfilledBy(p)) return false;
            }
            return true;
        }
    }
}
