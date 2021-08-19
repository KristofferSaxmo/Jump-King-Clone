﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jump_King_Clone.Models;
using System.Collections.Generic;
using Jump_King_Clone.Interfaces;
using System;

namespace Jump_King_Clone.Sprites
{
    public class Player : Sprite, ICollidableAndMovable
    {
        private KeyboardState _currentKey;

        private Vector2 _direction;

        public double jumpCharge;

        private bool _inAir, _hitInAir, _fallen;

        public Input Input { get; set; }

        public Player(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        private void Move()
        {
            if (Velocity.Y < 40)
            Velocity = new Vector2(Velocity.X, Velocity.Y + 1); // Gravity

            if (Velocity.Y > 1)
                _inAir = true;

            if (_inAir) return;

            Velocity = new Vector2(0, Velocity.Y);

            if (_currentKey.IsKeyDown(Input.Jump) || jumpCharge > 0)
            {
                Jump();
                return;
            }
            
            Walk();
        }

        private void Jump()
        {
            /*
             * Charge Jump
             */
            if (_currentKey.IsKeyDown(Input.Jump) && jumpCharge < 0.7)
            {
                jumpCharge += 0.02;
                return;
            }

            /*
             * Jump straight up
             */
            if (!_currentKey.IsKeyDown(Input.Left) && !_currentKey.IsKeyDown(Input.Right))
            {
                Velocity = new Vector2(0, (float)jumpCharge * -50);
                _inAir = true;
                jumpCharge = 0;
                return;
            }

            /*
             * Jump left
             */
            if (_currentKey.IsKeyDown(Input.Left))
            {
                if (_currentKey.IsKeyDown(Input.Right)) return;
                Velocity = new Vector2((float)jumpCharge * -10, (float)jumpCharge * -50);
                _isFacingLeft = true;
                _inAir = true;
                jumpCharge = 0;
            }

            /*
             * Jump right
             */
            if (_currentKey.IsKeyDown(Input.Right))
            {
                Velocity = new Vector2((float)jumpCharge * 10, (float)jumpCharge * -50);
                _isFacingLeft = false;
                _inAir = true;
                jumpCharge = 0;
            }
        }

        private void Walk()
        {
            /*
             * Walk left
             */
            if (_currentKey.IsKeyDown(Input.Left))
            {
                if (_currentKey.IsKeyDown(Input.Right)) return;
                Velocity = new Vector2(-6, Velocity.Y);
                _isFacingLeft = true;
            }

            /*
             * Walk right
             */
            if (_currentKey.IsKeyDown(Input.Right))
            {
                Velocity = new Vector2(6, Velocity.Y);
                _isFacingLeft = false;
            }
        }

        private void ChangeAnimation()
        {
            if (_inAir)
            {
                if (_hitInAir)
                    _animationManager.Play(_animations["Falling"]);

                else if (Velocity.Y < 0)
                    _animationManager.Play(_animations["JumpUp"]);

                else if (Velocity.Y > 0)
                    _animationManager.Play(_animations["JumpDown"]);

                return;
            }

            if (jumpCharge > 0)
            {
                _animationManager.Play(_animations["Charge"]);
                return;
            }

            if (Velocity.X != 0) _fallen = false;

            if (_fallen)
            {
                _animationManager.Play(_animations["Fallen"]);
                return;
            }

            if (Velocity.X == 0)
            {
                _animationManager.Play(_animations["Idle"]);
                return;
            }

            _animationManager.Play(_animations["Walk"]);
        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            _currentKey = Keyboard.GetState();

            Move();
            UpdateRectangle();
            ChangeAnimation();
        }

        public void UpdateRectangle()
        {
            Rectangle = new Rectangle(Position.ToPoint(), new Point(32 * Scale, 32 * Scale));
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)Position.X + 7 * Scale, (int)Position.Y + 6 * Scale, 32 * Scale - 14 * Scale, 32 * Scale - 6 * Scale);
            if (Hitbox.Bottom >= 670 && Velocity.Y == 30)
            {

            }
        }

        public void OnCollision(Sprite sprite)
        {
            if (IsTouchingTop(sprite))
            {
                if (_contactVelocity.Y == 40)
                {
                    _fallen = true;
                    _contactVelocity.Y = 0;
                }
                _inAir = false;
                _hitInAir = false;
                return;
            }

            if ((IsTouchingLeft(sprite) || IsTouchingRight(sprite)) && _inAir)
            {
                Velocity = new Vector2(-_contactVelocity.X * 0.4f, Velocity.Y);
                _hitInAir = true;
                return;
            }
        }
    }
}