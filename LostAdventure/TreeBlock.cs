using System;
using Microsoft.Xna.Framework;

namespace LostAdventure.MacOS
{
    public class TreeBlock : DepthBlock
    {
        private bool drawInFront = false;
        private Rectangle playerRect;
        public TreeBlock(Rectangle box, Rectangle coll, String blockName, Rectangle source, String blockType)
         : base(box, coll, blockName, source, blockType)
        {

        }

        public bool shouldDrawInFront(int xOff, int yOff, bool colliding, bool bodyColliding)
        {
            if (base.getBlockName().Equals("115"))
            {
                int s = 5;
            }
            Player player = base.getPlayer();
            int x = player.getCollXPos() - xOff;
            int y = player.getCollYPos() - yOff;

            playerRect.X = x;
            playerRect.Y = y;
            playerRect.Width = player.getCollWidth();
            playerRect.Height = player.getCollHeight();

            Rectangle toCollideWith = base.getBounds();

            int bottomX = player.getXPos() - (xOff * Constants.BLOCK_SCALE);
            int bottomY = player.getYPos() - (yOff * Constants.BLOCK_SCALE) + player.getHeight();
            String blockName = base.getBlockName();

            if (blockName.Equals("45"))
            {
                int s = 5;
            }

            if (bottomY < toCollideWith.Y + toCollideWith.Height + toCollideWith.Height)
            {
                drawInFront = true;
            }
            else
            {
                drawInFront = false;
            }



            return drawInFront;
        }


    }
}
