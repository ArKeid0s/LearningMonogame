using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsometricMap
{
	internal class TileMap
	{
		private int[,] _data;
		public int MapWidth { get; private set; }
		public int MapHeight { get; private set; }
		public int TileWidth2D { get; private set; }
		public int TileHeight2D { get; private set; }
		public int TileWidth3D { get; private set; }
		public int TileHeight3D { get; private set; }

		public TileMap()
		{
		}

		public TileMap Set2DTileSize(int tileWidth, int tileHeight) // sprite size
		{
			TileWidth2D = tileWidth;
			TileHeight2D = tileHeight;
			return this;
		}

		public TileMap Set3DTileSize(int tileWidth, int tileHeight) 
		{
			TileWidth3D = tileWidth;
			TileHeight3D = tileHeight;
			return this;
		}

		public TileMap SetData(int[,] data)
		{
			_data = data;
			MapHeight = data.GetLength(0);
			MapWidth = data.GetLength(1);
			return this;
		}

		public int getColAt(int pX)
		{
			return (int)pX / TileWidth2D;
		}

		public int getLineAt(int pY)
		{
			return (int)pY / TileHeight2D;
		}

		public int GetId(int x, int y)
		{
			if (x >= 0 && x < MapHeight && y >= 0 && y < MapWidth)
            {
                return _data[x, y];
            }
			else
			{
				throw new ArgumentOutOfRangeException($"{nameof(GetId)} => Argument x: {x} and/or y: {y} is out of range");
			}
        }

		public int getIDAt(Vector2 pCoord)
		{
			int c = getColAt((int)pCoord.X);
			int l = getLineAt((int)pCoord.Y);
			Trace.WriteLine("c:" + c + " l:" + l);
			if (l >= 0 && l < MapHeight && c >= 0 && c < MapWidth)
			{
				return _data[l, c];
			}
			return -1;
		}

		public Vector2 To3D(Vector2 coord2D)
		{
			Vector2 newCoord = new Vector2();
			newCoord.X = coord2D.X - coord2D.Y;
			newCoord.Y = (coord2D.X + coord2D.Y) / 2;
			return newCoord;
		}

		public Vector2 To2D(Vector2 pCoord)
		{
			// Ajuste la position X pour prendre en compte l'origine d'affichage non centrée sur le sprite
			Vector2 testCoord = new Vector2();
			testCoord.X = pCoord.X - (TileWidth3D / 2);
			testCoord.Y = pCoord.Y;

			Vector2 newCoord = new Vector2();
			newCoord.X = (2 * testCoord.Y + testCoord.X) / 2;
			newCoord.Y = (2 * testCoord.Y - testCoord.X) / 2;
			return newCoord;
		}
	}
}
