using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetDetectiveG.src.gr;

namespace StreetDetectiveG.src.cont
{
    public class ExploreContent : ContentManager
    {
        public ExploreContent() { }
        public override void LoadSprites()
        {
            SpriteLoader.LoadExplore(this);
        }
        /// <summary>
        /// unload sprites (edit)
        /// </summary>
        public override void UnloadSprites()
        {
            //unload sprites
        }
    }
}
