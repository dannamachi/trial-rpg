using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class ClearToken : Token
    {
        //fields
        //constructors
        public ClearToken(string desc) : base()
        {
            SetDesc(desc);
        }
        //properties
        //methods
        public bool IsClearedBy(GameChapter chap)
        {
            string[] array = Name.Split('_');
            if (array[0] == "read")
            {
                return chap.HasRead(array[1]);
            }
            else if (array[0] == "win")
            {
                return chap.WonBy(array[1]);
            }
            return false;
        }
        public bool IsClearedBy(Player p)
        {
            string[] array = Name.Split('_');
            if (array[0] == "read")
            {
                return p.Has(array[1], "C");
            }
            else if (array[0] == "has")
            {
                return p.Has(array[1], "A");
            }
            else if (array[0] == "doing")
            {
                return p.Has(array[1], "Q");
            }
            else if (array[0] == "complete")
            {
                return p.Has(array[1], "CQ");
            }
            else if (array[0] == "holding")
            {
                return p.Holding.Name == array[1];
            }
            return false;
        }
    }
}
