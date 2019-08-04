using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetDetectiveG.src.gr;

namespace StreetDetectiveG.src.cont
{
    public abstract class ContentManager
    {
        protected Dictionary<string, Sprite> _spriteDict = new Dictionary<string, Sprite>();
        public ContentManager()
        {
            LoadSprites();
        }
        public Sprite GetSprite(string name)
        {
            return _spriteDict[name];
        }
        public abstract void LoadSprites();
        public abstract void UnloadSprites();
    }
}
