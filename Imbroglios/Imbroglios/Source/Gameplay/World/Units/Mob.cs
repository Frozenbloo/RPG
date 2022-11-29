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
        public bool isPathing;

        private TBTimer repathTimer = new TBTimer(100);

        public Mob(string filePATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(filePATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2f;
            isPathing= false;
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            AI(ENEMY.player, GRID);

            base.Update(OFFSET, ENEMY, GRID);
        }

        public virtual void AI(Player PLAYER, SquareGrid GRID)
        {
            repathTimer.UpdateTimer();
            //If there are no moves in the list OR if it is 100ms after last path, get another path
            if (moves == null || (moves.Count == 0 && position.X == move2.X && position.Y == move2.Y) || repathTimer.Test())
            {
                if (!isPathing)
                {
                    //Runs the repath task off the main thread by threading to improve performance
                    Task repathTask = new Task(() =>
                    {
						isPathing = true;
						moves = FindPath(GRID, GRID.GetSlotFromPixel(PLAYER.position, Vector2.Zero));
                        //TODO Add checking to see if the enemy is at the grid boundary
						move2 = moves[0];
						moves.RemoveAt(0);

						repathTimer.ResetToZero();
						isPathing = false;
					});
                    repathTask.Start();
				}
                
            }
            else //move to the next node
            {
                MoveUnit();
            }

            if (Globals.GetDistance(position, PLAYER.position) < GRID.slotDims.X * 1.2f) //check to see if the player is in position
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
