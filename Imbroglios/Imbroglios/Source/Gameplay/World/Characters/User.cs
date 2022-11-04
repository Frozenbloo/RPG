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

#endregion

namespace Imbroglios
{
    public class User : Character
    {

        public User(int ID) : base(ID)
        {
            player = new Player("2D\\Player\\player", new Vector2(Globals.screenWidth / 2, Globals.screenHeight/2), new Vector2(48, 48), new Vector2(1, 1), id);
        }

        public override void Update(Character ENEMY, Vector2 OFFSET)
        {
            base.Update(ENEMY, OFFSET);
        }
    }
}
