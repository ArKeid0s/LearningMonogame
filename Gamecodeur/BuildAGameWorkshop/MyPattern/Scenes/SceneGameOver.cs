using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPattern.Scenes
{
	internal class SceneGameOver : Scene
	{
		public SceneGameOver(MainGame game) : base(game)
		{
		}

		public override void Load()
		{
			base.Load();
		}

		public override void Unload()
		{
			base.Unload();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			game.SpriteBatch.DrawString(AssetsManager.MainFont, "Game Over", new Vector2(100, 100), Color.Red);

			base.Draw(gameTime);
		}
	}
}
