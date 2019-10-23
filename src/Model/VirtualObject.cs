using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public abstract class VirtualObject {
        //fields
        //constructors
        //properties
        public string Name { get; set; }
        //methods
        public abstract bool IsCalled(string name);
    }
}
