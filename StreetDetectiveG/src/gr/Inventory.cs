using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StreetDetectiveG.src.gr
{
    public class Inventory : Container
    {
        //fields
        private StaticSprite _frame;
        private StaticSprite _slots;
        private TextSprite _detail;
        private int _showingIndex;
        //properties
        public Rectangle SlotRect { get; set; }
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
            _showingIndex = -1;
            SlotRect = new Rectangle(38, 110, 600, 415);
        }
        //methods
        public void CheckShowDetail(Point mousept)
        {
            if (SlotRect.Contains(mousept))
            {
                int col = 0;
                int count = 0;
                int row;
                Rectangle slot;
                for (int i = 0; i < _items.Count; i++)
                {
                    _showingIndex = -1;
                    row = count % 4;
                    if (count != 0 && row == 0)
                    {
                        col += 1;
                    }
                    slot = new Rectangle(SlotX + col * SlotWidth, SlotY + row * SlotHeight, SlotWidth, SlotHeight);
                    if (slot.Contains(mousept))
                    {
                        _detail.AddLine(_items[i].Name);
                        _showingIndex = i;
                        break;
                    }
                    count += 1;
                }
            }
        }
        public void ResetScroll()
        {
            SlotX = 38;
        }
        public void SetSprite(Texture2D frameSprite, Texture2D slotsSprite, Texture2D detailSprite)
        {
            _frame = new StaticSprite(frameSprite);
            _slots = new StaticSprite(slotsSprite);
            _detail = new TextSprite(detailSprite, "na");
            _detail.WidthDrawn = 200;
            _detail.HeightDrawn = 100;
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
                for (int i = 0; i < _items.Count; i++)
                {
                    row = count % 4;
                    if (count != 0 && row == 0)
                    {
                        col += 1;
                    }
                    _items[i].DrawIcon(spriteBatch, SlotX + col * SlotWidth, SlotY + row * SlotHeight, SlotWidth, SlotHeight);
                    count += 1;
                    if (_showingIndex == i)
                    {
                        _detail.Draw(spriteBatch, new Vector2(SlotX + col * SlotWidth + SlotWidth, SlotY + row * SlotHeight));
                    }
                }
                _frame.Draw(spriteBatch, new Vector2(space.X, space.Y));
                SlotRect = new Rectangle(38, 110, _slots.WidthDrawn, _slots.HeightDrawn);
            }
        }
    }
}
