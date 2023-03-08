using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	public static class Util
	{
		public static Random Random { get; private set; } = new Random();
		public static void SetSeed(int seed) => Random = new Random(seed);
		public static float RandomFloat(float min, float max) => (float)Random.NextDouble() * (max - min) + min;
		public static int RandomInt(int min, int max) => Random.Next(min, max);
		public static bool AreBoxColliding(IActor a, IActor b)
		{
			return a.Bounds.Intersects(b.Bounds);
		}
	}
}
