using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StreetDetectiveG.src.be;

namespace StreetDetectiveG.src.cont
{
    public static class DrawCommon
    {
        private static int ScreenWidth { get; set; }
        private static int ScreenHeight { get; set; }
        private static SpriteBatch SpriteBatch { get; set; }
        public static void Initialize(SpriteBatch spriteBatch, int screenwidth, int screenheight)
        {
            SpriteBatch = spriteBatch;
            ScreenWidth = screenwidth;
            ScreenHeight = screenheight;
        }
        /// <summary>
        /// draw Sprite associated with IAct (edit)
        /// </summary>
        /// <param name="act"></param>
        /// <param name="conmanager"></param>
        public static void Draw(IAct act,ContentManager conmanager)
        {
            //draw stuff
        }
        /// <summary>
        /// convert Vector2 to PVector (edit)
        /// </summary>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static PVector ToPVector(Vector2 v2)
        {
            return new PVector(0, 0);
        }
        /// <summary>
        /// convert PVector to Vector2 (edit)
        /// </summary>
        /// <param name="pvt"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(PVector pvt)
        {
            return new Vector2();
        }
    }
}
