using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class ArtifactTrigger : Trigger
    {
        //fields
        private Artifact _art;
        //properties
        //constructors
        public ArtifactTrigger(Artifact art)
        {
            _art = art;
        }
        //methods
        public override string PrintInfo()
        {
            return _art.Name;
        }
        public override void FlippedBy(Player p)
        {
            p.TakeArtifact(_art);
        }
    }
}
