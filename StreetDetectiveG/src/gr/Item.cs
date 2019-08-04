using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StreetDetectiveG.src.gr
{
    public class Item
    {
        //fields
        //properties
        public Vector2 Location { get; set; }
        public Sprite Sprite { get; set; }
        public string Name { get; set; }
        //constructors
        public Item(string name)
        {
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
        public void DrawIcon(SpriteBatch spriteBatch, int posX, int posY, int iconWidth, int iconHeight)
        {
            int width, height;
            width = Sprite.WidthDrawn;
            height = Sprite.HeightDrawn;
            Sprite.WidthDrawn = iconWidth;
            Sprite.HeightDrawn = iconHeight;
            Sprite.Draw(spriteBatch, new Vector2(posX, posY));
            Resize(width, height);
        }
    }
}
