using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay.Units;

namespace TopDownShooter.Source.Gameplay
{
	public class User : Player
	{
        public User() : base()
		{
			hero = new Hero("Hero\\sPlayerIdle_strip4", new System.Numerics.Vector2(300, 300), new System.Numerics.Vector2(40, 40));
		}

		public override void Update(Player enemy, Vector2 offset)
		{
			base.Update(enemy, offset);
		 }
	}
}
