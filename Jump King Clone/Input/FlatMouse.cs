using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jump_King_Clone.Input
{
    public sealed class FlatMouse
    {
        private static readonly Lazy<FlatMouse> Lazy = new Lazy<FlatMouse>(() => new FlatMouse());

        public static FlatMouse Instance
        {
            get { return Lazy.Value; }
        }

        private MouseState _prevMouseState;
        private MouseState _currentMouseState;

        public Point WindowPosition
        {
            get { return _currentMouseState.Position; }
        }

        public FlatMouse()
        {
            _prevMouseState = Mouse.GetState();
            _currentMouseState = _prevMouseState;
        }

        public void Update()
        {
            _prevMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        public bool IsLeftButtonDown()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightButtonDown()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleButtonDown()
        {
            return _currentMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool IsLeftButtonClicked()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed
                && _prevMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsRightButtonClicked()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed
                && _prevMouseState.RightButton == ButtonState.Released;
        }

        public bool IsMiddleButtonClicked()
        {
            return _currentMouseState.MiddleButton == ButtonState.Pressed
                && _prevMouseState.MiddleButton == ButtonState.Released;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(WindowPosition.X, WindowPosition.Y);
        }
    }
}
