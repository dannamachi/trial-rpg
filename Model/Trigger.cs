using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public abstract class Trigger {
    //fields
    //constructors
    public Trigger() {
      Flipped = false;
    }
    //properties
    public bool Flipped { get; set; }
    //methods
    public void FlippedBy(Player p) {
      if (!Flipped) {
        PerformFlip(p);
      }
      Flipped = true;
    }
    protected abstract void PerformFlip(Player p);
  }
}
