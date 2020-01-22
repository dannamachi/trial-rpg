using System;
using System.Collections.Generic;
using System.Linq;
using SEVirtual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DarkJourneyV20;

namespace SEGraphic
{
    public class UserAgent
    {
        //fields
        private Player _p;
        private GraphicManager _gm;
        private PlayerInput _input;
        private GraphicConverter _gcon;
        //constructors
        public UserAgent(Player p, GraphicManager gm)
        {
            _p = p;
            _gm = gm;
            _gcon = new GraphicConverter();
        }
        //properties
        public static MouseState PrevMouse { get; set; }
        public static MouseState CurrMouse { get; set; }
        public PlayerInput Input { get => _input; }
        //methods
        public void PassInput(GameCP gcp)
        {
            if (_input != null)
                gcp.PerformAction(_input);
        }
        private PlayerInput GetInput(MonoButton mono)
        {
            PlayerInput inp = new PlayerInput();
            inp.AddButton(_gcon.GetButton(mono));
            return inp;
        }
        public void CheckInput(GameCP gcp)
        {
            _input = null;
            UpdateMouse(CurrMouse.X, CurrMouse.Y);
            GraphicObject pressed;
            if (CurrMouse.LeftButton == ButtonState.Pressed && !(PrevMouse.LeftButton == ButtonState.Pressed))
                pressed = ButtonPressed(ZonePressed(gcp));
            else
                pressed = null;
            List<MonoButton> monos = _gcon.GetMonos(gcp.GetButtons());
            foreach (MonoButton mono in monos)
            {
                if (mono.IsPressed(pressed))
                {
                    _input = GetInput(mono);
                }
            }
        }
        public GraphicObject ButtonPressed(Zone zn)
        {
            int pri = -1;
            GraphicObject chosen = zn;
            if (zn == null) return null;
            foreach (GraphicObject go in zn.Objects)
            {
                if (go.IsPressed((int)_gm.Mouse.X, (int)_gm.Mouse.Y))
                {
                    if (go.Priority > pri)
                    {
                        chosen = go;
                        pri = go.Priority;
                    }
                }
            }
            return chosen;
        }
        public Zone ZonePressed(GameCP gcp)
        {
            List<Zone> sorted = _gm.SortZone(_gm.GetZones(gcp, _p));
            Zone zn;
            for (int i = sorted.Count - 1; i >= 0; i--)
            {
                zn = sorted[i];
                if (zn.IsPressed((int)_gm.Mouse.X, (int)_gm.Mouse.Y)) return zn;
            }
            return null;
        }
        public void UpdateMouse(float x, float y)
        {
            _gm.Mouse = new Vector2(x, y);
        }
    }
}
