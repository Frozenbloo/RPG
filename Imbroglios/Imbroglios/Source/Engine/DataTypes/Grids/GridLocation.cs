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
        public bool isFilled, imPassible /**You can't walk through it and it counts as being filled**/ , unPathable /**You can't walk through it but isnt filled**/;
        public float fScore, cost, currentDist;
        public Vector2 parentNode, position;

        public GridLocation(float COST, bool FILLED)
        {
            cost = COST;
            isFilled = FILLED;

            unPathable = false;
            imPassible = false;
        }

        public virtual void SetToFilled(bool IMPASSIBLE)
        {
            //Synonymous 
            isFilled = true;
            imPassible = IMPASSIBLE;
        }
    }
}
