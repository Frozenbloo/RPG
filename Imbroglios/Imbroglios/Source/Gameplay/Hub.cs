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
using System.Security.AccessControl;

#endregion

namespace Imbroglios
{
	public class Hub : World
	{

		PassObject ResetWorld;

		public Hub(PassObject RESETWORLD, PassObject CHANGEGAMESTATE) : base(RESETWORLD, CHANGEGAMESTATE)
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

		public override void Update()
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

		public override void CheckScreenScroll(object INFO)
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

		public override void Draw(Vector2 OFFSET)
		{
			grid.DrawGrid(offset);

			for (int i = 0; i < projectiles.Count; i++)
			{
				projectiles[i].Draw(offset);
			}

			user.Draw(offset);

			//Keep at bottom to draw on top
			ui.Draw(this);
		}
	}
}
