using System;
namespace LostAdventure.MacOS
{
    public class ScreenPlaceHolder
    {
        private int xOff, yOff, pX, pY;
        private Block[,] level;

        public ScreenPlaceHolder()
        {

        }

        public Block[,] getLevel()
        {
            return level;
        }

        public void set(int xF, int yF, int xP, int yP, Block[,] b)
        {
            xOff = xF;
            yOff = yF;
            pX = xP;
            pY = yP;
            level = b;
        }

        public int getPX()
        {
            return pX;
        }

        public int getPY()
        {
            return pY;
        }

        public int getXOff()
        {
            return xOff;
        }

        public int getYOff()
        {
            return yOff;
        }


    }
}
