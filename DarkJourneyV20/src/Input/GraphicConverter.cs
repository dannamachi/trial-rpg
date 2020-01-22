using System;
using System.Collections.Generic;

namespace SEGraphic
{
    public class GraphicConverter
    {
        //fields
        //constructors
        public GraphicConverter()
        {

        }
        //properties
        //methods
        public Button GetButton(MonoButton mono)
        {
            return MonoButton.GetButton(mono);
        }
        public List<MonoButton> GetMonos(List<Button> bus)
        {
            List<MonoButton> monos = new List<MonoButton>();
            foreach (Button bu in bus)
            {
                monos.Add(bu.GetMono());
            }
            return monos;
        }
    }
}
