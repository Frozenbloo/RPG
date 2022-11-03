#region Using
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Imbroglios
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;

        public float delta;

        Gameplay gameplay;

        Basic2D cursor;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
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

            gameplay = new Gameplay();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = gameTime;
            Globals.keyboard.Update();
            Globals.mouse.Update();

            gameplay.Update();

            Globals.keyboard.UpdateOld();
            Globals.mouse.UpdateOld();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Slightly less efficient but allows the anti-alias shader to run properly.
            Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            gameplay.Draw();

            cursor.Draw(new Vector2(Globals.mouse.newMousePos.X, Globals.mouse.newMousePos.Y), new Vector2(0, 0), Color.White);
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

