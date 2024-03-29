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
    public class Spider : Mob
    {

        public TBTimer spawnTimer;

        public Spider(Vector2 POS, int OWNERID) : base("2D\\Units\\Mobs\\Spider", POS, new Vector2(55,55), new Vector2(1, 1), OWNERID)
        {
            speed = 0.75f;

            hp = 3;
            hpMax = hp;

            spawnTimer = new TBTimer(8000);
            spawnTimer.AddToTimer(4000);
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            spawnTimer.UpdateTimer();
            if (spawnTimer.Test())
            {
                SpawnSpiderEgg();
                spawnTimer.ResetToZero();
            }
            //rotation = Globals.RotateTowards(position, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));
            base.Update(OFFSET, ENEMY, GRID);
        }

        public virtual void SpawnSpiderEgg()
        {
            GameGlobals.PassSpawnPoint(new SpiderEgg(new Vector2(position.X, position.Y), new Vector2(35,35), new Vector2(1, 1), ownerId));
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
