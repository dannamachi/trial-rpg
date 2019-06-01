using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TRPG.src
{
    public class AlertRect : InteractiveRect
    {
        public event ClickEvent ClickNo, ClickYes;

        public AlertRect(Texture2D textureFrame, Texture2D buttons, int width, int height, Vector2 location) : base(textureFrame,width,height,location)
        {
            Button buYes = new Button();
            Button buNo = new Button();
            buYes.SetSprite(buttons, 0, 0, buttons.Width / 2, buttons.Height);
            buYes.Resize(Sprite.WidthDrawn / 5, Sprite.HeightDrawn / 5);
            buYes.Location = new Vector2(Location.X + Sprite.WidthDrawn / 4, Location.Y + Sprite.HeightDrawn * 3 / 4);
            buNo.SetSprite(buttons, buttons.Width / 2, 0, buttons.Width / 2, buttons.Height);
            buNo.Resize(Sprite.WidthDrawn / 5, Sprite.HeightDrawn / 5);
            buNo.Location = new Vector2(Location.X + Sprite.WidthDrawn / 2, Location.Y + Sprite.HeightDrawn * 3 / 4);

            _buttonDict[buYes] = "yes";
            _buttonDict[buNo] = "no";

            _eventDict["yes"] = new EventHandler(OnClickYes);
            _eventDict["no"] = new EventHandler(OnClickNo);
        }

        private void OnClickYes()
        {
            ClickYes?.Invoke();
        }
        private void OnClickNo()
        {
            ClickNo?.Invoke();
        }

        public override void CheckEvent(Point mousept)
        {
            foreach (Button button in _buttonDict.Keys)
            {
                if (button.IsPressed(mousept))
                {
                    _eventDict[_buttonDict[button]]();
                }
            }
        }
    }
}
