using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDetectiveG.src.be
{
    public interface IHaveArtifact
    {
        Artifact LookFor(string name);
        bool Has(string name);
        void Add(Artifact art);
        void Remove(string name);
        void Pass(string name, IHaveArtifact container);
        int Count { get; }
        int Capacity { get; set; }
        bool HasSpace { get; }
    }
}
