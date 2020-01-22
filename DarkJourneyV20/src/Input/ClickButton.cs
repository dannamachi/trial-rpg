using System;
using System.Collections.Generic;

namespace SEGraphic
{
    public class ClickButton : Button
    {
        //fields
        private string _name;
        //constructors
        public ClickButton(string name) : base()
        {
            _name = name;
        }
        //properties
        public string BName { get => _name; }
        //methods
        public override MonoButton GetMono()
        {
            MonoButton mono = new MonoButton(_name);
            mono.IsSlow = IsSlow;
            return mono;
        }
        public override bool Equals(Button bu)
        {
            if (!(bu is ClickButton)) return false;
            return _name.ToLower() == (bu as ClickButton).BName.ToLower();
        }
    }
}
