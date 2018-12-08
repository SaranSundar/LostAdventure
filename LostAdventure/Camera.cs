using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAdventure.MacOS
{
    public class Camera
    {
        private int row;
        private int col;
        private double xOffSet;
        private double yOffSet;
        private Block[,] currentLevel;
        private int blockConstant;
        private Keys lastPress;
        private bool debug = false;
        private int startX, startY;
        private Texture2D spriteSh;
        private Texture2D empty;
        private GraphicsDevice graphicsDevice;
        private Color debugColl;
        private Block currentBlock;
        private int prevX, prevY;
        private bool blockNull = false;
        private bool flashScreen = false;
        private int red = 0;
        private int green = 0;
        private Rectangle screen;
        private int blue = 0;
        private bool callMethod = false;
        private Color flashColor;
        private int alpha = 150;
        private ScreenPlaceHolder screenPlaceHolderEntrance;
        private ScreenPlaceHolder screenPlaceHolderExit;
        private String dir;
        private String dirToMoveIn;
        private int flashMoved = 0;
        private List<Block> blocksToDrawOnTop;
        private bool collidingWithColor = false;
        private bool bodyCollidingWithColor = false;
        private bool attacking = false;
        //private Player player;
        //make sure starting offset matches players starting position!

        public Camera(int row, int col, int xOffSet, int yOffSet, Texture2D collSh, GraphicsDevice graphicsDevice, Texture2D empty)
        {
            spriteSh = collSh;
            this.row = row;
            this.col = col;
            this.empty = empty;
            this.xOffSet = xOffSet;
            this.graphicsDevice = graphicsDevice;
            this.yOffSet = yOffSet;
            flashColor = new Color(red, green, blue);
            screen = new Rectangle(0, 0, Constants.cameraWidthInBlocks * Constants.BLOCK_SIZE * Constants.BLOCK_SCALE, Constants.cameraHeightInBlocks * Constants.BLOCK_SIZE * Constants.BLOCK_SCALE);
            debugColl = Color.White;
            blockConstant = Constants.BLOCK_SIZE;// * Constants.BLOCK_SCALE;
            screenPlaceHolderEntrance = new ScreenPlaceHolder();
            screenPlaceHolderExit = new ScreenPlaceHolder();
            dir = "RIGHT";
            blocksToDrawOnTop = new List<Block>();

        }

        public void draw(SpriteBatch sb, Texture2D spriteSh, Player player, Texture2D topLayer)
        {
            //int startY = (int)(Math.Floor(((double)yOffSet / (double)blockConstant)));
            //int startX = (int)(Math.Floor((double)xOffSet / ((double)blockConstant)));

            int playerX = player.getCollXPos();
            int playerY = player.getCollYPos();
            String pos = getGridPosToCollideWith(playerX, playerY, (int)xOffSet, (int)yOffSet);
            String[] pl = pos.Split(' ');
            int gridX = Convert.ToInt32(pl[1]);
            int gridY = Convert.ToInt32(pl[0]);

            int sX = gridX - (col / 2);
            int sY = gridY - (row / 2);
            int fX = gridX + (col / 2);
            int fY = gridY + (row / 2);

            //Console.WriteLine("SX " + sX + " " + "SY" + sY);
            if (flashScreen == false)
            {
                int endX = startX + col;
                int endY = startY + row;

                for (int r = sY; r < fY; r++)
                {
                    for (int c = sX; c < fX; c++)
                    {
                        if (r >= 0 && c >= 0 && c < currentLevel.GetLength(1) && r < currentLevel.GetLength(0))
                        {
                            Block b = currentLevel[r, c];
                            b.drawWithOffset(sb, spriteSh, (int)xOffSet, (int)yOffSet, debugColl);
                            if (b is DepthBlock)
                            {
                                if ((b is TreeBlock))
                                {
                                    if (((TreeBlock)b).shouldDrawInFront((int)xOffSet, (int)yOffSet, collidingWithColor, bodyCollidingWithColor))
                                    {
                                        blocksToDrawOnTop.Add(b);
                                    }
                                }
                                else
                                {
                                    if (((DepthBlock)b).shouldDrawInFront((int)xOffSet, (int)yOffSet, collidingWithColor, bodyCollidingWithColor))
                                    {
                                        blocksToDrawOnTop.Add(b);
                                    }
                                }

                            }
                        }

                    }
                }
                player.draw(sb);// dlete this part once depth block is done
                //nowd raw list of blocks
                for (int i = 0; i < blocksToDrawOnTop.Count; i++)
                {
                    blocksToDrawOnTop[i].drawWithOffset(sb, topLayer, (int)xOffSet, (int)yOffSet, debugColl);
                }
                blocksToDrawOnTop.Clear();

            }
            else
            {
                int xOff = 0, yOff = 0;
                if (callMethod == false)
                {
                    startX = screenPlaceHolderEntrance.getPX();
                    startY = screenPlaceHolderEntrance.getPY();
                    currentLevel = screenPlaceHolderEntrance.getLevel();
                    xOff = screenPlaceHolderEntrance.getXOff();
                    yOff = screenPlaceHolderEntrance.getYOff();
                }
                else
                {
                    startX = screenPlaceHolderExit.getPX();
                    startY = screenPlaceHolderExit.getPY();
                    currentLevel = screenPlaceHolderExit.getLevel();
                    xOff = screenPlaceHolderExit.getXOff();
                    yOff = screenPlaceHolderExit.getYOff();
                }

                int endX = startX + col;
                int endY = startY + row;


                for (int r = sY; r < fY; r++)
                {
                    for (int c = sX; c < fX; c++)
                    {
                        if (r >= 0 && c >= 0 && c < currentLevel.GetLength(1) && r < currentLevel.GetLength(0))
                        {
                            Block b = currentLevel[r, c];
                            b.drawWithOffset(sb, spriteSh, (int)xOffSet, (int)yOffSet, debugColl);
                            if (b is DepthBlock)
                            {
                                if ((b is TreeBlock))
                                {
                                    if (((TreeBlock)b).shouldDrawInFront((int)xOffSet, (int)yOffSet, collidingWithColor, bodyCollidingWithColor))
                                    {
                                        blocksToDrawOnTop.Add(b);
                                    }
                                }
                                else
                                {
                                    if (((DepthBlock)b).shouldDrawInFront((int)xOffSet, (int)yOffSet, collidingWithColor, bodyCollidingWithColor))
                                    {
                                        blocksToDrawOnTop.Add(b);
                                    }
                                }

                            }
                        }
                    }
                }
                player.draw(sb);
                for (int i = 0; i < blocksToDrawOnTop.Count; i++)
                {
                    blocksToDrawOnTop[i].drawWithOffset(sb, topLayer, (int)xOffSet, (int)yOffSet, debugColl);
                }
                blocksToDrawOnTop.Clear();
                sb.Draw(empty, screen, flashColor);
                updateColor();


            }
        }

        public void updateWorld(KeyboardState kb, Player player)
        {
            int playerX = player.getCollXPos();
            int playerY = player.getCollYPos();
            int tempX = (int)xOffSet;
            int tempY = (int)yOffSet;
            dir = "STILL";
            bool allowMovement = false;

            if ((callMethod == true || flashScreen == true))
            {
                if (callMethod == true)
                {
                    if (flashColor.A < 100)
                    {
                        //allowMovement = true;
                    }
                }
            }
            else
            {
                allowMovement = true;
            }

            if (allowMovement == true)
            {
                bool notAttacking = false;
                if (kb.IsKeyDown(Keys.Z))
                {
                    dir = "BA";
                    attacking = true;
                    notAttacking = true;
                }
                else if (kb.IsKeyDown(Keys.X))
                {
                    dir = "SA";
                    attacking = true;
                    notAttacking = true;
                }

                if (notAttacking == false)
                {
                    if (kb.IsKeyDown(Keys.Up))
                    {
                        yOffSet = yOffSet + Constants.yMoveSpeed;
                        dir = "UP";
                        attacking = false;
                        dirToMoveIn = dir;

                    }
                    else if (kb.IsKeyDown(Keys.Down))
                    {
                        yOffSet = yOffSet - Constants.yMoveSpeed;
                        dir = "DOWN";
                        attacking = false;
                        dirToMoveIn = dir;
                    }

                    if (kb.IsKeyDown(Keys.Right))
                    {
                        xOffSet = xOffSet - Constants.xMoveSpeed;
                        dir = "RIGHT";
                        attacking = false;
                        dirToMoveIn = dir;
                    }
                    else if (kb.IsKeyDown(Keys.Left))
                    {
                        xOffSet = xOffSet + Constants.xMoveSpeed;
                        dir = "LEFT";
                        attacking = false;
                        dirToMoveIn = dir;
                    }

                    if (kb.IsKeyDown(Keys.D))
                    {
                        Console.WriteLine("XOFFSET: " + xOffSet + " " + "YOFFSETT: " + yOffSet);
                    }
                    if (kb.IsKeyDown(Keys.S))
                    {
                        Console.WriteLine("START Y: " + (int)(Math.Floor(((double)yOffSet / (double)blockConstant))) + " " + "START X: " + xOffSet / blockConstant);
                    }
                }


                collidingWithColor = false;
                if (collides(getGridPosToCollideWith(playerX, playerY, (int)xOffSet, tempY), player, (int)xOffSet, tempY, true) == true)
                {
                    xOffSet = tempX;
                    collidingWithColor = true;
                    //yOffSet = tempY;
                }
                if (collides(getGridPosToCollideWith(playerX, playerY, tempX, (int)yOffSet), player, tempX, (int)yOffSet, false) == true)
                {
                    //xOffSet = tempX;
                    collidingWithColor = true;
                    yOffSet = tempY;
                }
            }


            if (callMethod == false)
            {
                player.update(kb, dir, attacking);
            }
            else if (callMethod == true)
            {
                player.update(kb, dirToMoveIn, attacking);
            }
            updateBlocks(player);

        }

        public void updateBlocks(Player player)
        {
            for (int r = 0; r < currentLevel.GetLength(0); r++)
            {
                for (int c = 0; c < currentLevel.GetLength(1); c++)
                {
                    currentLevel[r, c].update(player);
                }
            }
        }

        public void update(Stack<Block[,]> level, KeyboardState kb, Player player, Stack<StateManager> states)
        {


            //this.player = player;

            if (currentBlock != null)
            {
                if (currentBlock is EntranceBlock)
                {
                    flashScreen = true;
                    screenPlaceHolderEntrance.set((int)xOffSet, (int)yOffSet, player.getXPos(), player.getYPos(), level.Peek());
                    ((EntranceBlock)currentBlock).addToStack(states, level, this, prevX, prevY, player);
                }
                else if (currentBlock is ExitBlock)
                {
                    flashScreen = true;
                    screenPlaceHolderEntrance.set((int)xOffSet, (int)yOffSet, player.getXPos(), player.getYPos(), level.Peek());
                    ((ExitBlock)currentBlock).removeFromStack(states, level, this);
                    //use for ExitBlock
                }
                currentBlock = null;
            }

            if (flashScreen == false || callMethod == true)
            {

                currentLevel = level.Peek();
                updateWorld(kb, player);
                startX = player.getXPos();
                startY = player.getYPos();
                if (callMethod == true)
                {
                    screenPlaceHolderExit.set((int)xOffSet, (int)yOffSet, player.getXPos(), player.getYPos(), level.Peek());
                }
            }
            else if (flashScreen == true && callMethod == false)
            {
                screenPlaceHolderExit.set((int)xOffSet, (int)yOffSet, player.getXPos(), player.getYPos(), level.Peek());
                //currentLevel = level.Peek();

            }


            //updates offset
        }

        public void updateColor()
        {
            //if (red < 15)
            //{
            //    red++;
            //}
            //else if (green < 255)
            //{
            //    green++;
            //}
            //else if (blue < 255)
            //{
            //    blue++;
            //}
            if (alpha + 4 < 255 && callMethod == false)
            {
                alpha = alpha + 4;//find dir you want to move in maybe opposite of entrance

            }
            else if (alpha - 4 > 0 && callMethod == true)
            {
                alpha = alpha - 4;
                simulateMovement(dirToMoveIn, flashMoved);
                flashMoved++;
            }
            else
            {
                //flashScreen = false;
                if (callMethod == false)
                {
                    callMethod = true;
                    alpha = 255;
                }
                else if (callMethod == true)
                {
                    flashScreen = false;
                    callMethod = false;
                    flashMoved = 0;

                    alpha = 150;
                }
                red = 0;
                green = 0;
                blue = 0;

            }
            flashColor.R = 0;//(byte)red;
            flashColor.G = 0;// (byte)green;
            flashColor.B = 0;// (byte)blue;
            flashColor.A = (byte)alpha;
        }

        public String getGridPosToCollideWith(int x, int y, int xOffSet, int yOffSet)
        {
            //Block pos = null;
            x = x / Constants.BLOCK_SCALE;
            y = y / Constants.BLOCK_SCALE;
            int nX = x - xOffSet;
            int nY = y - yOffSet;
            int xP = nX / (Constants.BLOCK_SIZE);
            int yP = nY / (Constants.BLOCK_SIZE);
            //Console.WriteLine("X POS IS: " + nX);
            //Console.WriteLine("X IS: " + " " + xP);
            //Console.WriteLine("Y POS IS: " + nY);
            //Console.WriteLine("Y IS: " + " " + yP);
            //Console.WriteLine("X D " + x);
            //Console.WriteLine("Y D IS " + y);
            String pos = yP + " " + xP;
            //pos = currentLevel[yP,xP];
            return pos;
        }

        public void simulateMovement(String dir, int moved)// do this for a max of 5 pix
        {
            if (moved < Constants.maxFlashMove)
            {
                if (dir.Equals("RIGHT"))
                {
                    xOffSet = xOffSet - Constants.xM;
                }
                else if (dir.Equals("LEFT"))
                {
                    xOffSet = xOffSet + Constants.xM;
                }
                else if (dir.Equals("UP"))
                {
                    yOffSet = yOffSet + Constants.yM;
                }
                else if (dir.Equals("DOWN"))
                {
                    yOffSet = yOffSet - Constants.yM;
                }
                else if (dir.Equals("STILL"))//make him auto walk out of cave while screen lights up
                {

                }
            }
            else
            {
                dirToMoveIn = "STILL";
            }

        }

        public bool collides(String pos, Player p, int xOffSet, int yOffSet, bool nullB)
        {
            String[] num = pos.Split(' ');
            int x = Convert.ToInt32(num[1]);
            int y = Convert.ToInt32(num[0]);
            //Console.WriteLine("X IS " + x);
            //Console.WriteLine("Y IS " + y);
            Block b = currentLevel[y, x];
            int rowLength = currentLevel.GetLength(0);
            int colLength = currentLevel.GetLength(1);
            p.updateOffset(xOffSet * Constants.BLOCK_SCALE, yOffSet * Constants.BLOCK_SCALE);
            bool collides = false;
            for (int r = y - 1; r <= y + 1; r++)
            {
                for (int c = x - 1; c <= x + 1; c++)
                {
                    if (c >= 0 && r >= 0 && c < colLength && r < rowLength)
                    {
                        //Console.WriteLine("C IS " + c + " R IS " + r);
                        if (checkCollisions(p, currentLevel[r, c]) == true)
                        {
                            collides = true;

                            currentBlock = currentLevel[r, c];
                            blockNull = false;

                            return collides;
                        }
                    }
                }
            }
            prevX = xOffSet;
            prevY = yOffSet;
            if (nullB)
            {
                blockNull = true;
            }
            if (blockNull == true)
            {
                currentBlock = null;
            }


            return collides;

        }

        private bool PixelCollisions(Texture2D sprite1, Texture2D sprite2, Rectangle player, Rectangle enemy, bool runCheck)
        {
            Color[] colorData1 = new Color[sprite1.Width * sprite1.Height];
            Color[] colorData2 = new Color[sprite2.Width * sprite2.Height];
            sprite1.GetData<Color>(colorData1);
            sprite2.GetData<Color>(colorData2);

            int top, bottom, left, right;
            top = Math.Max(player.Top, enemy.Top);
            bottom = Math.Min(player.Bottom, enemy.Bottom);
            left = Math.Max(player.Left, enemy.Left);
            right = Math.Min(player.Right, enemy.Right);

            for (int y = top; y < bottom; y++)
            {

                for (int x = left; x < right; x++)
                {

                    Color A = colorData1[(y - player.Top) * (player.Width) + (x - player.Left)];
                    Color B = colorData2[(y - enemy.Top) * (enemy.Width) + (x - enemy.Left)];
                    //Console.WriteLine("COLORS");
                    if (A.A != 0 && B.A != 0)
                    {
                        //Color c = new Color(A.R, A.G, A.B, A.A);
                        debugColl = Color.White;
                        if (runCheck == true)
                        {
                            bodyCollidingWithColor = true;
                        }
                        //Console.WriteLine("Color is " + Color.FromNonPremultiplied(B.ToVector4()).ToString());
                        return true;
                    }
                }
            }
            bodyCollidingWithColor = false;
            debugColl = Color.White;
            return false;

        }

        public static Texture2D Crop(Texture2D image, Rectangle source)
        {
            var graphics = image.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, source.Width, source.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret); // draw to image
            graphics.Clear(new Color(0, 0, 0, 0));

            graphics.SetRenderTarget(null); // set back to main window

            return (Texture2D)ret;
        }

        public bool checkCollisions(Player a, Block b)
        {
            Rectangle player = a.getSource();
            Rectangle enemy = b.getSource();

            int scale = Constants.BLOCK_SCALE;

            Rectangle playerPos = a.getOffsetRect();
            playerPos.X = playerPos.X / scale;
            playerPos.Y = playerPos.Y / scale;
            playerPos.Width = playerPos.Width / scale;
            playerPos.Height = playerPos.Height / scale;

            Rectangle enemyPos = b.getBounds();
            enemyPos.X = enemyPos.X / scale;
            enemyPos.Y = enemyPos.Y / scale;
            enemyPos.Width = enemyPos.Width / scale;
            enemyPos.Height = enemyPos.Height / scale;


            Rectangle playerColl = playerPos; //use this for destination for playerTexture
            //Rectangle playerCollDest = a.getOffsetRect();
            Rectangle enemyColl = enemyPos; //use this for destination for enemyTexture

            Texture2D playerTexture = CreatePartImage(player, a.getCollImg());
            Texture2D enemyTexture = CreatePartImage(enemy, spriteSh);
            Texture2D playerBody = CreatePartImage(player, a.getSpriteSh());
            PixelCollisions(playerBody, enemyTexture, playerColl, enemyColl, true);
            return PixelCollisions(playerTexture, enemyTexture, playerColl, enemyColl, false);


        }

        public Texture2D CreatePartImage(Rectangle bounds, Texture2D source)
        {

            Texture2D result;

            Color[]

                sourceColors,

                resultColors;
            result = new Texture2D(graphicsDevice, bounds.Width, bounds.Height);



            //Setup the color arrays

            sourceColors = new Color[source.Height * source.Width];

            resultColors = new Color[bounds.Height * bounds.Width];



            //Get the source colors

            source.GetData<Color>(sourceColors);



            //Loop through colors on the y axis

            for (int y = bounds.Y; y < bounds.Height + bounds.Y; y++)
            {

                //Loop through colors on the x axis

                for (int x = bounds.X; x < bounds.Width + bounds.X; x++)
                {

                    //Get the current color

                    resultColors[x - bounds.X + (y - bounds.Y) * bounds.Width] =

                        sourceColors[x + y * source.Width];

                }

            }

            result.SetData<Color>(resultColors);
            return result;

        }

        public void setOffSet(int prevX, int prevY)
        {
            xOffSet = prevX;
            yOffSet = prevY;
        }

        public void setXOffSet(int x)
        {
            xOffSet = x;
        }

        public void setYOffSet(int y)
        {
            yOffSet = y;
        }

    }
}
