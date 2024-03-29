﻿#region Using
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
    public class Player : Unit
    {
        /// <summary>
        /// Creates a new player type
        /// </summary>
        /// <param name="filePATH">The file path to the texture</param>
        /// <param name="POS">The spawn location</param>
        /// <param name="DIMS">The dimensions</param>
        /// <param name="FRAMES">Animated2D Frames</param>
        /// <param name="OWNERID">The ID of its owner</param>
        public Player(string filePATH, Vector2 POS, Vector2 DIMS, Vector2 FRAMES, int OWNERID) : base(filePATH, POS, DIMS, FRAMES, OWNERID)
        {
            speed = 2.5f;
            hp = 1;
            hpMax = hp;
        }

        public override void Update(Vector2 OFFSET)
        {
            bool checkScreenScroll = false;

            #region Movement & Input

            if (Globals.keyboard.GetPress("A"))
            {
                Vector2 newPosition = new Vector2(position.X - speed, position.Y);
                if ((newPosition.X > -OFFSET.X - (Globals.screenWidth/2)))
                {
                    position = newPosition;
                }
                checkScreenScroll = true;
            }

            if (Globals.keyboard.GetPress("D"))
            {
                Vector2 newPosition = new Vector2(position.X + speed, position.Y);
                if (!(newPosition.X > -OFFSET.X + (Globals.screenWidth)))
                {
                    position= newPosition;
                }
                checkScreenScroll = true;
            }

            if (Globals.keyboard.GetPress("W"))
            {
                Vector2 newPosition = new Vector2(position.X, position.Y - speed);
				if ((newPosition.Y < -OFFSET.Y + (Globals.screenHeight)))
				{
					position = newPosition;
				}
				checkScreenScroll = true;
            }

            if (Globals.keyboard.GetPress("S"))
            {
				Vector2 newPosition = new Vector2(position.X, position.Y + speed);
				if (!(newPosition.Y > -OFFSET.Y + (Globals.screenHeight)))
				{
					position = newPosition;
				}
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
