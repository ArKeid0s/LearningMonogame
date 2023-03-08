using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine
{
	public class MainGame : Game
	{
		private GraphicsDeviceManager _graphics;
		public SpriteBatch SpriteBatch;

		private readonly GameState _gameState;
		public GameState GameState => _gameState;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_gameState = new GameState(this);
		}

		protected override void Initialize()
		{
			_gameState.ChangeScene(GameState.SceneType.Menu);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			AssetsManager.Load(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			_gameState.CurrentScene?.Update(gameTime);

            base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			SpriteBatch.Begin();
			_gameState.CurrentScene?.Draw(gameTime);
			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}