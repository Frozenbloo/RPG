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
    public class Unit : AttackableObject
    {

        public Unit(string filePATH, Vector2 POS, Vector2 DIMS, int OWNERID) : base(filePATH, POS, DIMS, OWNERID)
        {

        }

        public override void Update(Vector2 OFFSET, Character ENEMY)
        {
            base.Update(OFFSET);
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
