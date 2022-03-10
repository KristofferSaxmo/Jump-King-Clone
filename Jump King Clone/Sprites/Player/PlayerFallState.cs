using Jump_King_Clone.Input;
using Microsoft.Xna.Framework;
using System;

namespace Jump_King_Clone.Sprites.Player
{
    public class PlayerFallState : PlayerBaseState
    {
        private int _stunCooldown = 0;
        public override void EnterState(PlayerStateManager player)
        {
            if (player.AnimationManager.CurrentAnimation != player.Animations["Falling"])
                player.AnimationManager.Play(player.Animations["JumpDown"]);
        }

        public override void UpdateState(PlayerStateManager player)
        {
            if (_stunCooldown > 0)
                _stunCooldown--;
        }

        public override void OnCollision(PlayerStateManager player, Sprite sprite)
        {
            if (player.IsTouchingLeft(sprite) || player.IsTouchingRight(sprite))
            {
                player.Velocity = new Vector2(-player.ContactVelocity.X * 0.4f, player.Velocity.Y);
                player.AnimationManager.Play(player.Animations["Falling"]);
            }

            FlatKeyboard keyboard = FlatKeyboard.Instance;
            if (player.IsTouchingTop(sprite))
            {
                if (player.ContactVelocity.Y >= 40)
                {
                    player.AnimationManager.Play(player.Animations["Fallen"]);
                    _stunCooldown = 60;
                }
                if (_stunCooldown == 0)
                {
                    if (keyboard.IsKeyDown(player.Input.Jump))
                    {
                        player.SwitchState(player.ChargeState);
                    }

                    if (keyboard.IsKeyDown(player.Input.Left) || keyboard.IsKeyDown(player.Input.Right))
                    {
                        player.SwitchState(player.WalkState);
                    }

                    player.SwitchState(player.IdleState);
                }
            }
        }
    }
}
