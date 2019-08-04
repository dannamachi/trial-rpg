using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreetDetectiveG.src.gr;
using StreetDetectiveG.src.be;

namespace StreetDetectiveG.src.cont
{
    public class ExploreActor : ActorManager
    {
        public override SceneID SceneID
        {
            get { return SceneID.EXPLORE; }
        }
        public ExploreActor(be.Player player) : base(player) { }
        protected override void LoadActors()
        {
            Button openbar = new Button("BUTTON_OPENBAR");
            openbar.Location = new PVector(2, 2);
            openbar.ResizeInPvt(5, 5);
            Button inventory = new Button("BUTTON_INVENTORY");
            openbar.Location = new PVector(7, 2);
            openbar.ResizeInPvt(5, 5);
            Button quest = new Button("BUTTON_QUEST");
            openbar.Location = new PVector(12, 2);
            openbar.ResizeInPvt(5, 5);
            Button setting = new Button("BUTTON_SETTING");
            openbar.Location = new PVector(17, 2);
            openbar.ResizeInPvt(5, 5);
            TiledBackground bg = new TiledBackground();
            bg.Location = new PVector(0, 0);

            _acts.Add(openbar);
            _acts.Add(inventory);
            _acts.Add(quest);
            _acts.Add(setting);
            _acts.Add(bg);
        }
        public override void ProcessInput()
        {
            _player.DoSomething(SceneID);
        }
    }
}
