using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class Quest : VirtualObject{
    //fields
    private string _name;
    private List<Request> _reqs;
    //constructors
    public Quest(string name, List<Request> requests) {
      _name = name;
      _reqs = new List<Request>();
      foreach (Request req in requests) {
        _reqs.Add(req);
      }
    }
    //properties
    //methods
    public bool IsFulfilledBy(Player p) {
      foreach (Request req in _reqs) {
        if (!req.IsFulfilledBy(p)) {
          return false;
        }
      }
      return true;
    }
    public override bool IsCalled(string name) {
      return name.ToLower() == name.ToLower();
    }
  }
}
