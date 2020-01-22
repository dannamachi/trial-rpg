using Microsoft.Xna.Framework.Input;
using DarkJourneyV20;

namespace SEGraphic
{
    public class MonoButton
    {
        //static
        public static Button GetButton(MonoButton bu)
        {
            if (bu.Name != "") return new ClickButton(bu.Name);
            if (bu.Key != Keys.None) return new KbButton(bu.Key);
            return null;
        }
        //fields
        public static KeyboardState PrevState { get; set; }
        public static KeyboardState CurrState { get; set; }
        //constructors
        public MonoButton(Keys key)
        {
            Key = key;
            Name = "";
            IsSlow = true;
        }
        public MonoButton(string name)
        {
            Name = name;
            Key = Keys.None;
            IsSlow = true;
        }
        //properties
        public bool IsSlow { get; set; }
        public string Name { get; set; }
        public Keys Key { get; set; }
        //methods
        public bool IsPressed(GraphicObject buttonpressed)
        {
            if (Key != Keys.None)
            {
                if (!IsSlow)
                    return CurrState.IsKeyDown(Key);// && PrevState.IsKeyUp(Key);
                else
                    return CurrState.IsKeyDown(Key) && !PrevState.IsKeyDown(Key);
            }
            else if (Name != "")
            { 
                if (buttonpressed == null) return false;
                return buttonpressed.IsCalled(Name);
            }
            return false;
        }
    }
}
