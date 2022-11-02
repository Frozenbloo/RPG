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
    public class World
    {
        public Vector2 offset;

        public UI ui;

        public User user;
        public AICharacter aICharacter;

        public List<Projectile2D> projectiles = new List<Projectile2D>();

        PassObject ResetWorld;

        public World(PassObject RESETWORLD)
        {
            ResetWorld = RESETWORLD;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.CheckScreenScroll = CheckScreenScroll;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;

            user = new User(1);
            aICharacter = new AICharacter(2);

            offset = new Vector2(0, 0);

            ui = new UI();
        }

        public virtual void Update()
        {
            if (!user.player.isDead/**user.player.isDead**/)
            {
                user.Update(aICharacter, offset);
                aICharacter.Update(user, offset);

                for (int i = 0; i < projectiles.Count; i++)
                {
                    projectiles[i].Update(offset, aICharacter.units.ToList<Unit>());
                    if (projectiles[i].projectDespawn)
                    {
                        projectiles.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                if (Globals.keyboard.GetPress("Enter"))
                {
                    ResetWorld(null);
                }
            }
            //Keep at bottom to draw on top
            ui.Update(this);
        }

        public virtual void AddMob(object INFO)
        {
            Unit tempUnit = (Unit)INFO;

            if (user.id == tempUnit.ownerId)
            {
                user.AddUnit(tempUnit);
            }
            else if (aICharacter.id == tempUnit.ownerId)
            {
                aICharacter.AddUnit(tempUnit);
            }

            aICharacter.AddUnit((Mob)INFO);
        }

        public virtual void AddProjectile(object INFO)
        {
            projectiles.Add((Projectile2D)INFO);
        }

        public virtual void AddSpawnPoint(object INFO)
        {
            SpawnPoint tempSpawnPoint = (SpawnPoint)INFO;

            if (user.id == tempSpawnPoint.ownerId)
            {
                user.AddSpawnPoint(tempSpawnPoint);
            }
            else if (aICharacter.id == tempSpawnPoint.ownerId)
            {
                aICharacter.AddSpawnPoint(tempSpawnPoint);
            }
        }

        public virtual void CheckScreenScroll(object INFO)
        {
            Vector2 tempPosition = (Vector2)INFO;

            #region Screen Scrolling
            //This should make it so the middle 20% of the screen is a deadzone, and the rest of the screen makes the camera move, although futher testing is required to double check that it 
            //actually works
            if (tempPosition.X < -offset.X + (Globals.screenWidth * .4f))
            {
                offset = new Vector2(offset.X + user.player.speed, offset.Y);
            }

            if (tempPosition.X > -offset.X + (Globals.screenWidth * .6f))
            {
                offset = new Vector2(offset.X - user.player.speed, offset.Y);
            }

            if (tempPosition.Y < -offset.Y + (Globals.screenHeight * .4f))
            {
                offset = new Vector2(offset.X, offset.Y + user.player.speed);
            }

            if (tempPosition.Y > -offset.Y + (Globals.screenHeight * .6f))
            {
                offset = new Vector2(offset.X, offset.Y - user.player.speed);
            }
            #endregion
        }

        public virtual void Draw(Vector2 OFFSET)
        {

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            aICharacter.Draw(offset);

            user.Draw(offset);

            //Keep at bottom to draw on top
            ui.Draw(this);
        }
    }
}
