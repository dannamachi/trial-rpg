using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class Storypage
    {
        //fields
        private string _p;
        private string _s;
        //constructors
        public Storypage (string person, string speech)
        {
            _p = person;
            _s = speech;
        }
        //properties
        public string PersonSpeaking { get => _p; }
        public string SpeechContent { get => _s; }
        //methods
    }
}
