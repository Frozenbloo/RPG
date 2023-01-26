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

        //Play, Settings and Exit Delegates
        public PassObject PlayClickDel, SettingClickDel, ExitClickDel;

        public List<Button2D> buttons = new List<Button2D>();

        /// <summary>
        /// Creates a new instance of a Main Menu
        /// </summary>
        /// <param name="PLAYCLICKDEL">The delegate for the play button</param>
        /// <param name="SETTINGCLICKDEL">The delegate for the settings button</param>
        /// <param name="EXITCLICKDEL">The delegate for the exit button</param>
        public MainMenu(PassObject PLAYCLICKDEL, PassObject SETTINGCLICKDEL, PassObject EXITCLICKDEL) 
        {
            PlayClickDel = PLAYCLICKDEL;
            ExitClickDel = EXITCLICKDEL;
            SettingClickDel = SETTINGCLICKDEL;

            background = new Animated2D("2D\\Units\\Mobs\\Ghost", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(Globals.screenWidth, Globals.screenHeight), new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), Color.White);

            buttons.Add(new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(128, 32), "Fonts\\Kenney Mini", "Play", PlayClickDel, 1));

            buttons.Add(new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(128, 32), "Fonts\\Kenney Mini", "Settings", SettingClickDel, 2));

            buttons.Add(new Button2D("2D\\Misc\\shade", new Vector2(0, 0), new Vector2(128, 32), "Fonts\\Kenney Mini", "Exit", ExitClickDel, null));
        }

        public virtual void Update()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(new Vector2(80, 780 + 40 * i));
            }
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(new Vector2(80, 780 + 40 * i));
            }
        }
    }
}
