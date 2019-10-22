using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class TriggerF : Trigger {
    //fields
    private List<string> _questNames;
    //constructors
    public TriggerF(List<string> questNames) : base() {
      _questNames = new List<string>();
      foreach (string name in questNames) {
        _questNames.Add(name);
      }
    }
    //properties
    //methods
    public override void FlippedBy(Player p) {
      
    }
  }
}
