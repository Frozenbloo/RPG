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

        
        #region A* (an A isnt good enough)

        public List<Vector2> GetPath(Vector2 START, Vector2 END, bool ALLOWDIAG)
        {
            List<GridLocation> viewable = new List<GridLocation>(), used = new List<GridLocation>();

            List<List<GridLocation>> masterGrid = new List<List<GridLocation>>();

            bool impassable = false;
            float cost = 1;
            for (int i = 0; i < slots.Count; i++)
            {
                //Adds the grids to the mastergrid
                masterGrid.Add (new List<GridLocation>());
                for (int j = 0; j < slots[i].Count; j++)
                {
                    //Sets if the slots are passable or not
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
                if (path.Count > 1)
                {
                    path.RemoveAt(0);
                }
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
            //Down
            if (VIEWABLE[0].position.Y >= 0 && VIEWABLE[0].position.Y + 1 < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X][(int)VIEWABLE[0].position.Y + 1].imPassable)
            {
                currentNode = MASTERGRID[(int)VIEWABLE[0].position.X][(int)VIEWABLE[0].position.Y + 1];
                down = currentNode.imPassable;
                SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, 1);
            }
            //Left
            if (VIEWABLE[0].position.X > 0 && VIEWABLE[0].position.X < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X - 1][(int)VIEWABLE[0].position.Y].imPassable)
            {
                currentNode = MASTERGRID[(int)VIEWABLE[0].position.X - 1][(int)VIEWABLE[0].position.Y];
                left = currentNode.imPassable;
                SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, 1);
            }
            //Right
            if (VIEWABLE[0].position.X >= 0 && VIEWABLE[0].position.X < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X + 1][(int)VIEWABLE[0].position.Y].imPassable)
            {
                currentNode = MASTERGRID[(int)VIEWABLE[0].position.X + 1][(int)VIEWABLE[0].position.Y];
                right = currentNode.imPassable;
                SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, 1);
            }

            if (ALLOWDIAG)
            {
				// Up and Right
				if (VIEWABLE[0].position.X >= 0 && VIEWABLE[0].position.X + 1 < MASTERGRID.Count && VIEWABLE[0].position.Y > 0 && VIEWABLE[0].position.Y < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X + 1][(int)VIEWABLE[0].position.Y - 1].imPassable && (!up || !right))
				{
					currentNode = MASTERGRID[(int)VIEWABLE[0].position.X + 1][(int)VIEWABLE[0].position.Y - 1];

					SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, (float)Math.Sqrt(2));
				}

				//Down and Right
				if (VIEWABLE[0].position.X >= 0 && VIEWABLE[0].position.X + 1 < MASTERGRID.Count && VIEWABLE[0].position.Y >= 0 && VIEWABLE[0].position.Y + 1 < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X + 1][(int)VIEWABLE[0].position.Y + 1].imPassable && (!down || !right))
				{
					currentNode = MASTERGRID[(int)VIEWABLE[0].position.X + 1][(int)VIEWABLE[0].position.Y + 1];

					SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, (float)Math.Sqrt(2));
				}

				//Down and Left
				if (VIEWABLE[0].position.X > 0 && VIEWABLE[0].position.X < MASTERGRID.Count && VIEWABLE[0].position.Y >= 0 && VIEWABLE[0].position.Y + 1 < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X - 1][(int)VIEWABLE[0].position.Y + 1].imPassable && (!down || !left))
				{
					currentNode = MASTERGRID[(int)VIEWABLE[0].position.X - 1][(int)VIEWABLE[0].position.Y + 1];

					SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, (float)Math.Sqrt(2));
				}

				// Up and Left
				if (VIEWABLE[0].position.X > 0 && VIEWABLE[0].position.X < MASTERGRID.Count && VIEWABLE[0].position.Y > 0 && VIEWABLE[0].position.Y < MASTERGRID[0].Count && !MASTERGRID[(int)VIEWABLE[0].position.X - 1][(int)VIEWABLE[0].position.Y - 1].imPassable && (!up || !left))
				{
					currentNode = MASTERGRID[(int)VIEWABLE[0].position.X - 1][(int)VIEWABLE[0].position.Y - 1];

					SetAStarNode(VIEWABLE, USED, currentNode, new Vector2(VIEWABLE[0].position.X, VIEWABLE[0].position.Y), VIEWABLE[0].currentDist, END, (float)Math.Sqrt(2));
				}
			}
			VIEWABLE[0].beenUsed = true;
			USED.Add(VIEWABLE[0]);
			VIEWABLE.RemoveAt(0);



			// sort
			/*viewable.Sort(delegate(AStarNode n1, AStarNode n2)
            {
                return n1.FScore.CompareTo(n2.FScore);
            });*/
		}

		public void SetAStarNode(List<GridLocation> viewable, List<GridLocation> used, GridLocation nextNode, Vector2 nextParent, float d, Vector2 target, float DISTMULT)
		{
			float f = d;
			float addedDist = (nextNode.cost * DISTMULT);
			//Add item
			if (!nextNode.isViewable && !nextNode.beenUsed)
			{
				//viewable.Add(new AStarNode(nextParent, f, new Vector2(nextNode.Pos.X, nextNode.Pos.Y), nextNode.CurrentDist + 1, nextNode.Cost, nextNode.Impassable));

				nextNode.SetNode(nextParent, f, d + addedDist);
				nextNode.isViewable = true;

				SetAStarNodeInsert(viewable, nextNode);
			}
			//Node is in viewable, so check if Fscore needs revised
			else if (nextNode.isViewable)
			{
				if (f < nextNode.fScore)
				{
					nextNode.SetNode(nextParent, f, d + addedDist);
				}
			}
		}

		public virtual void SetAStarNodeInsert(List<GridLocation> LIST, GridLocation NEWNODE)
		{
			bool added = false;
			for (int i = 0; i < LIST.Count; i++)
			{
				if (LIST[i].fScore > NEWNODE.fScore)
				{
					//Cant insert at 0, because that would take up the looking at node...
					LIST.Insert(Math.Max(1, i), NEWNODE);
					added = true;
					break;
				}
			}

			if (!added)
			{
				LIST.Add(NEWNODE);
			}
		}

		#endregion



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
