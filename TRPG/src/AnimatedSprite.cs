using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class AnimatedSprite : Sprite
    {
        //fields
        protected int _totalFrames;
        protected int _frameTicks;
        protected int _frameDrawn;
        //constructors
        public AnimatedSprite(Texture2D texture, int rows, int columns) : base (texture)
        {
            Rows = rows;
            Columns = columns;
            WidthDrawn = Width;
            HeightDrawn = Height;
            _frameDrawn = 0;
            _totalFrames = Rows * Columns;
            _frameTicks = 0;
        }
        //properties
        public override int Width { get => Texture.Width / Columns; }
        public override int Height { get => Texture.Height / Rows; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        //methods
        public virtual void Update()
        {
            if (_frameTicks == 3)
            {
                _frameDrawn += 1;
                _frameTicks = 0;
            }
            _frameTicks += 1;
            if (_frameDrawn == _totalFrames)
            {
                _frameDrawn = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)_frameDrawn / (float)Columns);
            int column = _frameDrawn % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, WidthDrawn, HeightDrawn);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
