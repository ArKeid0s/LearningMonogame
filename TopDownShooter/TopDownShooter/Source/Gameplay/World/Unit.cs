using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter.Source.Gameplay
{
	public class Unit : AttackableObject
	{
		public Unit(string path, Vector2 pos, Vector2 dims, int ownerId) : base(path, pos, dims, ownerId)
		{
		}

		public override void Update(Vector2 offset, Player enemy)
		{
			base.Update(offset);
		}

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
