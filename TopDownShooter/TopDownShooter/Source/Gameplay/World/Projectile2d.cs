using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TopDownShooter.Source.Engine;

namespace TopDownShooter.Source.Gameplay
{
	public class Projectile2d : Basic2d
	{
		public bool IsDead { get; set; }

		private float _speed;
		protected float Speed { get { return _speed; } set { _speed = value; } }

		private Vector2 _direction;
		protected Vector2 Direction { get { return _direction; } set { _direction = value; } }

		private Unit _owner;
		protected Unit Owner { get { return _owner; } set { _owner = value; } }

		protected SETimer Timer { get; set; }

		public Projectile2d(string path, Vector2 pos, Vector2 dims, Unit owner, Vector2 targetPosition) : base(path, pos, dims)
		{
			IsDead = false;
			_speed = 5f;
			_owner = owner;
			_direction = targetPosition - _owner.Position;
			_direction.Normalize();

			Rotation = Globals.RotateTowards(Position, new Vector2(targetPosition.X, targetPosition.Y));

			Timer = new SETimer(1200);
		}

		public virtual void Update(Vector2 offset, List<Unit> units)
		{
			Position += _direction * _speed;

			Timer.UpdateTimer();
			if (Timer.Test())
			{
				IsDead = true;
			}

			if (HitSomething(units))
			{
				IsDead = true;
			}
		}

		public virtual bool HitSomething(List<Unit> units)
		{
			foreach (Unit unit in units)
			{
				if (Globals.GetDistance(Position, unit.Position) < unit.HitDistance)
				{
					unit.GetHit(1);
					return true;
				}
			}

			return false;
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
