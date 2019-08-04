using System;
using System.Collections.Generic;
using System.Text;

namespace StreetDetectiveG.src.be
{
    public class ArtifactRequest : Request
    {
        //fields
        //properties
        public string ItemWanted { get; set; }
        //constructors
        public ArtifactRequest(string name)
        {
            ItemWanted = name;
        }
        //methods
        public override string PrintInfo()
        {
            return ItemWanted;
        }
        public override bool IsFulfilledBy(Player p)
        {
            return p.HasArtifact(ItemWanted);
        }
    }
}
