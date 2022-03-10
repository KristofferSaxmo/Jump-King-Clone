using Jump_King_Clone.Input;
using Microsoft.Xna.Framework;
using System;

namespace Jump_King_Clone.Sprites.Player
{
    public class PlayerChargeState : PlayerBaseState
    {
        private const double JumpChargePerSecond = 0.02;
        private const double MINJumpCharge = 0.1;
        private const double MAXJumpCharge = MINJumpCharge + JumpChargePerSecond * 35;
        private const int JumpChargeMultiplier = 95;

        public override void EnterState(PlayerStateManager player)
        {
            player.Velocity = new Vector2(0, player.Velocity.Y);
            player.AnimationManager.Play(player.Animations["Charge"]);
            player.JumpCharge = MINJumpCharge;
        }

        public override void UpdateState(PlayerStateManager player)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;

            if (player.JumpCharge < MAXJumpCharge)
                Charge(player, keyboard);

            CheckForStateChange(player, keyboard);
        }

        private void Charge(PlayerStateManager player, FlatKeyboard keyboard)
        {
            player.JumpCharge += JumpChargePerSecond;
            if (keyboard.IsKeyDown(player.Input.Left))
            {
                player.Angle = MathHelper.ToRadians(180f + (float)player.JumpCharge * JumpChargeMultiplier);
            }
            else if (keyboard.IsKeyDown(player.Input.Right))
            {
                player.Angle = MathHelper.ToRadians(0f - (float)player.JumpCharge * JumpChargeMultiplier);
            }
        }

        private void CheckForStateChange(PlayerStateManager player, FlatKeyboard keyboard)
        {
            if (player.Velocity.Y > 1)
            {
                player.SwitchState(player.FallState);
            }

            if (!keyboard.IsKeyDown(player.Input.Jump) || player.JumpCharge == MAXJumpCharge)
                player.SwitchState(player.JumpState);
        }

        public override void OnCollision(PlayerStateManager player, Sprite sprite)
        {

        }
    }
}