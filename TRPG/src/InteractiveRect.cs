using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class InteractiveRect
    {
        private Dictionary<Button, ClickEvent> _buttonDict;
        private Dictionary<string, ClickEvent> _eventDict;

        public StaticSprite Sprite { get; set; }

        public InteractiveRect(Texture2D texture, int widthDrawn, int heightDrawn)
        {
            _buttonDict = new Dictionary<Button, ClickEvent>();
            _eventDict = new Dictionary<string, ClickEvent>();
            Sprite = new StaticSprite(texture);
            Sprite.WidthDrawn = widthDrawn;
            Sprite.HeightDrawn = heightDrawn;
        }

        private bool HasEvent(string eventname)
        {
            foreach (string name in _eventDict.Keys)
            {
                if (name == eventname) { return true; }
            }
            return false;
        }

        public void SetButton(Button button, string eventname)
        {
            if (HasEvent(eventname)) {
                _buttonDict[button] = _eventDict[eventname];
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Sprite.Draw(spriteBatch, location);
            foreach (Button button in _buttonDict.Keys)
            {
                button.Draw(spriteBatch);
            }
        }

        public delegate void ClickEvent();

        public event ClickEvent ClickQuit;

        //protected void OnClickQuit()
        //{
        //    ClickQuit?.Invoke();
        //}

        public void CheckEvent(Point mousept)
        {
            foreach (Button button in _buttonDict.Keys)
            {
                if (button.IsPressed(mousept))
                {
                    _buttonDict[button]?.Invoke();
                }
            }
        }
    }
}
