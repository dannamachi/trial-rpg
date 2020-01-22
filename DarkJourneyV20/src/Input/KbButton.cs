using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace SEGraphic
{
    public class KbButton : Button
    {
        //fields
        private Keys _key;
        //constructors
        public KbButton(Keys key) : base()
        {
            _key = key;
        }
        //properties
        public Keys Key { get => _key; }
        //methods
        public bool IsCalled(string name)
        {
            if (name == "W" && Key == Keys.W) return true;
            if (name == "S" && Key == Keys.S) return true;
            if (name == "D" && Key == Keys.D) return true;
            if (name == "A" && Key == Keys.A) return true;
            return false;
        }
        public override MonoButton GetMono()
        {
            MonoButton mono = new MonoButton(_key);
            mono.IsSlow = IsSlow;
            return mono;
        }
        public override bool Equals(Button bu)
        {
            if (!(bu is KbButton)) return false;
            return _key == (bu as KbButton).Key;
        }
    }
}
