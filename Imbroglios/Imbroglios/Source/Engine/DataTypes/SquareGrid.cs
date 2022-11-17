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

        public virtual Vector2 GetPosFromLoc(Vector2 LOCATION)
        {
            return physicalStartPos + new Vector2((int)LOCATION.X * slotDims.X, (int)LOCATION.Y * slotDims.Y);
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

        /**
        #region A* (an A isnt good enough)

        public List<Vector2> GetPath(Vector2 START, Vector2 END, bool ALLOWDIAG)
        {
            List<GridLocation> viewable = new List<GridLocation>(), user = new List<GridLocation>();

            List<List<GridLocation>> masterGrid = new List<List<GridLocation>>();

            bool impassable = false;
            float cost = 1;
            for (int i = 0; i < slots.Count; i++)
            {
                masterGrid.Add (new List<GridLocation>());
                for (int j = 0; j < slots[i].Count; j++)
                {
                    impassable = slots[i][j].imPassable;

                    if (slots[i][j].imPassable || slots[i][j].isFilled)
                    {
                        impassable = true;
                    }

                    cost = slots[i][j].cost;

                    masterGrid[i].Add(new GridLocation(new Vector2(i, j), cost, impassable, 999999999));
                }
            }

            viewable.Add(masterGrid[(int)START.X][(int)START.Y]);

            while (viewable.Count > 0 && !(viewable[0].position.X == END.X && viewable[0].position.Y == END.Y))
            {
                TestAStarNode(masterGrid, viewable, used, END, ALLOWDIAG);
            }

            List<Vector2> path = new List<Vector2>();

            if (viewable.Count > 0)
            {
                int currentViewableStart = 0;
                GridLocation currentNode = viewable[currentViewableStart];

                path.Clear();
                Vector2 tempPos;

                while (true)
                {
                    tempPos = GetPosFromLoc(currentNode.position) + slotDims / 2;
                    path.Add(new Vector2(tempPos.X, tempPos.Y));

                    if (currentNode.position == START)
                    {
                        break;
                    }
                    else
                    {
                        if ((int)currentNode.parentNode.X != -1 && (int)currentNode.parentNode.Y != -1)
                        {
                            if (currentNode.position.X == masterGrid[(int)currentNode.parentNode.X][(int)currentNode.parentNode.Y].position.X && currentNode.position.Y == masterGrid[(int)currentNode.parentNode.X][(int)currentNode.parentNode.Y].position.Y)
                            {
                                //Curent node points to itself
                                currentNode = viewable[currentViewableStart];
                                currentViewableStart++;
                            }

                            currentNode = masterGrid[(int)currentNode.parentNode.X][(int)currentNode.parentNode.Y];
                        }
                        else
                        {
                            //Node isnt on grid
                            currentNode = viewable[currentViewableStart];
                            currentViewableStart++;
                        }
                    }
                }
                path.Reverse();
            }
            return path;
        }

        public void TestAStarNode(List<List<GridLocation>> MASTERGRID, List<GridLocation> VIEWABLE, List<GridLocation> USED, Vector2 END, bool ALLOWDIAG)
        {
            GridLocation currentNode;
            bool up = true, down = true, left = true, right = true;

            //Up
            if (VIEWABLE[0].position.Y > 0 && VIEWABLE[0].position.Y < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X][(int)VIEWABLE[0].position.Y - 1].imPassable)
            {
                currentNode = MASTERGRID[(int)VIEWABLE[0].position.X][(int)VIEWABLE[0].position.Y - 1];
                up = currentNode.imPassable;
                SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, 1);
            }

            //14:34 finish coding time
        }

        #endregion
        **/

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
                        else if (slots[i][j].isFilled)
                        {
                            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.DarkGray.ToVector4());
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
