using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Jump_King_Clone.States;
using System;

namespace Jump_King_Clone
{
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public static Random Random = new Random();

        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;

        private State _currentState;
        private State _nextState;

        private Texture2D _defaultTex;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _defaultTex = new Texture2D(GraphicsDevice, 1, 1);
            _defaultTex.SetData(new Color[1] { Color.White });

            _currentState = new MenuState(this, Content, _defaultTex);
            _currentState.LoadContent();
            _nextState = null;
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }

            _currentState.Update(gameTime);

            base.Update(gameTime);
        }
        public void ChangeState(State state)
        {
            _nextState = state;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
