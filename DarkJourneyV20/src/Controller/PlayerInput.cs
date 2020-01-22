using System;
using System.Collections.Generic;

namespace SEGraphic
{
    public class PlayerInput
    {
        //static
        public static PlayerInput GetInput2(List<Button> bus)
        {
            PlayerInput inp = new PlayerInput();
            foreach (Button bu in bus)
            {
                inp.AddButton(bu);
            }
            return inp;
        }
        //fields
        private List<Button> _buttons;
        //constructors
        public PlayerInput()
        {
            _buttons = new List<Button>();
        }
        //properties
        //methods
        public bool HasButton(string name)
        {
            foreach (Button bu in _buttons)
            {
                if (bu is KbButton)
                {
                    return (bu as KbButton).IsCalled(name);
                }
            }
            return false;
        }
        public List<Button> GetButtons()
        {
            return _buttons;
        }
        public bool IsFlagged(PlayerInput inp)
        {
            foreach (Button bu0 in inp.GetButtons())
            {
                foreach (Button bu1 in _buttons)
                {
                    if (bu1.Equals(bu0)) return true;
                }
            }
            return false;
        }
        public void AddButton(Button bu)
        {
            if (!_buttons.Contains(bu)) _buttons.Add(bu);
        }
    }
}
