using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public delegate void ActionVoid();
    public delegate void ActionMove(TDir dir);
    public class RRLine {
        //fields
        private ActionVoid _voidFunc;
        private ActionMove _moveFunc;

        //constructors
        public RRLine(ActionVoid voidfunc) {
            _voidFunc = voidfunc;
            _moveFunc = null;
            Activated = false;
        }
        public RRLine(ActionMove movefunc)
        {
            _moveFunc = movefunc;
            _voidFunc = null;
            Activated = false;
        }
        //properties
        public PlayerInput PlayerInput { get; set; }
        public bool Activated { get;set; }
        //methods
        public bool IsOfType(string type)
        {
            if (type == "V" && _voidFunc != null) { return true; }
            if (type == "M" && _moveFunc != null) { return true; }
            return false;
        }
        public bool IsFlagged(PlayerInput inp)
        {
            return inp.EqualsTo(PlayerInput);
        }
        public void Run() {
            _voidFunc();
        }
        public void Run(TDir dir) {
            _moveFunc(dir);
        }
    }
}
