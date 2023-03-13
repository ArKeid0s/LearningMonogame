using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TopDownShooter.Source.Engine.Input;

namespace TopDownShooter.Source.Engine
{
	public delegate void PassObject(object obj);
	public delegate object PassObjectAndReturn(object obj);

	internal class Globals
	{
		public static ContentManager ContentManager { get; set; }
		public static SpriteBatch SpriteBatch { get; set; }
		public static KeyboardManager KeyboardManager { get; set; }
		public static MouseManager MouseManager { get; set; }
		public static GameTime GameTime { get; set; }
		public static Random rng { get; set; } = new Random();

		public static int ScreenHeight { get; set; }
		public static int ScreenWidth { get; set; }

		public static float GetDistance(Vector2 pos, Vector2 target)
		{
			return (float)Math.Sqrt(Math.Pow(pos.X - target.X, 2) + Math.Pow(pos.Y - target.Y, 2));
		}

		public static Vector2 RadialMovement(Vector2 focus, Vector2 position, float speed)
		{
			float distance = Globals.GetDistance(position, focus);

			if (distance <= speed)
			{
				return focus - position;
			}
			else
			{
				return (focus - position) * speed / distance;
			}
		}

		public static float RotateTowards(Vector2 Pos, Vector2 focus)
		{

			float h, sineTheta, angle;
			if (Pos.Y - focus.Y != 0)
			{
				h = (float)Math.Sqrt(Math.Pow(Pos.X - focus.X, 2) + Math.Pow(Pos.Y - focus.Y, 2));
				sineTheta = (float)(Math.Abs(Pos.Y - focus.Y) / h); //* ((item.Pos.Y-focus.Y)/(Math.Abs(item.Pos.Y-focus.Y))));
			}
			else
			{
				h = Pos.X - focus.X;
				sineTheta = 0;
			}

			angle = (float)Math.Asin(sineTheta);

			// Drawing diagonial lines here.
			//Quadrant 2
			if (Pos.X - focus.X > 0 && Pos.Y - focus.Y > 0)
			{
				angle = (float)(Math.PI * 3 / 2 + angle);
			}
			//Quadrant 3
			else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y < 0)
			{
				angle = (float)(Math.PI * 3 / 2 - angle);
			}
			//Quadrant 1
			else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y > 0)
			{
				angle = (float)(Math.PI / 2 - angle);
			}
			else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y < 0)
			{
				angle = (float)(Math.PI / 2 + angle);
			}
			else if (Pos.X - focus.X > 0 && Pos.Y - focus.Y == 0)
			{
				angle = (float)Math.PI * 3 / 2;
			}
			else if (Pos.X - focus.X < 0 && Pos.Y - focus.Y == 0)
			{
				angle = (float)Math.PI / 2;
			}
			else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y > 0)
			{
				angle = (float)0;
			}
			else if (Pos.X - focus.X == 0 && Pos.Y - focus.Y < 0)
			{
				angle = (float)Math.PI;
			}

			return angle;
		}
	}
}
