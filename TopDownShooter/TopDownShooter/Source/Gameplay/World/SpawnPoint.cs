using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Units.Mobs;

namespace TopDownShooter.Source.Gameplay
{
	public class SpawnPoint : Basic2d
	{
		public bool IsDead { get; protected set; }

		private float _hitDistance;
		public float HitDistance { get { return _hitDistance; } protected set { _hitDistance = value; } }

		public SETimer SpawnTimer { get; private set; } = new SETimer(5000);

		public SpawnPoint(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
		{
			IsDead = false;
			_hitDistance = 35f;
		}

		public override void Update(Vector2 offset)
		{
			SpawnTimer.UpdateTimer();
			if (SpawnTimer.Test())
			{
				SpawnMob();
				SpawnTimer.ResetToZero();
			}

			base.Update(offset);
		}

		public void GetHit()
		{
			IsDead = true;
		}

		private void SpawnMob()
		{
			GameGlobals.PassMob(new BeanMob(new Vector2(Position.X, Position.Y)));
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
