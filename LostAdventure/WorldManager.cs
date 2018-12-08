using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LostAdventure.MacOS
{
    public class WorldManager
    {
        private ResourceManager res;
        private Texture2D gameSpriteSh;
        private Texture2D topLayerSpriteSh;
        private Camera camera;
        private Stack<StateManager> state;
        private Stack<Block[,]> world;
        private Player player;

        public WorldManager(ResourceManager res)
        {
            this.res = res;
            world = new Stack<Block[,]>();
            world.Push(res.createLevel1());
            gameSpriteSh = res.getGameSpriteSheet();
            topLayerSpriteSh = res.createTopLayerSpriteSheet();
            camera = res.createCamera();
            player = res.createPlayer();
            state = new Stack<StateManager>();
            state.Push(StateManager.IN_GAME);
        }

        public void update(KeyboardState kb)
        {

            camera.update(world, kb, player, state);
        }

        public void draw(SpriteBatch sb)
        {
            camera.draw(sb, gameSpriteSh, player, topLayerSpriteSh);
            //player.draw(sb);
        }
    }
}
