using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace LunarLander
{
	public class MainGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private KeyboardState _oldKeyboardState;
		private KeyboardState _newKeyboardState;

		private Lander _player;

		private SoundEffect _sfx;
		private SoundEffectInstance _sfxInst;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_player = new Lander
			{
				position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2)
			};

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			_player.img = Content.Load<Texture2D>("ship");
			_player.imgEngine = Content.Load<Texture2D>("engine");
			Song music = Content.Load<Song>("cool");
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(music);
			MediaPlayer.Volume = 0.2f;

			_sfx = Content.Load<SoundEffect>("sfx_movement_jump13");
			_sfxInst = _sfx.CreateInstance();
			_sfxInst.Volume = 0.1f;
		}

		protected override void Update(GameTime gameTime)
		{
			_newKeyboardState = Keyboard.GetState();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

            if (_newKeyboardState.IsKeyDown(Keys.Right))
            {
                _player.angle += 2f;
            }

            if (_newKeyboardState.IsKeyDown(Keys.Left))
            {
                _player.angle -= 2f;
            }

			if (_newKeyboardState.IsKeyDown(Keys.Up))
            {
                _player.isEngineOn = true;

				float angle = MathHelper.ToRadians(_player.angle);
				float xForce = (float)Math.Cos(angle);
				float yForce = (float)Math.Sin(angle);

				_player.velocity += new Vector2(xForce, yForce) * _player.speed;

				if (_sfxInst.State != SoundState.Playing)
					_sfxInst.Play();
            }
            else
            {
                _player.isEngineOn = false;
            }

			_player.Update();

            if (_player.position.X < 0)
            {
                _player.position = new Vector2(0, _player.position.Y);
				_player.velocity = new Vector2(0 - _player.velocity.X, _player.velocity.Y);
            }
            if (_player.position.X >= GraphicsDevice.Viewport.Width)
            {
                _player.position = new Vector2(GraphicsDevice.Viewport.Width, _player.position.Y);
				_player.velocity = new Vector2(0 - _player.velocity.X, _player.velocity.Y);
            }
            if (_player.position.Y < 0)
            {
                _player.position = new Vector2(_player.position.X, 0);
				_player.velocity = new Vector2(_player.velocity.X, 0 - _player.velocity.Y);
            }
            if (_player.position.Y >= GraphicsDevice.Viewport.Height)
            {
				_player.position = new Vector2(_player.position.X, GraphicsDevice.Viewport.Height);
				_player.velocity = new Vector2(_player.velocity.X, 0 - _player.velocity.Y);
			}

            _oldKeyboardState = _newKeyboardState;
            base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.DarkGray);

			_spriteBatch.Begin();

			if (_player.isEngineOn)
				_spriteBatch.Draw(_player.imgEngine, _player.position, null, Color.White, MathHelper.ToRadians(_player.angle), _player.originEngine, Vector2.One, SpriteEffects.None, 0f);
			_spriteBatch.Draw(_player.img, _player.position, null, Color.White, MathHelper.ToRadians(_player.angle), _player.origin, Vector2.One, SpriteEffects.None, 0f);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}