using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class Inventory
    {
        //fields
        private List<Artifact> _arts;
        //properties
        //constructors
        public Inventory()
        {
            _arts = new List<Artifact>();
        }
        //methods
        public Artifact LookFor(string name)
        {
            foreach (Artifact art in _arts)
            {
                if (art.IsCalled(name))
                {
                    return art;
                }
            }
            return null;
        }
        public bool Has(string name)
        {
            return LookFor(name) != null;
        }
        public void Add(Artifact art)
        {
            _arts.Add(art);
        }
        public void Remove(Artifact art)
        {
            _arts.Remove(LookFor(art.Name));
        }
    }
}
