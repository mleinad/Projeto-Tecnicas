using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace ZombieApocalypse
{
    class UI

    {
        Texture2D uitexture;
        public Rectangle hitbox;
        public Vector2 Position;
        public Vector2 Origin;




        internal virtual void LoadContent(ContentManager Content)
        {
            uitexture = Content.Load<Texture2D>("interface");
            hitbox = new Rectangle(0, 0, uitexture.Width, uitexture.Height);
            Origin = new Vector2(uitexture.Width / 2, uitexture.Height / 2);
        }

        internal virtual void Update(GameTime gameTime)
        {

        }

        internal virtual void draw(SpriteBatch spriteBatch)

        {


            spriteBatch.Draw(uitexture, new Vector2(0, 0), Color.White);


        }

    }
}