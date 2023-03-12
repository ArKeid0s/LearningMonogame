using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;

namespace TopDownShooter.Source.Gameplay
{
	public class Unit : Basic2d
	{
		public bool IsDead { get; protected set; }

		private float _speed, _hitDistance, _health, _healthMax;
		public float Health { get { return _health; } protected set { _health = value; } }
		public float HealthMax { get { return _healthMax; } protected set { _healthMax = value; } }
		public float HitDistance { get { return _hitDistance; } protected set { _hitDistance = value; } }
		public float Speed { get { return _speed; } protected set { _speed = value; } }

		public Unit(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
		{
			IsDead = false;
			_speed = 2f;

			_health = 1;
			_healthMax = _health;

			_hitDistance = 35f;
		}

		public override void Update(Vector2 offset)
		{


			base.Update(offset);
		}

		public virtual void GetHit(float damage)
		{
			_health -= damage;
			if (_health <= 0)
			{
				IsDead = true;
			}
		}

		public override void Draw(Vector2 offset, Color color, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, Color.White, spriteEffect);
		}
	}
}
