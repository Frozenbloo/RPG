#region Using
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
    public class SpawnPoint : AttackableObject
    {
        public TBTimer spawnTimer = new TBTimer(5000);

        public SpawnPoint(string filePATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(filePATH, POS, DIMS, FRAMES, OWNERID)
        {
            isDead = false;

            hp = 3;
            hpMax = hp;

            hitDistance = 35.0f;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            spawnTimer.UpdateTimer();
            if (spawnTimer.Test())
            {
                SpawnMob();
                spawnTimer.ResetToZero();
            }

            base.Update(OFFSET, ENEMY, GRID);
        }

        public virtual void SpawnMob()
        {
            GameGlobals.PassMob(new Skeleton(new Vector2(position.X, position.Y), ownerId));
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
