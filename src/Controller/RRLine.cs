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
            Activated = false;
        }
        public RRLine(ActionMove movefunc)
        {
            _moveFunc = movefunc; 
            Activated = false;
        }
        //properties
        public bool Activated { get;set; }
        //methods
        public void Run() {
            _voidFunc();
        }
        public void Run(TDir dir) {
            _moveFunc(dir);
        }
    }
}
