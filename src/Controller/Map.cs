using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{
    public class Map : VirtualObject
    {
        //fields
        private List<string> _wins;
        private List<ClearToken> _tokens;
        //constructors
        public Map(string name,List<string> wins, List<ClearToken> tokens)
        {
            Name = name;
            _wins = wins;
            _tokens = tokens;
        }
        //properties
        public List<string> Wins { get => _wins; }
        public List<ClearToken> ClearTokens { get => _tokens; }
        //methods
    }
}
