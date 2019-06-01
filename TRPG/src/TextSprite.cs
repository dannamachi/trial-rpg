using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TRPG.src
{
    public class TextSprite : StaticSprite
    {
        //fields
        private List<string> _texts;
        //properties
        //constructors
        public TextSprite(Texture2D texture, string text) : base(texture)
        {
            string line;
            string[] segments;
            _texts = new List<string>();  
            line = BreakContentIntoLines(text);
            segments = line.Split('\n');
            foreach (string segment in segments) { _texts.Add(segment); } 
        }
        //methods
        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            base.Draw(spriteBatch, location);
            spriteBatch.Begin();
            for (int i = 0; i < _texts.Count; i++)
            {
                spriteBatch.DrawString(Game1.Font20, _texts[i], new Vector2(location.X + WidthDrawn / 10, location.Y + HeightDrawn / 10 + i * 25), Color.Blue);
            }
            spriteBatch.End();
        }
        public void AddLine(string st)
        {
            _texts = new List<string>();
            string line = BreakContentIntoLines(st);
            string[] segments = line.Split('\n');
            foreach (string segment in segments) { _texts.Add(segment); }
        }
        private int GetLastIndexOnLine(string substr)
        {
            int result = -1;
            int end = substr.Length;
            int start = 0;
            int count = 0;
            int at = 0;
            while ((start <= end) && (at > -1))
            {
                count = end - start;
                at = substr.IndexOf(" ", start, count);
                if (at == -1) break;
                result = at;
                start = at + 1;
            }
            return result;
        }
        private string BreakContentIntoLines(string stuff)
        {
            string content = "";
            string temp;
            int lastIndex = 30;
            int firstIndex = 0;
            int n = stuff.Length;

            n = n / 30 + 1;
            for (int i = 0; i < n; i++)
            {
                if (i < n - 1)
                {
                    temp = stuff.Substring(firstIndex, 30);
                }
                else
                {
                    temp = stuff.Substring(firstIndex);
                }

                lastIndex = GetLastIndexOnLine(temp);
                if (lastIndex == -1)
                {
                    content += temp;
                    if (i < n - 1)
                        firstIndex += 30;
                }
                else if (i < n - 1)
                {
                    content += temp.Substring(0, GetLastIndexOnLine(temp)) + "\n";
                    firstIndex += lastIndex + 1;
                }
                else
                {
                    content += temp;
                }

            }
            return content;
        }
    }
}
