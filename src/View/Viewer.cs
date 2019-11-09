using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public static class Viewer
    {
        //fields
        //constructors
        //properties
        //methods
        public static void Display(string ash)
        {
            Console.Write(ash);
        }
        public static void Display(ViewLens lens)
        {
            Console.Write(lens.DisplayString);
        }
    }
}
