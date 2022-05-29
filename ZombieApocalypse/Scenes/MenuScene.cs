using ZombieApocalypse.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieApocalypse.Scenes
{
    class MenuScene : Component
    {

        private Texture2D Buttons1; private Texture2D Buttons3; private Texture2D Buttons2; private Texture2D Wallpaper;
        private Rectangle btnRectangle1; private Rectangle btnRectangle2; private Rectangle btnRectangle3;
        private Rectangle msRect;
        private MouseState ms, OldMs;


        internal override void LoadContent(ContentManager Content)
        {


          //  Wallpaper = Content.Load<Texture2D>("pablo-rodriguez-valero-sb-t01");

            Buttons1 = Content.Load<Texture2D>("btn1");
            Buttons2 = Content.Load<Texture2D>("btn2");
            Buttons3 = Content.Load<Texture2D>("btn3");


            btnRectangle1 = new Rectangle(0, 0, Buttons1.Width, Buttons1.Height);
            btnRectangle2 = new Rectangle(0, 100, Buttons2.Width, Buttons2.Height);
            btnRectangle3 = new Rectangle(0, 200, Buttons2.Width, Buttons3.Height);
        }

        internal override void Update(GameTime gameTime)
        {
            OldMs = ms;
            ms = Mouse.GetState();
            msRect = new Rectangle(ms.X, ms.Y, 1, 1);

            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRectangle1)) 
            {
                Data.CurrentState = Data.Scenes.Game;
            }

        }


        internal override void Draw(SpriteBatch spriteBatch)
        {

        //    spriteBatch.Draw(Wallpaper, new Vector2(0, 0),Color.White);
            
            spriteBatch.Draw(Buttons1, btnRectangle1, Color.White);
            spriteBatch.Draw(Buttons2, btnRectangle2, Color.White);
            spriteBatch.Draw(Buttons3, btnRectangle3, Color.White);

            if (msRect.Intersects(btnRectangle1)) 
            {
                spriteBatch.Draw(Buttons1, btnRectangle1, Color.Gray);
            }
            else if (msRect.Intersects(btnRectangle2))
                spriteBatch.Draw(Buttons2, btnRectangle2, Color.Gray);
            else if (msRect.Intersects(btnRectangle3))
                spriteBatch.Draw(Buttons3, btnRectangle3, Color.Gray);
      
        }

    }
}
