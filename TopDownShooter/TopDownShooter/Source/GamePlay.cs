using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDownShooter.Source.Gameplay;

namespace TopDownShooter.Source
{
    public class GamePlay
	{
        int playState;
        World _world;

        public GamePlay()
        {
            playState = 0;

            ResetWorld(null);
        }

        public void Update()
        {
            if (playState == 0)
            {
                _world.Update();
            }
        }

        public void ResetWorld(object info)
        {
            _world = new World(ResetWorld);
        }

        public void Draw()
        {
            if (playState == 0)
            {
                _world.Draw(Vector2.Zero);
            }
        }

    }
}
