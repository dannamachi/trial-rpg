using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDetectiveG.src.cont
{
    public abstract class ActorManager
    {
        public abstract SceneID SceneID { get; }
        protected be.Player _player;
        protected List<IAct> _acts;
        public ActorManager(be.Player player)
        {
            _acts = new List<IAct>();
            _acts.Add(player.Avatar);
            _player = player;
            LoadActors();
        }
        protected abstract void LoadActors();
        public IAct GetActor(string name)
        {
            foreach (IAct act in _acts)
            {
                if (act.IsCalled(name))
                    return act;
            }
            return null;
        }
        public IAct GetBackground()
        {
            foreach (IAct act in _acts)
            {
                if (act.IsCalled("BACKGROUND"))
                {
                    return act;
                }
            }
            return null;
        }
        public List<IAct> GetActors()
        {
            return _acts;
        }
        public abstract void ProcessInput();
        public virtual void Update() { }
    }
}
