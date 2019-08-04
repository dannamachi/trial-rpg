using System.Collections.Generic;

namespace StreetDetectiveG.src.gr
{
    public class Container
    {
        //fields
        protected List<Item> _items;
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
        public Item Remove(string name)
        {
            Item item = null;
            if (Have(name))
            {
                foreach (Item itm in _items)
                {
                    if (itm.Name == name) { item = itm; break; }
                }
                _items.Remove(item);
            }
            return item;
        }
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
                if (!_items.Contains(itm))
                    _items.Add(itm);
            }
            return HasSpace;
        }
    }
}
