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
    public class UI
    {
        public SpriteFont font;

        public Button2D resetBtn, menuBtn;

        public QuantityDisplayBar hpBar, xpBar;

        public UI(PassObject RESET, PassObject CHANGEGAMESTATE)
        {
            font = Globals.content.Load<SpriteFont>("Fonts\\Kenney Mini");

            resetBtn = new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\Kenney Mini", "Reset", RESET, null);
            menuBtn = new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(220, 32), "Fonts\\Kenney Mini", "Quit to Main Menu", CHANGEGAMESTATE, 0);

            hpBar = new QuantityDisplayBar(new Vector2(208, 20), 0, Color.Red);
            xpBar = new QuantityDisplayBar(new Vector2(208, 7.5f), 0, Color.GreenYellow);
        }

        public void Update(World World)
        {
            hpBar.Update(World.user.player.hp, World.user.player.hpMax);
            if (World.user.player.isDead)
            {
                resetBtn.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
            }

            if (GameGlobals.isPaused)
            {
                menuBtn.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
            }
        }

        public void Draw(World World)
        {
            //Greater than .6 and .4 so doesnt enter loop to ensure text doesnt become wonky.
            Globals.normalEffect.Parameters["xSize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["ySize"].SetValue(1.0f);
            Globals.normalEffect.Parameters["xDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["yDraw"].SetValue(1.0f);
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            string tempStr = "Internal Prototype v0.0.1";
            Vector2 strDimensions = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth/2 - (strDimensions.X/2), Globals.screenHeight - 49), Color.Black);

            if (World.user.player.isDead)
            {
                tempStr = "YOU DIED!";
                strDimensions = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - (strDimensions.X / 2), Globals.screenHeight/2), Color.Black);

                resetBtn.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight/2 + 100));
            }

            if (GameGlobals.isPaused && !World.user.player.isDead)
            {
                tempStr = "Game Paused";
                strDimensions = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - (strDimensions.X / 2), Globals.screenHeight / 2), Color.Black);
                menuBtn.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 100));
            }

            hpBar.Draw(new Vector2(20, Globals.screenHeight - 40));
            xpBar.Draw(new Vector2(20, Globals.screenHeight - 45));
        }
    }
}
