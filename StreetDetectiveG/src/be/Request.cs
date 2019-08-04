using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public abstract class Request
    {
        //fields
        //properties
        //constructors
        //methods
        public abstract bool IsFulfilledBy(Player p);
        public abstract string PrintInfo();
    }
}
