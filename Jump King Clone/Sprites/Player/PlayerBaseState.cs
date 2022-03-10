using Jump_King_Clone.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jump_King_Clone.Sprites.Player
{
    public abstract class PlayerBaseState
    {
        public abstract void EnterState(PlayerStateManager player);

        public abstract void UpdateState(PlayerStateManager player);

        public abstract void OnCollision(PlayerStateManager player, Sprite sprite);
    }
}
