using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter.Source.Engine.DataTypes
{
	public class QuantityDisplayBar
	{
		public int _border;
		public Basic2d _bar, _barBackground;
		public Color _color;
		public QuantityDisplayBar(Vector2 dims, int border, Color color)
		{
			_border = border;
			_color = color;
			_bar = new Basic2d("Misc\\sSolid", Vector2.Zero, new Vector2(dims.X - (_border * 2), dims.Y - (_border * 2)));
			_barBackground = new Basic2d("Misc\\sShade", Vector2.Zero, new Vector2(dims.X, dims.Y));
		}

		public void Update(float current, float max)
		{
			_bar.Dims = new Vector2(current / max * (_barBackground.Dims.X - (_border * 2)), _bar.Dims.Y);
		}

		public void Draw(Vector2 offset)
		{
			_barBackground.Draw(offset, Vector2.Zero, Color.Black);
			_bar.Draw(offset + new Vector2(_border, _border), Vector2.Zero, _color);
		}
	}
}
