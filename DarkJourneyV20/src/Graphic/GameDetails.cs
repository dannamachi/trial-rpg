using System;
using System.Collections.Generic;
using System.Text;

namespace SEGraphic
{
    public class GameDetails
    {
        //fields
        //constructors
        public GameDetails()
        {
            X = 0;
            Y = 0;
            W = 0;
            H = 0;
        }
        public GameDetails(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }
        //properties
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        //methods
        public bool Contains(int x, int y)
        {
            if (x < X || x > X + W) return false;
            if (y < Y || y > Y + H) return false;
            return true;
        }
    }
}
