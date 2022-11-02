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
    public class Projectile2D : Basic2D
    {
        public bool projectDespawn;

        public float speed;

        public Vector2 direction;

        public Unit owner;

        public TBTimer timer;

        public Projectile2D(string filePATH, Vector2 POS, Vector2 DIMS, Unit OWNER, Vector2 TARGET) : base(filePATH, POS, DIMS)
        {
            projectDespawn = false;

            speed = 5.0f;

            owner = OWNER;

            direction = (TARGET - owner.position);
            //Ensures Vector is always of length 1
            direction.Normalize();
            //Ensures the project is only spawned for 1.2 seconds to stop mem overflow
            //1.2 seconds as the timer is in MS

            rotation = Globals.RotateTowards(position, new Vector2(TARGET.X, TARGET.Y));

            timer = new TBTimer(1200);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            position += direction * speed;

            timer.UpdateTimer();
            if (timer.Test())
            {
                projectDespawn = true;
            }

            if (CollisionTest(UNITS))
            {
                projectDespawn = true;
            }
        }

        public virtual bool CollisionTest(List<Unit> UNITS)
        {
            for (int i = 0; i < UNITS.Count; i++)
            {
                if (Globals.GetDistance(position, UNITS[i].position) < UNITS[i].hitDistance)
                {
                    UNITS[i].getHit(1);
                    return true;
                }
            }

            return false;
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }

    }
}
