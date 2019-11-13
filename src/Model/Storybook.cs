using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class Storybook : VirtualObject
    {
        //fields
        private List<Storypage> _pages;
        private Dictionary<string, Storybook> _choicebooks;
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
            Chosen = "";
            NeedChoice = false;
        }
        //properties
        public List<string> Choices
        {
            get
            {
                List<string> clist = new List<string>();
                foreach (string c in _choicebooks.Keys)
                {
                    clist.Add(c);
                }
                return clist;
            }
        }
        public bool NeedChoice { get; set; }
        public List<Storypage> Pages { get => _pages; }
        public bool IsSpecial { get => _choicebooks != null; }
        public string Chosen { get; set; }
        public string DisplayPage
        {
            get
            {
                string text = "";
                if (_count >= _pages.Count)
                {
                    if (IsSpecial && Chosen != "")
                    {
                        NeedChoice = false;
                        Name += "." + _choicebooks[Chosen].Name;
                        foreach (Storypage page in _choicebooks[Chosen].Pages)
                        {
                            _pages.Add(page);
                        }
                        _choicebooks = null;
                    }
                    else if (IsSpecial && Chosen == "")
                    {
                        text += "\n>>>>>Press c to choose";
                        NeedChoice = true;
                    }
                    else _isr = true;
                }
                if (_count < _pages.Count)
                {
                    text += "\n\t\t" + _pages[_count].PersonSpeaking + " said:";
                    text += "\n\t" + _pages[_count].SpeechContent;
                    _count += 1;
                }
                return text;
            }
        }
        public bool IsRead { get => _isr; }
        //methods
        public void AddChoice(string choice, Storybook book)
        {
            if (_choicebooks == null)
                _choicebooks = new Dictionary<string, Storybook>();
            _choicebooks[choice] = book;
        }
    }
}