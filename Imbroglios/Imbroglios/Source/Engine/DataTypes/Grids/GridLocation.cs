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
    public class GridLocation
    {
        public bool isFilled, imPassable /**You can't walk through it and it counts as being filled**/ , unPathable /**You can't walk through it but isnt filled**/, beenUsed, isViewable;
        public float fScore, cost, currentDist;
        public Vector2 parentNode, position;

        public GridLocation(float COST, bool FILLED)
        {
            cost = COST;
            isFilled = FILLED;

            unPathable = false;
            imPassable = false;
            beenUsed = false;
            imPassable = false;
        }

        public GridLocation(Vector2 POS, float COST, bool FILLED, float FSCORE)
        {
            cost = COST;
            isFilled = FILLED;
            imPassable = FILLED;
            unPathable = false;
            beenUsed = false;
            isViewable = false;

            position = POS;

            fScore = FSCORE;
        }

        public void SetNode(Vector2 PARENT, float FSCORE, float CURRENTDIST)
        {
            parentNode = PARENT;
            fScore = FSCORE;
            currentDist = CURRENTDIST;
        }

        public virtual void SetToFilled(bool IMPASSABLE)
        {
            //Synonymous 
            isFilled = true;
            imPassable = IMPASSABLE;
        }
    }
}
