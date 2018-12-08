using System;
using Microsoft.Xna.Framework;

namespace LostAdventure.MacOS
{
    public class StaticBlock : Block
    {
        private Rectangle box;
        private String blockName;
        private Rectangle source;
        private String blockType;
        private Rectangle coll;

        public StaticBlock(Rectangle box, Rectangle coll, String blockName, Rectangle source, String blockType)
        : base(box, coll, blockName, source, blockType)
        {
            this.box = box;
            this.blockName = blockName;
            this.source = source;
            this.coll = coll;
        }

        public void print()
        {
            Console.WriteLine("Static Block");
            Console.WriteLine("\n");
            base.print();
        }
    }
}
