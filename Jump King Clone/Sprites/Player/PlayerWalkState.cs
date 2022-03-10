using Jump_King_Clone.Input;
using Microsoft.Xna.Framework;

namespace Jump_King_Clone.Sprites.Player
{
    public class PlayerWalkState : PlayerBaseState
    {
        public override void EnterState(PlayerStateManager player)
        {
            player.AnimationManager.Play(player.Animations["Walk"]);
        }

        public override void UpdateState(PlayerStateManager player)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;

            player.Velocity = new Vector2(0, player.Velocity.Y);

            Walk(player, keyboard);

            CheckForStateChange(player, keyboard);
        }

        private void Walk(PlayerStateManager player, FlatKeyboard keyboard)
        {
            if (keyboard.IsKeyDown(player.Input.Left))
            {
                if (keyboard.IsKeyDown(player.Input.Right))
                    return;

                player.Velocity = new Vector2(-6, player.Velocity.Y);
                player.IsFacingLeft = true;
            }

            if (keyboard.IsKeyDown(player.Input.Right))
            {
                player.Velocity = new Vector2(6, player.Velocity.Y);
                player.IsFacingLeft = false;
            }
        }

        private void CheckForStateChange(PlayerStateManager player, FlatKeyboard keyboard)
        {
            if (player.Velocity.Y > 1)
            {
                player.SwitchState(player.FallState);
            }

            if (keyboard.IsKeyDown(player.Input.Jump))
            {
                player.SwitchState(player.ChargeState);
            }

            if (!keyboard.IsKeyDown(player.Input.Left) && !keyboard.IsKeyDown(player.Input.Right))
            {
                player.SwitchState(player.IdleState);
            }
        }

        public override void OnCollision(PlayerStateManager player, Sprite sprite)
        {

        }
    }
}
