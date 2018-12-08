using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAdventure.MacOS
{
    interface CharacterInterface
    {

        void update(KeyboardState kb, String dir, bool attacking);

        void draw(SpriteBatch sb);

    }
}
