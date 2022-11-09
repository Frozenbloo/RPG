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
    public class AttackableObject : Animated2D
    {
        public bool isDead;

        public int ownerId;

        public float speed, hitDistance, hp, hpMax;

        public AttackableObject(string filePATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(filePATH, POS, DIMS, FRAMES, Color.White)
        {
            ownerId = OWNERID;
            isDead = false;
            speed = 2f;

            hp = 1;
            hpMax = hp;

            hitDistance = 35.0f;
        }

        public virtual void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            base.Update(OFFSET);
        }

        public virtual void getHit(float DMG)
        {
            hp -= DMG;
            if (hp <= 0)
            {
                isDead = true;
            }
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
