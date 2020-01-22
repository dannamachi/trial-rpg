using System;
using System.Collections.Generic;
using System.Text;

namespace SEGraphic
{
    public class GraphicObject
    {
        //fields
        private GameDetails _gd;
        //constructors
        public GraphicObject(GameDetails gd)
        {
            _gd = gd;
            Name = "Generic|Object";
            Priority = 0;
        }
        //properties
        public int X { get => _gd.X; }
        public int Y { get => _gd.Y; }
        public int Priority { get; set; }
        public string Name { get; set; }
        //methods
        public bool IsAbove(GraphicObject zn)
        {
            return Priority >= zn.Priority;
        }
        public bool IsCalled(string name)
        {
            return name.ToLower() == Name.ToLower();
        }
        public int[] GetDestRect()
        {
            return new int[4] { _gd.X, _gd.Y, _gd.W, _gd.H };
        }
        public void SetLocation(int x, int y)
        {
            _gd.X = x;
            _gd.Y = y;
        }
        public bool IsPressed(int x, int y)
        {
            return _gd.Contains(x, y);
        }
    }
}
