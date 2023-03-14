using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootingGallery
{
	public class Main : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private Texture2D _targetSprite;
		private Texture2D _crosshairsSprite;
		private Texture2D _backgroundSprite;

		private SpriteFont _font;
		private Vector2 _targetPosition = new(300, 300);
		private const int _targetRadius = 45;

		private MouseState _mouseState;
		bool _isMouseReleased = true;
		private int _score;

		public Main()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_targetSprite = Content.Load<Texture2D>("target");
			_crosshairsSprite = Content.Load<Texture2D>("crosshairs");
			_backgroundSprite = Content.Load<Texture2D>("sky");
			_font = Content.Load<SpriteFont>("galleryFont");
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			_mouseState = Mouse.GetState();

			if (_mouseState.LeftButton == ButtonState.Pressed && _isMouseReleased)
			{
				float mouseTargetDist = Vector2.Distance(_targetPosition, _mouseState.Position.ToVector2());
				if (mouseTargetDist < _targetRadius)
				{
					_score++;

					Random rand = new();

					_targetPosition.X = rand.Next(_targetRadius, _graphics.PreferredBackBufferWidth - _targetRadius);
					_targetPosition.Y = rand.Next(_targetRadius, _graphics.PreferredBackBufferHeight - _targetRadius);
				}
				_isMouseReleased = false;
			}

			if (_mouseState.LeftButton == ButtonState.Released)
			{
				_isMouseReleased = true;
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			_spriteBatch.Draw(_backgroundSprite, new Rectangle(0, 0, 800, 480), Color.White);
			_spriteBatch.Draw(_targetSprite, new Vector2(_targetPosition.X - _targetRadius, _targetPosition.Y - _targetRadius), Color.White);
			_spriteBatch.DrawString(_font, $"Current score: {_score}", new Vector2(10, 10), Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}