using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SEVirtual
{
    public class ObjectBuilder
    {
        //fields
        private List<GameObject> _objs;
        private Player _player;
        private string _filename;
        //constructors
        public ObjectBuilder(Player p)
        {
            _objs = new List<GameObject>();
            _player = p;
            _filename = "objectdata.txt";
        }
        //properties
        //methods
        public void SetMap(string pref)
        {
            _filename = "data/" + pref + "_" + _filename;
        }
        private ActionObject GetActionObject(List<string> array, StreamReader reader)
        {
            string name = array[0];
            int numcon = Convert.ToInt32(array[1]);
            string line;
            int numreq;
            ConToken ctoken;
            List<ClearToken> tokens = new List<ClearToken>();
            List<ConToken> ctokens = new List<ConToken>();

            //make action line
            line = reader.ReadLine();
            ConToken utoken = new ConToken(line);
            ctokens.Add(utoken);
            numreq = Convert.ToInt32(reader.ReadLine());
            for (int j = 0; j < numreq; j++)
            {
                tokens.Add(new ClearToken(reader.ReadLine()));
            }
            ConLine cline = new ConLine(new ActionUse(_player.UseArtifact), utoken, tokens);

            //make move/place lines
            for (int i = 0; i < numcon - 1; i++)
            {
                ctoken = new ConToken(reader.ReadLine());
                ctokens.Add(ctoken);
            }
            ActionObject obj = new ActionObject(name, cline);
            obj.SetTokens(ctokens);
            return obj;
        }
        private GameObject GetGameObject(List<string> array)
        {
            string name = array[0];
            GameObject obj = new GameObject(name);
            return obj;
        }
        public List<TileV> AddObjectFromFile(List<TileV> tiles)
        {
            StreamReader reader = new StreamReader(_filename);
            if (reader != null)
            {
                TileVBuilder builder = new TileVBuilder();
                GameObject obj = null;
                int objno = Convert.ToInt32(reader.ReadLine());
                string line;
                List<string> array;
                for (int i = 0; i < objno; i++)
                {
                    line = reader.ReadLine();
                    array = line.Split('|').ToList();
                    int type = Convert.ToInt32(array[0]);
                    int x = Convert.ToInt32(array[1]);
                    int y = Convert.ToInt32(array[2]);
                    for (int j = 0; j < 3; j++)
                    {
                        array.RemoveAt(0);
                    }
                    TileV tile = builder.FindTileAt(tiles, x, y);
                    switch (type)
                    {
                        case 0:
                            obj = GetGameObject(array);
                            break;
                        case 1:
                            int m = Convert.ToInt32(array[0]);
                            int n = Convert.ToInt32(array[1]);
                            for (int j = 0; j < 2; j++)
                            {
                                array.RemoveAt(0);
                            }
                            obj = GetActionObject(array, reader);
                            TileV atile = builder.FindTileAt(tiles, m, n);
                            if (obj != null && atile != null)
                                atile.LinkedTo = obj as ActionObject;
                            break;
                    }
                    if (obj != null && tile != null)
                        tile.Object = obj;
                }
            }
            return tiles;
        }
    }
}
