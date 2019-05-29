using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class Player
    {
        //fields
        private float _posX;
        private float _posY;
        private float _speed;
        private FlippableAnimatedSprite _sprite;
        private FacingWhichSide _facingside;
        //properties
        public float Y { get => _posY; }
        public float X { get => _posX; }
        public float Width { get => _sprite.Width; }
        public float Height { get => _sprite.Height; }
        public float Speed { get => _speed; }
        //constructors
        public Player()
        {
            _posX = 0;
            _posY = 0;
            _speed = 100;
            _facingside = FacingWhichSide.Right;
        }
        //methods
        public void FaceTo(FacingWhichSide side)
        {
            _facingside = side;
        }
        public void UpdateAnimation()
        {
            _sprite.Update(_facingside);
        }
        public bool WillBeWithinRect(float distance, Direction direction, Rectangle rect)
        {
            float newX = X, newY = Y;
            if (direction == Direction.East) { newX += distance; }
            else if (direction == Direction.West) { newX -= distance; }
            else if (direction == Direction.North) { newY -= distance; }
            else if (direction == Direction.South) { newY += distance; }

            if (newX < rect.X) { return false; }
            if (newX > rect.X + rect.Width - Width) { return false; }
            if (newY < rect.Y) { return false; }
            if (newY > rect.Y + rect.Height - Height) { return false; }
            return true;
        }
        public void SetSprite(Texture2D texture, int rows, int cols)
        {
            _sprite = new FlippableAnimatedSprite(texture, rows, cols);
        }
        public void Move(Direction direction, float distance)
        {
            if (direction == Direction.North) { _posY -= distance; }
            else if (direction == Direction.South) { _posY += distance; }
            else if (direction == Direction.West) { _posX -= distance; }
            else if (direction == Direction.East) { _posX += distance; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_sprite != null) { _sprite.Draw(spriteBatch, new Vector2(_posX, _posY)); }
        }
    }
}
