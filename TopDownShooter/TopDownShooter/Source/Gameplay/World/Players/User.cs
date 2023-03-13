using Microsoft.Xna.Framework;
using TopDownShooter.Source.Gameplay.Units;

namespace TopDownShooter.Source.Gameplay
{
	public class User : Player
	{
        public User(int id) : base(id)
		{
			hero = new Hero("Hero\\sPlayerIdle_strip4", new System.Numerics.Vector2(300, 300), new System.Numerics.Vector2(40, 40), id);
		}

		public override void Update(Player enemy, Vector2 offset)
		{
			base.Update(enemy, offset);
		 }
	}
}
