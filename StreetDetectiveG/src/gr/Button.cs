using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StreetDetectiveG.src.cont;
using StreetDetectiveG.src.be;

namespace StreetDetectiveG.src.gr
{
    public class Button : IAct
    {
        //fields
        private string _name;
        //properties
        private int WidthInPvt { get; set; }
        private int HeightInPvt { get; set; }
        public PVector Location { get; set; }
        //constructors
        /// <summary>
        /// initialize button and event links to managers (edit)
        /// </summary>
        /// <param name="name"></param>
        public Button(string name)
        {
            _name = name;
            WidthInPvt = 1;
            HeightInPvt = 1;
        }
        //methods
        public void ResizeInPvt(int x, int y)
        {
            WidthInPvt = x;
            HeightInPvt = y;
        }
        /// <summary>
        /// whether at some point (editing)
        /// </summary>
        /// <param name="pvt"></param>
        /// <returns></returns>
        public bool IsAt(PVector pvt)
        {
            return true;
        }
        public bool IsCalled(string name)
        {
            return name == _name;
        }
        /// <summary>
        /// bool if button pressed (edit)
        /// </summary>
        /// <param name="pvt"></param>
        /// <returns></returns>
        public bool IsPressed(PVector pvt)
        {
            return false;
        }
    }
}
