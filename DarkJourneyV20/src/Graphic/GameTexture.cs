using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SEGraphic
{
    public class GameTexture
    {
        //fields
        private string _map;
        private Dictionary<string, string> _charDict, _bgDict;
        private Dictionary<string, MiniSprite> _spriteDict;
        //constructors
        public GameTexture()
        {
            _spriteDict = new Dictionary<string, MiniSprite>();
            _charDict = new Dictionary<string, string>();
            _bgDict = new Dictionary<string, string>();
        }
        //properties
        //methods
        public void InitializeMap(string name, Texture2D arts, Texture2D chas, Texture2D objs, Texture2D tils, Texture2D bgs)
        {
            _map = name;
            LoadMapTexture(arts,chas,objs,tils,bgs);
        }
        public bool Initialize(Texture2D commontext, Texture2D guitext, Texture2D titlet, Texture2D defaultt)
        {
            LoadBackground(titlet, defaultt);
            return LoadCommonTexture(commontext, guitext);
        }
        private bool CheckCommon()
        {
            if (!File.Exists("Content/common/common.xnb")) return false;
            if (!File.Exists("Content/common/guis.xnb")) return false;
            if (!File.Exists("data/common/commontexture.txt")) return false;
            if (!File.Exists("data/common/guitexture.txt")) return false;
            if (!File.Exists("data/common/commonsprite.txt")) return false;
            if (!File.Exists("data/common/guisprite.txt")) return false;
            return true;
        }
        private void LoadBackground(Texture2D titlet, Texture2D defaultt) 
        {
            MiniSprite titlesp = new MiniSprite(titlet, new Vector2(0,0),1,1,800,600);
            MiniSprite defaultsp = new MiniSprite(defaultt, new Vector2(0,0),1,1,800,600);

            _spriteDict["titleBG"] = titlesp;
            _spriteDict["defaultBG"] = defaultsp;

            //List<string> titlenames = new List<string>();
            //titlenames.Add("title");
            //List<string> defnames = new List<string>();
            //defnames.Add("default");

            _spriteDict["titleBG"].SetObjects(new List<string>() { "title" });
            _spriteDict["defaultBG"].SetObjects(new List<string>() { "default" });
        }
        /// <summary>
        /// load texture specific to each map
        /// items, person, bg, map icon
        /// txt to specify which is which
        /// </summary>
        private void LoadMapTexture(Texture2D artifacts, Texture2D characters, Texture2D objects, Texture2D tiles, Texture2D bgs)
        {
            StreamReader reader;
            string line, name;
            List<string> numbers;
            _charDict = new Dictionary<string, string>();
            _bgDict = new Dictionary<string, string>();
            reader = new StreamReader("data/" + _map + "/spritedetails.txt");
            if (reader != null)
            {
                int x, y, c, r, w, h;
                MiniSprite spr;
                List<Texture2D> pack = new List<Texture2D> { artifacts, characters, objects, tiles, bgs };
                line = reader.ReadLine();
                for (int i = 0; i < 5; i++)
                {
                    numbers = line.Split('|').ToList();
                    x = Convert.ToInt32(numbers[0]);
                    y = Convert.ToInt32(numbers[1]);
                    c = Convert.ToInt32(numbers[2]);
                    r = Convert.ToInt32(numbers[3]);
                    w = Convert.ToInt32(numbers[4]);
                    h = Convert.ToInt32(numbers[5]);
                    name = numbers[6];
                    spr = new MiniSprite(pack[i], new Vector2(x, y), c, r, w, h);
                    _spriteDict[name] = spr;
                    line = reader.ReadLine();
                }
                reader.Close();

                reader = new StreamReader("data/" + _map + "/objecttexture.txt");
                if (reader != null)
                {
                    line = reader.ReadLine();
                    for (int i = 0; i < 4; i++)
                    {
                        name = line;
                        line = reader.ReadLine();
                        numbers = line.Split('|').ToList();
                        _spriteDict[name].SetObjects(numbers);
                        line = reader.ReadLine();
                    }
                    reader.Close();
                }

                reader = new StreamReader("data/" + _map + "/characters.txt");
                if (reader != null)
                {
                    int count = Convert.ToInt32(reader.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        line = reader.ReadLine();
                        numbers = line.Split('|').ToList();
                        _charDict[numbers[0]] = numbers[1];
                    }
                    _spriteDict["characters"].SetObjects(_charDict.Values.ToList());
                    reader.Close();
                }

                reader = new StreamReader("data/" + _map + "/backgrounds.txt");
                if (reader != null)
                {
                    List<string> bglist = new List<string>();
                    int count = Convert.ToInt32(reader.ReadLine());
                    for (int i = 0; i < count; i++)
                    {
                        line = reader.ReadLine();
                        numbers = line.Split('|').ToList();
                        string bgname = numbers[0];
                        int numscenes = Convert.ToInt32(numbers[1]);
                        for (int j = 0; j < numscenes; j++)
                        {
                            _bgDict[numbers[2 + j]] = bgname;
                        }
                        bglist.Add(bgname);
                    }
                    _spriteDict["backgrounds"].SetObjects(bglist);
                    reader.Close();
                }
            }
        }
        /// <summary>
        /// load texture for every map
        /// txt to specify which sprite is which
        /// trigger, gui
        /// </summary>
        private bool LoadCommonTexture(Texture2D commontext, Texture2D guitext)
        {
            if (!CheckCommon()) return false;
            StreamReader reader = new StreamReader("data/common/commonsprite.txt");
            string line, name;
            List<string> numbers;
            if (reader != null)
            {
                int x, y, c, r, w, h;
                MiniSprite spr;
                line = reader.ReadLine();
                numbers = line.Split('|').ToList();
                x = Convert.ToInt32(numbers[0]);
                y = Convert.ToInt32(numbers[1]);
                c = Convert.ToInt32(numbers[2]);
                r = Convert.ToInt32(numbers[3]);
                w = Convert.ToInt32(numbers[4]);
                h = Convert.ToInt32(numbers[5]);
                spr = new MiniSprite(commontext, new Vector2(x, y), c, r, w, h);
                _spriteDict["common"] = spr;
                reader.Close();

                reader = new StreamReader("data/common/commontexture.txt");
                if (reader != null)
                {
                    line = reader.ReadLine();
                    numbers = line.Split('|').ToList();
                    spr.SetObjects(numbers);
                    reader.Close();

                    reader = new StreamReader("data/common/guisprite.txt");
                    if (reader != null)
                    {
                        line = reader.ReadLine();
                        while (line != null)
                        {
                            numbers = line.Split('|').ToList();
                            x = Convert.ToInt32(numbers[0]);
                            y = Convert.ToInt32(numbers[1]);
                            c = Convert.ToInt32(numbers[2]);
                            r = Convert.ToInt32(numbers[3]);
                            w = Convert.ToInt32(numbers[4]);
                            h = Convert.ToInt32(numbers[5]);
                            name = numbers[6];
                            spr = new MiniSprite(guitext, new Vector2(x, y), c, r, w, h);
                            _spriteDict[name] = spr;
                            line = reader.ReadLine();
                        }
                        reader.Close();

                        reader = new StreamReader("data/common/guitexture.txt");
                        if (reader != null)
                        {
                            line = reader.ReadLine();
                            while (line != null)
                            {
                                name = line;
                                line = reader.ReadLine();
                                numbers = line.Split('|').ToList();
                                _spriteDict[name].SetObjects(numbers);
                                line = reader.ReadLine();
                            }
                            reader.Close();
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// get rect from which to cut from texture
        /// </summary>
        /// <param name="id"></param>
        public Rectangle GetRect(string id)
        {
            if (id == "Generic|Object") return new Rectangle(0,0,800,600);
            List<string> parts = id.Split('|').ToList();
            if (parts[0] != "characters" && parts[0] != "backgrounds")
                return _spriteDict[parts[0]].GetSrcRect(parts[1]);
            else if (parts[0] == "characters") 
                return _spriteDict["characters"].GetSrcRect(_charDict[parts[1]]);
            else if (parts[0] == "backgrounds") 
                return _spriteDict["backgrounds"].GetSrcRect(_bgDict[parts[1]]);
            else
                return new Rectangle();
        }
        /// <summary>
        /// get texture that graphic object is taken from
        /// </summary>
        /// <param name="id"></param>
        public Texture2D GetTexture(string id)
        {
            List<string> parts = id.Split('|').ToList();
            if (_spriteDict.ContainsKey(parts[0]))
                return _spriteDict[parts[0]].Texture;
            else
                return _spriteDict["defaultBG"].Texture;
        }
    }
}
