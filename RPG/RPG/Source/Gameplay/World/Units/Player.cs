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
    public class Player : Unit
    {

        public Player(string filePATH, Vector2 POS, Vector2 DIMS, int OWNERID) : base(filePATH, POS, DIMS, OWNERID)
        {
            speed = 2.5f;

            hp = 10;
            hpMax = hp;
        }

        public override void Update(Vector2 OFFSET)
        {
            bool checkScreenScroll = false;

            #region Movement & Input

            if (Globals.keyboard.GetPress("A"))
            {
                position = new Vector2(position.X - speed, position.Y);
                checkScreenScroll = true;
            }

            if (Globals.keyboard.GetPress("D"))
            {
                position = new Vector2(position.X + speed, position.Y);
                checkScreenScroll = true;
            }

            if (Globals.keyboard.GetPress("W"))
            {
                position = new Vector2(position.X, position.Y - speed);
                checkScreenScroll = true;
            }

            if (Globals.keyboard.GetPress("S"))
            {
                position = new Vector2(position.X, position.Y + speed);
                checkScreenScroll = true;
            }

            if (checkScreenScroll)
            {
                GameGlobals.CheckScreenScroll(position);
            }

            if (Globals.mouse.LeftClick())
            {
                GameGlobals.PassProjectile(new Rocket(new Vector2(position.X, position.Y), this, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y) - OFFSET));
            }
            
            #endregion

            //rotation = Globals.RotateTowards(position, new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y));
            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
