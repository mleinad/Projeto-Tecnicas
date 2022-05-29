using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZombieApocalypse
{
    class Player : Sprite
    {
        public Bullet Bullet;

       

        float dx;
        float dy;

        public MouseState mousePos;
        public MouseState mousePrev;
        private int playerShootTick=0;
        private int playerShootDelay=7;

        public Player(Texture2D texture)
            : base(texture)
        {

        }
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {


            mousePrev = mousePos;
            mousePos = Mouse.GetState();

            previousKey = currentKey;
            currentKey = Keyboard.GetState();


            if (mousePos.X != mousePrev.X || mousePos.Y != mousePrev.Y)
            {
                dx = mousePos.X - Position.X;
                dy = mousePos.Y - Position.Y;

                _rotation = (float)Math.Atan2((double)dy, (double)dx);
            }

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            if (currentKey.IsKeyDown(Keys.LeftShift)) 
            {
                Speed=8f;
            }
            if (currentKey.IsKeyUp(Keys.LeftShift))
            {
                Speed = 5f;
            }

            if (currentKey.IsKeyDown(Keys.A))
            {
                Position.X -= Speed;
            }
            if (currentKey.IsKeyDown(Keys.D))
            {
                Position.X += Speed;
            }
            if (currentKey.IsKeyDown(Keys.W))
            {
                Position.Y -= Speed;
            }
            if (currentKey.IsKeyDown(Keys.S))
            {
                Position.Y += Speed;
            }

            hitbox.X = (int)Position.X;

            hitbox.Y = (int)Position.Y;


            //shooting dynamics
            switch (WeaponType)
            {
                case 1:
                    break;
                case 2:
                    if (mousePos.LeftButton == ButtonState.Pressed && mousePrev.LeftButton == ButtonState.Released&&magazine > 0)
                    {

                        
                        AddBullet(sprites, 1, 2);
                        magazine--;
                    }
                    break;
                case 3:
                    if (mousePos.LeftButton == ButtonState.Pressed && mousePrev.LeftButton == ButtonState.Released&&magazine>0)
                    {
                        int speed = 1;
                        for (int i = 0; i < 3; i++)
                        {
                            AddBullet(sprites, speed, 6f);
                            speed++;
                            magazine--;
                        }
                    }
                    break;
                case 4:
                    if (mousePos.LeftButton == ButtonState.Pressed&& magazine > 0)
                    {
                        if (playerShootTick < playerShootDelay)
                        {
                            playerShootTick++;
                        }
                        else
                        {
                            AddBullet(sprites, 3.5f, 2.5f);
                            magazine--;
                            playerShootTick = 0;
                        }
                    }
                    break;
                default:
                    break;
            }




            //reload dynamics
            if (currentKey.IsKeyDown(Keys.R) && previousKey.IsKeyUp(Keys.R)) 
            {

                switch (WeaponType)
                {
                    case 1:
                        
                        break;
                    case 2:
                        if (recharge > 15&&magazine<15) 
                        {
                            recharge -= 15;
                            magazine = 15;
                        }
                        else if (recharge > 0) 
                        {
                            recharge = 0;
                            magazine = recharge;
                        }
                        break;
                    case 3:

                        if (recharge > 9 && magazine<9){
                            recharge -= 9;
                            magazine = 9;
                        }
                        break;
                    case 4:
                        if (recharge > 40&&magazine<40)
                        {
                            recharge -= 40;
                            magazine = 40;
                        }
                        else if (recharge > 0)
                        {
                            recharge = 0;
                            magazine = recharge;
                        }
                        break;
                    default:
                        break;
                }
            }
            
        }
        private void AddBullet(List<Sprite> sprites, float speed, float damage)
        {
            
                var bullet = Bullet.Clone() as Bullet;
                bullet.Direction = this.Direction*speed;
                bullet.Position = this.Position;
                bullet.LinearVelocity = this.LinearVelocity * 2;
                bullet.LifeSpan = 2f;
                bullet._rotation = this._rotation;
                 bullet.damage = damage;
                sprites.Add(bullet);
            
        }
    }
}

