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
    public class Gameplay
    {
        int playState;

        World world;

        PassObject ChangeGameState;

        public Gameplay(PassObject changeGameState)
        {
            playState = 0;

            ChangeGameState = changeGameState;
            ResetWorld(null);
        }

        public virtual void Update()
        {
            if (playState == 0)
            {
                world.Update();
            }
        }

        public virtual void ResetWorld(Object INFO)
        {
            world = new World(ResetWorld, ChangeGameState);
        }

        public virtual void Draw()
        {
            if (playState == 0)
            {
                world.Draw(Vector2.Zero);
            }
        }
    }
}
