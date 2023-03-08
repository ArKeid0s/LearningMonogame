using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	public class Sprite : IActor
	{
		private Texture2D _texture;
		public Texture2D Texture { get => _texture; set => _texture = value; }

		private bool _isInitialized;
		private Vector2 _position;
		public Vector2 Position {
			get { return _position; }
			set
			{
				_position = value;
				if (!_isInitialized)
				{
					InitBounds();
				}
			}
		}

		private Rectangle _bounds;
		public Rectangle Bounds => _bounds;

		private Vector2 _velocity;
		public Vector2 Velocity { get => _velocity; set => _velocity = value; }

		public bool IsAlive { get; set; }

        public Sprite(Texture2D texture)
        {
			_isInitialized = false;
            _texture = texture;
			IsAlive = true;
			_bounds = new Rectangle(Point.Zero, new Point(_texture.Width, _texture.Height));
		}

		public void InitBounds()
		{
			_bounds = new Rectangle(_position.ToPoint(), new Point(_texture.Width, _texture.Height));
			_isInitialized = true;
		}

		public void Move(Vector2 direction)
		{
			_position += direction;
		}

        public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, _position, Color.White);
		}

		public virtual void Update(GameTime gameTime)
		{
			Move(_velocity);
			_bounds.Location = _position.ToPoint();
		}

		public virtual void OnCollide(IActor other)
		{
		}
	}
}
