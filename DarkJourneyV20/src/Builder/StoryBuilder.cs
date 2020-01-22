using System;
using System.Collections.Generic;
using System.IO;

namespace SEVirtual
{
    public class StoryBuilder
    {
        //fields
        private List<Storybook> _branches;
        private string _bofname;
        private string _brfname;
        //constructors
        public StoryBuilder()
        {
            _bofname = "storybooks.txt";
            _brfname = "storybranches.txt";
        }
        //properties
        //methods
        private bool HasBranch(string name)
        {
            foreach (Storybook book in _branches)
            {
                if (book.IsCalled(name)) { return true; }
            }
            return false;
        }
        private Storybook FindBranch(string name)
        {
            foreach (Storybook book in _branches)
            {
                if (book.IsCalled(name)) { return book; }
            }
            return null;
        }
        public void LoadBranches()
        {
            StreamReader reader = new StreamReader(_brfname);
            if (reader != null)
            {
                _branches = new List<Storybook>();
                string line = reader.ReadLine();
                int pageno;
                string storytitle;
                string[] array;
                Storybook book;
                Storypage page;
                List<Storypage> pages;
                while (line != null)
                {
                    pageno = Convert.ToInt32(line);
                    storytitle = reader.ReadLine();
                    pages = new List<Storypage>();
                    for (int i = 0; i < pageno; i++)
                    {
                        array = reader.ReadLine().Split('|');
                        page = new Storypage(array[0], array[1]);
                        pages.Add(page);
                    }
                    book = new Storybook(storytitle, pages);
                    _branches.Add(book);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
        }
        public List<TileV> LoadBooks(List<TileV> tiles, TileVBuilder builder)
        {
            StreamReader reader = new StreamReader(_bofname);
            if (reader != null)
            {
                string line = reader.ReadLine();
                int pageno, x, y, special;
                string storytitle;
                string[] array;
                Storybook book;
                Storypage page;
                List<Storypage> pages;
                TileV tile;
                while (line != null)
                {
                    array = line.Split('|');
                    pageno = Convert.ToInt32(array[0]);
                    special = Convert.ToInt32(array[1]);
                    array = reader.ReadLine().Split('|');
                    x = Convert.ToInt32(array[0]);
                    y = Convert.ToInt32(array[1]);
                    storytitle = reader.ReadLine();
                    pages = new List<Storypage>();
                    for (int i = 0; i < pageno; i++)
                    {
                        array = reader.ReadLine().Split('|');
                        page = new Storypage(array[0], array[1]);
                        pages.Add(page);
                    }
                    book = new Storybook(storytitle, pages);
                    if (special == 1)
                    {
                        pageno = Convert.ToInt32(reader.ReadLine());
                        for (int j = 0; j < pageno; j++)
                        {
                            line = reader.ReadLine();
                            array = line.Split('|');
                            if (HasBranch(array[1]))
                            {
                                book.AddChoice(array[0], FindBranch(array[1]));
                            }
                        }
                    }
                    tile = builder.FindTileAt(tiles, x, y);
                    if (tile != null)
                    {
                        if (!tile.Blocked)
                        {
                            tile.Storybook = book;
                        }
                    }
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            return tiles;
        }
        public void SetMap(string pref)
        {
            _bofname = "data/" + pref + "/" + _bofname;
            _brfname = "data/" + pref + "/" + _brfname;
        }
    }
}
