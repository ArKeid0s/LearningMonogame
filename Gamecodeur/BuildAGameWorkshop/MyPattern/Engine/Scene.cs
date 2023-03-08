using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	abstract public class Scene : IScene
	{
		protected MainGame game;
		protected List<IActor> actors;

		public Scene(MainGame game)
		{
			this.game = game;
			actors = new List<IActor>();
		}

		public virtual void Load()
		{
		}

		public virtual void Unload()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
			foreach (var actor in actors)
			{
				actor.Update(gameTime);
			}
		}

		public virtual void Draw(GameTime gameTime)
		{
			foreach (var actor in actors)
			{
				actor.Draw(game.SpriteBatch);
			}
		}
	}
}
