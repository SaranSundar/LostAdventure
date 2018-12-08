using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LostAdventure.MacOS
{
    public class SpriteSheet
    {
        private List<Rectangle> sources;
        private Rectangle destination;
        private Texture2D spriteSheet;
        private int current;
        private int frameRate;
        private int counter;
        private int size;
        private bool alternate;
        private int min;
        private int max;
        private bool updateCycle = false;
        private bool startMoving = false;

        public SpriteSheet(List<Rectangle> sources, Rectangle destination, Texture2D spriteSheet, int current, int frameRate, int counter)
        {
            this.sources = sources;
            this.destination = destination;
            this.spriteSheet = spriteSheet;
            this.current = current;
            this.frameRate = frameRate;
            this.counter = counter;
            size = sources.Count;
            //size--;
            alternate = false;
            min = 1;
            max = size - 1;
        }

        public Texture2D getSpriteSheet()
        {
            return spriteSheet;
        }

        public Rectangle getSource()
        {
            return sources[current];
        }

        public Rectangle getDestination()
        {
            return destination;
        }

        public int getCurrent()
        {
            return current;
        }

        public bool getUpdateCycle()
        {
            return updateCycle;
        }

        public void update(bool moving, bool active)
        {
            if (active == false)
            {
                counter = 0;
                current = 0;
                startMoving = false;
            }
            else//size is size - 1 so it doesnt include standing still img
            {
                if (moving == true)
                {
                    if (startMoving == false)
                    {
                        current = 1;
                        startMoving = true;
                    }

                    counter++;
                    if (counter >= frameRate)
                    {
                        counter = 0;

                        if (current >= max && alternate == false)
                        {
                            alternate = true;
                        }
                        else if (current <= min && alternate == true)
                        {
                            alternate = false;
                        }

                        if (alternate == false)
                        {
                            current++;
                        }
                        else if (alternate == true)
                        {
                            current--;
                        }

                    }
                    if (current == 0 || current == max)
                    {
                        updateCycle = true;
                    }
                    else
                    {
                        updateCycle = false;
                    }
                }
                else
                {
                    startMoving = false;
                    counter = 0;
                    current = 0;
                }

            }
            //Console.WriteLine(current);
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(spriteSheet, destination, sources[current], Color.White);
        }
    }
}
