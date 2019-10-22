using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class Inventory {
    //fields
    private List<VirtualObject> _arts;
    //constructors
    public Inventory() {
      _arts = new List<VirtualObject>();
    }
    //properties
    //methods
    ///add an artifact to inventory
    public void Add(VirtualObject art) {
      if (!(_arts.Contains(art)))
        _arts.Add(art);
    }
    ///return boolean whether inventory contains an artifact (by name)
    public bool Has(string name) {
      foreach(VirtualObject art in _arts) {
        if (art.IsCalled(name))
          return true;
      }
      return false;
    }
    ///return artifact/null in inventory without removing
    public VirtualObject Find(string name) {
      foreach (VirtualObject art in _arts) {
        if (art.IsCalled(name)) {
          return art;
        }
      }
      return null;
    }
    ///return artifact/null with removing
    public VirtualObject Remove(string name) {
      VirtualObject art = Find(name);
      if (name)
        _arts.Remove(art);
      return art;
    }
  }
}
