using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace LostAdventure.MacOS
{
    public static class Constants
    {
        public static char textFileSeperator = '$';
        public static String testLevel1TextFilePath = "GameContent/TestWorld/TestLevel1/Level1.txt";
        public static String staticBlockKey = "Static_Block_Key";
        public static char textFileValueSeperator = ',';
        public static int gameSpriteSheetCol = 15;
        public static int gameSpriteSheetRow = 15;
        public static int screenWidth = 881;
        public static int screenHeight = 561;
        public static int playerSpawnX = 400;
        public static int playerSpawnY = 200;
        public static Dictionary<int, String> blockKey = new Dictionary<int, String>();
        public static int BLOCK_SIZE = 16;
        public static int BLOCK_SCALE = 5;
        public static Keys rightKey = Keys.Right;
        public static Keys leftKey = Keys.Left;
        public static Keys upKey = Keys.Up;
        public static Keys downKey = Keys.Down;
        public static int cameraWidthInBlocks = 22;
        public static int cameraHeightInBlocks = 14;
        public static int xMoveSpeed = 1;
        public static int yMoveSpeed = 1;
        public static int maxFlashMove = 25;
        public static double xM = .5;
        public static double yM = .5;
        public static int characterWidth = 12;
        public static int characterHeight = 21;
        public static int playerFeetHeight = 4;
        public static int playerFeetWidth = 10;
        public static String entranceBlockKey = "D";
        public static String depthBlockKey = "P";
        public static String exitBlockKey = "E";//change this to an int[]
        public static String treeBlockKey = "T";


        public static void createBlockKey()
        {
            blockKey.Add(1, "summer_grass");
            blockKey.Add(2, "summer_pink_flowers");
            blockKey.Add(3, "summer_sand");
            //blockKey.Add(4, "")
        }

        public static List<String> createTreeBlocksKey(String fileName)
        {
            List<String> treeBlocks = new List<String>();
            StreamReader kb = new StreamReader(@fileName);
            String line = "";
            while ((line = kb.ReadLine()) != null)
            {
                String[] key = line.Split(',');
                for (int i = 0; i < key.Length; i++)
                {
                    String val = key[i];
                    val = val.TrimStart();
                    val = val.Trim();
                    val = val.TrimEnd();
                    treeBlocks.Add(key[i]);
                }
            }
            return treeBlocks;
        }

        public static List<String> createDepthBlocksKey(String fileName)
        {
            List<String> depthBlocks = new List<String>();
            StreamReader kb = new StreamReader(@fileName);
            String line = "";
            while ((line = kb.ReadLine()) != null)
            {
                String[] key = line.Split(',');
                for (int i = 0; i < key.Length; i++)
                {
                    String val = key[i];
                    val = val.TrimStart();
                    val = val.Trim();
                    val = val.TrimEnd();
                    depthBlocks.Add(key[i]);
                }
            }
            return depthBlocks;
        }
    }
}
