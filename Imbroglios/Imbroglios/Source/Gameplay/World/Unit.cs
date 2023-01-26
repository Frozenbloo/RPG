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
    public class Unit : AttackableObject
    {
        protected Vector2 move2;
        protected List<Vector2> moves = new List<Vector2>();

        public Unit(string filePATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(filePATH, POS, DIMS, FRAMES, OWNERID)
        {
            move2 = new Vector2(POS.X, POS.Y);
        }

        public override void Update(Vector2 OFFSET, Character ENEMY, SquareGrid GRID)
        {
            base.Update(OFFSET, ENEMY, GRID);
        }

        /// <summary>
        /// Returns a list of nodes from the current position to the end slot
        /// </summary>
        /// <param name="GRID">The Grid to use</param>
        /// <param name="ENDSLOT">The slot to path to</param>
        /// <returns></returns>
        public virtual List<Vector2> FindPath(SquareGrid GRID, Vector2 ENDSLOT)
        {
            //Clears the list of nodes the ai can move to
            moves.Clear();
            //Gets the current location in the grid
            Vector2 tempStartSlot = GRID.GetSlotFromPixel(position, Vector2.Zero);


            List<Vector2> tempPath = GRID.GetPath(tempStartSlot, ENDSLOT, true);
            if (tempPath == null || tempPath.Count == 0)
            {

            }

            return tempPath;
        }

        /// <summary>
        /// Moves the unit
        /// </summary>
        public virtual void MoveUnit()
        {
            if (position.X != move2.X || position.Y != move2.Y)
            {
                position += Globals.RadialMovement(move2, position, speed);
            }
            else if (moves.Count > 0)
            {
                move2 = moves[0];
                moves.RemoveAt(0);

                position += Globals.RadialMovement(move2, position, speed);
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
