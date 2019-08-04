using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace StreetDetectiveG.src.gr
{
    public class TextSprite : StaticSprite
    {
        //fields
        private TextBox _textbox;
        //properties
        //constructors
        public TextSprite(Texture2D texture, string text) : base(texture)
        {
            _textbox = new TextBox(text);
        }
        //methods
        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            base.Draw(spriteBatch, location);
            spriteBatch.Begin();
            for (int i = 0; i < _textbox.LineCount; i++)
            {
                spriteBatch.DrawString(Game1.Font20, _textbox.GetLine(i), new Vector2(location.X + WidthDrawn / 10, location.Y + HeightDrawn / 10 + i * 25), Color.Blue);
            }
            spriteBatch.End();
        }
    }
}
