using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class Item
    {
        //fields
        //properties
        public Vector2 Location { get; set; }
        public Sprite Sprite { get; set; }
        public string Name { get; set; }
        //constructors
        public Item(string name, Sprite sprite)
        {
            Sprite = sprite;
            Name = name;
            Location = new Vector2(0, 0);
        }
        //methods
        public void Resize(int width, int height)
        {
            Sprite.WidthDrawn = width;
            Sprite.HeightDrawn = height;

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Location);
        }
    }
}
