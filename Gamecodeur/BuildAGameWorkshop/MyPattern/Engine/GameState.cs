using MyPattern.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	public class GameState
	{
		public enum SceneType { Menu, Game, GameOver }

		protected MainGame game;
		public Scene CurrentScene { get; protected set; }

        public GameState(MainGame game)
        {
            this.game = game;
        }

		public void ChangeScene(SceneType sceneType)
		{
            if (CurrentScene is not null)
            {
                CurrentScene.Unload();
                CurrentScene = null;
            }

			switch (sceneType)
			{
				case SceneType.Menu:
					CurrentScene = new SceneMenu(game);
					break;
				case SceneType.Game:
					CurrentScene = new SceneGame(game);
					break;
				case SceneType.GameOver:
					CurrentScene = new SceneGameOver(game);
					break;
				default:
					break;
			}

			CurrentScene?.Load();
		}
	}
}
