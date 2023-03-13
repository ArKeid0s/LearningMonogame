using Microsoft.Xna.Framework;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Gameplay.SpawnPoints;

namespace TopDownShooter.Source.Gameplay
{
	public class AIPlayer : Player
	{
        public AIPlayer(int id) : base(id)
        {
			spawnPoints.Add(new Portal(new Vector2(50, 50), id));
			spawnPoints.Add(new Portal(new Vector2(Globals.ScreenWidth / 2, 50), id));
			spawnPoints[spawnPoints.Count - 1].SpawnTimer.AddToTimer(500);
			spawnPoints.Add(new Portal(new Vector2(Globals.ScreenWidth - 50, 50), id));
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
