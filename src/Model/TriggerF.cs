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
    protected override void PerformFlip(Player p) {
      foreach (string questName in _questNames) {
        if (p.Has(questName,"Q")) {
          if (p.Find(questName,"Q").IsFulfilledBy(p)) {
            p.Complete(questName);
          }
        }
      }
    }
  }
}
