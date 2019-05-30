using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class Inventory : Container
    {
        //fields
        private StaticSprite _frame;
        private StaticSprite _slots;
        //properties
        private int SlotWidth { get => _slots.WidthDrawn / 25; }
        private int SlotHeight { get => _slots.HeightDrawn / 4; }
        public bool CanScrollRight { get => SlotX + _slots.WidthDrawn > 676; }
        public bool CanScrollLeft { get => SlotX < 38; }
        public int SlotX { get; set; }
        public int SlotY { get; set; }
        //constructors
        public Inventory() : base()
        {
            SlotX = 38;
            SlotY = 110;
            Capacity = 100;
        }
        //methods
        public void ResetScroll()
        {
            SlotX = 38;
        }
        public void SetSprite(Texture2D frameSprite, Texture2D slotsSprite)
        {
            _frame = new StaticSprite(frameSprite);
            _slots = new StaticSprite(slotsSprite);
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle space)
        { 
            if (_frame != null & _slots != null)
            {
                _frame.WidthDrawn = space.Width;
                _frame.HeightDrawn = space.Height;
                _slots.HeightDrawn = _frame.HeightDrawn - 150;

                _slots.Draw(spriteBatch, new Vector2(SlotX, SlotY));

                int col = 0;
                int count = 0;
                int row;
                foreach (Item itm in _items)
                {
                    row = count % 4;
                    if (count != 0 && row == 0)
                    {
                        col += 1;
                    }
                    itm.DrawIcon(spriteBatch, SlotX + col * SlotWidth, SlotY + row * SlotHeight, SlotWidth, SlotHeight);
                    count += 1;
                }
                _frame.Draw(spriteBatch, new Vector2(space.X, space.Y));
            }
        }
    }
}
