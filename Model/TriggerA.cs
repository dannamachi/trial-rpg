using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class TriggerA : Trigger {
    //fields
    private List<Artifact> _arts;
    //constructors
    public TriggerA(List<Artifact> arts) : base() {
      _arts = new List<Artifact>();
      foreach (Artifact art in arts) {
        _arts.Add(art);
      }
    }
    //properties
    //methods
    protected override void PerformFlip(Player p) {
      foreach (Artifact art in _arts) {
        p.Add(art);
      }
    }
  }
}
