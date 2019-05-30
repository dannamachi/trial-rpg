using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public enum FacingWhichSide
    {
        Left,
        Right
    }
    public class FlippableAnimatedSprite : AnimatedSprite
    {
        //fields
        private int _currentFrame;
        private FacingWhichSide _facingSide;
        //constructors
        public FlippableAnimatedSprite(Texture2D texture, int rows, int columns) : base(texture,rows,columns)
        {
            _currentFrame = 0;
            _facingSide = FacingWhichSide.Right;

        }
        //properties
        public FacingWhichSide Facing { get => _facingSide; set => _facingSide = value; }
        //methods
        public override void Update()
        {
            if (_frameTicks == 3)
            {
                _currentFrame += 1;
                _frameTicks = 0;
            }
            _frameTicks += 1;
            if (_currentFrame == _totalFrames / 2)
            {
                _currentFrame = 0;
            }
            if (_facingSide == FacingWhichSide.Left)
            {
                _frameDrawn = _currentFrame + _totalFrames / 2;
            }
            else
            {
                _frameDrawn = _currentFrame;
            }
        }
    }
}
