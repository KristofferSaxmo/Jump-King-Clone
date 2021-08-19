using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Jump_King_Clone.Controls;
using System;
using System.Collections.Generic;

namespace Jump_King_Clone.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, ContentManager content, Texture2D defaultTex) : base(game, content, defaultTex)
        {
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("misc/button");
            var buttonFont = _content.Load<SpriteFont>("misc/font");

            _components = new List<Component>()
            {
                new Button(buttonTexture, buttonFont)
                {
                    Text = "Play",
                    Position = new Vector2(Game1.ScreenWidth / 2, 400),
                    Click = new EventHandler(Button_Play_Clicked),
                    Layer = 0.1f
                },
                new Button(buttonTexture, buttonFont)
                {
                    Text = "Quit",
                    Position = new Vector2(Game1.ScreenWidth / 2, 450),
                    Click = new EventHandler(Button_Quit_Clicked),
                    Layer = 0.1f
                },
            };
        }

        private void Button_Play_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _content, _defaultTex));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
