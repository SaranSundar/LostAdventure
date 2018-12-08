using System;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    interface BlockInterface
    {
        void update(Player player);

        void draw(SpriteBatch sb, Texture2D sh);
    }
}
