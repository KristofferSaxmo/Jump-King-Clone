using Jump_King_Clone.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jump_King_Clone.Sprites
{
    public class Platform : Sprite, ICollidable
    {
        public Platform(Texture2D texture, Vector2 position, Point size) : base(texture)
        {
            Rectangle = new Rectangle(position.ToPoint(), size);
            Hitbox = Rectangle;
        }

        public void OnCollision(Sprite sprite)
        {

        }
    }
}
