﻿#region Using
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
using System.Security.AccessControl;

#endregion

namespace Imbroglios
{
    public class World
    {
        public Vector2 offset;

        public UI ui;

        public User user;
        public AICharacter aICharacter;

        public SquareGrid grid;

        public List<Projectile2D> projectiles = new List<Projectile2D>();

        public RecursiveBacktrack maze;

        PassObject ResetWorld;

        public World(PassObject RESETWORLD, PassObject CHANGEGAMESTATE)
        {
            ResetWorld = RESETWORLD;

            GameGlobals.PassProjectile = AddProjectile;
            GameGlobals.PassMob = AddMob;
            GameGlobals.CheckScreenScroll = CheckScreenScroll;
            GameGlobals.PassSpawnPoint = AddSpawnPoint;

            GameGlobals.isPaused = false;

            user = new User(1);
            aICharacter = new AICharacter(2);

            offset = new Vector2(0, 0);

            grid = new SquareGrid(new Vector2(25, 25), new Vector2(-500, -500), new Vector2(Globals.screenWidth + 2000, Globals.screenHeight + 2000));

            maze = new RecursiveBacktrack(grid, 50, 50);

            ui = new UI(ResetWorld, CHANGEGAMESTATE);
		}

        public virtual void Update()
        {
            if (!user.player.isDead && !GameGlobals.isPaused)
            {
                user.Update(aICharacter, offset, grid);
                aICharacter.Update(user, offset, grid);

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

            if (Globals.keyboard.GetSinglePress("Escape"))
            {
                GameGlobals.isPaused = !GameGlobals.isPaused;
            }

            if (Globals.keyboard.GetSinglePress("G"))
            {
                grid.showGrid = !grid.showGrid;
            }

            if (grid != null)
            {
                grid.Update(offset);
            }

            maze.Update(offset);

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
            grid.DrawGrid(offset);

            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            aICharacter.Draw(offset);

            user.Draw(offset);

            maze.Draw(offset);

            //Keep at bottom to draw on top
            ui.Draw(this);
        }
    }
}
