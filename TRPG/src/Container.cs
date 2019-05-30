using System.Collections.Generic;

namespace TRPG.src
{
    public class Container
    {
        //fields
        private List<Item> _items;
        //properties
        public int Count { get => _items.Count; }
        public int Capacity { get; set; }
        public bool HasSpace { get => Count < Capacity; }
        //constructors
        public Container()
        {
            _items = new List<Item>();
            Capacity = 10;
        }
        //methods
        public bool Have(string name)
        {
            foreach (Item itm in _items)
            {
                if (itm.Name == name) { return true; }
            }
            return false;
        }
        public bool Take(Item itm)
        {
            if (Count < Capacity)
            {
                _items.Add(itm);
            }
            return HasSpace;
        }
    }
}
