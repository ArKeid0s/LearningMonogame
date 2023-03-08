using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
	public delegate void OnClick(Button button);

	public class Button : Sprite
	{
		public bool IsHovered { get; private set; }
		private MouseState _oldMouseState;
		public OnClick OnClick { get; set; }

		public Button(Texture2D texture) : base(texture)
		{
		}

		public override void Update(GameTime gameTime)
		{
			MouseState newMouseState = Mouse.GetState();
			Point mousePos = newMouseState.Position;

			if (Bounds.Contains(mousePos))
            {
                if (!IsHovered)
                {
                    IsHovered = true;
                }
            }
			else
			{
				IsHovered = false;
			}

            if (IsHovered)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton != ButtonState.Pressed)
                {
					Debug.WriteLine("Button pressed");
                    if (OnClick is not null)
                    {
						OnClick(this);
                    }
                }
            }

            _oldMouseState = newMouseState;
			base.Update(gameTime);
		}
	}
}
