using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class Storybook : VirtualObject
    {
        //fields
        private List<Storypage> _pages;
        private bool _isr;
        private int _count;
        //constructors
        public Storybook(string title, List<Storypage> pages)
        {
            Name = title;
            _pages = new List<Storypage>();
            foreach (Storypage page in pages)
            {
                _pages.Add(page);
            }
            _isr = false;
            _count = 0;
        }
        //properties
        public string DisplayPage
        {
            get
            {
                string text = "";
                text += "\n\t\t" + _pages[_count].PersonSpeaking + " said:";
                text += "\n\t" + _pages[_count].SpeechContent;
                _count += 1;
                if (_count >= _pages.Count) { _isr = true; }
                return text;
            }
        }
        public bool IsRead { get => _isr; }
        //methods
    }
}