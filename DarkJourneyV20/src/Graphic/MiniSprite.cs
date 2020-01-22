using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SEGraphic
{
    public class MiniSprite
    {
        //fields
        private Dictionary<string, int> _pointDict;
        private Texture2D _texture;
        private Rectangle _rect;
        private int _swidth;
        private int _sheight;
        //constructors
        public MiniSprite(Texture2D texture, Vector2 origin, int col, int row, int singlewidth, int singleheight)
        {
            _texture = texture;
            _rect = new Rectangle((int)origin.X, (int)origin.Y, col, row);
            _swidth = singlewidth;
            _sheight = singleheight;
            _pointDict = new Dictionary<string, int>();
        }
        //properties
        public Texture2D Texture { get => _texture; }
        //methods
        public Rectangle GetSrcRect(string name)
        {
            Rectangle rect = new Rectangle();
            int order = _pointDict[name] + 1;
            int row = ((order + _rect.Width - 1) / _rect.Width) - 1; //0th index
            int col = (order - _rect.Width * row) - 1; //0th index
            rect.X = _rect.X + _swidth * col;
            rect.Y = _rect.Y + _sheight * row;
            rect.Width = _swidth;
            rect.Height = _sheight;
            return rect;
        }
        public bool Contains(string name)
        {
            return _pointDict.ContainsKey(name);
        }
        public void SetObjects(List<string> objectnames)
        {
            _pointDict = new Dictionary<string, int>();
            for (int i = 0; i < objectnames.Count; i++)
            {
                _pointDict[objectnames[i]] = i;
            }
        }
    }
}
