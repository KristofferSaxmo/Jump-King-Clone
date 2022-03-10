using Jump_King_Clone.Input;
using Microsoft.Xna.Framework;
using System;

namespace Jump_King_Clone.Sprites.Player
{
    public class PlayerJumpState : PlayerBaseState
    {

        private const int JumpPower = 42;

        private Vector2 _direction;

        public override void EnterState(PlayerStateManager player)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;
            player.AnimationManager.Play(player.Animations["JumpUp"]);
            Jump(player, keyboard);
        }

        public override void UpdateState(PlayerStateManager player)
        {
            CheckForStateChange(player);
        }

        private void Jump(PlayerStateManager player, FlatKeyboard keyboard)
        {
            /*
            * Jump straight up
            */
            if (!(keyboard.IsKeyDown(player.Input.Left) ^ keyboard.IsKeyDown(player.Input.Right))) // If both or none of Left and Right are pressed
            {
                player.Velocity = new Vector2(0, (float)player.JumpCharge * -50);
                return;
            }

            /*
            * Jump left
            */
            if (keyboard.IsKeyDown(player.Input.Left))
                player.IsFacingLeft = true;

            /*
             * Jump right
             */
            if (keyboard.IsKeyDown(player.Input.Right))
                player.IsFacingLeft = false;


            _direction = new Vector2((float)Math.Cos(player.Angle), (float)Math.Sin(player.Angle));
            _direction *= (float)player.JumpCharge * JumpPower;
            player.Velocity = _direction;
        }

        private void CheckForStateChange(PlayerStateManager player)
        {
            if (player.Velocity.Y > 0)
            {
                player.SwitchState(player.FallState);
            }
        }

        public override void OnCollision(PlayerStateManager player, Sprite sprite)
        {
            if (player.IsTouchingLeft(sprite) || player.IsTouchingRight(sprite))
            {
                player.Velocity = new Vector2(-player.ContactVelocity.X * 0.6f, player.Velocity.Y);
                player.AnimationManager.Play(player.Animations["Falling"]);
            }
        }
    }
}
