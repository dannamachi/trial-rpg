using System;
using System.Collections.Generic;
using System.Text;

namespace SEGraphic
{
    public class Zone : GraphicObject
    {
        //fields
        private List<GraphicObject> _buttons;
        private int _min, _max;
        //constructors
        public Zone(GameDetails gd, List<GraphicObject> bus, int min, int max) : base(gd)
        {
            _buttons = new List<GraphicObject>();
            foreach (GraphicObject bu in bus)
            {
                _buttons.Add(bu);
            }
            SortObjects();
            Name = "Generic_Zone";
            _min = min;
            _max = max;
        }
        //properties
        public List<GraphicObject> Objects { get => _buttons; }
        //methods
        private void SortObjects()
        {
            GraphicObject temp;
            for (int i = 0; i < _buttons.Count; i++)
            {
                for (int j = i + 1; j < _buttons.Count; j++)
                {
                    if (_buttons[i].IsAbove(_buttons[j]))
                    {
                        temp = _buttons[i];
                        _buttons[i] = _buttons[j];
                        _buttons[j] = temp;
                    }
                }
            }
        }
        public bool IsButtonPressed(int x, int y)
        {
            foreach (GraphicObject bu in _buttons)
            {
                if (bu.IsPressed(x, y)) return true;
            }
            return false;
        }
        public GraphicObject GetPressedButton(int x, int y)
        {
            foreach (GraphicObject bu in _buttons)
            {
                if (bu.IsPressed(x, y)) return bu;
            }
            return null;
        }
    }
}
