using System.Collections.Generic;

namespace TRPG.src
{
    public class TextBox
    {
        //fields
        private List<string> _texts;
        //properties
        public int LineCount { get => _texts.Count; }
        public int Width { get; set; }
        //constructors
        public TextBox(string text)
        {
            string line;
            string[] segments;
            _texts = new List<string>();
            line = BreakContentIntoLines(text);
            segments = line.Split('\n');
            foreach (string segment in segments) { _texts.Add(segment); }
            Width = 30;
        }
        //methods
        public string GetLine(int index)
        {
            if (index >= 0 && index < LineCount)
            {
                return _texts[index];
            }
            return null;
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
            int lastIndex = Width;
            int firstIndex = 0;
            int n = stuff.Length;

            n = n / Width + 1;
            for (int i = 0; i < n; i++)
            {
                if (i < n - 1)
                {
                    temp = stuff.Substring(firstIndex, Width);
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
                        firstIndex += Width;
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
