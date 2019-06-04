using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TRPG.src.controller
{
    public abstract class ContentManager
    {
        private Dictionary<string, Texture2D> _spriteDict;
        private Dictionary<string, Song> _songDict;
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public Texture2D GetTexture(string name)
        {
            return _spriteDict[name];
        }
        public Song GetSong(string name)
        {
            return _songDict[name];
        }
    }
}
