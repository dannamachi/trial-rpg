using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public abstract class VirtualObject {
        //fields
        //constructors
        //properties
        public virtual string Name { get; set; }
        //methods
        public virtual bool IsCalled(string name)
        {
            return name.ToLower() == Name.ToLower();
        }
    }
}
