using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPattern.Scenes
{
	class Hero : Sprite
	{
		public float Energy;
		public Hero(Texture2D texture) : base(texture)
		{
			Energy = 100f;
		}

		public override void OnCollide(IActor other)
		{
            if (other is Meteor)
            {
				Energy -= 1f;
			}
        }
	}

	class Meteor : Sprite
	{
		public Meteor(Texture2D texture) : base(texture)
		{
			float speed = 0.1f;
			do
			{
				Velocity = new Vector2(Util.RandomInt(-3, 3), Util.RandomInt(-3, 3));
			} while (Velocity.X == 0f);

			do
			{
				Velocity = new Vector2(Util.RandomInt(-3, 3), Util.RandomInt(-3, 3));
			} while (Velocity.Y == 0f);

			Velocity *= speed;
		}

		public override void OnCollide(IActor other)
		{
			IsAlive = false;
			base.OnCollide(other);
		}
	}

	internal class SceneGame : Scene
	{
		private KeyboardState _oldKeyboardState;

		private Hero _hero;

		public SceneGame(MainGame game) : base(game)
		{
		}

		public override void Load()
		{
			_oldKeyboardState = Keyboard.GetState();

			Rectangle screen = game.Window.ClientBounds;

			for (int i = 0; i < 50; i++)
			{
				Meteor m = new(game.Content.Load<Texture2D>("circle"));
				m.Position = new Vector2(Util.RandomInt(0, screen.Width - m.Bounds.Width), Util.RandomInt(0, screen.Height - m.Bounds.Height));
				actors.Add(m);
			}

			_hero = new Hero(game.Content.Load<Texture2D>("square_face"));
			_hero.Position = new Vector2(screen.Width / 2 - _hero.Bounds.Width / 2, screen.Height / 2 - _hero.Bounds.Height / 2);
			actors.Add(_hero);

            base.Load();
		}

		public override void Unload()
		{
			base.Unload();
		}

		public override void Update(GameTime gameTime)
		{
            if (_hero.Energy <= 0)
            {
				game.GameState.ChangeScene(GameState.SceneType.GameOver);
            }

            Rectangle screen = game.Window.ClientBounds;
            foreach (var actor in actors)
            {
                if (actor is Meteor m)
                {
					float x = m.Velocity.X;
					float y = m.Velocity.Y;

					if (m.Position.X < 0)
					{
						x = 0 - m.Velocity.X;
						m.Position = new Vector2(0, m.Position.Y);
					}

					if (m.Position.X + m.Bounds.Width > screen.Width)
					{
						x = 0 - m.Velocity.X;
						m.Position = new Vector2(screen.Width - m.Bounds.Width, m.Position.Y);
					}

					if (m.Position.Y < 0)
					{
						y = 0 - m.Velocity.Y;
						m.Position = new Vector2(m.Position.X, 0);
					}

					if (m.Position.Y + m.Bounds.Height > screen.Height)
					{
						y = 0 - m.Velocity.Y;
						m.Position = new Vector2(m.Position.X, screen.Height - m.Bounds.Height);
					}

					m.Velocity = new Vector2(x, y);

					if (Util.AreBoxColliding(m, _hero))
					{
						_hero.OnCollide(m);
						m.OnCollide(_hero);
					}
				}
            }

            actors.RemoveAll(a => a.IsAlive is false);

            KeyboardState newKeyboardState = Keyboard.GetState();

			if (newKeyboardState.IsKeyDown(Keys.Up))
			{
				_hero.Move(new Vector2(0, -1));
			}

			if (newKeyboardState.IsKeyDown(Keys.Down))
			{
				_hero.Move(new Vector2(0, 1));
			}

			if (newKeyboardState.IsKeyDown(Keys.Left))
			{
				_hero.Move(new Vector2(-1, 0));
			}

			if (newKeyboardState.IsKeyDown(Keys.Right))
			{
				_hero.Move(new Vector2(1, 0));
			}

			if (newKeyboardState.IsKeyDown(Keys.Space) && !_oldKeyboardState.IsKeyDown(Keys.Space))
			{
				Debug.WriteLine("space on gameplay");
			}

			_oldKeyboardState = Keyboard.GetState();

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			game.SpriteBatch.DrawString(AssetsManager.MainFont, $"Game - Energy {_hero.Energy}", new Vector2(1, 1), Color.White);

			base.Draw(gameTime);
		}
	}
}
