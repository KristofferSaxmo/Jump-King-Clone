using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jump_King_Clone.Managers;
using Jump_King_Clone.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jump_King_Clone.Sprites
{
    public class Sprite : Component, ICloneable
    {
        #region Fields

        protected Dictionary<string, Animation> _animations;
        protected Animation _animation;
        protected AnimationManager _animationManager;
        protected Texture2D Texture;
        protected int _scale = 4;
        protected Vector2 _position;

        #endregion

        #region Properties

        public List<Sprite> Children { get; set; }
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }
        public Vector2 Velocity { get; set; }
        public Vector2 ContactVelocity { get; set; }
        public float Angle { get; set; }
        public Color Color { get; set; }
        public bool IsRemoved { get; protected set; }
        public int Scale { get; set; }
        public bool IsFacingLeft { get; set; }
        public Rectangle Rectangle { get; set; }
        public Rectangle Hitbox { get; set; }
        public Sprite Parent;

        #endregion

        #region Methods

        public Sprite(Texture2D texture)
        {
            Texture = texture;

            Scale = 4;

            IsFacingLeft = false;

            Children = new List<Sprite>();

            Color = Color.White;
        }

        public Sprite(Animation animation)
        {
            Texture = null;

            Scale = 4;

            IsFacingLeft = false;

            Children = new List<Sprite>();

            _animation = animation;

            _animationManager = new AnimationManager(animation, Scale) { Color = Color.White };
        } // Animated sprite

        public Sprite(Dictionary<string, Animation> animations)
        {
            Texture = null;

            Scale = 4;

            IsFacingLeft = false;

            Children = new List<Sprite>();

            _animations = animations;

            var animation = _animations.FirstOrDefault().Value;

            _animationManager = new AnimationManager(animation, Scale) { Color = Color.White };
        } // Animated sprite dictionary

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Rectangle, Color);
            }

            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch, IsFacingLeft);
        }

        public object Clone()
        {
            var sprite = this.MemberwiseClone() as Sprite;

            if (_animations != null)
            {
                sprite._animations = this._animations.ToDictionary(c => c.Key, v => v.Value.Clone() as Animation);
                sprite._animationManager = sprite._animationManager.Clone() as AnimationManager;
            }

            return sprite;
        }

        #region Collision

        public bool Intersects(Sprite sprite)
        {
            ContactVelocity = Vector2.Zero;
            if (Velocity.X > 0 && IsTouchingLeft(sprite))
            {
                ContactVelocity = new Vector2(Velocity.X, ContactVelocity.Y);
                Velocity = new Vector2(sprite.Hitbox.Left - Hitbox.Right, Velocity.Y);
                return true;
            }

            if (Velocity.X < 0 & IsTouchingRight(sprite))
            {
                ContactVelocity = new Vector2(Velocity.X, ContactVelocity.Y);
                Velocity = new Vector2(sprite.Hitbox.Right - Hitbox.Left, Velocity.Y);
                return true;
            }
            if (Velocity.Y > 0 && IsTouchingTop(sprite))
            {
                ContactVelocity = new Vector2(ContactVelocity.X, Velocity.Y);
                Velocity = new Vector2(Velocity.X, sprite.Hitbox.Top - Hitbox.Bottom);
                return true;
            }
            if (Velocity.Y < 0 & IsTouchingBottom(sprite))
            {
                ContactVelocity = new Vector2(ContactVelocity.X, Velocity.Y);
                Velocity = new Vector2(Velocity.X, sprite.Hitbox.Bottom - Hitbox.Top);
                return true;
            }

            return Hitbox.Intersects(sprite.Hitbox);
        }

        public bool IsTouchingLeft(Sprite sprite)
        {
            return Hitbox.Right + Velocity.X >= sprite.Hitbox.Left &&
              Hitbox.Left < sprite.Hitbox.Left &&
              Hitbox.Bottom > sprite.Hitbox.Top &&
              Hitbox.Top < sprite.Hitbox.Bottom;
        }

        public bool IsTouchingRight(Sprite sprite)
        {
            return Hitbox.Left + Velocity.X <= sprite.Hitbox.Right &&
              Hitbox.Right > sprite.Hitbox.Right &&
              Hitbox.Bottom > sprite.Hitbox.Top &&
              Hitbox.Top < sprite.Hitbox.Bottom;
        }

        public bool IsTouchingTop(Sprite sprite)
        {
            return Hitbox.Bottom + Velocity.Y >= sprite.Hitbox.Top &&
              Hitbox.Top < sprite.Hitbox.Top &&
              Hitbox.Right > sprite.Hitbox.Left &&
              Hitbox.Left < sprite.Hitbox.Right;
        }

        public bool IsTouchingBottom(Sprite sprite)
        {
            return Hitbox.Top + Velocity.Y <= sprite.Hitbox.Bottom &&
              Hitbox.Bottom > sprite.Hitbox.Bottom &&
              Hitbox.Right > sprite.Hitbox.Left &&
              Hitbox.Left < sprite.Hitbox.Right;
        }

        #endregion

        #endregion
    }
}