using System;
using System.Collections.Generic;
using System.Text;

namespace SEVirtual
{ 
    public class GamePath
    {
        //fields
        private static GamePath _path;
        private List<GameChapter> _chapters;
        //constructors
        private GamePath()
        {
            _chapters = new List<GameChapter>();
        }
        public static GamePath GetPath()
        {
            if (_path == null)
            {
                _path = new GamePath();
            }
            return _path;
        }
        public static void Renew()
        {
            _path = null;
        }
        //properties
        //methods
        private bool HasMap(string name)
        {
            foreach (GameChapter chap in _chapters)
            {
                if (chap.IsCalled(name))
                    return true;
            }
            return false;
        }
        public bool IsMapDoable(Map map)
        {
            if (!HasMap(map.Name))
            {
                if (map.ClearTokens == null) return true;
                foreach (ClearToken token in map.ClearTokens)
                {
                    bool result = false;
                    foreach (GameChapter chap in _chapters)
                    {
                        if (token.IsClearedBy(chap))
                        {
                            result = true;
                            break;
                        }
                    }
                    if (!result) return false;
                }
                return true;
            }
            return false;
        }
        public void AddChapter(PlayerCP playCP)
        {
            _chapters.Add(new GameChapter(playCP));
        }
    }
}
