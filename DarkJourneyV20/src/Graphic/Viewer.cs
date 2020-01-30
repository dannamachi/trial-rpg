using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SEGraphic;

namespace SEVirtual
{
    public static class Viewer
    {
        //fields
        private static Dictionary<GameMode, List<Zone>> _zoneDict = new Dictionary<GameMode, List<Zone>>();
        private static Dictionary<string, Rectangle> _places = new Dictionary<string, Rectangle>();
        private static int _size = 50;
        private static Rectangle _tiles = new Rectangle(0, 0, 10, 10);
        private static int _xcorner = 20;
        private static int _ycorner = 20;
        //constructors
        //properties
        //methods
        public static Rectangle GetRect(string name)
        {
            if (_places.ContainsKey(name))
                return _places[name];
            return new Rectangle(0, 0, 100, 100);
        }
        public static Vector2 GetPosition(string name)
        {
            Rectangle rect = _places[name];
            return new Vector2(rect.X, rect.Y);
        }
        public static void Initialize()
        {
            InitializePositions();
            InitializeZones();
        }
        public static List<Zone> GetBaseObjs(GameMode gmode)
        {
            return _zoneDict[gmode];
        }
        private static void InitializePositions()
        {
            _places = new Dictionary<string, Rectangle>();

            _places.Add("title", new Rectangle(0,0,800,600));
            _places.Add("default", new Rectangle(0,0,800,600));

            _places.Add("button_start", new Rectangle(300, 200, 200, 50));
            _places.Add("button_map", new Rectangle(300, 300, 200, 50));
            _places.Add("button_credits", new Rectangle(300, 400, 200, 50));
            _places.Add("button_exit", new Rectangle(300, 500, 200, 50));

            _places.Add("button_toggleinv", new Rectangle(640, 20, 140, 80));
            _places.Add("button_togglesys", new Rectangle(640, 120, 140, 80));
            _places.Add("button_find", new Rectangle(640, 220, 140, 80));
            _places.Add("button_pickplace", new Rectangle(640, 320, 140, 80));
            _places.Add("button_use", new Rectangle(640, 420, 140, 80));

            _places.Add("buttonsys_help", new Rectangle(0, 200, 100, 50));
            _places.Add("buttonsys_journal", new Rectangle(0, 275, 100, 50));
            _places.Add("buttonsys_complete", new Rectangle(0, 350, 100, 50));
            _places.Add("buttonsys_savequit", new Rectangle(0, 425, 100, 50));
            //_places.Add("invslot", new Rectangle(0, 550, 50, 50));
            _places.Add("hover_art", new Rectangle(500, 500, 250, 100));
            _places.Add("hover_quest", new Rectangle(500, 500, 250, 100));
            _places.Add("infoline", new Rectangle(40,530,500,100));

            _places.Add("button_yes", new Rectangle(310, 350, 60, 40));
            _places.Add("button_no", new Rectangle(430, 350, 60, 40));
            _places.Add("alert_frame", new Rectangle(250, 200, 300, 200));

            _places.Add("info_up", new Rectangle(150, 450, 100, 40));
            _places.Add("info_menu", new Rectangle(300, 450, 100, 40));
            _places.Add("info_game", new Rectangle(450, 450, 100, 40));
            _places.Add("info_down", new Rectangle(600, 450, 100, 40));
            _places.Add("info_frame", new Rectangle(100, 100, 600, 400));

            _places.Add("button_choosemap", new Rectangle(300, 200, 200, 50));
            _places.Add("button_menu", new Rectangle(300, 300, 200, 50));

            _places.Add("dial_name", new Rectangle(0, 300, 100, 50));
            _places.Add("dial_box", new Rectangle(0, 350, 800, 250));
            _places.Add("person", new Rectangle(210,130,350,400));

            _places.Add("button_easy", new Rectangle(300, 200, 200, 50));
            _places.Add("button_hard", new Rectangle(300, 300, 200, 50));

            _places.Add("option_box", new Rectangle(300, 150, 200, 50));
            _places.Add("option_down", new Rectangle(300, 200, 100, 50));
            _places.Add("option_up", new Rectangle(400, 200, 100, 50));
            _places.Add("option_yes", new Rectangle(325, 250, 150, 50));
            _places.Add("option_no", new Rectangle(325, 350, 150, 50));
            _places.Add("option_frame", new Rectangle(250, 100, 300, 400));
        }
        private static GraphicObject CreateButton(string name, int pri)
        {
            Rectangle rect = _places[name];
            GameDetails gd = new GameDetails(rect.X, rect.Y, rect.Width, rect.Height);
            GraphicObject bu = new GraphicObject(gd);
            bu.Priority = pri;
            bu.Name = name;
            return bu;
        }
        public static Zone LoadGameMap(List<TileV> tiles, Player p)
        {
            List<GraphicObject> bus = new List<GraphicObject>();
            GraphicObject tileG;
            int overlay;
            foreach (TileV tile in tiles)
            {
                overlay = 0;
                tileG = Convert(tile);
                //tile pick
                bus.Add(tileG);
                //overlay
                if (tile.Storybook != null)
                {
                    if (!tile.Storybook.IsRead)
                    {
                        if (p.CanRead(tile.Trigger))
                            bus.Add(Convert(tile.Storybook,tileG.X, tileG.Y));
                        overlay += 1;
                    }
                }
                if (tile.Object != null && overlay == 0)
                {
                    if (tile.Object is ActionObject && tile.CanBeFlippedBy(p))
                    {
                        if (tile.Trigger != null)
                        {
                            if (!tile.Trigger.IsFlipped)
                                bus.Add(Convert(tile.Trigger, tileG.X, tileG.Y));
                            overlay += 1;
                        }
                    }
                    else
                    {
                        bus.Add(Convert(tile.Object, tileG.X, tileG.Y));
                        overlay += 1;
                    }
                }
                if (tile.Trigger != null && overlay == 0)
                {
                    if (tile.CanBeFlippedBy(p) && !tile.Trigger.IsFlipped)
                        bus.Add(Convert(tile.Trigger, tileG.X, tileG.Y));
                }
                //player
                if (p.Tile.X == tile.X && p.Tile.Y == tile.Y)
                {
                    bus.Add(Convert(p));
                }
            }
            GraphicObject bug = CreateButton("default", 1);
            bug.Name = "defaultBG|default";
            bus.Add(bug);
            Zone zn = new Zone(new GameDetails(20, 20, 620, 520), bus, 0, 5);
            zn.Priority = 1;
            return zn;
        }
        public static Zone LoadSystemButtons(Player p)
        {
            Zone zn = null;
            if (p.EComm)
            {
                zn = new Zone(new GameDetails(0, 200, 100, 300), LoadGameButtons2(), 0, 5);
                zn.Priority = 4;
            }
            return zn;
        }
        public static Zone LoadInventoryItems(Player p)
        {
            List<GraphicObject> bus = new List<GraphicObject>();
            GraphicObject bu;
            List<string> stuffs = p.GetStuffList();
            for (int i = 0; i < stuffs.Count; i++)
            {
                bu = Convert(stuffs[i], i, p.SeeingArtifact);
                bus.Add(bu);
            }
            if (p.Holding != null)
                bus.Add(Convert(p.Holding));
            Zone zn = new Zone(new GameDetails(0, 600 - _size, 800, _size), bus, 0, 5);
            zn.Priority = 3;
            return zn;
        }
        public static Zone LoadGameInventory(int num)
        {
            List<GraphicObject> bus = new List<GraphicObject>();
            GraphicObject bu;
            //inv slots
            for (int i = 0; i < num; i++)
            {
                bu = new GraphicObject(new GameDetails(i * _size, 600 - _size, _size, _size));
                bu.Priority = 1;
                bu.Name = "game_inv|invslot";
                bus.Add(bu);
            }
            //holding slot
            GraphicObject bu1 = new GraphicObject(new GameDetails(800 - _size, 600 - _size, _size, _size));
            bu1.Priority = 1;
            bu1.Name = "game_inv|invslot";
            bus.Add(bu1);
            Zone zn = new Zone(new GameDetails(0, 600 - _size, _size * num, _size), bus, 0, 5);
            zn.Priority = 2;
            return zn;
        }
        public static Zone LoadCharacter(Player p) {
            List<GraphicObject> bus = new List<GraphicObject>();

            Storybook book = p.GetCurrBook();
            GraphicObject go = Convert(book);           
            bus.Add(go);

            GraphicObject bug = CreateButton("default",1);
            bug.Name = "backgrounds|" + book.Name;
            bus.Add(bug);

            Zone zn = new Zone(new GameDetails(0, 0, 800, 600), bus, 0, 5);
            zn.Priority = 2;
            return zn;
        }
        private static List<GraphicObject> LoadGameButtons2()
        {
            List<GraphicObject> bus = new List<GraphicObject>();
            bus.Add(CreateButton("buttonsys_help", 5));
            bus.Add(CreateButton("buttonsys_journal", 5));
            bus.Add(CreateButton("buttonsys_complete", 5));
            bus.Add(CreateButton("buttonsys_savequit", 5));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "game_but|" + bu.Name;
            }
            return bus;
        }
        private static List<GraphicObject> LoadGameButtons1()
        {
            List<GraphicObject> bus = new List<GraphicObject>();
       
            bus.Add(CreateButton("button_toggleinv", 2));
            bus.Add(CreateButton("button_togglesys", 2));
            bus.Add(CreateButton("button_find", 2));
            bus.Add(CreateButton("button_pickplace", 2));
            bus.Add(CreateButton("button_use", 2));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "game_but|" + bu.Name;
            }
            return bus;
        }
        private static List<GraphicObject> LoadMenuButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();

            bus.Add(CreateButton("button_start",2));
            bus.Add(CreateButton("button_map",2));
            bus.Add(CreateButton("button_credits",2));
            bus.Add(CreateButton("button_exit",2));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "menu_but|" + bu.Name;
            }
            GraphicObject bug = CreateButton("title", 1);
            bug.Name = "titleBG|title";
            bus.Add(bug);
            return bus;
        }
        private static List<GraphicObject> LoadMapButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();
           
            bus.Add(CreateButton("button_choosemap", 2));
            bus.Add(CreateButton("button_menu", 2));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "map_but|" + bu.Name;
            }
            GraphicObject bug = CreateButton("default", 1);
            bug.Name = "defaultBG|default";
            bus.Add(bug);

            return bus;
        }
        private static List<GraphicObject> LoadAlertButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();

            bus.Add(CreateButton("button_yes", 3));
            bus.Add(CreateButton("button_no", 3));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "alert_but|" + bu.Name;
            }
            GraphicObject bu1 = CreateButton("alert_frame", 2);
            bu1.Name = "alert_box|" + bu1.Name;
            bus.Add(bu1);
            GraphicObject bug = CreateButton("default", 1);
            bug.Name = "defaultBG|default";
            bus.Add(bug);
            return bus;
        }
        private static List<GraphicObject> LoadInfoButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();

            bus.Add(CreateButton("info_up", 3));
            bus.Add(CreateButton("info_down", 3));
            bus.Add(CreateButton("info_menu", 3));
            bus.Add(CreateButton("info_game", 3));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "info_but|" + bu.Name;
            }
            GraphicObject bu1 = CreateButton("info_frame", 2);
            bu1.Name = "info_box|" + bu1.Name;
            bus.Add(bu1);
            GraphicObject bug = CreateButton("default", 1);
            bug.Name = "defaultBG|default";
            bus.Add(bug);
            return bus;
        }
        private static List<GraphicObject> LoadDialButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();

            GraphicObject bu1 = CreateButton("dial_name", 2);
            bu1.Name = "dial_name|" + bu1.Name;
            bus.Add(bu1);
            bu1 = CreateButton("dial_box", 2);
            bu1.Name = "dial_box|" + bu1.Name;
            bus.Add(bu1);
            // GraphicObject bug = CreateButton("default", 1);
            // bug.Name = "defaultBG|default";
            // bus.Add(bug);
            return bus;
        }
        private static List<GraphicObject> LoadSetButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();

            bus.Add(CreateButton("button_easy", 2));
            bus.Add(CreateButton("button_hard", 2));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "set_but|" + bu.Name;
            }
            GraphicObject bug = CreateButton("default", 1);
            bug.Name = "defaultBG|default";
            bus.Add(bug);
            return bus;
        }
        private static List<GraphicObject> LoadChooseButtons()
        {
            List<GraphicObject> bus = new List<GraphicObject>();

            bus.Add(CreateButton("option_up", 3));
            bus.Add(CreateButton("option_down", 3));
            bus.Add(CreateButton("option_yes", 3));
            bus.Add(CreateButton("option_no", 3));
            foreach (GraphicObject bu in bus)
            {
                bu.Name = "choose_but|" + bu.Name;
            }
            GraphicObject bu1 = CreateButton("option_box", 3);
            bu1.Name = "choose_box|" + bu1.Name;
            bus.Add(bu1);
            bu1 = CreateButton("option_frame", 2);
            bu1.Name = "choose_frame|" + bu1.Name;
            bus.Add(bu1);
            GraphicObject bug = CreateButton("default", 1);
            bug.Name = "defaultBG|default";
            bus.Add(bug);
            return bus;
        }
        private static void InitializeZones()
        {
            _zoneDict = new Dictionary<GameMode, List<Zone>>();
            List<Zone> zones;
            Zone zone;

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(620, 20, 200, 500), LoadGameButtons1(), 0, 5);
            zone.Priority = 3;
            zones.Add(zone);
            _zoneDict.Add(GameMode.GAME, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(300, 200, 200, 350), LoadMenuButtons(), 0, 5);
            zone.Priority = 1;
            zones.Add(zone);
            _zoneDict.Add(GameMode.MENU, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(300, 200, 200, 150), LoadMapButtons(), 0, 5);
            zone.Priority = 1;
            zones.Add(zone);
            _zoneDict.Add(GameMode.MAP, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(250, 200, 300, 200), LoadAlertButtons(), 0, 5);
            zone.Priority = 1;
            zones.Add(zone);
            _zoneDict.Add(GameMode.ALERT, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(100, 100, 600, 400), LoadInfoButtons(), 0, 5);
            zone.Priority = 1;
            zones.Add(zone);
            _zoneDict.Add(GameMode.INFO, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(0, 300, 800, 600), LoadDialButtons(), 0, 5);
            zone.Priority = 3;
            zones.Add(zone);
            zone = new Zone(new GameDetails(0, 0, 800, 600), new List<GraphicObject>(), 0, 1);
            zone.Priority = 4;
            zone.Name = "zone_overall";
            zones.Add(zone);
            _zoneDict.Add(GameMode.DIAL, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(300, 200, 200, 150), LoadSetButtons(), 0, 5);
            zone.Priority = 1;
            zones.Add(zone);
            _zoneDict.Add(GameMode.SET, zones);

            zones = new List<Zone>();
            zone = new Zone(new GameDetails(250, 100, 300, 400), LoadChooseButtons(), 0, 5);
            zone.Priority = 1;
            zones.Add(zone);
            _zoneDict.Add(GameMode.CHOOSE, zones);
        }
        /// <summary>
        /// convert inventory items to GO
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static GraphicObject Convert(string name, int index, bool isArt)
        {
            int w = _size;
            int h = _size;
            int x = index * _size;
            int y = 600 - _size;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            if (isArt)
                go.Name = "artifacts|" + name;
            else
                go.Name = "common|quest";
            go.Priority = 3;
            return go;
        }
        /// <summary>
        /// convert gameobject (holding) to GO
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static GraphicObject Convert(GameObject obj)
        {
            int w = _size;
            int h = _size;
            int x = 800 - _size;
            int y = 600 - _size;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            go.Name = "objects|" + obj.Name;
            go.Priority = 4;
            return go;
        }
        /// <summary>
        /// convert objs to GO (tile)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="xtile"></param>
        /// <param name="ytile"></param>
        /// <returns></returns>
        public static GraphicObject Convert(GameObject obj, int xtile, int ytile)
        {
            int w = _size;
            int h = _size;
            int x = xtile;
            int y = ytile;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            if (obj is ActionObject)
            {
                go.Name = "common|aobject";
            }
            else
            {
                go.Name = "common|gobject";
            }
            go.Priority = 3;
            return go;
        }
        /// <summary>
        /// convert convo to GO (tile)
        /// </summary>
        /// <param name="bk"></param>
        /// <param name="xtile"></param>
        /// <param name="ytile"></param>
        /// <returns></returns>
        public static GraphicObject Convert(Storybook bk, int xtile, int ytile)
        {
            int w = _size;
            int h = _size;
            int x = xtile;
            int y = ytile;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            go.Name = "common|convo";
            go.Priority = 3;
            return go;
        } 
        /// <summary>
        /// convert trigger to GO (tile)
        /// </summary>
        /// <param name="trig"></param>
        /// <param name="xtile"></param>
        /// <param name="ytile"></param>
        /// <returns></returns>
        public static GraphicObject Convert(Trigger trig, int xtile, int ytile)
        {
            int w = _size;
            int h = _size;
            int x = xtile;
            int y = ytile;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            if (trig is TriggerA)
            {
                go.Name = "common|artifact";
            }
            else if (trig is TriggerQ)
            {
                go.Name = "common|quest";
            }
            else
            {
                go.Name = "common|finish";
            }
            go.Priority = 3;
            return go;
        }
        /// <summary>
        /// convert player to GO (tile)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static GraphicObject Convert(Player p)
        {
            int w = _size;
            int h = _size;
            int x = _xcorner + p.Tile.X * w;
            int y = _ycorner + p.Tile.Y * h;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            go.Name = "common|player";
            go.Priority = 4;
            return go;
        }
        /// <summary>
        /// convert tile to GO (tile)
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="xcorner"></param>
        /// <param name="ycorner"></param>
        /// <returns></returns>
        public static GraphicObject Convert(TileV tile)
        {
            int w = _size;
            int h = _size;
            int x = _xcorner + tile.X * w;
            int y = _ycorner + tile.Y * h;
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            if (tile.Blocked)
            {
                go.Name = "tiles|Blocked";
            }
            else
            {
                go.Name = "tiles|Unblocked";
            }
            go.Priority = 2;
            return go;
        }
        /// <summary>
        /// convert speaking person to char sprite
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static GraphicObject Convert(Storybook book)
        {
            int w = _places["person"].Width;
            int h = _places["person"].Height;
            int x = _places["person"].X;
            int y = _places["person"].Y;

            Storypage page = book.GetCurrPage();
            GraphicObject go = new GraphicObject(new GameDetails(x, y, w, h));
            if (page != null)
            {
                go.Name = "characters|" + page.PersonSpeaking;
                go.Priority = 2;
            }
            return go;
        }

    }
}
