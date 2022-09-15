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

namespace RPG
{
    public class AttackableObject : Basic2D
    {
        public bool isDead;

        public int ownerId;

        public float speed, hitDistance, hp, hpMax;

        public AttackableObject(string filePATH, Vector2 POS, Vector2 DIMS, int OWNERID) : base(filePATH, POS, DIMS)
        {
            ownerId = OWNERID;
            isDead = false;
            speed = 2f;

            hp = 1;
            hpMax = hp;

            hitDistance = 35.0f;
        }

        public virtual void Update(Vector2 OFFSET, Character ENEMY)
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
            base.Draw(OFFSET);
        }
    }
}
