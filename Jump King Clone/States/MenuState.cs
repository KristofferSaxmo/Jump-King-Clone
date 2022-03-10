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

        public MenuState(Game1 game, ContentManager content) : base(game, content)
        {
        }

        public override void LoadContent()
        {
            _components = new List<Component>()
            {
                new Button(new Text("Start Game", 5, new Vector2(700, 400)))
                {
                    Click = new EventHandler(Button_Play_Clicked)
                },
                new Button(new Text("Exit Menu", 5, new Vector2(700, 500)))
                {
                    Click = new EventHandler(Button_Quit_Clicked)
                },
            };
        }

        private void Button_Play_Clicked(object sender, EventArgs args)
        {
            _game.SwitchState(new GameState(_game, _content));
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
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
