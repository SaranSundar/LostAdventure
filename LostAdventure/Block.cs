using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace LostAdventure.MacOS
{
    public class Block : BlockInterface
    {
        private Rectangle box;
        private Rectangle offsetBox;
        private Rectangle source;
        private Rectangle coll;
        private String blockName;
        private String blockType;
        private Point offsetPoint;
        private int BLOCK_CONSTANT;
        private Player player;

        public Block(Rectangle box, Rectangle coll, String blockName, Rectangle source, String blockType)
        {
            this.box = box;
            this.blockName = blockName;
            this.source = source;
            this.blockType = blockType;
            this.coll = coll;
            offsetPoint = new Point(0, 0);
            BLOCK_CONSTANT = Constants.BLOCK_SCALE; ;//Constants.BLOCK_SIZE * Constants.BLOCK_SCALE;
            offsetBox = new Rectangle();
        }

        public int getBlockConstant()
        {
            return BLOCK_CONSTANT;
        }

        public String getBlockName()
        {
            return blockName;
        }

        public String getBlockType()
        {
            return blockType;
        }

        public void print()
        {
            Console.WriteLine("BLOCK TYPE IS: \n");
            Console.WriteLine(blockName);
            Console.WriteLine("Rectangle is \n");
            Console.WriteLine(box.ToString());
            Console.WriteLine("\n");
        }

        public Rectangle getSource()
        {
            return source;
        }

        public Rectangle getBounds()
        {
            return box;
        }

        public Point getOffSetPoint()
        {
            return offsetPoint;
        }

        public Rectangle getCollisionBounds(int xOffSet, int yOffSet)
        {
            offsetBox = coll;
            offsetPoint.X = xOffSet;
            offsetPoint.Y = yOffSet;
            offsetBox.Offset(offsetPoint);
            return offsetBox;
        }

        public void printName()
        {
            Console.WriteLine(blockType);
        }

        public void drawFromSpriteSheet(SpriteBatch sb, Texture2D sh)
        {
            sb.Draw(sh, box, source, Color.White);
        }

        public void drawWithOffset(SpriteBatch sb, Texture2D sh, int xOffSet, int yOffSet)
        {
            offsetBox = box;
            offsetPoint.X = xOffSet * BLOCK_CONSTANT;
            offsetPoint.Y = yOffSet * BLOCK_CONSTANT;
            offsetBox.Offset(offsetPoint);
            //Console.WriteLine("X " + xOffSet + " " + BLOCK_CONSTANT);
            //Console.WriteLine("BOX IS \n");
            //Console.WriteLine(box.ToString());
            sb.Draw(sh, offsetBox, source, Color.White);
        }

        public void drawWithOffset(SpriteBatch sb, Texture2D sh, int xOffSet, int yOffSet, Color c)
        {
            offsetBox = box;
            offsetPoint.X = xOffSet * BLOCK_CONSTANT;
            offsetPoint.Y = yOffSet * BLOCK_CONSTANT;
            offsetBox.Offset(offsetPoint);
            //Console.WriteLine("X " + xOffSet + " " + BLOCK_CONSTANT);
            //Console.WriteLine("BOX IS \n");
            //Console.WriteLine(box.ToString());
            sb.Draw(sh, offsetBox, source, c);
        }

        public void draw(SpriteBatch sb, Texture2D sh)
        {
            sb.Draw(sh, box, source, Color.White);
        }

        public void update(Player player)
        {
            this.player = player;
        }

        public Player getPlayer()
        {
            return player;
        }
    }
}
