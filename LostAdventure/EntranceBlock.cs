using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class EntranceBlock : Block
    {
        private Rectangle box;
        private String blockName;
        private Rectangle source;
        private String blockType;
        private Block[,] floor;
        private Rectangle coll;
        private int xSet, ySet;

        public EntranceBlock(Rectangle box, Rectangle coll, String blockName, Rectangle source, String blockType, Block[,] floor)
            : base(box, coll, blockName, source, blockType)
        {
            this.box = box;
            this.blockName = blockName;
            this.source = source;
            this.floor = floor;

        }

        public void setSet(int x, int y)
        {
            xSet = x;
            ySet = y;
        }

        public void draw(SpriteBatch sb, Texture2D spriteSh)
        {
            sb.Draw(spriteSh, box, source, Color.White);
            base.draw(sb, spriteSh);
        }

        public void addToStack(Stack<StateManager> states, Stack<Block[,]> world, Camera camera, int x, int y, Player player)
        {
            //setSet(x, y);
            states.Push(StateManager.IN_BUILDING);
            world.Push(floor);//look for exit block
            for (int r = 0; r < floor.GetLength(0); r++)
            {
                for (int c = 0; c < floor.GetLength(1); c++)
                {
                    if (floor[r, c].getBlockType().Equals(Constants.exitBlockKey))
                    {
                        ((ExitBlock)(floor[r, c])).setOffSet(x, y);
                        Block b = floor[r, c];
                        Rectangle pos = b.getBounds();//-149 -33

                        int xP = pos.X / 8;
                        int yP = pos.Y / 13;
                        //int pX = player.getXPos();// / Constants.BLOCK_SIZE;
                        //int pY = player.getYPos();// / Constants.BLOCK_SIZE;



                        camera.setOffSet(-xP, -yP);
                        //camera.setXOffSet(-x);
                        //camera.setYOffSet(-y);
                        //camera.setOffSet(xSet, ySet);
                    }
                }
            }
        }

        public void print()
        {
            Console.WriteLine("Entrance Block");
            Console.WriteLine("\n");
            base.print();
        }
    }
}
