using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public delegate void ActionVoid();
    public delegate void ActionMove(TDir dir);
    public delegate void ActionUse(string name);
    public class RRLine {
        //fields
        private List<ActionVoid> _voidFuncs;
        private ActionMove _moveFunc;
        private ActionUse _useFunc;

        //constructors
        private RRLine()
        {
            _voidFuncs = new List<ActionVoid>();
            _moveFunc = null;
            _useFunc = null;
            Activated = false;
        }
        public RRLine(ActionUse usefunc) : this()
        {
            _useFunc = usefunc;
        }
        public RRLine(ActionVoid voidfunc) : this() 
        {
            _voidFuncs.Add(voidfunc);
        }
        public RRLine(ActionMove movefunc) : this()
        {
            _moveFunc = movefunc;
        }
        //properties
        public PlayerInput PlayerInput { get; set; }
        public bool Activated { get;set; }
        //methods
        public void Add(ActionVoid av)
        {
            _voidFuncs.Add(av);
        }
        public bool IsOfType(string type)
        {
            if (type == "V" && _voidFuncs.Count != 0) { return true; }
            if (type == "M" && _moveFunc != null) { return true; }
            return false;
        }
        public bool IsFlagged(PlayerInput inp)
        {
            return inp.EqualsTo(PlayerInput);
        }
        public void Run(string name)
        {
            _useFunc(name);
        }
        public void Run() {
            foreach (ActionVoid av in _voidFuncs)
            {
                av();
            }
        }
        public void Run(TDir dir) {
            _moveFunc(dir);
        }
    }
}
