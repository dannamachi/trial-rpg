using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class GameObject : VirtualObject
    {
        //fields
        private RRLine _move;
        private RRLine _place;
        private bool _ish;
        //constructors
        public GameObject(string name)
        {
            Name = name;
            _ish = false;
        }
        //properties
        public virtual RRLine ActionLine { 
            get
            {
                if (_ish) return _place;
                else return _move;
            } 
        }
        //methods
        public void SetMovePlace(RRLine move, RRLine place)
        {
            _move = move;
            _place = place;
        }
        public void ToggleHold()
        {
            _ish = !_ish;
        }
    }
}
