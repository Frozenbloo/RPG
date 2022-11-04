#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text.RegularExpressions;

#endregion

namespace Imbroglios { 
    public class MainMenu
    {
        public Basic2D background;

        //Play and Exit Delegates
        public PassObject PlayClickDel, ExitClickDel;

        public List<Button2D> buttons = new List<Button2D>();

        public MainMenu(PassObject PLAYCLICKDEL, PassObject EXITCLICKDEL) 
        {
            PlayClickDel = PLAYCLICKDEL;
            ExitClickDel = EXITCLICKDEL;

            background = new Basic2D("2D\\Units\\Mobs\\Ghost", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight));

            buttons.Add(new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\KenneyPixel", "Play", PlayClickDel, 1));

            //Settings Button Later

            buttons.Add(new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\KenneyPixel", "Exit", ExitClickDel, null));
        }

        public virtual void Update()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(new Vector2(80, 800 + 60 * i));
            }
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(new Vector2(80, 800 + 60 * i));
            }
        }
    }
}
