using ZombieApocalypse.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombieApocalypse.Scenes
{
    class GameScene : Component
    {
       
        private UI ui;
        bool CloseToBox=false;
        Texture2D bg;
        Texture2D Ptexture;
        Texture2D Ztexture;
        SpriteFont font;
        WaveManager Wave;
        // Sprite trackArea;
        int spawnCount;
        private List<Sprite> _sprites;

        protected KeyboardState currentKey;
        protected KeyboardState previousKey;

        bool openBox=false;
        Random rnd = new Random();
        Sprite Box;
        Texture2D BoxTexture;

        Texture2D gunBox;
        Texture2D riffleBox;
        Texture2D shotgunBox;
        Texture2D bulletAbox;
        Texture2D bulletBbox;
        Texture2D bulletCbox;

        Texture2D gunPNG;

        private Rectangle msRect;
        private MouseState ms, OldMs;

        private Rectangle btnRectangle1; private Rectangle btnRectangle2; private Rectangle btnRectangle3;
        
        
        private float count;

        internal override void LoadContent(ContentManager Content)
        {
           
            ui = new UI();
            ui.LoadContent(Content);
            Wave = new WaveManager() 
            {
            time= Content.Load<SpriteFont>("galleryFont")
            };
            //_camera = new Camera();


             gunBox = Content.Load<Texture2D>("gunbox");
             riffleBox = Content.Load<Texture2D>("rifflebox");
             shotgunBox = Content.Load<Texture2D>("shotgunbox");
             bulletAbox = Content.Load<Texture2D>("bulletAbox");
             bulletBbox = Content.Load<Texture2D>("bulletBbox");
             bulletCbox = Content.Load<Texture2D>("bulletCbox");

            gunPNG = Content.Load<Texture2D>("gunPNG");

            btnRectangle1 = new Rectangle(590, 410, riffleBox.Width, riffleBox.Height);
            btnRectangle2 = new Rectangle(0, 100, shotgunBox.Width, shotgunBox.Height);
            btnRectangle3 = new Rectangle(600, 630, gunBox.Width, gunBox.Height);            
           
            BoxTexture = Content.Load<Texture2D>("BoxSprite");

            Texture2D bulletTexture = Content.Load<Texture2D>("bullet");
            bg = Content.Load<Texture2D>("backgorund");
            Wave.time = Content.Load<SpriteFont>("galleryFont");
            font= Content.Load<SpriteFont>("galleryFont");
            Ptexture = Content.Load<Texture2D>("player");
            Ztexture = Content.Load<Texture2D>("zombie");
            var bt = Content.Load<Texture2D>("bullet");
            var boxT = Content.Load<Texture2D>("ui");
            Box = new Sprite(boxT)
            {
                Position = new Vector2(400, 400),
                hitbox = new Rectangle(400, 400, boxT.Width + 10, boxT.Height + 10),

            };


            _sprites = new List<Sprite>()
            {
                new Player(Ptexture)
                {
                    hitbox= new Rectangle(100, 100, Ptexture.Width, Ptexture.Height),
                        Bullet = new Bullet(bulletTexture)
                        {
                            hitbox=new Rectangle(100, 100, bulletTexture.Width, bulletTexture.Height),
                        },
                },
                new Enemy(Ztexture)
                {
                    color=Color.Green,
                    Position=new Vector2(100, 100),
                    Speed=1f,
                    hitbox=new Rectangle(100, 100, Ztexture.Width, Ztexture.Height),
                }
            };

        }

        internal override void Update(GameTime gameTime)
        {
            OldMs = ms;
            ms = Mouse.GetState();
            msRect = new Rectangle(ms.X, ms.Y, 1, 1);

            ui.Update(gameTime);
            Wave.Update(gameTime);
            count += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites.ToList())
                sprite.Update(gameTime, _sprites);

            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            
            if (currentKey.IsKeyDown(Keys.Escape))
            {
                Data.CurrentState = Data.Scenes.Menu;
            }



            //if box is close to player
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite1 = _sprites[i];

                if (sprite1 is Player)
                {
                    if (sprite1.hitbox.Intersects(Box.hitbox))
                    {
                        CloseToBox = true;
                    }
                    else
                        CloseToBox = false;
                }
            }

            if (currentKey.IsKeyDown(Keys.E) && CloseToBox == true)
            {
                openBox = true;

                if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRectangle1)&&_sprites[0].points>=1500)
                {
                    _sprites[0].WeaponType = 4;
                    _sprites[0].points -= 1500;
                }
                else if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRectangle2) && _sprites[0].points >= 120)
                {
                    _sprites[0].WeaponType = 2;
                    _sprites[0].points -= 120;

                }
                else if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRectangle3) && _sprites[0].points >= 800)
                {
                    _sprites[0].WeaponType = 3;
                    _sprites[0].points -= 800;

                }

            }

            else
                openBox = false;


            //check collision
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite1 = _sprites[i];

                if (sprite1 is Player)
                {
                    for (int j = 0; j < _sprites.Count; j++)
                    {
                        var sprite2 = _sprites[j];
                        if (sprite2 is Enemy)
                        {
                            if (sprite1.hitbox.Intersects(sprite2.hitbox))
                            {
                                sprite1.color = Color.Red;
                                sprite1.HealthPoints--;     
                            }
                            else
                            {
                                sprite1.color = Color.White;
                            }
                        }

                    }
                }
            }

            //check damage
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite1 = _sprites[i];

                if (sprite1 is Bullet)
              {
                    for (int j = 0; j < _sprites.Count; j++)
                    {
                        var sprite2 = _sprites[j];
                        if (sprite2 is Enemy)
                        {
                            if (sprite2.hitbox.Intersects(sprite1.hitbox))
                            {

                               sprite1.IsRemoved = true;


                                sprite2.color = Color.Red; 

                                sprite2.HealthPoints-=(int)sprite1.damage;
                               
                               //sprite1.IsRemoved = true;

                            }
                            else if (!sprite2.hitbox.Intersects(sprite1.hitbox))
                            {
                                sprite2.color = Color.Green;
                            }
                        }
                    }
                }
            }


            //follow player
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite1 = _sprites[i];

                if (sprite1 is Player)
                {
                    for (int j = 0; j < _sprites.Count; j++)
                    {
                        var sprite2 = _sprites[j];
                        if (sprite2 is Enemy)
                        {
                            sprite2.PlayerPosition = sprite1.Position;
                        }

                    }
                }
            }

            //removes sprites 
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
            int enemyN=0;

            for (int i = 0; i < _sprites.Count; i++) 
            {
                var sprite1 = _sprites[i];

                if (sprite1 is Enemy)
                {
                    enemyN++;
                }
            }
                if (Wave.WaveStart == true) 
                {
                    spawnCount = Wave.SpawnRate();
                    Wave.WaveStart = false;
                }



            if (spawnCount > 0 && count>=(rnd.Next(3,30)))
            {
                _sprites.Add(AddEnemy());
                spawnCount--;
                count = 0;
            }


            //_camera.Follow(_sprites[0]);
        }

        
       //generates a new enemy 
        public Enemy AddEnemy()
        {

            Random rnd = new Random();
            int Y = rnd.Next(0, Data.ScreenH);
            int X = rnd.Next(0, Data.ScreenW);
            return new Enemy(Ztexture)
            {
                color = Color.Green,
                Position = new Vector2(X, Y),
                Speed = rnd.Next(1, 5),
                hitbox = new Rectangle(100, 100, Ztexture.Width, Ztexture.Height),
            };
        }


        internal override void Draw(SpriteBatch spriteBatch)
        {

            ui.draw(spriteBatch);

            // spriteBatch.Begin(transformMatrix: _camera.Transform);


            spriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0f,
                            Vector2.Zero, 1f, SpriteEffects.None, 0f);




            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            if (CloseToBox == true)
            {
                spriteBatch.DrawString(font, "press (E) to open armory", new Vector2(800, 400), Color.White);

            }
            if (openBox == true)
            {
                spriteBatch.Draw(BoxTexture, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(riffleBox, new Vector2(590, 410), null, Color.White, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0f);
                spriteBatch.Draw(gunBox, new Vector2(600, 630), null, Color.White, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 0f);
                spriteBatch.Draw(shotgunBox, new Vector2(590, 500), null, Color.White, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0f);


                spriteBatch.Draw(bulletCbox, new Vector2(748, 405), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(bulletAbox, new Vector2(748, 612), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);
                spriteBatch.Draw(bulletBbox, new Vector2(749, 505), null, Color.White, 0, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);

                if (msRect.Intersects(btnRectangle1))
                {
                    spriteBatch.Draw(riffleBox, new Vector2(590, 410), null, Color.Green, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0f);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                    if ((ms.LeftButton == ButtonState.Pressed && _sprites[0].points >= 1500))
                        spriteBatch.Draw(riffleBox, new Vector2(590, 410), null, Color.DarkSeaGreen, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0f);
                }
                else if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRectangle2) && _sprites[0].points >= 120)
                {
                    spriteBatch.Draw(shotgunBox, new Vector2(590, 500), null, Color.DarkSeaGreen, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0f);
                }
                else if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRectangle3) && _sprites[0].points >= 800)
                {

                    spriteBatch.Draw(gunBox, new Vector2(590, 500), null, Color.DarkSeaGreen, 0, new Vector2(0, 0), 0.45f, SpriteEffects.None, 0f);
                }
            }







            Wave.Draw(spriteBatch);
            // spriteBatch.End();

          //  Box.Draw(spriteBatch);
            ui.draw(spriteBatch);


            spriteBatch.DrawString(font, "HP " + _sprites[0].HealthPoints, new Vector2(1399, 985), Color.DarkSeaGreen);
            spriteBatch.DrawString(font, "POINTS " + _sprites[0].points, new Vector2(1599, 985), Color.DarkSeaGreen);
            spriteBatch.DrawString(font, "M" + _sprites[0].magazine, new Vector2(350, 985), Color.DarkSeaGreen);
            spriteBatch.DrawString(font, "R" + _sprites[0].recharge, new Vector2(240, 985), Color.DarkSeaGreen);

            switch (_sprites[0].WeaponType)
            {
                case 1:
                    break;
                case 2:
                    spriteBatch.Draw(gunPNG, new Vector2(50, 985), Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(gunPNG, new Vector2(50, 985), Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(gunPNG, new Vector2(50, 985), Color.White);
                    break;
                default:
                    break;



            }
            Wave.Draw(spriteBatch);

           
        }

    }
}
