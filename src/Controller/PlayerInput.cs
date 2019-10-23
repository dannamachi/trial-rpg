using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class PlayerInput
    {
        //fields
        //constructors
        public PlayerInput(ConsoleKeyInfo cki)
        {
            CKI = cki;
        }
        //properties
        public ConsoleKeyInfo CKI { get; set; }
        //methods
        public bool EqualsTo(PlayerInput inp)
        {
            if (CKI != null)
            {
                return inp.CKI.Key == CKI.Key;
            }
            return false;
        }
    }
}
