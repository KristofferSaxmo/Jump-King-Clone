using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jump_King_Clone.Input;
using Jump_King_Clone.Models;
using System.Collections.Generic;
using Jump_King_Clone.Interfaces;
using Jump_King_Clone.Managers;

namespace Jump_King_Clone.Sprites.Player
{
    public class PlayerStateManager : Sprite, ICollidableAndMovable
    {
        public InputKeys Input { get; set; }

        public AnimationManager AnimationManager
        {
            get { return _animationManager; }
            set { _animationManager = value; }
        }

        public Dictionary<string, Animation> Animations
        {
            get { return _animations; }
        }

        public double JumpCharge { get; set; }

        public PlayerBaseState _currentState { get; private set; }
        public PlayerIdleState IdleState = new PlayerIdleState();
        public PlayerWalkState WalkState = new PlayerWalkState();
        public PlayerChargeState ChargeState = new PlayerChargeState();
        public PlayerJumpState JumpState = new PlayerJumpState();
        public PlayerFallState FallState = new PlayerFallState();


        public PlayerStateManager(Dictionary<string, Animation> animations) : base(animations)
        {
            _currentState = IdleState;
            _currentState.EnterState(this);
        }

        public void SwitchState(PlayerBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            if (Velocity.Y < 40)
                Velocity = new Vector2(Velocity.X, Velocity.Y + 1); // Gravity

            _currentState.UpdateState(this);

            UpdateRectangle();
            UpdateHitbox();
        }

        public void UpdateRectangle()
        {
            Rectangle = new Rectangle(Position.ToPoint(), new Point(32 * Scale, 32 * Scale));
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X + 7 * Scale, (int)Position.Y + 6 * Scale, 32 * Scale - 14 * Scale, 32 * Scale - 6 * Scale);
        }

        public void OnCollision(Sprite sprite)
        {
            _currentState.OnCollision(this, sprite);
        }
    }
}
