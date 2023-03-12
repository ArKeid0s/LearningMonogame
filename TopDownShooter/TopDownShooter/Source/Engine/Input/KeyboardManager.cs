using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TopDownShooter.Source.Engine.Input
{
	// TODO: this class needs improvement but good base
	internal class KeyboardManager
	{
		private KeyboardState _newKeyboard, _oldKeyboard;
		private List<Key> _pressedKeys = new(), _previousPressedKeys = new();

		public KeyboardManager()
		{
		}

		public virtual void Update()
		{
			_newKeyboard = Keyboard.GetState();

			GetPressedKeys();
		}

		public void UpdateOld()
		{
			_oldKeyboard = _newKeyboard;

			_previousPressedKeys = new List<Key>();
			foreach (var key in _pressedKeys)
			{
				_previousPressedKeys.Add(key);
			}
		}

		public bool IsPressed(string pressedKey)
		{
			foreach (var key in _pressedKeys)
			{
				if (key.key == pressedKey)
				{
					return true;
				}
			}

			return false;
		}

		public void GetPressedKeys()
		{
			_pressedKeys.Clear();
			foreach (var key in _newKeyboard.GetPressedKeys())
			{
				_pressedKeys.Add(new Key(key.ToString(), 1));
			}
		}
	}
}
