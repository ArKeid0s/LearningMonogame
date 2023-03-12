using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace IsometricMap
{
	/*
	 * 
	 * Articles: https://clintbellanger.net/articles/isometric_math/
	 * 
	 */
	public class MainGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private List<Texture2D> _textures2D;
		private List<Texture2D> _textures3D;

		private TileMap _tileMap;
		private Vector2 _map2DOrigin;
		private Vector2 _map3DOrigin;

		private List<Vector2> _sprites;

		public MainGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			_graphics.PreferredBackBufferWidth = 1600;
			_graphics.PreferredBackBufferHeight = 900;
			_graphics.ApplyChanges();
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			int[,] mapData = new int[,]
			{
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,1,1,2,2,2,2 },
				{ 2,2,2,2,1,1,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
				{ 2,2,2,2,2,2,2,2,2,2 },
			};

			_tileMap = new TileMap()
				.Set2DTileSize(32, 32)
				.Set3DTileSize(64, 32)
				.SetData(mapData);

			_map2DOrigin = new Vector2(10, (GraphicsDevice.Viewport.Height / 2) - ((_tileMap.TileHeight2D * _tileMap.MapHeight) / 2));
			_map3DOrigin = new Vector2(
				10 + (_tileMap.TileWidth2D * _tileMap.MapWidth) + (_tileMap.TileWidth3D * (_tileMap.MapWidth / 2))
				, (GraphicsDevice.Viewport.Height / 2) - ((_tileMap.TileHeight2D * _tileMap.MapHeight) / 2));

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_textures2D = new List<Texture2D>
			{
				Content.Load<Texture2D>("stone2D"),
				Content.Load<Texture2D>("dirt2D")
			};

			_textures3D = new List<Texture2D>()
			{
				Content.Load<Texture2D>("stone3D"),
				Content.Load<Texture2D>("dirt3D")
			};
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (newMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
			{
				Trace.WriteLine("Clic !");

				// Quelle tile sur la map 3D?
				Vector2 clicPosition = newMouseState.Position.ToVector2();
				clicPosition -= map3D_origin;
				Vector2 Coord2D = myMap.To2D(clicPosition);
				int ID3D = myMap.getIDAt(Coord2D);
				Trace.WriteLine("3D Tile " + ID3D);

				// Change la valeur
				if (ID3D >= 0)
				{
					int column = myMap.getColAt((int)Coord2D.X);
					int line = myMap.getLineAt((int)Coord2D.Y);
					if (ID3D == 1)
						myMap.changeIDAt(line, column, 2);
					else
						myMap.changeIDAt(line, column, 1);
				}
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin();

			// 2D MAP
			for (int i = 0; i < _tileMap.MapWidth; i++)
			{
				for (int j = 0; j < _tileMap.MapHeight; j++)
				{
					int id = _tileMap.GetId(i, j);
					int x = j * _tileMap.TileWidth2D;
					int y = i * _tileMap.TileHeight2D;
					Texture2D texture = _textures2D[id - 1];
					Vector2 position = new(x, y);
					position += _map2DOrigin;
					_spriteBatch.Draw(texture, position, Color.White);
				}
			}

			// 3D MAP
			for (int i = 0; i < _tileMap.MapWidth; i++)
			{
				for (int j = 0; j < _tileMap.MapHeight; j++)
				{
					int id = _tileMap.GetId(i, j);
					int x = j * _tileMap.TileWidth2D;
					int y = i * _tileMap.TileHeight2D;
					Texture2D texture = _textures3D[id - 1];

					Vector2 position = new(x, y);
					position = _tileMap.To3D(position);
					position += _map3DOrigin;
					_spriteBatch.Draw(texture, position, Color.White);
				}
			}

			// Sort Z order
			_sprites.Sort(delegate (Sprite s1, Sprite s2)
			{
				float z1 = s1.Position.X + s1.Position.Y;
				float z2 = s2.Position.X + s2.Position.Y;
				return z1.CompareTo(z2);
			});

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}