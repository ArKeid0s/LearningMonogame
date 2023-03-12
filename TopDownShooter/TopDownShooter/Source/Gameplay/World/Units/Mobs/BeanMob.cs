using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter.Source.Gameplay.Units.Mobs
{
	public class BeanMob : Mob
	{
		public BeanMob(Vector2 pos) : base("Enemies\\sEnemy_strip7", pos, new Vector2(40, 40))
		{
			Speed = 2f;
		}

		public override void Update(Vector2 offset, Hero hero)
		{
			base.Update(offset, hero);
		}

		public override void Draw(Vector2 offset, Color color, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, Color.White, spriteEffect);
		}
	}
}
