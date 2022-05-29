using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieApocalypse
{
    public class Bullet : Sprite
    {
        private float _timer;
        
        public Bullet(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            Position +=  Direction * LinearVelocity;
            
            hitbox.X = (int)Position.X;

            hitbox.Y = (int)Position.Y;
        }
    }
}
