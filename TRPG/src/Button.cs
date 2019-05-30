using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class Button
    {
        //fields
        private StaticSprite _sprite;
        //properties
        public Rectangle ButtonRect { get => new Rectangle((int)Location.X, (int)Location.Y, WidthDrawn, HeightDrawn); }
        public int WidthDrawn { get => _sprite.WidthDrawn; }
        public int HeightDrawn { get => _sprite.HeightDrawn; }
        public Vector2 Location { get; set; }
        public string Text { get; set; }
        //constructors
        public Button()
        {
            Location = new Vector2(0, 0);
        }
        //methods
        public bool IsPressed(Point mousept)
        {
            return ButtonRect.Contains(mousept);
        }
        public void Resize (int width, int height)
        {
            _sprite.WidthDrawn = width;
            _sprite.HeightDrawn = height;
        }
        public void SetSprite(Texture2D texture, int horiDist, int vertDist, int width, int height)
        {
            _sprite = new StaticSprite(texture, horiDist,vertDist,width,height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_sprite != null) { _sprite.Draw(spriteBatch, Location); }
        }
    }
}
