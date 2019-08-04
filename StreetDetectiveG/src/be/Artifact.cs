using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class Artifact 
    {
        //fields
        //properties
        public string Name { get; set; }
        //constructors
        public Artifact(string name)
        {
            Name = name;
        }
        //methods
        public static Artifact Create()
        {
            return new Artifact("Something");
        }
        public bool IsCalled(string name)
        {
            return Name.ToLower().Trim() == name.ToLower().Trim();
        }
    }
}
