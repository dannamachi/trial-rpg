using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    /// <summary>
    /// Floating point value out of 100
    /// with 100 being maximum screen width/height
    /// </summary>
    public class PVector
    {
        //fields
        //properties
        public int X { get; set; }
        public int Y { get; set; }
        //constructors
        public PVector(int x, int y)
        {
            if (x < 0) X = 0;
            if (x > 100) X = 100;
            if (y < 0) Y = 0;
            if (y > 100) Y = 100;
            X = x;
            Y = y;
        }
        //methods
        public bool IsAt(PVector pvt)
        {
            return (pvt.X == X) && (pvt.Y == Y);
        }
    }
}
