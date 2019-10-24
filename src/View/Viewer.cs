using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class Viewer
    {
        //fields
        //constructors
        public Viewer() { }
        //properties
        //methods
        public void Display(ViewLens lens)
        {
            Console.Write(lens.DisplayString);
        }
    }
}
