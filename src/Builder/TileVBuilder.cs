using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace SEVirtual
{
    public class TileVBuilder
    {
        //fields
        private List<TileV> _tileVs;
        //constructors
        public TileVBuilder() { }
        //properties
        //methods
        private TileV GetEmptyTile(int x, int y)
        {
            TileV tile = new TileV(null, true, x, y);
            return tile;
        }
        private TileV GetBlockedTile(int x, int y)
        {
            TileV tile = new TileV(null, false, x, y);
            return tile;
        }
        private TileV GetQuestTile(List<string> line, int x, int y)
        {
            string qname,qdesc,objname;
            int reqno,objcode;
            List<Quest> qs = new List<Quest>();
            int qno = Convert.ToInt32(line[0]);
            int z = 1;
            while (z < line.Count)
            {
                qname = line[z];
                qdesc = line[z + 1];
                List<Request> reqs = new List<Request>();
                reqno = Convert.ToInt32(line[z + 2]);
                int i = 0;
                while (i < reqno)
                {
                    objname = line[i];
                    objcode = Convert.ToInt32(line[i + 1]);
                    reqs.Add(new Request(objname, objcode));
                    i += 2;
                }
                Quest q = new Quest(qname, reqs);
                q.Description = qdesc;
                qs.Add(q);
                z += 3 + reqno * 2;
            }
            TileV tile = new TileV(new TriggerQ(qs), true, x, y);
            return tile;
        }
        private TileV GetArtTile(List<string> line, int x, int y)
        {
            string aname,adesc;
            List<Artifact> As = new List<Artifact>();
            int ano = Convert.ToInt32(line[0]);
            int z = 1;
            while (z < line.Count)
            {
                aname = line[z];
                adesc = line[z + 1];
                Artifact a = new Artifact(aname);
                a.Description = adesc;
                As.Add(a);
                z += 2;
            }
            TileV tile = new TileV(new TriggerA(As), true, x, y);
            return tile;
        }
        private TileV GetFinQuestTile(List<string> line, int x, int y)
        {
            string fname;
            List<string> names = new List<string>();
            int fno = Convert.ToInt32(line[0]);
            int z = 1;
            while (z < line.Count)
            {
                fname = line[z];
                names.Add(fname);
                z += 1;
            }
            TileV tile = new TileV(new TriggerF(names), true, x, y);
            return tile;
        }
        private TileV FindTileAt(List<TileV> tiles, int x, int y)
        {
            foreach (TileV tile in tiles)
            {
                if (tile.IsAt(x, y))
                    return tile;
            }
            return null;
        }
        private List<TileV> LinkTileVs(List<TileV> tiles, int max_col, int max_row)
        {
            for (int y = 0; y < max_row; y++)
            {
                for (int x = 0; x < max_col; x++)
                {
                    TileV tile = FindTileAt(tiles, x, y);
                    if (tile.Blocked == false)
                    {
                        Dictionary<TDir, TileV> tileDict = new Dictionary<TDir, TileV>();
                        if (x > 0) { tileDict.Add(TDir.LEFT, FindTileAt(tiles, x - 1, y)); }
                        if (y > 0) { tileDict.Add(TDir.TOP, FindTileAt(tiles, x, y - 1)); }
                        if (x < max_col - 1) { tileDict.Add(TDir.RIGHT, FindTileAt(tiles, x + 1, y)); }
                        if (y < max_row - 1) { tileDict.Add(TDir.BOTTOM, FindTileAt(tiles, x, y + 1)); }
                        tile.LinkTileDict(tileDict);
                    }
                }
            }
            return tiles;
        }
        private void LoadTileDataV()
        {
            _tileVs = new List<TileV>();
            StreamReader reader = new StreamReader("tileDataV.txt");
            if (reader != null)
            {
                TileV tileV;
                string line, tiletype;
                List<string> lineparts;

                int col = Convert.ToInt32(reader.ReadLine());
                int row = Convert.ToInt32(reader.ReadLine());
                for (int y = 0; y < row; y++)
                {
                    for (int x = 0; x < col; x++)
                    {
                        line = reader.ReadLine();
                        lineparts = line.Split(',').ToList();
                        tiletype = lineparts[0];
                        lineparts.RemoveAt(0);
                        switch (tiletype)
                        {
                            case "x":
                                tileV = GetEmptyTile(x, y);
                                break;
                            case "o":
                                tileV = GetBlockedTile(x, y);
                                break;
                            case "q":
                                tileV = GetQuestTile(lineparts, x, y);
                                break;
                            case "a":
                                tileV = GetArtTile(lineparts, x, y);
                                break;
                            case "f":
                                tileV = GetFinQuestTile(lineparts, x, y);
                                break;
                            default:
                                tileV = GetBlockedTile(x, y);
                                break;
                        }
                        _tileVs.Add(tileV);
                    }
                }
                _tileVs = LinkTileVs(_tileVs, col, row);
            }
        }
        public List<TileV> LoadTileVsFromFile()
        {
            LoadTileDataV();
            return _tileVs;
        }
    }
}
