using Jump_King_Clone.Sprites.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
//using Jump_King_Clone.Sprites.GUI;

namespace Jump_King_Clone.Managers
{
    public class GuiManager
    {
        private Texture2D _circle, _circleIndicator;
        private Text _positionText, _velocityText, _jumpChargeText, _jumpAngleText, _stateText;
        private float _circleIndicatorAngle;

        public GuiManager(ContentManager content, Texture2D defaultTex)
        {
            _circle = content.Load<Texture2D>("misc/circle");

            _circleIndicator = content.Load<Texture2D>("misc/circleIndicator");

            _positionText = new Text("", 2, new Vector2(20, 20));
            _velocityText = new Text("", 2, new Vector2(20, 40));
            _jumpChargeText = new Text("", 2, new Vector2(20, 60));
            _jumpAngleText = new Text("", 2, new Vector2(55, 80));
            _stateText = new Text("", 3, new Vector2(20, 100));
        }

        public void Update(GameTime gameTime, PlayerStateManager player)
        {
            _positionText.SetContent("Position: " + player.Position.ToString());

            _velocityText.SetContent("Velocity: " + player.Velocity.ToString());

            _jumpChargeText.SetContent("Jump Charge: " + player.JumpCharge.ToString());

            _jumpAngleText.SetContent((MathHelper.ToDegrees(player.Angle) + 90).ToString());
                
            _circleIndicatorAngle = player.Angle + MathHelper.ToRadians(90f);

            _stateText.SetContent(player._currentState.GetType().ToString());
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _positionText.Draw(gameTime, spriteBatch);
            _velocityText.Draw(gameTime, spriteBatch);
            _jumpChargeText.Draw(gameTime, spriteBatch);
            _jumpAngleText.Draw(gameTime, spriteBatch);
            _stateText.Draw(gameTime, spriteBatch);

            spriteBatch.Draw(_circle, new Vector2(20, 80), Color.White);

            spriteBatch.Draw(_circleIndicator, new Vector2(36, 96), new Rectangle(0, 0, 32, 32), Color.Red, _circleIndicatorAngle, new Vector2(16, 16), 1, SpriteEffects.None, 1);
        }
    }
}