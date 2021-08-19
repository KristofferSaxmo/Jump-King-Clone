using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jump_King_Clone.Interfaces;
using Jump_King_Clone.Managers;
using Jump_King_Clone.Models;
using Jump_King_Clone.Sprites;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Jump_King_Clone.States
{
    public class GameState : State
    {

        public static int Score;

        #region Fields

        private KeyboardState _previousKeyboardState;
        private bool _showHitboxes = false;
        private GuiManager _guiManager;
        private List<Sprite> _sprites;
        private List<Player> _players;
        private SpriteFont _font;

        #endregion

        public GameState(Game1 game, ContentManager content, Texture2D defaultTex) : base(game, content, defaultTex)
        {

        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("misc/font");

            _guiManager = new GuiManager(_content, _defaultTex);

            _sprites = new List<Sprite>()
            {
                new Player(new Dictionary<string, Animation>()
                {
                    {
                        "Idle", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                            new int[] {0},
                                            new float[] {1f},
                                            32) { IsLooping = true }
                    },
                    {
                        "Walk", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                            new int[] {1, 2, 3, 2},
                                            new float[] {0.2f, 0.05f, 0.2f, 0.05f},
                                            32)
                    },
                    {
                        "Charge", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                              new int[] {4},
                                              new float[] {1f},
                                              32) { IsLooping = true }
                    },
                    {
                        "JumpUp", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                              new int[] {5},
                                              new float[] {1f},
                                              32) { IsLooping = true }
                    },
                    {
                        "JumpDown", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                                new int[] {6},
                                                new float[] {1f},
                                                32) { IsLooping = true }
                    },
                    {
                        "Fallen", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                              new int[] {7},
                                              new float[] {1f},
                                              32) { IsLooping = true }
                    },
                    {
                        "Falling", new Animation(_content.Load<Texture2D>("sprites/knight/base"),
                                               new int[] {8},
                                               new float[] {1f},
                                               32) { IsLooping = true }
                    },
                })
                {
                    Position = new Vector2(400, 500),
                    Input = new Input()
                    {
                        Jump = Keys.Space,
                        Left = Keys.Left,
                        Right = Keys.Right
                    },
                },

                new Platform(_defaultTex, new Vector2(300, 700), new Point(800, 200)),
                new Platform(_defaultTex, new Vector2(300, 400), new Point(100, 300))
            };

            _players = _sprites.OfType<Player>().ToList();
        }

        private void DetectCollisions()
        {
            var hitboxSprites = _sprites.Where(c => c is ICollidable) as Sprite[] ?? _sprites.Where(c => c is ICollidable).ToArray();
            foreach (var sprite in hitboxSprites)
            {
                if (sprite is ICollidableAndMovable collidableAndMovableSprite)
                    collidableAndMovableSprite.UpdateHitbox();
            }

            foreach (var spriteA in hitboxSprites)
            {
                foreach (var spriteB in hitboxSprites)
                {
                    if (spriteA == spriteB) continue;

                    if (spriteA.Intersects(spriteB))
                        ((ICollidable)spriteA).OnCollision(spriteB);
                }
                spriteA.Position += spriteA.Velocity;
            }
        }

        private void AddChildren()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];
                foreach (var child in sprite.Children)
                    _sprites.Add(child);

                sprite.Children = new List<Sprite>();
            }
        }

        private void RemoveSprites()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (!_sprites[i].IsRemoved) continue;

                _sprites.RemoveAt(i);
                i--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content, _defaultTex));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            AddChildren();

            DetectCollisions();

            RemoveSprites();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !_previousKeyboardState.IsKeyDown(Keys.Enter))
            {
                _showHitboxes = !_showHitboxes;
            }

            _previousKeyboardState = Keyboard.GetState();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);

            //Shows Hurtboxes. Only for testing
            if (_showHitboxes)
            {
                foreach (var sprite1 in _sprites.Where(c => c is ICollidable))
                {
                    var sprite = (ICollidable)sprite1;
                    spriteBatch.Draw(_defaultTex, ((Sprite)sprite).Hitbox, Color.Blue);
                }
            }

            else
            {
                foreach (var sprite in _sprites)
                {
                    sprite.Draw(gameTime, spriteBatch);
                }
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            spriteBatch.DrawString(_font, "Position: " + _players[0].Position.ToString(), new Vector2(20, 20), Color.White);

            spriteBatch.DrawString(_font, "Velocity: " + _players[0].Velocity.ToString(), new Vector2(20, 40), Color.White);

            spriteBatch.DrawString(_font, "Jump Charge: " + _players[0].jumpCharge.ToString(), new Vector2(20, 60), Color.White);

            // GUI

            spriteBatch.End();

        }
    }
}
