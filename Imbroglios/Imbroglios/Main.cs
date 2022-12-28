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
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;

        public float delta;

        Gameplay gameplay;
        Settings settings;
        MainMenu menu;

        Basic2D cursor;

        bool lockUpdate;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            lockUpdate = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Globals.screenWidth = 1600; //800  1600
            Globals.screenHeight = 900; //500  900

            graphics.PreferredBackBufferWidth = Globals.screenWidth;
            graphics.PreferredBackBufferHeight = Globals.screenHeight;

            /** To add a fullscreen option later on in development **/
            //graphics.IsFullScreen = true;

            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            cursor = new Basic2D("2D\\Misc\\cursorHand_grey", new Vector2(0, 0), new Vector2(28, 28));

            Globals.normalEffect = Globals.content.Load<Effect>("Effects\\NormalFlat");

            Globals.keyboard = new TBKeyboard();
            Globals.mouse = new TBMouseControl();
            menu = new MainMenu(ChangeGameState, ChangeGameState, ExitGame);
            settings = new Settings(ChangeGameState);
            gameplay = new Gameplay(ChangeGameState);
        }

        protected override void Update(GameTime gameTime)
        {
            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            lockUpdate = false;
            for (int i = 0; i < Globals.messageList.Count; i++)
            {
                Globals.messageList[i].Update();
                if (!Globals.messageList[i].canRemove)
                {
					lockUpdate = Globals.messageList[i].lockScreen;
				}
                else
                {
                    Globals.messageList.RemoveAt(i);
                    i--;
                }
            }

            if (!lockUpdate)
            {
				if (Globals.gameState == 0)
				{
					menu.Update();
				}
				else if (Globals.gameState == 1)
				{
					gameplay.Update();
				}
				else if (Globals.gameState == 2)
				{
					settings.Update();
				}
			}

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();
            base.Update(gameTime);
        }

        //Changes the current gamestate
        public virtual void ChangeGameState(object INFO)
        {
            Globals.gameState = Convert.ToInt32(INFO);
        }

        //Closes the Game, Also calls the dispose function and runs on every version
        public virtual void ExitGame(object INFO)
        {
            System.Environment.Exit(0);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Slightly less efficient but allows the anti-alias shader to run properly.
            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp);

            if (Globals.gameState == 0)
            {
                menu.Draw();
            }
            else if (Globals.gameState == 1)
            {
                gameplay.Draw();
            }
            else if (Globals.gameState == 2)
            {
                settings.Draw();
            }

            Globals.normalEffect.Parameters["xSize"].SetValue((float)cursor.myModel.Bounds.Width);
            Globals.normalEffect.Parameters["ySize"].SetValue((float)cursor.myModel.Bounds.Height);
            Globals.normalEffect.Parameters["xDraw"].SetValue((float)((int)cursor.dimensions.X));
            Globals.normalEffect.Parameters["yDraw"].SetValue((float)((int)cursor.dimensions.Y));
            Globals.normalEffect.Parameters["filterColor"].SetValue(Color.White.ToVector4());
            Globals.normalEffect.CurrentTechnique.Passes[0].Apply();

            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);

            for (int i = 0; i < Globals.messageList.Count; i++)
            {
                Globals.messageList[i].Draw();
            }

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }


    public static class Program
    {
        static void Main()
        {
            using var game = new Main();
            game.Run();
        }
    }
}

