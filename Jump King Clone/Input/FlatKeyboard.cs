using Microsoft.Xna.Framework.Input;
using System;

namespace Jump_King_Clone.Input
{
    public sealed class FlatKeyboard
    {
        private static readonly Lazy<FlatKeyboard> Lazy = new Lazy<FlatKeyboard>(() => new FlatKeyboard());

        public static FlatKeyboard Instance
        {
            get { return Lazy.Value; }
        }

        private KeyboardState _prevKeyboardState;
        private KeyboardState _currentKeyboardState;

        public FlatKeyboard()
        {
            _prevKeyboardState = Keyboard.GetState();
            _currentKeyboardState = _prevKeyboardState;
        }

        public void Update()
        {
            _prevKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyClicked(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_prevKeyboardState.IsKeyDown(key);
        }
    }
}
