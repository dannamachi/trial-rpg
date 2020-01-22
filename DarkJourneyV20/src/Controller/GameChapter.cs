using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class GameChapter : VirtualObject
    {
        //fields
        private string _winby;
        private List<string> _convos;
        //constructors
        public GameChapter(PlayerCP playCP)
        {
            _winby = playCP.WinBy;
            _convos = new List<string>();
            foreach (string vo in playCP.Player.ConvoList)
            {
                _convos.Add(vo);
            }
            Name = playCP.Map;
        }
        //properties
        //methods
        public bool WonBy(string qname)
        {
            return _winby.ToLower() == qname.ToLower();
        }
        public bool HasRead(string convo)
        {
            return _convos.Contains(convo);
        }
    }
}
