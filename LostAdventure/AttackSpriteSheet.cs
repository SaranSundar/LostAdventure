using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class AttackSpriteSheet : SpriteSheet
    {
        private int current, counter, frameRate;
        private List<Rectangle> up, down, left, right;
        private List<Rectangle> collU, collD, collL, collR;
        private Dictionary<String, List<Rectangle>> map;
        private bool attacking = false;
        private bool startMoving = false;
        private bool updateCycle = false;
        private int size;
        private bool alternate;
        private int min, max;
        private String dir;
        public AttackSpriteSheet(List<Rectangle> sources, List<Rectangle> collisions, Rectangle destination, Texture2D spriteSheet, int current, int frameRate, int counter)
            : base(sources, destination, spriteSheet, current, frameRate, counter)
        {
            this.current = current;
            this.counter = counter;
            this.frameRate = frameRate;
            size = sources.Count;
            //size--;
            alternate = false;
            min = 1;
            max = size - 1;
        }

        public void setAttackSpriteSheets(List<Rectangle> u, List<Rectangle> d, List<Rectangle> l, List<Rectangle> r, List<Rectangle> cU, List<Rectangle> cD, List<Rectangle> cR, List<Rectangle> cL)
        {
            up = u;
            down = d;
            left = l;
            right = r;
            collD = cD;
            collL = cL;
            collR = cR;
            collU = cU;
            map = new Dictionary<string, List<Rectangle>>();
            map.Add("RIGHT", right);
            map.Add("LEFT", left);
            map.Add("UP", up);
            map.Add("DOWN", down);
            dir = "RIGHT";

        }

        public Rectangle getSource()
        {
            switch (dir)
            {
                case "RIGHT":
                    return right[current];
                    break;
                case "LEFT":
                    return left[current];
                    break;
                case "UP":
                    return up[current];
                    break;
                case "DOWN":
                    return down[current];
                    break;
                default:
                    return right[current];
                    break;

            }
        }

        public int getCurrent()
        {
            return current;
        }

        public bool getUpdateCycle()
        {
            return updateCycle;
        }

        public void update(bool moving, bool active, String dir)
        {

            if (active == false)
            {
                counter = 0;
                current = 0;
                startMoving = false;
                updateCycle = true;
            }
            else//size is size - 1 so it doesnt include standing still img
            {
                if (this.dir.Equals(dir))
                {
                    if (moving == true)
                    {
                        updateCycle = false;
                        counter++;
                        if (counter >= frameRate)
                        {
                            counter = 0;
                            current++;
                            if (current > max)
                            {
                                updateCycle = true;
                                current = 0;
                            }
                        }
                    }
                }
                else
                {
                    updateCycle = true;
                    this.dir = dir;
                    startMoving = false;
                    counter = 0;
                    current = 0;
                }

            }
            //Console.WriteLine(current);
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(base.getSpriteSheet(), base.getDestination(), getSource(), Color.White);
        }
    }
}
