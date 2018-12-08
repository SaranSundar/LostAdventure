using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class Player : Character
    {


        public Player(Texture2D spriteSh, Rectangle destination, Rectangle source, Texture2D collImg)
            : base(spriteSh, destination, source, collImg)
        {

            //base.create(spriteSh, destination, source);
        }

        public int getCollXPos()// x Value used to collide with middle of player and not images 0,0 origin
        {
            return base.getDestination().X + Constants.BLOCK_SCALE;
        }

        public int getCollYPos()// y Value used to collide with middle of player
        {
            return base.getDestination().Y + base.getDestination().Height - (Constants.playerFeetHeight * Constants.BLOCK_SCALE);
        }

        public int getCollWidth()
        {
            return base.getDestination().Width;
        }

        public int getCollHeight()
        {
            return base.getDestination().Height;
        }

        public void draw(SpriteBatch sb)
        {
            base.draw(sb);
        }
    }
}
