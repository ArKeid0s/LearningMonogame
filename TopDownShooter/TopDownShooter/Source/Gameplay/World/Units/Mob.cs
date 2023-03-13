using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;

namespace TopDownShooter.Source.Gameplay.Units
{
	public class Mob : Unit
	{
		public Mob(string path, Vector2 pos, Vector2 dims, int id) : base(path, pos, dims, id)
		{
			Speed = 2f;

			Health = 1;
			HealthMax = Health;
		}

		public override void Update(Vector2 offset, Player enemy)
		{
			AiLogic(enemy.hero);
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

		public override void Draw(Vector2 offset, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			base.Draw(offset, spriteEffect);
		}
	}
}
