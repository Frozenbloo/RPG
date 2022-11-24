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
    public class Mob : Unit
    {

        public Mob(string filePATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(filePATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2f;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            AI(ENEMY.player, GRID);

            base.Update(OFFSET, ENEMY, GRID);
        }

        public virtual void AI(Player PLAYER, SquareGrid GRID)
        {
            if (moves == null || (moves.Count == 0 && position.X == move2.X && position.Y == move2.Y))
            {
                moves = FindPath(GRID, PLAYER.position);
                move2 = moves[0];
                moves.RemoveAt(0);
            }
            position += Globals.RadialMovement(PLAYER.position, position, speed);

            if (Globals.GetDistance(position, PLAYER.position) < 35)
            {
                PLAYER.getHit(1);
                isDead = true;
            }
            //49:55
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
