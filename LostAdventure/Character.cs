using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAdventure.MacOS
{
    public class Character : CharacterInterface
    {
        private Texture2D spriteSh;
        private Rectangle destination;
        private Rectangle source;
        private Rectangle collision;
        private int xPos, yPos;
        private int width, height;
        private int offsetX, offsetY;
        private Texture2D collImg;
        private Rectangle offsetRect;
        private SpriteSheet wR;
        private SpriteSheet wL;
        private SpriteSheet wU;
        private SpriteSheet wD;
        private AttackSpriteSheet bA, sA;
        private SpriteSheet currentChoice;
        private String movementDir = "RIGHT";

        public Character(Texture2D spriteSh, Rectangle destination, Rectangle source, Texture2D collImg)
        {
            this.spriteSh = spriteSh;
            this.destination = destination;
            this.source = source;
            collision = destination;
            offsetRect = destination;
            xPos = destination.X;
            yPos = destination.Y;
            width = destination.Width;
            height = destination.Height;
            this.collImg = collImg;
        }

        public void setAttacksSpriteSheet(AttackSpriteSheet bA, AttackSpriteSheet sA)
        {
            this.bA = bA;
            this.sA = sA;
        }

        public void setSpriteSheets(SpriteSheet wR, SpriteSheet wL, SpriteSheet wD, SpriteSheet wU)
        {
            this.wR = wR;
            this.wL = wL;
            this.wU = wU;
            this.wD = wD;
            currentChoice = wR;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public Texture2D getCollImg()
        {
            return collImg;
        }


        public void updateOffset(int x, int y)
        {
            offsetRect.X = destination.X - x;
            offsetRect.Y = destination.Y - y;
        }

        public Rectangle getOffsetRect()
        {
            return offsetRect;
        }

        public Rectangle getSource()
        {
            return source;
        }

        public int getXPos()
        {
            return xPos;
        }

        public int getYPos()
        {
            return yPos;
        }

        public Character()
        {

        }

        public Texture2D getSpriteSh()
        {
            return spriteSh;
        }

        public void setCollisionsRectangle(int x, int y)
        {
            collision.X = x;
            collision.Y = y;
        }

        public Rectangle getDestination()
        {
            if (currentChoice != null)
            {
                destination = currentChoice.getDestination();
            }
            return destination;
        }

        public Rectangle getBounds()
        {
            return collision;
        }

        public void update(KeyboardState kb, String dir, bool attacking)
        {
            bool start = false;
            if (currentChoice is AttackSpriteSheet)//currentchoice updatecycle compelte one full movement
            {
                if (((AttackSpriteSheet)(currentChoice)).getUpdateCycle() == false)
                {
                    start = true;
                }
            }
            //else
            //{
            //    if (currentChoice.getUpdateCycle() == false)
            //    {
            //        start = true;
            //    }
            //}
            if (start == false)
            {
                if (attacking == false)
                {
                    if (dir.Equals("RIGHT"))
                    {
                        wR.update(true, true);
                        currentChoice = wR;
                        movementDir = "RIGHT";
                    }
                    else if (dir.Equals("LEFT"))
                    {
                        wL.update(true, true);
                        currentChoice = wL;
                        movementDir = "LEFT";
                    }
                    else if (dir.Equals("UP"))
                    {
                        wU.update(true, true);
                        currentChoice = wU;
                        movementDir = "UP";
                    }
                    else if (dir.Equals("DOWN"))
                    {
                        wD.update(true, true);
                        currentChoice = wD;
                        movementDir = "DOWN";
                    }
                    else if (dir.Equals("STILL"))//make him auto walk out of cave while screen lights up
                    {
                        wR.update(false, false);
                        wL.update(false, false);
                        bA.update(false, false);
                        wU.update(false, false);
                        wD.update(false, false);
                        currentChoice.update(false, true);
                    }

                }
                else
                {
                    if (dir.Equals("BA"))
                    {
                        bA.update(true, true, movementDir);
                        currentChoice = bA;
                    }
                    else if (dir.Equals("SA"))
                    {
                        sA.update(true, true, movementDir);
                        currentChoice = sA;
                    }
                    else
                    {
                        bA.update(false, false, movementDir);
                        sA.update(false, false, movementDir);
                        ((AttackSpriteSheet)currentChoice).update(false, true, movementDir);
                    }
                }
            }
            else
            {
                if (currentChoice is AttackSpriteSheet)
                {
                    ((AttackSpriteSheet)currentChoice).update(true, true, movementDir);
                }
                else
                {
                    //    currentChoice.update(true, true);
                }

            }

        }

        public void draw(SpriteBatch sb)
        {
            //sb.Draw(spriteSh, destination, source, Color.White);
            if (currentChoice is AttackSpriteSheet)
            {
                ((AttackSpriteSheet)(currentChoice)).draw(sb);
            }
            else
            {
                currentChoice.draw(sb);
            }

        }
    }
}
