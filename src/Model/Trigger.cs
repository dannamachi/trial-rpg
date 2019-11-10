using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
    public abstract class Trigger {
        //fields
        //constructors
        public Trigger() {
        }
        //properties
        public abstract List<string> Namelist { get; }
        public abstract int Count { get; }
        public abstract string Info { get; }
        //methods
        public void FlippedBy(Player p) {
            PerformFlip(p);
        }
        protected abstract void PerformFlip(Player p);
    }
}
