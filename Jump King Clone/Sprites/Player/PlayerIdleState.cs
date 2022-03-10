using Jump_King_Clone.Input;
using Microsoft.Xna.Framework;

namespace Jump_King_Clone.Sprites.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        public override void EnterState(PlayerStateManager player)
        {
            player.Velocity = new Vector2(0, player.Velocity.Y);
            if (player.AnimationManager.CurrentAnimation != player.Animations["Fallen"])
                player.AnimationManager.Play(player.Animations["Idle"]);
        }

        public override void UpdateState(PlayerStateManager player)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;
            CheckForStateChange(player, keyboard);
        }

        private void CheckForStateChange(PlayerStateManager player, FlatKeyboard keyboard)
        {

            if (keyboard.IsKeyDown(player.Input.Jump))
            {
                player.SwitchState(player.ChargeState);
            }

            if (keyboard.IsKeyDown(player.Input.Left) || keyboard.IsKeyDown(player.Input.Right))
            {
                player.SwitchState(player.WalkState);
            }
        }
        public override void OnCollision(PlayerStateManager player, Sprite sprite)
        {

        }
    }
}
