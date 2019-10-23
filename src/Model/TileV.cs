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
        public TileV(Trigger trig, Dictionary<TDir,TileV> tileDict, bool canMoveTo, int x, int y) {
            Trigger = trig;
            Blocked = canMoveTo;
            _tileDict = tileDict;
            X = x;
            Y = y;
        }
        public TileV(int x, int y) : this(null,null,false,x,y) {}
        public TileV(Dictionary<TDir,TileV> tileDict,int x,int y) : this(null,tileDict,true,x,y) {}
        //properties
        public int X { get; }
        public int Y { get; }
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
