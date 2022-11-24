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
    public class AICharacter : Character
    {

        public AICharacter(int ID) : base(ID)
        {
            
            spawnPoints.Add(new Nest(new Vector2(50, 50), new Vector2(35, 35), new Vector2(1, 1), id));

            spawnPoints.Add(new Nest(new Vector2(Globals.screenWidth / 2, 50), new Vector2(35, 35), new Vector2(1, 1), id));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(1000);

            spawnPoints.Add(new Nest(new Vector2(Globals.screenWidth - 50, 50), new Vector2(35, 35), new Vector2(1, 1), id));
            spawnPoints[spawnPoints.Count - 1].spawnTimer.AddToTimer(2000);
            
        }

        public override void Update(Character ENEMY, Vector2 OFFSET, SquareGrid GRID)
        {
            base.Update(ENEMY, OFFSET, GRID);
        }
    }
}
