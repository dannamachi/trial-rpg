using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class RRLine {
    public delegate void ActionVoid();
    public delegate void ActionMove(TDir dir);
    //fields
    private Delegate _singleFunc;
    //constructors
    public RRLine(Delegate func) {
      _singleFunc = func;
      Activated = false;
    }
    //properties
    public bool Activated { get;set; }
    //methods
    public void Run() {
      _singleFunc();
    }
    public void Run(TDir dir) {
      _singleFunc(dir);
    }
  }
}
