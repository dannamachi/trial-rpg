using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class ConToken : Token
    {
        //fields
        //constructors
        public ConToken(string desc) : base()
        {
            SetDesc(desc);
        }
        //properties
        public string ActionType { get => _desc[1]; }
        private bool IsNonKey { get => _desc.Count < 3; }
        //methods
        public bool IsDoneBy(Player p)
        {
            return p.Has(Name, "CT");
        }
        public bool BelongsToLine(string linename)
        {
            return linename.ToLower() == _desc[0].ToLower();
        }
        public void SetKey(string key)
        {
            if (!IsNonKey)
            { 
                _desc[_desc.Count - 1] = key; 
            }
            else
            {
                _desc.Add(key);
            }

        }
        public ConToken GetNonKeyDuplicate()
        {
            string line = "";
            for (int i = 0; i < _desc.Count; i++)
            {
                if (i < _desc.Count - 1)
                {
                    line += _desc[i];
                    if (i < _desc.Count - 2)
                        line += "|";
                }
            }
            ConToken token = new ConToken(line);
            return token;
        }
    }
}
