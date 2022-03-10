using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Jump_King_Clone.Controls
{
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;

        private bool _isHovering;

        private MouseState _previousMouse;

        #endregion

        #region Properties

        public EventHandler Click;

        public Text Text { get; set; }

        public bool Clicked { get; private set; }

        public Vector2 Position => Text.Position;

        public Rectangle Rectangle => Text.Rectangle;

        #endregion

        #region Methods

        public Button(Text text)
        {
            Text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Text.Color = Color.White;

            if (_isHovering)
                Text.Color = Color.DarkTurquoise;

            Text.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Text.Update(gameTime);

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
