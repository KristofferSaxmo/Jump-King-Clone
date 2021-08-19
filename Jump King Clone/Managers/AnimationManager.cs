using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jump_King_Clone.Models;
using System;

namespace Jump_King_Clone.Managers
{
    public class AnimationManager : ICloneable
    {
        private float _timer;

        private int _currentFrame;

        public Animation CurrentAnimation { get; private set; }

        public Color Color { get; set; }

        public float Layer { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }

        public int Scale { get; set; }

        public AnimationManager(Animation animation, int scale)
        {
            _currentFrame = 0;
            CurrentAnimation = animation;
            Scale = scale;
        }

        public void Play(Animation animation)
        {
            if (CurrentAnimation == animation)
                return;

            CurrentAnimation = animation;

            _currentFrame = 0;

            CurrentAnimation.CurrentFrame = CurrentAnimation.Frames[_currentFrame];

            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;

            CurrentAnimation = null;
        }

        public void Update(GameTime gameTime)
        {
            if (CurrentAnimation == null)
                return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_currentFrame == CurrentAnimation.Frames.Length)
            {
                Stop();
                return;
            }

            if (!(_timer > CurrentAnimation.FrameSpeed[_currentFrame])) return;

            _timer = 0f;

            _currentFrame++;

            if (_currentFrame < CurrentAnimation.Frames.Length)
                CurrentAnimation.CurrentFrame = CurrentAnimation.Frames[_currentFrame];

            if (_currentFrame < CurrentAnimation.Frames.Length - 1) return;

            if (CurrentAnimation.IsLooping)
            {
                _currentFrame = 0;
                CurrentAnimation.CurrentFrame = CurrentAnimation.Frames[0];
                return;
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool isFacingLeft)
        {
            if (CurrentAnimation == null)
                return;

            if (isFacingLeft)
            {
                spriteBatch.Draw(
              CurrentAnimation.Texture,
              Position,
              new Rectangle(
                CurrentAnimation.CurrentFrame * CurrentAnimation.FrameWidth,
                0,
                CurrentAnimation.FrameWidth,
                CurrentAnimation.FrameHeight
                ),
              Color,
              0f,
              Origin,
              Scale,
              SpriteEffects.FlipHorizontally,
              Layer
              );
                return;
            }

            spriteBatch.Draw(
              CurrentAnimation.Texture,
              Position,
              new Rectangle(
                CurrentAnimation.CurrentFrame * CurrentAnimation.FrameWidth,
                0,
                CurrentAnimation.FrameWidth,
                CurrentAnimation.FrameHeight
                ),
              Color,
              0f,
              Origin,
              Scale,
              SpriteEffects.None,
              Layer
              );
        }

        public object Clone()
        {
            var animationManager = this.MemberwiseClone() as AnimationManager;

            animationManager.CurrentAnimation = animationManager.CurrentAnimation.Clone() as Animation;

            return animationManager;
        }
    }
}
