using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.Units;
using static System.Reflection.Metadata.BlobBuilder;

namespace TopDownShooter.Source.Gameplay
{
	public class Player
	{
        public Hero hero;
        public List<Unit> units = new List<Unit>();
        public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

        public Player()
        {
            
        }

        public virtual void Update(Player enemy, Vector2 offset)
		{
			hero?.Update(offset);

			foreach (var spawnPoint in spawnPoints)
			{
				spawnPoint.Update(offset);
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
            units.Add((Unit)unit);
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
