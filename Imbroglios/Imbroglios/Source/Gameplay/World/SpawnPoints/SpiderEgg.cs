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
    public class SpiderEgg : SpawnPoint
    {
        int maxSpawns, totalSpawns;

        public SpiderEgg(Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base("2D\\SpawnPoints\\Egg", POS, DIMS, FRAMES, OWNERID)
        {
            totalSpawns = 0;
            maxSpawns = 3;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {

            base.Update(OFFSET, ENEMY, GRID);
        }

        public override void SpawnMob()
        {
            Mob tempMob = new Spiderling(new Vector2(position.X, position.Y), ownerId);

            if (tempMob != null)
            {
                GameGlobals.PassMob(tempMob);
                totalSpawns++;
                if (totalSpawns >= maxSpawns)
                {
                    isDead = true;
                }
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
