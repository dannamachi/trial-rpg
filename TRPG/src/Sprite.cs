using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public abstract class Sprite
    {
        //fields
        //properties
        public virtual int WidthDrawn { get; set; }
        public virtual int HeightDrawn { get; set; }
        public virtual int Width { get => Texture.Width; }
        public virtual int Height { get => Texture.Height; }
        public Texture2D Texture { get; set; }
        //constructors
        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }
        //methods
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
