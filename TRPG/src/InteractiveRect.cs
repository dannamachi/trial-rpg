using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class InteractiveRect
    {
        //private List<Button> _buttons;
        private Button _button;

        public StaticSprite Sprite { get; set; }

        public InteractiveRect(Texture2D texture, int widthDrawn, int heightDrawn, Button button)
        {
            _button = button;
            Sprite = new StaticSprite(texture);
            Sprite.WidthDrawn = widthDrawn;
            Sprite.HeightDrawn = heightDrawn;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Sprite.Draw(spriteBatch, location);
            _button.Draw(spriteBatch);
        }

        public delegate void ClickEvent();

        public event ClickEvent ClickQuit;

        protected void OnClickQuit()
        {
            ClickQuit?.Invoke();
        }

        public void CheckEvent(Point mousept)
        {
            if (_button.IsPressed(mousept))
            {
                OnClickQuit();
            }
        }
    }
}
