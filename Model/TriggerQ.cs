using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class TriggerQ : Trigger {
    //fields
    private List<Quest> _quests;
    //constructors
    public TriggerQ(List<Quest> quests) : base() {
      _quests = new List<Quest>();
      foreach (Quest q in quests) {
        _quests.Add(q);
      }
    }
    //properties
    //methods
    protected override void PerformFlip(Player p) {
      foreach (Quest q in _quests) {
        p.Add(q);
      }
    }
  }
}
