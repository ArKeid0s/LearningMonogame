using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Units;

namespace TopDownShooter.Source.Gameplay
{
	public class World
	{
		private Vector2 _offset;
		private readonly UI _ui;

		public User user;
		public AIPlayer aiPlayer;

		private readonly List<Projectile2d> _projectiles = new();

		PassObject ResetWorld;

		public World(PassObject resetWorld)
		{
			ResetWorld = resetWorld;

			GameGlobals.PassProjectile = AddProjectile;
			GameGlobals.PassMob = AddMob;
			GameGlobals.CheckScroll = CheckScroll;

			user = new User();
			aiPlayer = new AIPlayer();

			_offset = Vector2.Zero;

			_ui = new UI();
		}

		public virtual void Update()
		{
			if (user.hero.IsDead is not true)
			{
				user.Update(aiPlayer, _offset);
				aiPlayer.Update(user, _offset);

				for (int i = _projectiles.Count - 1; i >= 0; i--)
				{
					var projectile = _projectiles[i];
					projectile.Update(_offset, aiPlayer.units);
					if (projectile.IsDead)
					{
						_projectiles.RemoveAt(i);
					}
				}
			}
            else
            {
                if (Globals.KeyboardManager.IsPressed("Enter"))
                {
					ResetWorld(null);
                }
            }

            _ui.Update(this);
		}

		public void AddMob(object mob)
		{
			aiPlayer.AddUnit(mob as Mob);
		}

		public void AddProjectile(object projectile)
		{
			_projectiles.Add((Projectile2d)projectile);
		}

		public void CheckScroll(object position)
		{
			Vector2 tempPos = (Vector2)position;

			if (tempPos.X < -_offset.X + (Globals.ScreenWidth * .4f))
			{
				_offset = new Vector2(_offset.X + user.hero.Speed * 2, _offset.Y);
			}

			if (tempPos.X > -_offset.X + (Globals.ScreenWidth * .6f))
			{
				_offset = new Vector2(_offset.X - user.hero.Speed * 2, _offset.Y);
			}

			if (tempPos.Y < -_offset.Y + (Globals.ScreenHeight * .4f))
			{
				_offset = new Vector2(_offset.X, _offset.Y + user.hero.Speed * 2);
			}

			if (tempPos.Y > -_offset.Y + (Globals.ScreenHeight * .6f))
			{
				_offset = new Vector2(_offset.X, _offset.Y - user.hero.Speed * 2);
			}
		}

		public virtual void Draw(Vector2 offset)
		{
			user.Draw(_offset);
			aiPlayer.Draw(_offset);

			foreach (var projectile in _projectiles)
			{
				projectile.Draw(_offset);
			}


			_ui.Draw(this);
		}
	}
}
