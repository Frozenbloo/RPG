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
    public class Projectile2D : Basic2D
    {
        public bool projectDespawn;

        public float speed;

        public Vector2 direction;

        public Unit owner;

        public TBTimer despawnTimer;

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

            despawnTimer = new TBTimer(1200);
        }

        public virtual void Update(Vector2 OFFSET, List<Unit> UNITS)
        {
            position += direction * speed;

            despawnTimer.UpdateTimer();
            if (despawnTimer.Test())
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
            Globals.normalEffect.Parameters["xSize"].SetValue((float)myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)dimensions.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)dimensions.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            base.Draw(OFFSET);
        }

    }
}
