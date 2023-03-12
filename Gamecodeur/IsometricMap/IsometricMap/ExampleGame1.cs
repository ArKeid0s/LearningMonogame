using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MoonSharp.Interpreter;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using System.Reflection;

namespace SimpleGrid
{
    struct tweenVector
    {
        public Vector2 startPos;
        public Vector2 value;
        public Vector2 currentPos;
        public double duration;
        public double currentTime;

        public void calcSinOut()
        {
            if (currentPos == null)
                currentPos = new Vector2();
            if (currentTime >= duration)
            {
                currentPos = startPos + value;
                return;
            }

            currentPos.X = getSinOut(startPos.X, value.X);
            currentPos.Y = getSinOut(startPos.Y, value.Y);
        }

        public float getSinOut(float start, float end)
        {
            return (float)(end * Math.Sin(currentTime / duration * (Math.PI / 2)) + start);
        }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ExampleGame1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        TileMap myMap;
        Vector2 map2D_origin;
        Vector2 map3D_origin;
        List<Texture2D> lstTextures2D;
        List<Texture2D> lstTextures3D;
        MouseState lastMouseState;
        tweenVector[,] mapTween;
        string test = "Test";

        public ExampleGame1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1024;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            IsMouseVisible = true;

            int[,] mapData = new int[,] {
              { 2,2,2,2,2,2,2,2,2,2 },
              { 2,1,2,2,1,1,2,2,1,2 },
              { 2,2,2,2,2,2,2,2,2,2 },
              { 2,1,2,2,2,2,2,2,2,2 },
              { 2,2,2,2,1,1,2,2,2,2 },
              { 2,2,2,2,1,1,2,2,2,2 },
              { 2,2,2,2,2,2,2,2,2,2 },
              { 2,2,2,2,2,2,2,2,2,2 },
              { 2,1,2,2,2,2,2,2,1,2 },
              { 2,2,2,2,2,2,2,2,2,2 },
            };

            myMap = new TileMap();
            myMap.setData(mapData);
            myMap.set2DSize(32, 32);
            myMap.set3DSize(64, 32);

            Random r = new Random();
            mapTween = new tweenVector[myMap.mapHeight,myMap.mapWidth];
            for (int line = 0; line < myMap.mapHeight; line++)
            {
                for (int column = 0; column < myMap.mapWidth; column++)
                {
                    int x = column * myMap.tileWidth2D;
                    int y = line * myMap.tileHeight2D;
                    mapTween[line, column].startPos = new Vector2(-1000, -1000);
                    mapTween[line, column].value = new Vector2(x+1000, y+1000);
                    mapTween[line, column].duration = r.Next(1000,3000);
                    mapTween[line, column].currentTime = 0;
                    mapTween[line, column].calcSinOut();
                }
            }

            map2D_origin = new Vector2(10, 300-320/2);
            map3D_origin = new Vector2(10 + (myMap.tileWidth2D * myMap.mapWidth) +
                (myMap.tileWidth3D *(myMap.mapWidth/2)) + 10, 300-320/2);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            lstTextures2D = new List<Texture2D>();
            lstTextures3D = new List<Texture2D>();

            Texture2D tx;
            tx = Content.Load<Texture2D>("stone2D");
            lstTextures2D.Add(tx);
            tx = Content.Load<Texture2D>("dirt2D");
            lstTextures2D.Add(tx);

            tx = Content.Load<Texture2D>("stone3D");
            lstTextures3D.Add(tx);
            tx = Content.Load<Texture2D>("dirt3D");
            lstTextures3D.Add(tx);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Tween
            for (int line = 0; line < myMap.mapHeight; line++)
            {
                for (int column = 0; column < myMap.mapWidth; column++)
                {
                    if (mapTween[line, column].currentTime < mapTween[line, column].duration)
                    {
                        mapTween[line, column].currentTime += gameTime.ElapsedGameTime.Milliseconds;
                        mapTween[line, column].calcSinOut();
                    }
                }
            }

            MouseState newMouseState = Mouse.GetState();
            if (newMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                Trace.WriteLine("Clic !");

                // Quelle tile sur la map 3D?
                Vector2 clicPosition = newMouseState.Position.ToVector2();
                clicPosition -= map3D_origin;
                Vector2 Coord2D = myMap.To2D(clicPosition);
                int ID3D = myMap.getIDAt(Coord2D);
                Trace.WriteLine("3D Tile "+ID3D);

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
                else
                {
                    // Quelle tile sur la map 2D ?
                    clicPosition = newMouseState.Position.ToVector2();
                    clicPosition -= map2D_origin;
                    int ID2D = myMap.getIDAt(clicPosition);
                    Trace.WriteLine("2D Tile " + ID2D);

                    // Change la valeur
                    if (ID2D >= 0)
                    {
                        int column = myMap.getColAt((int)clicPosition.X);
                        int line = myMap.getLineAt((int)clicPosition.Y);
                        if (ID2D == 1)
                            myMap.changeIDAt(line, column, 2);
                        else
                            myMap.changeIDAt(line, column, 1);
                    }
                }

            }

            lastMouseState = newMouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int line = 0; line < myMap.mapHeight; line++)
            {
                for (int column = 0; column < myMap.mapWidth; column++)
                {
                    int id = myMap.getID(line,column);
                    if (id > 0)
                    {
                        int x = column * myMap.tileWidth2D;
                        int y = line * myMap.tileHeight2D;
                        Texture2D tx = lstTextures2D[id - 1];
                        spriteBatch.Draw(tx, new Vector2(map2D_origin.X + x, map2D_origin.Y + y));
                    }
                }
            }

            for (int line = 0; line < myMap.mapHeight; line++)
            {
                for (int column = 0; column < myMap.mapWidth; column++)
                {
                    int id = myMap.getID(line, column);
                    if (id > 0)
                    {
                        Vector2 pos = mapTween[line, column].currentPos;
                        pos = myMap.To3D(pos);
                        pos += map3D_origin;
                        Texture2D tx = lstTextures3D[id - 1];
                        spriteBatch.Draw(tx, pos);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
