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

namespace RPG
{
    public class User : Character
    {

        public User(int ID) : base(ID)
        {
            player = new Player("2D\\Player\\player", new Vector2(300, 300), new Vector2(48, 48), id);
        }

        public override void Update(Character ENEMY, Vector2 OFFSET)
        {
            base.Update(ENEMY, OFFSET);
        }
    }
}
