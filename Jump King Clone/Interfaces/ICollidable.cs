using Jump_King_Clone.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jump_King_Clone.Interfaces
{
    public interface ICollidable
    {
        void OnCollision(Sprite sprite);
    }
}
