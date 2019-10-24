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
        //methods
        public void FlippedBy(Player p) {
            PerformFlip(p);
        }
        protected abstract void PerformFlip(Player p);
    }
}
