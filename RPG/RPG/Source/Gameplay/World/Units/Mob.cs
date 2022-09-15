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
    public class Mob : Unit
    {

        public Mob(string filePATH, Vector2 POS, Vector2 DIMS, int OWNERID) : base(filePATH, POS, DIMS, OWNERID)
        {
            speed = 2f;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY)
        {
            AI(ENEMY.player);

            base.Update(OFFSET);
        }

        public virtual void AI(Player PLAYER)
        {
            position += Globals.RadialMovement(PLAYER.position, position, speed);


            if (Globals.GetDistance(position, PLAYER.position) < 35)
            {
                PLAYER.getHit(1);
                isDead = true;
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
