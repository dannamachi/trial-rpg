using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class Artifact : VirtualObject {
        //fields
        //constructors
        public Artifact(string name) {
            Name = name;
        }
        //properties
        //methods
        public override bool IsCalled(string name) {
            return name.ToLower() == _name.ToLower();
        }
    }
}
