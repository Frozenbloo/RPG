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
    public class Nest : SpawnPoint
    {


        public Nest(Vector2 POS, Vector2 DIMS, int OWNERID) : base("2D\\SpawnPoints\\Nest", POS, DIMS, OWNERID)
        {

        }

        public override void Update(Vector2 OFFSET)
        {
            base.Update(OFFSET);
        }


        //Flesh out more to add more variation to enemy spawning depending on the area in the maze
        public override void SpawnMob()
        {
            int randNum = Globals.random.Next(0, 10 + 1);
            Mob tempMob = null;
            if (randNum < 5)
            {
                tempMob = new Skeleton(new Vector2(position.X, position.Y), ownerId);
            }
            else if(randNum < 7)
            {
                tempMob = new Spider(new Vector2(position.X, position.Y), ownerId);
            }
            else if (randNum < 8)
            {
                tempMob = new Ghost(new Vector2(position.X, position.Y), ownerId);
            }
            if (tempMob != null)
            {
                GameGlobals.PassMob(tempMob);
            }
        }

        public override void Draw(Vector2 OFFSET)
        {
            base.Draw(OFFSET);
        }
    }
}
