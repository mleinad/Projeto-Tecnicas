using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieApocalypse
{
    class Enemy : Sprite
    {
          float dx, dy;
        Texture2D[] zombiesprite = new Texture2D[16];

        public Enemy(Texture2D texture)
            : base(texture)
        {

        }


        internal virtual void LoadContent(ContentManager Content) 
        {
                for(int i=0; i<17; i++) 
                {
                zombiesprite[0] = Content.Load<Texture2D>($"skeleton-move_{i}");
                }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            if (HealthPoints <=0)
            {
                sprites[0].points += 10;  
                IsRemoved = true;
            }
            

            hitbox.X = (int)Position.X;
            hitbox.Y = (int)Position.Y;

            dx = PlayerPosition.X - Position.X;
            dy = PlayerPosition.Y - Position.Y;

            Vector2 moveDir = PlayerPosition - Position;
            moveDir.Normalize();
            Position += moveDir * Speed;


            _rotation = (float)Math.Atan2((double)dy, (double)dx);


        }
        public void Draw(SpriteBatch spriteBatch) 
        {
        for(int i=0; i < zombiesprite.Length; i++) 
            {
            spriteBatch.Draw(zombiesprite[i], Position, null, color, _rotation, Origin, 1, SpriteEffects.None, 0f);
            }
        }
    }
}