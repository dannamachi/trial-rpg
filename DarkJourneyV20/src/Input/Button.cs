using System;
using System.Collections.Generic;
using System.Linq;


namespace SEGraphic
{
    public abstract class Button
    {
        //static
        public abstract MonoButton GetMono();
        //fields
        //constructors
        public Button()
        {
            IsSlow = true;
        }
        //properties
        public bool IsSlow { get; set; }
        //methods
        public abstract bool Equals(Button bu);
    }
}
