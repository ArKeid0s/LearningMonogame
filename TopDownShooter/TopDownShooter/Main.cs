using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TopDownShooter.Source;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Engine.Input;
using TopDownShooter.Source.Gameplay;

namespace TopDownShooter
{
	public class Main : Game
	{
		private GraphicsDeviceManager _graphics;

		private GamePlay _gameplay;

		private Basic2d _cursor;

		public Main()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			Globals.ScreenWidth = 1600;
			Globals.ScreenHeight = 900;

			_graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
			_graphics.PreferredBackBufferHeight = Globals.ScreenHeight;

			_graphics.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			Globals.ContentManager = this.Content;
			Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);

			_cursor = new Basic2d("UI\\cursorArrow", Vector2.Zero, new Vector2(28, 28));

			Globals.KeyboardManager = new KeyboardManager();
			Globals.MouseManager = new MouseManager();

			_gameplay = new GamePlay();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			Globals.GameTime = gameTime;
			Globals.KeyboardManager.Update();
			Globals.MouseManager.Update();


			_gameplay.Update();


			Globals.KeyboardManager.UpdateOld();
			Globals.MouseManager.UpdateOld();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			_gameplay.Draw();

			_cursor.Draw(
				offset: Globals.MouseManager.newMousePos,
				origin: Vector2.Zero,
				color: Color.White);

			Globals.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}