using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class ExitBlock : Block
    {
        private Rectangle box;
        private String blockName;
        private Rectangle source;
        private String blockType;
        private Rectangle coll;
        private int xOff, yOff;

        public ExitBlock(Rectangle box, Rectangle coll, String blockName, Rectangle source, String blockType)
            : base(box, coll, blockName, source, blockType)
        {
            this.box = box;
            this.blockName = blockName;
            this.source = source;

        }


        public void draw(SpriteBatch sb, Texture2D spriteSh)
        {
            sb.Draw(spriteSh, box, source, Color.White);
            base.draw(sb, spriteSh);
        }

        public void setOffSet(int x, int y)
        {
            xOff = x;
            yOff = y;
        }

        public void removeFromStack(Stack<StateManager> states, Stack<Block[,]> world, Camera camera)
        {
            //if colliding and facing the right direction, remove floor from stack
            states.Pop();
            world.Pop();//look for exit block
                        //for (int r = 0; r < world.Peek().GetLength(0); r++)
                        // {
                        //for (int c = 0; c < world.Peek().GetLength(1); c++)
                        // {
                        //   if ((world.Peek())[r, c].getBlockName().Equals("" + Constants.entranceBlockKey))
                        //  {
                        //Block b = (world.Peek())[r, c];
                        //int x = b.getBounds().X / Constants.BLOCK_SCALE;
                        //int y = b.getBounds().Y / Constants.BLOCK_SCALE;
                        //x = x - Constants.BLOCK_SIZE;

            //camera.setXOffSet(-x);
            //camera.setYOffSet(-y);
            camera.setOffSet(xOff, yOff);
            //    }
            //  }
            //}
        }

        public void print()
        {
            Console.WriteLine("Exit Block");
            Console.WriteLine("\n");
            base.print();
        }
    }
}
