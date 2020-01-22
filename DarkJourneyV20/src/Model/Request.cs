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
        public string GetNeededQuest
        {
            get
            {
                if (_reqCode == 2 || _reqCode == 3) return _objName;
                return null;
            }
        }
        public string GetNeededArtifact
        {
            get
            {
                if (_reqCode == 1) return _objName;
                return null;
            }
        }
        public string Info()
        {
            string text = "";
            switch (_reqCode)
            {
                //player must not have artifact
                case 0:
                    text = "not have:" + _objName;
                    break;
                //player must have artifact
                case 1:
                    text = "have:" + _objName;
                    break;
                //player must be doing quest
                case 2:
                    text = "doing quest:" + _objName;
                    break;
                //player must have completed quest
                case 3:
                    text = "completed quest:" + _objName;
                    break;
                //player must not be doing/have done quest
                case 4:
                    text = "not doing/completed quest:" + _objName;
                    break;
            }
            return text;
        }
        ///method to determine if request is fulfilled
        ///0-1: artifact no/yes
        ///2-4: quest no/yes/not
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
            //player must not be doing/have done quest
            case 4:
                result = !p.Has(_objName, "Q") && !p.Has(_objName, "CQ");
                break;
            }
            return result;
        }
    }
}
