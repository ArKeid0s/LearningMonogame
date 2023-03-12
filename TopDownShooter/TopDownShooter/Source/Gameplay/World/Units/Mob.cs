using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;

namespace TopDownShooter.Source.Gameplay.Units
{
	public class Mob : Unit
	{
		public Mob(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
		{
			Speed = 2f;
		}

		public virtual void Update(Vector2 offset, Hero hero)
		{
			AiLogic(hero);
			base.Update(offset);
		}

		public void AiLogic(Hero hero)
		{
			Position += Globals.RadialMovement(hero.Position, Position, Speed);
			Rotation = Globals.RotateTowards(Position, hero.Position);

            if (Globals.GetDistance(Position, hero.Position) < 15) // Collide with hero
            {
				hero.GetHit(1);
				IsDead = true;
            }
        }

		public override void Draw(Vector2 offset, Color color, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, Color.White, spriteEffect);
		}
	}
}
