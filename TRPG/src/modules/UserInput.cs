using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TRPG.src;

namespace TRPG.src.modules
{
    public static class UserInput
    {
        public static void ProcessMovement(float distance, Player p, TiledBackground gbackground, KeyboardState kstate) 
        {
            if (kstate.IsKeyDown(Keys.Up))
            {
                if (!gbackground.Move(distance, Direction.South))
                {
                    if (p.WillBeWithinRect(distance, Direction.North, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight)))
                        p.Move(Direction.North, distance);
                }
                else
                {
                    if (p.WillBeWithinRect(distance, Direction.North, new Rectangle(0, Game1.ScreenHeight / 3, Game1.ScreenWidth, Game1.ScreenHeight * 2 / 3)))
                        p.Move(Direction.North, distance);
                }
                p.UpdateAnimation();
            }

            else if (kstate.IsKeyDown(Keys.Down))
            {
                if (!gbackground.Move(distance, Direction.North))
                {
                    if (p.WillBeWithinRect(distance, Direction.South, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight)))
                        p.Move(Direction.South, distance);
                }
                else
                {
                    if (p.WillBeWithinRect(distance, Direction.South, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight * 2 / 3)))
                        p.Move(Direction.South, distance);
                }
                p.UpdateAnimation();
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                p.FaceTo(FacingWhichSide.Left);
                if (!gbackground.Move(distance, Direction.East))
                {
                    if (p.WillBeWithinRect(distance, Direction.West, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight)))
                        p.Move(Direction.West, distance);
                }
                else
                {
                    if (p.WillBeWithinRect(distance, Direction.West, new Rectangle(Game1.ScreenWidth / 3, 0, Game1.ScreenWidth * 2 / 3, Game1.ScreenHeight / 3)))
                        p.Move(Direction.West, distance);
                }
                p.UpdateAnimation();
            }

            else if (kstate.IsKeyDown(Keys.Right))
            {
                p.FaceTo(FacingWhichSide.Right);
                if (!gbackground.Move(distance, Direction.West))
                {
                    if (p.WillBeWithinRect(distance, Direction.East, new Rectangle(0, 0, Game1.ScreenWidth, Game1.ScreenHeight)))
                        p.Move(Direction.East, distance);
                }
                else
                {
                    if (p.WillBeWithinRect(distance, Direction.East, new Rectangle(0, 0, Game1.ScreenWidth * 2 / 3, Game1.ScreenHeight)))
                        p.Move(Direction.East, distance);
                }
                p.UpdateAnimation();
            }
        }
        public static void ProcessScroll(Player p, KeyboardState kstate)
        {
            if (kstate.IsKeyDown(Keys.D)) { if (p.Inventory.CanScrollRight) { p.Inventory.SlotX -= 10; } }
            if (kstate.IsKeyDown(Keys.A)) { if (p.Inventory.CanScrollLeft) { p.Inventory.SlotX += 10; } }
        }
    }
}
