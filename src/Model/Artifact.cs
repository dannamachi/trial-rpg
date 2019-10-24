using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public class Artifact : VirtualObject {
        //fields
        //constructors
        public Artifact(string name) {
            Name = name;
            Description = "An item called " + Name;
        }
        //properties
        public string Description { get; set; }
        //methods
        public override bool IsCalled(string name) {
            return name.ToLower() == Name.ToLower();
        }
    }
}
