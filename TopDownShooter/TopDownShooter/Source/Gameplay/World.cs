using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Units;

namespace TopDownShooter.Source.Gameplay
{
	public class World
	{
		public int _numKilled;

		private Vector2 _offset;
		private readonly Hero _hero;
		public Hero Hero => _hero;
		private readonly UI _ui;

		private readonly List<Projectile2d> _projectiles = new();
		private readonly List<Mob> _mobs = new();
		private readonly List<SpawnPoint> _spawnPoints = new();

		public World()
		{
			_numKilled = 0;

			_offset = Vector2.Zero;
			_hero = new Hero("Hero\\sPlayerIdle_strip4", new System.Numerics.Vector2(300, 300), new System.Numerics.Vector2(40, 40));

			GameGlobals.PassProjectile = AddProjectile;
			GameGlobals.PassMob = AddMob;
			GameGlobals.CheckScroll = CheckScroll;

			_spawnPoints.Add(new SpawnPoint("Misc\\sCircle", new Vector2(50, 50), new Vector2(35, 35)));
			_spawnPoints.Add(new SpawnPoint("Misc\\sCircle", new Vector2(Globals.ScreenWidth / 2, 50), new Vector2(35, 35)));
			_spawnPoints[_spawnPoints.Count - 1].SpawnTimer.AddToTimer(500);
			_spawnPoints.Add(new SpawnPoint("Misc\\sCircle", new Vector2(Globals.ScreenWidth - 50, 50), new Vector2(35, 35)));
			_spawnPoints[_spawnPoints.Count - 1].SpawnTimer.AddToTimer(500);

			_ui = new UI();
		}

		public virtual void Update()
		{
			_hero.Update(_offset);

			foreach (var spawnPoint in _spawnPoints)
			{
				spawnPoint.Update(_offset);
			}

			for (int i = _projectiles.Count - 1; i >= 0; i--)
			{
				var projectile = _projectiles[i];
				projectile.Update(_offset, _mobs.ToList<Unit>());
				if (projectile.IsDead)
				{
					_projectiles.RemoveAt(i);
				}
			}

			for (int i = _mobs.Count - 1; i >= 0; i--)
			{
				var mob = _mobs[i];
				mob.Update(_offset, _hero);
				if (mob.IsDead)
				{
					_numKilled++;
					_mobs.RemoveAt(i);
				}
			}

			_ui.Update(this);
		}

		public void AddMob(object mob)
		{
			_mobs.Add((Mob)mob);
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
				_offset = new Vector2(_offset.X + _hero.Speed * 2, _offset.Y);
			}

			if (tempPos.X > -_offset.X + (Globals.ScreenWidth * .6f))
			{
				_offset = new Vector2(_offset.X - _hero.Speed * 2, _offset.Y);
			}

			if (tempPos.Y < -_offset.Y + (Globals.ScreenHeight * .4f))
			{
				_offset = new Vector2(_offset.X, _offset.Y + _hero.Speed * 2);
			}

			if (tempPos.Y > -_offset.Y + (Globals.ScreenHeight * .6f))
			{
				_offset = new Vector2(_offset.X, _offset.Y - _hero.Speed * 2);
			}
		}

		public virtual void Draw(Vector2 offset)
		{
			_hero.Draw(_offset, Color.White);

			foreach (var spawnPoint in _spawnPoints)
			{
				spawnPoint.Draw(_offset, Color.White);
			}

			foreach (var projectile in _projectiles)
			{
				projectile.Draw(_offset, Color.White);
			}

			foreach (var mob in _mobs)
			{
				mob.Draw(_offset, Color.White);
			}

			_ui.Draw(this);
		}
	}
}
