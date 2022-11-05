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
    public class SquareGrid
    {

        public bool showGrid; //For Debugging ONLY

        public Vector2 slotDims, gridDims, physicalStartPos, totalPhysicalDims, currentHoverSlot;

        public Basic2D gridImg;

        public List<List<GridLocation>> slots  = new List<List<GridLocation>>();

        public SquareGrid(Vector2 SLOTDIMS, Vector2 STARTPOS, Vector2 TOTALDIMS)
        {
            showGrid = false;

            slotDims = SLOTDIMS;

            physicalStartPos = new Vector2((int)STARTPOS.X, (int)STARTPOS.Y);
            totalPhysicalDims = new Vector2((int)TOTALDIMS.X, (int)TOTALDIMS.Y);

            //Not on the grid so not hovering over anything
            currentHoverSlot = new Vector2(-1, -1);

            SetBaseGrid();


            //SlotDims/2 draws in middle when the coord is top left corner, slotDims.X/Y - 2 leaves gaps so it isnt one full img
            gridImg = new Basic2D("2D\\Misc\\shade", slotDims / 2, new Vector2(slotDims.X - 2, slotDims.Y - 2));
        }

        public virtual void Update(Vector2 OFFSET)
        {
            currentHoverSlot = GetSlotFromPixel(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), OFFSET);
        }

        public virtual GridLocation GetSlotFromLocation(Vector2 LOCATION)
        {
            if (LOCATION.X >= 0 && LOCATION.Y >= 0 && LOCATION.X < slots.Count && LOCATION.Y < slots[(int)LOCATION.X].Count)
            {
                return slots[(int)LOCATION.X][(int)LOCATION.Y];
            }

            return null;
        }

        public virtual Vector2 GetSlotFromPixel(Vector2 PIXEL, Vector2 OFFSET)
        {
            Vector2 adjustedPos = PIXEL - physicalStartPos - OFFSET;

            Vector2 tempVector = new Vector2(Math.Min(Math.Max(0, (int)(adjustedPos.X / slotDims.X)), slots.Count - 1), Math.Min(Math.Max(0, (int)(adjustedPos.Y / slotDims.Y)), slots[0].Count - 1));

            return tempVector;
        }

        public virtual void SetBaseGrid()
        {
            gridDims = new Vector2((int)(totalPhysicalDims.X / slotDims.X), (int)(totalPhysicalDims.Y / slotDims.Y));

            slots.Clear();
            for (int i = 0; i < gridDims.X; i++)
            {
                //Creates a new collumn
                slots.Add(new List<GridLocation>());

                for (int j = 0; j < gridDims.Y; j++)
                {
                    //Creates a new Row
                    slots[i].Add(new GridLocation(1, false));
                }
            }
        }

        public virtual void DrawGrid(Vector2 OFFSET)
        {
            if (showGrid)
            {
                Vector2 topLeft = GetSlotFromPixel(new Vector2(0, 0), OFFSET);
                Vector2 bottomRight = GetSlotFromPixel(new Vector2(Globals.screenWidth, Globals.screenHeight), OFFSET);

                Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

                for (int i = (int)topLeft.X; i <= bottomRight.X && i < slots.Count; i++)
                {
                    for (int j = (int)topLeft.Y; j <= bottomRight.Y && j < slots[0].Count; j++)
                    {
                        if (currentHoverSlot.X == i && currentHoverSlot.Y == j)
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.Red.ToVector4());
                            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();
                        }
                        else
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
                            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();
                        }

                        gridImg.Draw(OFFSET + physicalStartPos + new Vector2(i * slotDims.X, j * slotDims.Y));
                    }
                }
            }
        }

    }
}
