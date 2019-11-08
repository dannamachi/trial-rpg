using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class ActionObject : GameObject
    {
        //fields
        private List<ConToken> _ctokens;
        private ConLine _cline;
        //constructors
        public ActionObject(string name, ConLine cline) : base(name)
        {
            _ctokens = new List<ConToken>();
            _cline = cline;
        }
        //properties
        public override RRLine ActionLine { get => _cline; }
        //methods
        public void SetTokens(List<ConToken> ctokens)
        {
            _ctokens = ctokens;
        }
        public bool IsSolvedBy(Player p)
        {
            foreach (ConToken token in _ctokens)
            {
                if (!token.IsDoneBy(p))
                    return false;
            }
            return true;
        }
    }
}
