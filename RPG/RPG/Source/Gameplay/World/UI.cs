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
    public class UI
    {
        public SpriteFont font;

        public QuantityDisplayBar hpBar;

        public UI()
        {
            font = Globals.content.Load<SpriteFont>("Fonts\\KenneyPixel");

            hpBar = new QuantityDisplayBar(new Vector2(208, 20), 0, Color.Red);
        }

        public void Update(World World)
        {
            hpBar.Update(World.user.player.hp, World.user.player.hpMax);
        }

        public void Draw(World World)
        {
            string tempStr = "Internal Prototype v0.0.1";
            Vector2 strDimensions = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth/2 - (strDimensions.X/2), Globals.screenHeight - 49), Color.Black);

            hpBar.Draw(new Vector2(20, Globals.screenHeight - 40));

            if (World.user.player.isDead)
            {
                tempStr = "YOU DIED!";
                strDimensions = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - (strDimensions.X / 2), Globals.screenHeight/2), Color.Black);
            }

        }
    }
}
