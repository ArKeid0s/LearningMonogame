using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;

namespace TopDownShooter.Source.Gameplay.Units.Mobs
{
	public class BabySpiderMob : Mob
	{
		public SETimer spawnTimer;

		public BabySpiderMob(Vector2 pos, int id) : base("Enemies\\sSpider", pos, new Vector2(25, 25), id)
		{
			Speed = 3f;
		}

		public override void Update(Vector2 offset, Player enemy)
		{
			base.Update(offset, enemy);
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
