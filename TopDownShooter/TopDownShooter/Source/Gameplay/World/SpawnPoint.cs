using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Units.Mobs;

namespace TopDownShooter.Source.Gameplay
{
	public class SpawnPoint : AttackableObject
	{
		public SETimer SpawnTimer { get; private set; } = new SETimer(5000);

		public SpawnPoint(string path, Vector2 pos, Vector2 dims, int ownerId) : base(path, pos, dims, ownerId)
		{
			IsDead = false;
			HitDistance = 35f;

			Health = 3;
			HealthMax = Health;
		}

		public override void Update(Vector2 offset, Player enemy)
		{
			SpawnTimer.UpdateTimer();
			if (SpawnTimer.Test())
			{
				SpawnMob();
				SpawnTimer.ResetToZero();
			}

			base.Update(offset);
		}

		public virtual void SpawnMob()
		{
			GameGlobals.PassMob(new BeanMob(new Vector2(Position.X, Position.Y), ownerId));
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
