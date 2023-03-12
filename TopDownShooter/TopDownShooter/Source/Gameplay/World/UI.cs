using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using TopDownShooter.Source.Engine;
using TopDownShooter.Source.Engine.DataTypes;

namespace TopDownShooter.Source.Gameplay
{
	internal class UI
	{
		public SpriteFont Font { get; set; }

		private QuantityDisplayBar _healthBar;

		public UI()
		{
			Font = Globals.ContentManager.Load<SpriteFont>("UI\\Fonts\\fGalleryFont");
			_healthBar = new QuantityDisplayBar(new Vector2(504, 16), 2, Color.Red);
		}

		public void Update(World world)
		{
			_healthBar.Update(world.Hero.Health, world.Hero.HealthMax);
		}

		public void Draw(World world)
		{
			string tempStr = $"Num killed = {world._numKilled}";
			Vector2 strDimensions = Font.MeasureString(tempStr);
			Globals.SpriteBatch.DrawString(Font, tempStr, new Vector2(Globals.ScreenWidth / 2 - strDimensions.X / 2, Globals.ScreenHeight - 30), Color.Black);

			_healthBar.Draw(new Vector2(20, Globals.ScreenHeight - 30));
		}
	}
}
