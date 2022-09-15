﻿#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace RPG
{
    public class QuantityDisplayBar
    {
        public int border;

        public Basic2D bar, barBKG;

        public Color colour;

        public QuantityDisplayBar(Vector2 DIMS, int BORDER, Color COLOUR)
        {
            border = BORDER;
            colour = COLOUR;

            bar = new Basic2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(DIMS.X, DIMS.Y));
            barBKG = new Basic2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(DIMS.X, DIMS.Y));
        }

        public virtual void Update(float CURRENT, float MAX)
        {
            bar.dimensions = new Vector2(CURRENT/MAX*(barBKG.dimensions.X-border*2), bar.dimensions.Y);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            barBKG.Draw(OFFSET, new Vector2(0, 0), Color.Black);
            bar.Draw(OFFSET + new Vector2(border, border), new Vector2(0, 0), colour);
        }
    }
}