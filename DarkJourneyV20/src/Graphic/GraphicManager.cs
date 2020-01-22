using System;
using System.Collections.Generic;
using System.Linq;
using SEVirtual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SEGraphic
{
    public class GraphicManager
    {
        //fields
        private GameTexture _text;
        private SpriteFont _font, _fontsmall;
        //map-spec texture
        private Texture2D _arts, _chas, _objs, _tils, _bgs;
        private Texture2D _common, _gui, _title, _default;
        //constructors
        public GraphicManager()
        {
            _text = new GameTexture();
        }
        //properties
        public SpriteFont FontSmall { get => _fontsmall; }
        public SpriteFont Font { get => _font; }
        public GameTexture GameTexture { get => _text; }
        public Vector2 Mouse { get; set; }
        //methods
        public Dictionary<string,Texture2D> GetLoadedTexture()
        {
            Dictionary<string, Texture2D> found = new Dictionary<string, Texture2D>();
            //found.Add("arts", _arts);
            //found.Add("chas", _chas);
            //found.Add("objs", _objs);
            //found.Add("tils", _tils);
            found.Add("common", _common);
            found.Add("gui", _gui);
            found.Add("titleBG", _title);
            found.Add("defaultBG", _default);
            return found;
        }
        public void LoadMapTexture(Texture2D arts, Texture2D chas, Texture2D objs, Texture2D tils, Texture2D bgs)
        {
            _arts = arts;
            _chas = chas;
            _objs = objs;
            _tils = tils;
            _bgs = bgs;
        }
        public void InitializeMap(string name)
        {
            _text.InitializeMap(name, _arts, _chas, _objs, _tils, _bgs);
        }
        public bool InitializeCommon(Texture2D common, Texture2D gui, Texture2D titlet, Texture2D defaultt)
        {
            _common = common;
            _gui = gui;
            _title = titlet;
            _default = defaultt;
            return _text.Initialize(common, gui, titlet, defaultt);
        }
        public List<Zone> GetZones(GameCP game, Player p)
        {
            List<Zone> zones = new List<Zone>();
            foreach (Zone zn in Viewer.GetBaseObjs(game.Mode))
            {
                zones.Add(zn);
            }
            if (game.Mode == GameMode.GAME)
            {
                if (Viewer.LoadSystemButtons(p) != null)
                {
                    zones.Add(Viewer.LoadSystemButtons(p));
                }
                zones.Add(Viewer.LoadGameInventory(p.Capacity));
                zones.Add(Viewer.LoadInventoryItems(p));
                zones.Add(Viewer.LoadGameMap(game.Tiles, p));
            }
            else if (game.Mode == GameMode.DIAL) {
                zones.Add(Viewer.LoadCharacter(p));
            }
            return zones;
        }
        private List<GraphicObject> GetSortedObjects(GameCP game, Player p)
        {
            List<GraphicObject> objs = new List<GraphicObject>();
            List<Zone> zones = SortZone(GetZones(game,p));
            for (int i = 0; i < zones.Count; i++)
            {
                foreach (GraphicObject go in zones[i].Objects)
                {
                    objs.Add(go);
                }
            }
            return objs;
        }
        public bool Initialize(SpriteFont font, SpriteFont fontsmall)
        {
            _text = new GameTexture();
            Viewer.Initialize();
            _font = font;
            _fontsmall = fontsmall;
            return true;
        }
        public List<Zone> SortZone(List<Zone> unsorted)
        {
            Zone temp;
            for (int i = 0; i < unsorted.Count; i++)
            {
                for (int j = i + 1; j < unsorted.Count; j++)
                {
                    if (unsorted[i].IsAbove(unsorted[j]))
                    {
                        temp = unsorted[i];
                        unsorted[i] = unsorted[j];
                        unsorted[j] = temp;
                    }
                }
            }
            return unsorted;
        }
        public void Draw(GameCP game, Player p, SpriteBatch sb)
        {
            List<GraphicObject> gos = GetSortedObjects(game, p);
            sb.Begin();
            for (int i = 0; i < gos.Count; i++)
            {
                int[] destarr = gos[i].GetDestRect();
                Rectangle dest = new Rectangle(destarr[0], destarr[1], destarr[2], destarr[3]);
                sb.Draw(_text.GetTexture(gos[i].Name), dest, _text.GetRect(gos[i].Name), Color.White);            
            }
            sb.End();
        }
        private string HoverInfo(string name, Player p, bool isHolding, int index)
        {
            string text = "";
            if (!isHolding)
            {
                if (p.SeeingArtifact)
                {
                    text += name + ": " + (p.Find(index, "A") as Artifact).Description;
                }
                else
                {
                    text += name + ": " + (p.Find(index, "Q") as Quest).Info();
                }
            }
            else
            {
                text += "Holding: " + name;
            }
            return text;
        }
        //enclosed by sb.Begin and sb.End
        private void DrawStringLines(SpriteBatch sb, string size, string containerkey, List<string> str, Vector2 loc, Color color) {
            int num = str.Count;
            for (int i = 0; i < num; i++) {
                loc.Y = DrawString(sb,size,containerkey,str[i],loc, color);
            }
        }
        //enclosed by sb.Begin and sb.End
        private int DrawString(SpriteBatch sb, string size, string containerkey, string str, Vector2 loc, Color color) {
            int nextinit = (int)loc.Y;
            
            SpriteFont fontused;
            if (size == "N") fontused = _font;
            else if (size == "S") fontused = _fontsmall;
            else fontused = _font;

            int maxwidth;
            // dialogue box
            if (containerkey == "D") maxwidth = 60;
            // game quest box
            else if (containerkey == "Q") maxwidth = 30;
            // info box
            else if (containerkey == "I") maxwidth = 50;
            // alert box
            else if (containerkey == "A") maxwidth = 30;
            else maxwidth = 30;

            int line = 0;
            int charint = str.Length;
            string segment = "";
            List<string> segments = new List<string>();
            foreach (char chr in str) {
                charint -= 1;
                line += 1;
                segment += chr;
                if (line == maxwidth || charint == 0) {
                    segments.Add(segment);
                    line = 0;
                    segment = "";
                }
            }
            for (int i = 0; i < segments.Count; i++)
            {
                sb.DrawString(fontused, segments[i], new Vector2(loc.X, loc.Y + i * 20), color);
                if (i + 1 == segments.Count)
                    nextinit = (int)loc.Y + i * 20 + 20;
            }
            return nextinit;
        }
        public void DrawActive(GameCP game, Player p, SpriteBatch sb)
        {
            Vector2 vec;
            Rectangle rect;
            List<string> lines;
            sb.Begin();
            switch (game.Mode)
            {
                case GameMode.GAME:
                    Zone gameinv = Viewer.LoadInventoryItems(p);
                    if (gameinv.IsPressed((int)Mouse.X, (int)Mouse.Y))
                    {
                        string key, key2, name;
                        GraphicObject go;
                        //draw game quest box
                        if (p.SeeingArtifact) key = "hover_a|hover_art";
                        else key = "hover_q|hover_quest";
                        key2 = key.Split('|')[1];
                        rect = Viewer.GetRect(key2);
                        sb.Draw(_text.GetTexture(key), rect, _text.GetRect(key), Color.White);
                        //draw content of game quest box
                        for (int i = 0; i < gameinv.Objects.Count; i++)
                        {
                            go = gameinv.Objects[i];
                            if (go.IsPressed((int)Mouse.X, (int)Mouse.Y))
                            {
                                bool isHolding = go.Name.Split('|')[0] == "objects";
                                name = go.Name.Split('|')[1];
                                lines = new List<string> { HoverInfo(name, p, isHolding, i) };
                                DrawStringLines(sb,"S","Q",lines,new Vector2(rect.X + 20, rect.Y + 20),Color.Black);
                                break;
                            }
                        }
                    }
                    break;
                case GameMode.INFO:
                    lines = game.GetInfo();
                    vec = Viewer.GetPosition("info_frame");
                    DrawStringLines(sb,"N","I",lines,new Vector2(vec.X + 20, vec.Y + 20), Color.White);
                    break;
                case GameMode.ALERT:
                    vec = Viewer.GetPosition("alert_frame");
                    lines = new List<string>();
                    lines.Add("You are about to ");
                    lines.Add(game.OpName);
                    DrawStringLines(sb,"N","A",lines,new Vector2(vec.X + 20, vec.Y + 20),Color.White);
                    break;
                case GameMode.DIAL:
                    List<string> dia = game.Dialogue.Split('|').ToList();
                    if (dia.Count == 2)
                    {
                        vec = Viewer.GetPosition("dial_name");
                        DrawString(sb,"N","D",dia[0],new Vector2(vec.X + 20, vec.Y + 20),Color.White);
                        vec = Viewer.GetPosition("dial_box");
                        DrawString(sb,"N","D",dia[1],new Vector2(vec.X + 20, vec.Y + 20),Color.White);
                    }
                    else
                    {
                        vec = Viewer.GetPosition("dial_box");
                        DrawString(sb,"N","D",dia[0],new Vector2(vec.X + 20, vec.Y + 20),Color.White);
                    }
                    break;
                case GameMode.CHOOSE:
                    vec = Viewer.GetPosition("option_box");
                    DrawString(sb,"N","D",game.GetChosen(),new Vector2(vec.X + 20, vec.Y + 20),Color.Red);
                    break;
            }
            sb.End();
        }
    }
}
