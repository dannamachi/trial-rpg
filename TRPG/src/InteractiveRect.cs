using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public abstract class InteractiveRect
    {
        protected Dictionary<Button,string> _buttonDict;
        protected Dictionary<string, EventHandler> _eventDict;

        public StaticSprite Sprite { get; set; }
        public Vector2 Location { get; set; }

        public InteractiveRect(Texture2D texture, int widthDrawn, int heightDrawn, Vector2 location)
        {
            _buttonDict = new Dictionary<Button, string>();
            _eventDict = new Dictionary<string, EventHandler>();
            Sprite = new StaticSprite(texture);
            Sprite.WidthDrawn = widthDrawn;
            Sprite.HeightDrawn = heightDrawn;
            Location = location;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Location);
            foreach (Button button in _buttonDict.Keys)
            {
                button.Draw(spriteBatch);
            }
        }

        public delegate void ClickEvent();

        public delegate void EventHandler();

        //protected void OnClickQuit()
        //{
        //    ClickQuit?.Invoke();
        //}

        public abstract void CheckEvent(Point mousept);
    }
}
