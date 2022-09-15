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
    public class Character
    {
        public int id;
        public Player player;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Character(int ID)
        {
            id = ID;
        }

        public virtual void Update(Character ENEMY, Vector2 OFFSET)
        {
            if (player != null)
            {
                player.Update(OFFSET);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Update(OFFSET);
                if (spawnPoints[i].isDead)
                {
                    spawnPoints.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Update(OFFSET, ENEMY);
                if (units[i].isDead)
                {
                    units.RemoveAt(i);
                    i--;
                }
            }

        }

        public virtual void AddUnit(object INFO)
        {
            units.Add((Unit)INFO);
        }

        public virtual void AddSpawnPoint(object INFO)
        {
            spawnPoints.Add((SpawnPoint)INFO);
        }

        public virtual void Draw(Vector2 OFFSET)
        {
            if (player != null)
            {
                player.Draw(OFFSET);
            }

            for (int i = 0; i < units.Count; i++)
            {
                units[i].Draw(OFFSET);
            }

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].Draw(OFFSET);
            }
        }
    }
}
