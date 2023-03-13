using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Units;
using TopDownShooter.Source.Gameplay.Units.Mobs;

namespace TopDownShooter.Source.Gameplay.SpawnPoints
{
	public class Portal : SpawnPoint
	{
		public Portal(Vector2 pos, int ownerId) : base("Buildings\\sPortal", pos, new Vector2(45, 45), ownerId)
		{
		}

		public override void Update(Vector2 offset)
		{
			base.Update(offset);
		}

		public override void SpawnMob()
		{
			int num = Globals.rng.Next(0, 10 + 1);

			Mob tempMob = null;

			if (num < 5)
			{
				tempMob = new BeanMob(new Vector2(Position.X, Position.Y), ownerId);

			}
			else if (num < 8)
			{
				tempMob = new SpiderMob(new Vector2(Position.X, Position.Y), ownerId);
			}

			if (tempMob is not null)
			{
				GameGlobals.PassMob(tempMob);
			}
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
