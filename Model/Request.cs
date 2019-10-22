using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual {
  public class Request {
    //fields
    private string _objName;
    private int _reqCode;
    //constructors
    public Request(string objName, int reqCode) {
      _objName = objName;
      _reqCode = reqCode;
    }
    //properties
    //methods
    ///method to determine if request is fulfilled
    ///0-1:artifact no/yes
    ///2-3: quest no/yes
    public bool IsFulfilledBy(Player p) {
      bool result = false;
      switch (_reqCode) {
        //player must not have artifact
        case 0:
          result = !p.Has(_objName);
          break;
        //player must have artifact
        case 1:
          result = p.Has(_objName);
          break;
        //player must be doing quest
        case 2:
          result = p.Has(_objName,"Q");
          break;
        //player must have completed quest
        case 3:
          result = p.Has(_objName,"CQ");
          break;
      }
      return result;
    }
  }
}
