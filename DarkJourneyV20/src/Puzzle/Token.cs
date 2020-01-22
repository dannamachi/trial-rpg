using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public abstract class Token : VirtualObject
    {
        //fields
        protected List<string> _desc;
        //constructors
        public Token()
        {
            _desc = new List<string>();
        }
        //properties
        public override string Name
        {
            get
            {
                string text = "";
                for (int i = 0; i < _desc.Count; i++)
                {
                    text += _desc[i];
                    if (i < _desc.Count - 1)
                        text += "_";
                }
                return text;
            }
        }
        //methods
        protected void SetDesc(string line)
        {
            _desc = new List<string>();
            string[] array = line.Split('|');
            for (int i = 0; i < array.Length; i++)
            {
                _desc.Add(array[i]);
            }
        }
    }
}
