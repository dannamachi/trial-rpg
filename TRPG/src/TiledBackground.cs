using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public enum Direction
    {
        North,
        South,
        East,
        West
    }
    public class TiledBackground
    {
        //fields
        private Vector2 _location;
        //properties
        public int CellHeight { get => Texture.Height / Rows; }
        public int CellWidth { get => Texture.Width / Cols; }
        public int Cols { get; set; }
        public int Rows { get; set; }
        public Texture2D Texture { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        //constructors
        public TiledBackground(Texture2D texture, int rows, int cols, int screenWidth, int screenHeight)
        {
            _location = new Vector2(0, 0);
            Texture = texture;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Rows = rows;
            Cols = cols;
        }
        //methods
        /// <summary>
        /// draw the background to screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle srcRect, destRect;
            spriteBatch.Begin();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    srcRect = new Rectangle(CellWidth * j, CellHeight * i, CellWidth, CellHeight);
                    destRect = new Rectangle((int)_location.X + j * CellWidth, (int)_location.Y + i * CellHeight, CellWidth, CellHeight);
                    spriteBatch.Draw(Texture, destRect, srcRect, Color.White);
                }
            }
            spriteBatch.End();
        }
        /// <summary>
        /// move the background in a direction by a distance
        /// return bool whether can move or not
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Move (float distance, Direction direction)
        {
            Vector2 nextLocation = new Vector2(_location.X,_location.Y);
            switch (direction)
            {
                case Direction.East:
                    nextLocation.X += distance;
                    break;
                case Direction.North:
                    nextLocation.Y -= distance;
                    break;
                case Direction.South:
                    nextLocation.Y += distance;
                    break;
                case Direction.West:
                    nextLocation.X -= distance;
                    break;
            }
            if (nextLocation.X > 0) { return false; }
            if (nextLocation.Y > 0) { return false; }
            if (nextLocation.X + Texture.Width - ScreenWidth < 0) { return false; }
            if (nextLocation.Y + Texture.Height - ScreenHeight < 0) { return false; }

            _location = nextLocation;
            return true;
        }
    }
}
