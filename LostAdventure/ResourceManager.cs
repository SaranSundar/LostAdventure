using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class ResourceManager
    {
        private Game game;
        private Texture2D gameSpriteSheet;
        private Texture2D collSpriteSheet;
        private GraphicsDevice graphicsDevice;
        private List<String> listOfDepthBlocks;
        private List<String> listOfTreeBlocks;

        public ResourceManager(Game game)
        {
            this.game = game;
            graphicsDevice = game.GraphicsDevice;
            listOfDepthBlocks = Constants.createDepthBlocksKey("GameContent/TestWorld/TestLevel1/depthBlocksKey.txt");
            listOfTreeBlocks = Constants.createTreeBlocksKey("GameContent/TestWorld/TestLevel1/treeBlocksKey.txt");
        }

        public Texture2D getGameSpriteSheet()
        {
            gameSpriteSheet = loadImage("TestWorld/TestLevel1/spriteSheet");
            return gameSpriteSheet;
        }

        public Texture2D getCollSpriteSheet()
        {
            collSpriteSheet = loadImage("TestWorld/TestLevel1/collisionsSpriteSheet");
            return collSpriteSheet;
        }

        public Texture2D loadImage(String path)
        {
            Texture2D img = game.Content.Load<Texture2D>(@path);
            return img;
        }

        public int getOccurenceOfString(String stringToCheck, char occurenceValue)
        {
            int count = 0;
            int stringLength = stringToCheck.Length;
            for (int i = 0; i < stringLength; i++)
            {
                if (stringToCheck.Substring(i, 1).Equals(occurenceValue.ToString()))
                {
                    count++;
                }
            }
            return count + 1;
        }

        public String[,] readTextFile(String path)
        {
            String next = "";
            String fileAsString = "";
            int row = 0, col = 0;
            StreamReader kb = new StreamReader(@path);
            while ((next = kb.ReadLine()) != null)
            {
                String line = next;
                int lineLength = line.Length;
                int values = getOccurenceOfString(line, Constants.textFileValueSeperator);
                if (values > col)
                {
                    col = values;

                }

                fileAsString += line;
                fileAsString += Constants.textFileSeperator;
                row++;
            }
            String[,] file = new String[row, col];
            String[] linesInFile = fileAsString.Split(Constants.textFileSeperator);
            for (int r = 0; r < row; r++)
            {
                String line = linesInFile[r];
                String[] values = line.Split(Constants.textFileValueSeperator);
                int lineLength = values.Length;
                for (int c = 0; c < col; c++)
                {
                    if (lineLength > c)
                    {
                        file[r, c] = values[c];
                    }

                }
            }
            return file;
        }

        public Block[,] createLevel1()
        {
            String[,] Testlevel1File = readTextFile(Constants.testLevel1TextFilePath);
            Block[,] TestLevel1Blocks = makeBlocks(Testlevel1File);
            //printArray(Testlevel1File);
            return TestLevel1Blocks;
        }

        public Block[,] createLevelFromFile(String fileName)
        {
            String[,] Testlevel1File = readTextFile(fileName);
            Block[,] TestLevel1Blocks = makeBlocks(Testlevel1File);
            //printArray(Testlevel1File);
            return TestLevel1Blocks;
        }

        public Block[,] makeBlocks(String[,] level)
        {
            int row = level.GetLength(0);
            int col = level.GetLength(1);
            int blockSetup = Constants.BLOCK_SIZE * Constants.BLOCK_SCALE;
            int collBlockSetup = Constants.BLOCK_SIZE;

            Block[,] blocks = new Block[row, col];

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    String value = level[r, c];

                    Rectangle pos = new Rectangle(c * blockSetup, r * blockSetup, blockSetup, blockSetup);
                    Rectangle coll = new Rectangle(c * collBlockSetup, r * collBlockSetup, collBlockSetup, collBlockSetup);
                    if (value != null && value.Equals(" ") == false && value.Equals("") == false)
                    {
                        value = value.Trim();
                        Rectangle source = getSpriteSheetSource(value);
                        //Console.WriteLine("Source IS: \n");
                        //Console.WriteLine(source.ToString());
                        //Console.WriteLine("Pos Is: \n");
                        //Console.WriteLine(pos.ToString());
                        //Console.WriteLine("\n");
                        Block block = getTypeOfBlock(pos, coll, value, source, "To Be Filled");
                        blocks[r, c] = block;
                    }
                    else
                    {
                        //value = value.Trim();
                        value = "16";
                        Rectangle source = getSpriteSheetSource(value);//value for empty square
                        Block staticBlock = getTypeOfBlock(pos, coll, value, source, "To Be Filled");
                        blocks[r, c] = staticBlock;
                    }
                    //Console.Write(value);
                }
                //Console.WriteLine();
            }
            return blocks;
        }

        public Block getTypeOfBlock(Rectangle pos, Rectangle coll, String blockName, Rectangle source, String blockType)
        {
            //int blockNumber = Convert.ToInt32(blockName);
            if (blockName.Equals("115"))
            {
                int s = 5;
            }
            if (blockName.Contains(Constants.entranceBlockKey))
            {
                Block[,] floor = createLevelFromFile("GameContent/TestWorld/TestLevel1/House.txt");
                blockType = Constants.entranceBlockKey;
                EntranceBlock entranceBlock = new EntranceBlock(pos, coll, blockName, source, blockType, floor);
                return entranceBlock;
            }
            else if (blockName.Contains(Constants.exitBlockKey))
            {
                blockType = Constants.exitBlockKey;
                ExitBlock exitBlock = new ExitBlock(pos, coll, blockName, source, blockType);
                return exitBlock;
            }
            else if (listOfDepthBlocks.Contains(blockName))
            {
                blockType = Constants.depthBlockKey;
                DepthBlock depthBlock = new DepthBlock(pos, coll, blockName, source, blockType);
                return depthBlock;
            }
            else if (listOfTreeBlocks.Contains(blockName))
            {
                blockType = Constants.treeBlockKey;
                TreeBlock treeBlock = new TreeBlock(pos, coll, blockName, source, blockType);
                return treeBlock;
            }
            else
            {
                blockType = Constants.staticBlockKey;
                StaticBlock defaultBlock = new StaticBlock(pos, coll, blockName, source, blockType);
                return defaultBlock;
            }
        }

        public Player createPlayer()
        {
            Texture2D playerSpriteSheet = loadImage("TestWorld/TestLevel1/playerSpriteSheet");
            Rectangle destination = new Rectangle(Constants.playerSpawnX, Constants.playerSpawnY, Constants.characterWidth * Constants.BLOCK_SCALE, Constants.characterHeight * Constants.BLOCK_SCALE);
            Rectangle source = new Rectangle(0, 0, Constants.characterWidth, Constants.characterHeight);
            Texture2D collImg = loadImage("TestWorld/TestLevel1/playerColl");
            Player player = new Player(playerSpriteSheet, destination, source, collImg);
            createPlayerSpriteSheets(player);
            createPlayerAttackSpriteSheets(player);
            return player;
        }

        public void createPlayerAttackSpriteSheets(Player player)
        {
            List<Rectangle> sourceBAL = new List<Rectangle>();
            List<Rectangle> sourceBAR = new List<Rectangle>();
            List<Rectangle> sourceBAU = new List<Rectangle>();
            List<Rectangle> sourceBAD = new List<Rectangle>();

            //List<Rectangle> sourceSA = new List<Rectangle>();
            List<Rectangle> collBAL = new List<Rectangle>();
            List<Rectangle> collBAR = new List<Rectangle>();
            List<Rectangle> collBAU = new List<Rectangle>();
            List<Rectangle> collBAD = new List<Rectangle>();
            Texture2D attackSpriteSheet = loadImage("TestWorld/TestLevel1/playerAttacksSpriteSheet");
            //List<Rectangle> collSA = new List<Rectangle>();
            //27w28h
            int width = 27;
            int height = 28;
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Rectangle source = new Rectangle(c * width, (r * 2) * height, width, height);
                    Rectangle coll = new Rectangle(c * width, (r + 1) * height, width, height);
                    switch (r)
                    {
                        case 0:
                            sourceBAD.Add(source);
                            collBAD.Add(coll);
                            break;
                        case 1:
                            sourceBAR.Add(source);
                            collBAR.Add(coll);
                            break;
                        case 2:
                            sourceBAL.Add(source);
                            collBAL.Add(coll);
                            break;
                        case 3:
                            sourceBAU.Add(source);
                            collBAU.Add(source);
                            break;
                    }
                }
            }
            Rectangle destination = new Rectangle(player.getXPos(), player.getYPos(), width * Constants.BLOCK_SCALE, height * Constants.BLOCK_SCALE);

            AttackSpriteSheet basicAttackSpriteSheet = new AttackSpriteSheet(sourceBAR, collBAR, destination, attackSpriteSheet, 0, 5, 0);
            basicAttackSpriteSheet.setAttackSpriteSheets(sourceBAU, sourceBAD, sourceBAL, sourceBAR, collBAU, collBAD, collBAR, collBAL);
            player.setAttacksSpriteSheet(basicAttackSpriteSheet, basicAttackSpriteSheet);
        }

        public void createPlayerSpriteSheets(Player player)
        {
            List<Rectangle> sourcesWR = new List<Rectangle>();
            List<Rectangle> sourcesWL = new List<Rectangle>();
            List<Rectangle> sourcesWU = new List<Rectangle>();
            List<Rectangle> sourcesWD = new List<Rectangle>();

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Rectangle pos = new Rectangle(c * Constants.characterWidth, r * Constants.characterHeight, Constants.characterWidth, Constants.characterHeight);

                    switch (r)
                    {
                        case 0:
                            sourcesWD.Add(pos);
                            break;
                        case 1:
                            sourcesWR.Add(pos);
                            break;
                        case 2:
                            sourcesWL.Add(pos);
                            break;
                        case 3:
                            sourcesWU.Add(pos);
                            break;
                    }
                }
            }

            SpriteSheet wR = new SpriteSheet(sourcesWR, player.getDestination(), player.getSpriteSh(), 0, 10, 0);
            SpriteSheet wL = new SpriteSheet(sourcesWL, player.getDestination(), player.getSpriteSh(), 0, 10, 0);
            SpriteSheet wU = new SpriteSheet(sourcesWU, player.getDestination(), player.getSpriteSh(), 0, 10, 0);
            SpriteSheet wD = new SpriteSheet(sourcesWD, player.getDestination(), player.getSpriteSh(), 0, 10, 0);

            player.setSpriteSheets(wR, wL, wD, wU);
        }

        public Texture2D createTopLayerSpriteSheet()
        {
            return loadImage("TestWorld/TestLevel1/topLayer");
        }

        public Camera createCamera()
        {
            Texture2D collSh = loadImage("TestWorld/TestLevel1/collisionsSpriteSheet");
            Texture2D empty = loadImage("TestWorld/TestLevel1/empty");
            Camera camera = new Camera(Constants.cameraHeightInBlocks, Constants.cameraWidthInBlocks, 0, 0, collSh, graphicsDevice, empty);

            return camera;
        }

        public Rectangle getSpriteSheetSource(String value)
        {
            //Rectangle source = new Rectangle(0, 0, 0, 0);
            int pos = 0;
            if (!value.Any(x => char.IsLetter(x)))
            {
                pos = Convert.ToInt32(value);
            }
            else
            {
                value = value.Substring(1);
                pos = Convert.ToInt32(value);
            }

            pos = pos - 1;
            int sourceXPos = (pos % Constants.gameSpriteSheetRow) * Constants.BLOCK_SIZE;
            int sourceYPos = (pos / Constants.gameSpriteSheetRow) * Constants.BLOCK_SIZE;
            //Console.WriteLine("POSITION IS: \n");
            //Console.WriteLine(pos);
            Rectangle source = new Rectangle(sourceXPos, sourceYPos, Constants.BLOCK_SIZE, Constants.BLOCK_SIZE);
            //Console.WriteLine(source.ToString());
            return source;
        }

        public void printArray(String[,] s)
        {
            for (int r = 0; r < s.GetLength(0); r++)
            {
                for (int c = 0; c < s.GetLength(1); c++)
                {
                    Console.Write(s[r, c]);
                }
                Console.WriteLine();
            }
        }
    }
}
