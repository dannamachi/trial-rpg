using System;
using System.Collections.Generic;
using System.Text;
using StreetDetectiveG.src.cont;

namespace StreetDetectiveG.src.be
{
    public class Player
    {
        //fields
        private Inventory _inv;
        private static Dictionary<SceneID, PlayerAction> _actionDict = new Dictionary<SceneID, PlayerAction>();
        //properties
        public Quest Quest { get; set; }
        public Avatar Avatar { get; set; }
        //constructors
        public Player()
        {
            _inv = new Inventory();
            Quest = null;
            Avatar = new Avatar("PLAYER");
            Avatar.Position = new PVector(-100, -100);
        }
        //methods
        public void DoSomething(SceneID sceneID)
        {
            _actionDict[sceneID].DoSomething();
        }
        public bool HasArtifact (string name)
        {
            return _inv.Has(name);
        }
        public void ActionAt(Tile tile)
        {
            if (tile.HasTrigger())
            {
                tile.Trigger.FlippedBy(this);
                tile.Flipped = true;
            }
        }
        /// <summary>
        /// 0 -> quest not done
        /// 1 -> quest done
        /// 2 -> no quest
        /// </summary>
        /// <returns></returns>
        public int HasCompletedQuest()
        {
            if (Quest == null) { return 2; }
            if (Quest.IsFulfilledBy(this)) return 1;
            return 0;
        }
        public void TakeQuest(Quest quest)
        {
            Quest = quest;
        }
        public void PlayerMove(int x_perc,int y_perc)
        {
            Avatar.Position = new PVector(Avatar.Position.X + x_perc, Avatar.Position.Y + y_perc);
        }
        public void TakeArtifact(Artifact art)
        {
            if (!_inv.Has(art.Name))
                _inv.Add(art);
        }
        public void DropArtifact(Artifact art)
        {
            if (_inv.Has(art.Name))
                _inv.Remove(art);
        }
    }
}
