using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetDetectiveG.src.gr;
using StreetDetectiveG.src.be;

namespace StreetDetectiveG.src.cont
{
    public interface IAct
    {
        bool IsCalled(string name);
        bool IsAt(PVector pvt);
    }
}
