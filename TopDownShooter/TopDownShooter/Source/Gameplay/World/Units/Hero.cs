using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Projectiles;

namespace TopDownShooter.Source.Gameplay.Units
{
	public class Hero : Unit
	{
		private bool _doCheckScroll;

		public Hero(string path, Vector2 pos, Vector2 dims) : base(path, pos, dims)
		{
			Speed = 2f;

			Health = 5;
			HealthMax = Health;

			_doCheckScroll = false;
		}

		public override void Update(Vector2 offset)
		{
			_doCheckScroll = false;

			float directionX = 0f;
			float directionY = 0f;
			if (Globals.KeyboardManager.IsPressed("A"))
			{
				directionX = -1;
				_doCheckScroll = true;
			}

			if (Globals.KeyboardManager.IsPressed("D"))
			{
				directionX = 1;
				_doCheckScroll = true;
			}

			if (Globals.KeyboardManager.IsPressed("W"))
			{
				directionY = -1;
				_doCheckScroll = true;
			}

			if (Globals.KeyboardManager.IsPressed("S"))
			{
				directionY = 1;
				_doCheckScroll = true;
			}

            var playerDirection = new Vector2(directionX, directionY);

			Position += playerDirection * Speed;

			if (_doCheckScroll)
			{
				GameGlobals.CheckScroll(Position);
			}

			if (Globals.MouseManager.LeftClick())
			{
				GameGlobals.PassProjectile(new Bullet(new Vector2(Position.X, Position.Y), this, new Vector2(Globals.MouseManager.newMousePos.X, Globals.MouseManager.newMousePos.Y) - offset));
			}

			base.Update(offset);
		}

		public override void Draw(Vector2 offset, Color color, SpriteEffects spriteEffect = SpriteEffects.None)
		{
			if (Globals.MouseManager.newMousePos.X < Position.X) spriteEffect = SpriteEffects.FlipHorizontally;
			else spriteEffect = SpriteEffects.None;

			base.Draw(offset, Color.White, spriteEffect);
		}
	}
}
