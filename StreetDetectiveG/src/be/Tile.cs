using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class Tile
    {
        //fields
        private string _decor;
        private string _sprite;
        //properties
        public string Sprite { get; set; }
        public bool Blocked
        {
            get
            {
                return _decor != null || Trigger != null;
            }
        }
        public bool Flipped { get; set; }
        public PVector Location { get; set; }
        public Trigger Trigger { get; set; }
        //contructors
        public Tile(PVector location)
        {
            Location = location;
            Flipped = false;
        }
        //methods (can draw itself by taking a content fetcher as argument)
        public void SetDecor(string spritename)
        {
            _decor = spritename;
        }
        public bool IsAt(PVector location)
        {
            return (location.X == Location.X) && (location.Y == Location.Y);
        }
        public bool HasTrigger()
        {
            return Trigger != null && !Flipped;
        }
    }
}
