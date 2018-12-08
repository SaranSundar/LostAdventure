using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class DepthBlock : Block
    {
        private bool drawInFront = false;
        private Rectangle playerRect;
        public DepthBlock(Rectangle box, Rectangle coll, String blockName, Rectangle source, String blockType)
        : base(box, coll, blockName, source, blockType)
        {

        }

        public bool shouldDrawInFront(int xOff, int yOff, bool colliding, bool bodyColliding)
        {

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

            if (base.getBlockName().Equals("60"))
            {
                int s = 5;
            }

            if (bottomY > toCollideWith.Y + toCollideWith.Height - 25)
            {
                drawInFront = false;
            }
            else
            {
                drawInFront = true;
            }



            return drawInFront;
        }

        public void drawWithOffset(SpriteBatch sb, Texture2D sh, int xOffSet, int yOffSet, Color c)
        {
            Rectangle offsetBox = base.getBounds();
            Point offsetPoint = base.getOffSetPoint();
            offsetPoint.X = xOffSet * base.getBlockConstant();
            offsetPoint.Y = yOffSet * base.getBlockConstant();
            offsetBox.Offset(offsetPoint);
            //Console.WriteLine("X " + xOffSet + " " + BLOCK_CONSTANT);
            //Console.WriteLine("BOX IS \n");
            //Console.WriteLine(box.ToString());
            sb.Draw(sh, offsetBox, base.getSource(), c);
        }
    }
}
