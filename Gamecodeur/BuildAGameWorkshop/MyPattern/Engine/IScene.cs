using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	internal interface IScene
	{
		void Load();
		void Unload();
		void Update(GameTime gameTime);
		void Draw(GameTime gameTime);
	}
}
