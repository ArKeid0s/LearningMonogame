using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.Units;
using TopDownShooter.Source.Gameplay.Units.Mobs;

namespace TopDownShooter.Source.Gameplay.SpawnPoints
{
	public class SpiderEggSac : SpawnPoint
	{
		int maxSpawns, totalSpawns;

		public SpiderEggSac(Vector2 pos, int ownerId) : base("Buildings\\sEggSac", pos, new Vector2(45, 45), ownerId)
		{
			totalSpawns = 0;
			maxSpawns = 3;
		}

		public override void Update(Vector2 offset, Player enemy)
		{
			base.Update(offset, enemy);
		}

		public override void SpawnMob()
		{
			Mob tempMob = new BabySpiderMob(new Vector2(Position.X, Position.Y), ownerId);
			if (tempMob != null)
			{
				GameGlobals.PassMob(tempMob);

				totalSpawns++;
                if (totalSpawns >= maxSpawns)
                {
					IsDead = true;
                }
            }
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
