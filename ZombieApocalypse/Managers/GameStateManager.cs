using ZombieApocalypse.Core;
using ZombieApocalypse.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieApocalypse.Managers
{
    internal partial class GameStateManager : Component
    {

        private MenuScene ms = new MenuScene();
        private GameScene gs = new GameScene();


        internal override void LoadContent(ContentManager Content)
        {
            ms.LoadContent(Content);
            gs.LoadContent(Content);
        }

        internal override void Update(GameTime gameTime)
        {
            switch (Data.CurrentState) 
            {
                case Data.Scenes.Menu:
                    ms.Update(gameTime);
                    break;
                case Data.Scenes.Game:
                    gs.Update(gameTime);
                    break;
                case Data.Scenes.Settings:
                    break;

            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (Data.CurrentState)
            {
                case Data.Scenes.Menu:
                    ms.Draw(spriteBatch);
                    break;
                case Data.Scenes.Game:
                    gs.Draw(spriteBatch);
                    break;
                case Data.Scenes.Settings:
                    break;

            }


        }

    }
}
