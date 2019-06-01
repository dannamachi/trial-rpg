using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public abstract class InteractiveRect
    {
        protected Dictionary<string,Button> _buttonDict;

        public StaticSprite Sprite { get; set; }
        public Vector2 Location { get; set; }

        public InteractiveRect(Texture2D texture, int widthDrawn, int heightDrawn, Vector2 location)
        {
            _buttonDict = new Dictionary<string, Button>();
            Sprite = new StaticSprite(texture);
            Sprite.WidthDrawn = widthDrawn;
            Sprite.HeightDrawn = heightDrawn;
            Location = location;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Location);
            foreach (Button button in _buttonDict.Values)
            {
                button.Draw(spriteBatch);
            }
        }

        public delegate void ClickEvent();

        //protected void OnClickQuit()
        //{
        //    ClickQuit?.Invoke();
        //}

        public abstract void CheckEvent(Point mousept);
    }
}
