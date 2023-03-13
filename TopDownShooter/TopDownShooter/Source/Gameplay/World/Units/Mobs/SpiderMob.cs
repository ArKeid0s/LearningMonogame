using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.SpawnPoints;

namespace TopDownShooter.Source.Gameplay.Units.Mobs
{
	public class SpiderMob : Mob
	{
		public SETimer spawnTimer;

		public SpiderMob(Vector2 pos, int id) : base("Enemies\\sSpider", pos, new Vector2(45, 45), id)
		{
			Speed = 1.5f;

			Health = 3;
			HealthMax = Health;

			spawnTimer = new SETimer(8000);
			spawnTimer.AddToTimer(4000);
		}

		public override void Update(Vector2 offset, Player enemy)
		{
			spawnTimer.UpdateTimer();
			if (spawnTimer.Test())
			{
				SpawnEggSac();
				spawnTimer.ResetToZero();
			}

			base.Update(offset, enemy);
		}

		private void SpawnEggSac()
		{
			GameGlobals.PassSpawnPoint(new SpiderEggSac(new Vector2(Position.X, Position.Y), ownerId));
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
