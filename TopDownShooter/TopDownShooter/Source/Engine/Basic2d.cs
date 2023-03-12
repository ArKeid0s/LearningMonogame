using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter.Source.Engine
{
	public class Basic2d
	{
		private float _rotation;
		public float Rotation { get { return _rotation; } set { _rotation = value; } }

		private Vector2 _position;
		public Vector2 Position { get { return _position; } set { _position = value; } }

		private Vector2 _dims;
		public Vector2 Dims { get { return _dims; } set { _dims = value; } }

		public Texture2D Sprite { get; set; }

		public Basic2d(string path, Vector2 pos, Vector2 dims)
		{
			_position = pos;
			_dims = dims;

			Sprite = Globals.ContentManager.Load<Texture2D>(path);
		}

		public virtual void Update(Vector2 offset)
		{

		}

		public virtual void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			Vector2 origin = new(Sprite.Bounds.Width / 2, Sprite.Bounds.Height / 2);
			Draw(offset, origin, Color.White, spriteEffect);
		}

		public virtual void Draw(Vector2 offset, Vector2 origin, Color color, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			if (Sprite != null)
			{
				Globals.SpriteBatch.Draw(
					texture: Sprite,
					destinationRectangle: new Rectangle((int)(_position.X + offset.X), (int)(_position.Y + offset.Y), (int)_dims.X, (int)_dims.Y),
					sourceRectangle: null,
					color: color,
					rotation: _rotation,
					origin: new Vector2(origin.X, origin.Y),
					effects: spriteEffect,
					layerDepth: 0);
			}
		}
	}
}
