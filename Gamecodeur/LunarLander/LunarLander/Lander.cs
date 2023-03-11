using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunarLander
{
	internal class Lander
	{
		public Texture2D img { get; set; }
		public Texture2D imgEngine { get; set; }

		public Vector2 position { get; set; } = Vector2.Zero;
		public Vector2 origin { get => new(img.Width / 2, img.Height / 2); }
		public Vector2 originEngine { get => new((imgEngine.Width / 2) + img.Width / 2, imgEngine.Height / 2); }
		public Vector2 velocity { get; set; } = Vector2.Zero;
		public float speed { get; set; } = 0.02f;
		public float angle { get; set; } = 0f;
		public bool isEngineOn { get; set; } = false;
		private float _maxSpeed = 2f;

		public void Update()
		{
			velocity += new Vector2(0, 0.005f);

			if (Math.Abs(velocity.X) >= _maxSpeed)
            {
                velocity = new Vector2(velocity.X < 0 ? 0 - _maxSpeed : _maxSpeed, velocity.Y);
            }

			if (Math.Abs(velocity.Y) >= _maxSpeed)
			{
				velocity = new Vector2(velocity.X, velocity.Y < 0 ? 0 - _maxSpeed : _maxSpeed);
			}

			position += velocity;
		}
	}
}
