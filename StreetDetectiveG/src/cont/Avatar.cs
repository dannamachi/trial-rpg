using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetDetectiveG.src.gr;
using StreetDetectiveG.src.be;

namespace StreetDetectiveG.src.cont
{
    public class Avatar : IAct
    {
        //fields
        private float _speed;
        private string _name;
        //properties
        public PVector Position { get; set; }
        public float Speed { get => _speed; }
        //constructors
        public Avatar(string name)
        {
            Position = new PVector(0, 0);
            _speed = 100;
            _name = name;
        }
        //methods
        /// <summary>
        /// whether at some point (edit)
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
        public void FaceTo(FacingWhichSide side)
        {
            //via radio line
        }
    }
}
