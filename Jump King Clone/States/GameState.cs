using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Jump_King_Clone.Interfaces;
using Jump_King_Clone.Managers;
using Jump_King_Clone.Input;
using Jump_King_Clone.Models;
using Jump_King_Clone.Sprites;
using System.Collections.Generic;
using Jump_King_Clone.Sprites.Player;
using System.Linq;

namespace Jump_King_Clone.States
{
    public class GameState : State
    {

        public static int Score;

        #region Fields

        private bool _showHitboxes = false;
        private GuiManager _guiManager;
        private List<Sprite> _sprites;
        private List<PlayerStateManager> _players;

        #endregion

        public GameState(Game1 game, ContentManager content) : base(game, content)
        {

        }

        public override void LoadContent()
        {
            _guiManager = new GuiManager(_content, Game1.DefaultTexture);

            _sprites = new List<Sprite>()
            {
                new PlayerStateManager(new Dictionary<string, Animation>()
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
                                            32) { IsLooping = true }
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
                    Input = new InputKeys()
                    {
                        Jump = Keys.Space,
                        Left = Keys.Left,
                        Right = Keys.Right
                    },
                },

                new Platform(Game1.DefaultTexture, new Vector2(300, 700), new Point(800, 200)),
                new Platform(Game1.DefaultTexture, new Vector2(300, 400), new Point(100, 300))
            };

            _players = _sprites.OfType<PlayerStateManager>().ToList();
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
            FlatKeyboard.Instance.Update();
            FlatMouse.Instance.Update();

            FlatKeyboard keyboard = FlatKeyboard.Instance;

            if (keyboard.IsKeyDown(Keys.Escape))
                _game.SwitchState(new MenuState(_game, _content));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            _guiManager.Update(gameTime, _players[0]);

            AddChildren();

            DetectCollisions();

            RemoveSprites();

            if (keyboard.IsKeyClicked(Keys.Enter))
            {
                _showHitboxes = !_showHitboxes;
            }
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
                    spriteBatch.Draw(Game1.DefaultTexture, ((Sprite)sprite).Hitbox, Color.Blue);
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

            _guiManager.Draw(gameTime, spriteBatch);

            spriteBatch.End();

        }
    }
}
