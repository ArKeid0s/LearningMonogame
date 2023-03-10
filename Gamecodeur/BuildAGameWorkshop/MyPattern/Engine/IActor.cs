using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	public interface IActor
	{
		Vector2 Position { get; }
		Rectangle Bounds { get; }
		void Update(GameTime gameTime);
		void Draw(SpriteBatch spriteBatch);
		void OnCollide(IActor other);
		bool IsAlive { get; set; }
	}
}
