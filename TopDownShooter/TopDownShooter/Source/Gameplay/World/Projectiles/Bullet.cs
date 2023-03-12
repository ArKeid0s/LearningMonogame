using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TopDownShooter.Source.Gameplay.Projectiles
{
	public class Bullet : Projectile2d
	{
		public Bullet(Vector2 pos, Unit owner, Vector2 targetPosition) : base("Projectiles\\sBullet", pos, new Vector2(16, 16), owner, targetPosition)
		{
		}

		public override void Update(Vector2 offset, List<Unit> units)
		{
			base.Update(offset, units);
		}

		public override void Draw(Vector2 offset, Color color, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, Color.White, spriteEffect);
		}
	}
}
