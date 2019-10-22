using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public enum TDir {
    TOP,
    LEFT,
    BOTTOM,
    RIGHT
  }
  public class TileV {
    //fields
    private Dictionary<TDir,TileV> _tileDict;
    //constructors
    public TileV(Trigger trig, Dictionary<TDir,TileV> tileDict, bool canMoveTo) {
      Trigger = trig;
      Blocked = canMoveTo;
      _tileDict = tileDict;
    }
    public TileV() : this(null,null,false) {}
    public TileV(Dictionary<TDir,TileV> tileDict) : this(null,tileDict,true) {}
    //properties
    public Trigger Trigger { get;set; }
    public bool Blocked { get;set; }
    //methods
    public TileV GetNextTile(TDir dir) {
      return _tileDict[dir];
    }
    public bool CanMoveTo(TDir dir) {
      return _tileDict.ContainsKey(dir);
    }
  }
}
