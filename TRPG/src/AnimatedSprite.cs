using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public enum FacingWhichSide
    {
        Left,
        Right
    }
    public class AnimatedSprite
    {
        //fields
        private int currentFrame;
        private int totalFrames;
        private int frameTicks;
        private int frameDrawn;
        //constructors
        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            frameDrawn = 0;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            frameTicks = 0;
        }
        //properties
        public int Width { get => Texture.Width / Columns; }
        public int Height { get => Texture.Height / Rows; }
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        //methods
        public void Update(FacingWhichSide side)
        {
            if (frameTicks == 3)
            {
                currentFrame += 1;
                frameTicks = 0;
            }
            frameTicks += 1;
            if (currentFrame == totalFrames / 2)
            {
                currentFrame = 0;
            }
            if (side == FacingWhichSide.Left)
            {
                frameDrawn = currentFrame + totalFrames / 2;
            }
            else
            {
                frameDrawn = currentFrame;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)frameDrawn / (float)Columns);
            int column = frameDrawn % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
