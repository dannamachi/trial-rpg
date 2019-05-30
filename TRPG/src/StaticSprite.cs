using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class StaticSprite : Sprite
    {
        //fields
        //properties
        public Rectangle SpriteRect { get; set; }
        //constructors
        public StaticSprite(Texture2D texture, int horiDist, int vertDist, int width, int height) : base (texture)
        {
            SpriteRect = new Rectangle(horiDist, vertDist, width, height);
        }
        //methods
        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, WidthDrawn, HeightDrawn);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, SpriteRect, Color.White);
            spriteBatch.End();
        }
    }
}
