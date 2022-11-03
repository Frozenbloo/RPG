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
    public class Basic2D
    {
        public float rotation;

        public Vector2 position, dimensions;

        public Texture2D myModel;

        public Basic2D(string filePATH, Vector2 POS, Vector2 DIMS)
        {
            position = POS;
            dimensions = DIMS;

            myModel = Globals.content.Load<Texture2D>(filePATH);
        }

        public virtual void Update(Vector2 OFFSET)
        {

        }

        public virtual bool Hover(Vector2 OFFSET)
        {
            return HoverIMG(OFFSET);
        }

        //Extremely basic hover function, but is less resource intensive compared to pixel hover checking.
        public virtual bool HoverIMG(Vector2 OFFSET)
        {
            Vector2 mousePos = new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y);

            if (mousePos.X >= (position.X + OFFSET.X)-dimensions.X/2 && mousePos.X <= (position.X + OFFSET.X) - dimensions.X / 2 && mousePos.Y >= (position.Y + OFFSET.Y) - dimensions.Y / 2 && mousePos.Y <= (position.Y + OFFSET.Y) - dimensions.Y / 2)
            {
                return true;
            }

            return false;
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            //Checks to ensure that texture has loaded before drawing to screen
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(position.X + OFFSET.X), (int)(position.Y + OFFSET.Y), (int)dimensions.X, (int)dimensions.Y), null, Color.White, rotation, new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), new SpriteEffects(), 0);
            }
        }

        public virtual void Draw(Vector2 OFFSET, Vector2 ORIGIN, Color COLOUR)
        {
            //Checks to ensure that texture has loaded before drawing to screen
            if (myModel != null)
            {
                Globals.spriteBatch.Draw(myModel, new Rectangle((int)(position.X + OFFSET.X), (int)(position.Y + OFFSET.Y), (int)dimensions.X, (int)dimensions.Y), null, COLOUR, rotation, new Vector2(ORIGIN.X, ORIGIN.Y), new SpriteEffects(), 0);
            }
        }
    }
}
