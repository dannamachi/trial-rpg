using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class ConLine : RRLine
    {
        //fields
        private List<ClearToken> _req;
        private ConToken _preset;
        //constructors
        public ConLine(ActionVoid voidfunc, ConToken preset) : base(voidfunc)
        {
            _preset = preset;
        }
        public ConLine(ActionUse usefunc, ConToken preset) : base(usefunc)
        {
            _preset = preset;
        }
        public ConLine(ActionUse usefunc,ConToken preset, List<ClearToken> reqs) : this(usefunc,preset)
        {
            _req = new List<ClearToken>();
            foreach (ClearToken token in reqs)
            {
                _req.Add(token);
            }
        }
        //properties
        public string ClearTokens 
        { 
            get
            {
                string text = "";
                foreach (ClearToken token in _req)
                {
                    text += token.Name + ",";
                }
                return text;
            } 
        }
        public string ActionType { get => _preset.ActionType; }
        //methods
        private ConToken GetNonKeyToken()
        {
            return _preset.GetNonKeyDuplicate();
        }
        public void IsAttemptedBy(Player p)
        {
            p.Add(GetNonKeyToken());
        }
        public bool IsDoableBy(Player p)
        {
            if (_req != null)
            {
                foreach (ClearToken token in _req)
                {
                    if (!token.IsClearedBy(p))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
