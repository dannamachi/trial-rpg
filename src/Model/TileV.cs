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
        private int _x;
        private int _y;
        private Dictionary<TDir,TileV> _tileDict;
        //constructors
        public TileV(Trigger trig, bool canMoveTo, int x, int y) {
            Trigger = trig;
            Blocked = !canMoveTo;
            _x = x;
            _y = y;
            _tileDict = new Dictionary<TDir, TileV>();
        }
        //properties
        public int X { get => _x; }
        public int Y { get => _y; }
        public Trigger Trigger { get;set; }
        public bool Blocked { get;set; }
        //methods
        public bool IsAt(int x, int y)
        {
            return _x == x && _y == y;
        }
        public void LinkTileDict(Dictionary<TDir,TileV> dict)
        {
            _tileDict = dict;
        }
        private void LinkTileV(TileV tileV,TDir dir)
        {
            if (tileV != null)
                _tileDict[dir] = tileV;
        }
        public TileV GetNextTile(TDir dir) {
            return _tileDict[dir];
        }
        public bool CanMoveTo(TDir dir) {
            return _tileDict.ContainsKey(dir);
        }
    }
}
