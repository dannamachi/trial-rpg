using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDetectiveG.src.cont
{
    public enum SceneID
    {
        EXPLORE,
        INVENTORY,
        QUEST,
        SETTING,
        TITLE,
        CREDITS
    }
    public class Scene
    {
        private SceneID SceneID { get; set; }
        private static Dictionary<SceneID, ActorManager> _actorManagerDict = new Dictionary<SceneID, ActorManager>();
        private static Dictionary<SceneID, ContentManager> _contentManagerDict = new Dictionary<SceneID, ContentManager>();
        private ActorManager ActorManager
        {
            get
            {
                return _actorManagerDict[SceneID];
            }
        }
        private ContentManager ContentManager
        {
            get
            {
                return _contentManagerDict[SceneID];
            }
        }
        public static void InitializeScenes()
        {
            //refresh scenes
            //fill up sceneID dicts
        }
        public void ChangeScene(SceneID sceneID)
        {
            SceneID = sceneID;
        }
        public void Run()
        {
            ActorManager.ProcessInput();
            ActorManager.Update();
            DrawCommon.Draw(ActorManager.GetBackground(), ContentManager);
            foreach (IAct act in ActorManager.GetActors())
            {
                DrawCommon.Draw(act, ContentManager);
            }
        }
    }
}
