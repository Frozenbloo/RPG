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

namespace Imbroglios
{
    public class Ghost : Mob
    {

        public Ghost(Vector2 POS, int OWNERID) : base("2D\\Units\\Mobs\\Ghost", POS, new Vector2(40,40), new Vector2(1, 1), OWNERID)
        {
            speed = 7.5f;
            hp = 1;
            hpMax = hp;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            //rotation = Globals.RotateTowards(position, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));
            base.Update(OFFSET, ENEMY, GRID);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
