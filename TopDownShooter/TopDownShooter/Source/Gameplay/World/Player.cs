using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using TopDownShooter.Source.Gameplay.Units;

namespace TopDownShooter.Source.Gameplay
{
	public class Player
	{
		public int id;
        public Hero hero;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Player(int id)
        {
            this.id = id;
        }

        public virtual void Update(Player enemy, Vector2 offset)
		{
			hero?.Update(offset);

			for (int i = spawnPoints.Count - 1; i >= 0; i--)
			{
				var spawnPoint = spawnPoints[i];
				spawnPoint.Update(offset, enemy);
				if (spawnPoint.IsDead)
				{
					spawnPoints.RemoveAt(i);
				}
			}

			for (int i = units.Count - 1; i >= 0; i--)
			{
				var mob = units[i];
				mob.Update(offset, enemy);
				if (mob.IsDead)
				{
					ChangeScore(1);
					units.RemoveAt(i);
				}
			}
		}

        public virtual void AddUnit(object unit)
        {
			Unit tempUnit = unit as Unit;
			tempUnit.ownerId = id;
            units.Add(tempUnit);
		}

		public virtual void AddSpawnPoint(object spawnpoint)
		{
			SpawnPoint tempSpawnPoint = spawnpoint as SpawnPoint;
			tempSpawnPoint.ownerId = id;
			spawnPoints.Add(tempSpawnPoint);
		}

		public virtual void ChangeScore(int score)
        {

        }

        public virtual void Draw(Vector2 offset)
        {
            hero?.Draw(offset);

			foreach (var mob in units)
			{
				mob.Draw(offset);
			}

			foreach (var spawnPoint in spawnPoints)
			{
				spawnPoint.Draw(offset);
			}
		}
    }
}
