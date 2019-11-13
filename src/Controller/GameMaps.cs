using System;
using System.Collections.Generic;
using System.IO;

namespace SEVirtual
{
    public static class GameMaps
    {
        //fields
        private static List<Map> _maps = new List<Map>();
        //properties
        //methods
        public static bool Initialize()
        {
            if (!File.Exists("data/maps.txt")) return false;
            if (!LoadMaps()) return false;
            foreach (Map m in _maps)
            {
                if (!File.Exists("data/" + m.Name + "_objectdata.txt")) return false;
                if (!File.Exists("data/" + m.Name + "_storybooks.txt")) return false;
                if (!File.Exists("data/" + m.Name + "_storybranches.txt")) return false;
                if (!File.Exists("data/" + m.Name + "_tiledataV.txt")) return false;
            }
            return true;
        }
        private static Map GetMap(string name)
        {
            foreach (Map m in _maps)
            {
                if (m.IsCalled(name)) return m;
            }
            return null;
        }
        private static bool HasMap(string name)
        {
            foreach (Map m in _maps)
            {
                if (m.IsCalled(name)) return true;
            }
            return false;
        }
        public static int GetAvailMapCount()
        {
            return GetMapsForPath().Count;
        }
        public static string GetAvailMaplist()
        {
            string text = "";
            List<Map> maps = GetMapsForPath();
            for (int i = 0; i < maps.Count; i++)
            {
                text += "\n" + i + " " + maps[i].Name;
            }
            return text;
        }
        private static List<Map> GetMapsForPath()
        {
            List<Map> maps = new List<Map>();
            foreach (Map m in _maps)
            {
                if (GamePath.GetPath().IsMapDoable(m))
                {
                    maps.Add(m);
                }
            }
            return maps;
        }
        private static bool LoadMaps()
        {
            _maps = new List<Map>();
            StreamReader reader = new StreamReader("data/maps.txt");
            if (reader != null)
            {
                string line = reader.ReadLine();
                string mapname;
                int wins = -1, tokeno = -1;
                List<string> qnames;
                List<ClearToken> tokens;
                Map map;
                while (line != null)
                {
                    mapname = line;
                    wins = Convert.ToInt32(reader.ReadLine());
                    if (wins > 0)
                    {
                        qnames = new List<string>();
                        for (int i = 0; i < wins; i++)
                        {
                            qnames.Add(reader.ReadLine());
                        }
                    }
                    else
                    {
                        qnames = null;
                    }
                    tokeno = Convert.ToInt32(reader.ReadLine());
                    if (tokeno > 0)
                    {
                        tokens = new List<ClearToken>();
                        for (int i = 0; i < tokeno; i++)
                        {
                            tokens.Add(new ClearToken(reader.ReadLine()));
                        }
                    }
                    else
                    {
                        tokens = null;
                    }
                    map = new Map(mapname, qnames, tokens);
                    _maps.Add(map);
                    line = reader.ReadLine();
                }
                return true;
            }
            return false;
        }
        public static string GetMapName(int index)
        {
            int i = 0;
            foreach (Map map in GetMapsForPath())
            {
                if (i == index) { return map.Name; }
                i += 1;
            }
            return null;
        }
        public static string GetWin(string mapname)
        {
            if (HasMap(mapname))
            {
                List<string> wins = GetMap(mapname).Wins;
                if (wins == null) return null;
                string text = "";
                for (int i = 0; i < wins.Count; i++)
                {
                    text += wins[i];
                    if (i < wins.Count - 1)
                    {
                        text += "|";
                    }
                }
                return text;
            }
            else return null;
        }
    }
}
