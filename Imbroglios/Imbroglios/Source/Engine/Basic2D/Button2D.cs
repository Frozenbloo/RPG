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
    public class Button2D : Basic2D
    {

        public bool isPressed, isHovered;
        public string text;

        public Color hoverColour;

        public SpriteFont font;

        public object info;

        //Delegate for passing things out
        PassObject ButtonClicked;

        public Button2D(string filePATH, Vector2 POS, Vector2 DIMS, string FONTPATH, string TEXT, PassObject BUTTONCLICKED, object INFO) : base(filePATH, POS, DIMS)
        {
            text = TEXT;
            ButtonClicked = BUTTONCLICKED;
            //If the font is null we don't need to load a font
            if (FONTPATH != "")
            {
                font = Globals.content.Load<SpriteFont>(FONTPATH);
            }

            isPressed = false;
            hoverColour = new Color(200, 230, 255);
        }

        public override void Update(Vector2 OFFSET)
        {
            if (Hover(OFFSET))
            {
                isHovered = true;

                if (Globals.mouse.LeftClick())
                {
                    isHovered = false;
                    isPressed = true;
                }
                else if (Globals.mouse.LeftClickRelease())
                {
                    RunBtnClick();
                }
            }
            else
            {
                isHovered = false;
            }

            if (!Globals.mouse.LeftClick() && ! Globals.mouse.LeftClickHold())
            {
                isPressed = false;
            }

            base.Update(OFFSET);
        }

        public virtual void Reset()
        {
            isPressed = false;
            isHovered = false;
        }

        public virtual void RunBtnClick()
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(info);
            }
            Reset();
        }


        public override void Draw(Vector2 OFFSET)
        {
            Color tempColour = Color.White;
            if (isPressed)
            {
                tempColour = Color.Gray;
            }
            else if (isHovered)
            {
                tempColour = hoverColour;
            }


            base.Draw(OFFSET);

            Vector2 stringDims = font.MeasureString(text);
            Globals.spriteBatch.DrawString(font, text, position + OFFSET + new Vector2(-stringDims.X / 2, -stringDims.Y / 2), Color.Black);
        }
    }
}
