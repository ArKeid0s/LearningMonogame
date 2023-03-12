using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.Units;

namespace TopDownShooter.Source.Gameplay
{
	public class AIPlayer : Player
	{
        public AIPlayer() : base()
        {
			spawnPoints.Add(new SpawnPoint("Misc\\sCircle", new Vector2(50, 50), new Vector2(35, 35)));
			spawnPoints.Add(new SpawnPoint("Misc\\sCircle", new Vector2(Globals.ScreenWidth / 2, 50), new Vector2(35, 35)));
			spawnPoints[spawnPoints.Count - 1].SpawnTimer.AddToTimer(500);
			spawnPoints.Add(new SpawnPoint("Misc\\sCircle", new Vector2(Globals.ScreenWidth - 50, 50), new Vector2(35, 35)));
			spawnPoints[spawnPoints.Count - 1].SpawnTimer.AddToTimer(500);
		}

		public override void Update(Player enemy, Vector2 offset)
		{
			base.Update(enemy, offset);
		}

		public override void ChangeScore(int score)
		{
			GameGlobals.score += score;
		}
	}
}
