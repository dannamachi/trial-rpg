using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class Inventory {
        //fields
        private List<VirtualObject> _arts;
        //constructors
        public Inventory() {
            _arts = new List<VirtualObject>();
        }
        //properties
        public VirtualObject GetLast { get => _arts[Count - 1]; }
        public int Count { get => _arts.Count; }
        public List<string> NameList
        {
            get 
            {
                List<string> namelist = new List<string>();
                foreach (VirtualObject vo in _arts)
                {
                    namelist.Add(vo.Name);
                }
                return namelist;
            }
        }
        //methods
        public VirtualObject Find(int num)
        {
            if (num >= 0 && num < Count)
            {
                return _arts[num];
            }
            return null;
        }
        public void CutDownTo(int num)
        {
            if (num >= 0)
            {
                List<VirtualObject> newlist = new List<VirtualObject>();
                for (int i = 0; i < num; i++)
                {
                    newlist.Add(_arts[i]);
                }
                _arts = newlist;
            }
        }
        ///add an artifact to inventory
        public void Add(VirtualObject art) {
            if (!(_arts.Contains(art)))
                _arts.Add(art);
        }   
        ///return boolean whether inventory contains an artifact (by name)
        public bool Has(string name) {
            foreach(VirtualObject art in _arts) {
                if (art.IsCalled(name))
                    return true;
            }
            return false;
        }
        ///return artifact/null in inventory without removing
        public VirtualObject Find(string name) {
            foreach (VirtualObject art in _arts) {
                if (art.IsCalled(name)) {
                    return art;
                }
            }
            return null;
        }
        ///return artifact/null with removing
        public VirtualObject Remove(string name) {
            VirtualObject art = Find(name);
            if (name != null)
                _arts.Remove(art);
            return art;
        }
    }
}
