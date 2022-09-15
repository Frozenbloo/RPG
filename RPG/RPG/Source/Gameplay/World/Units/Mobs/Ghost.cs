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
    public class Ghost : Mob
    {

        public Ghost(Vector2 POS, int OWNERID) : base("2D\\Units\\Mobs\\Ghost", POS, new Vector2(40,40), OWNERID)
        {
            speed = 7.5f;
            hp = 1;
            hpMax = hp;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY)
        {
            //rotation = Globals.RotateTowards(position, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));
            base.Update(OFFSET, ENEMY);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}