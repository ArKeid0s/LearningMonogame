using Engine;
using Microsoft.Xna.Framework;
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
	internal class SceneMenu : Scene
	{
		private KeyboardState _oldKeyboardState;
		private Button _playButton;

		public SceneMenu(MainGame game) : base(game)
		{

		}

		public void OnClickPlay(Button button)
		{
			game.GameState.ChangeScene(GameState.SceneType.Game);
		}

		public override void Load()
		{
			Rectangle screen = game.Window.ClientBounds;

			_oldKeyboardState = Keyboard.GetState();

			_playButton = new Button(game.Content.Load<Texture2D>("button"));
			_playButton.Position = new Vector2(screen.Width / 2 - _playButton.Bounds.Width / 2, screen.Height / 2 - _playButton.Bounds.Height / 2);
			_playButton.OnClick = OnClickPlay;

			actors.Add(_playButton);

			base.Load();
		}

		public override void Unload()
		{
			base.Unload();
		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState newKeyboardState = Keyboard.GetState();

			if (newKeyboardState.IsKeyDown(Keys.Space) && !_oldKeyboardState.IsKeyDown(Keys.Space))
			{
				game.GameState.ChangeScene(GameState.SceneType.Game);
			}

			_oldKeyboardState = Keyboard.GetState();

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			game.SpriteBatch.DrawString(AssetsManager.MainFont, "Menu", new Vector2(100, 100), Color.White);

			base.Draw(gameTime);
		}
	}
}
