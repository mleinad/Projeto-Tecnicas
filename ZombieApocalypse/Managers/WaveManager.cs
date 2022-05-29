using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace ZombieApocalypse {
    public class WaveManager
    {

        public enum TimeOfDay {sunrise,midday,sunset,dusk, midnight, dawn}
        public int ToD;
        float count;
        public SpriteFont time;
        int WaveNumber=1;
        public bool WaveStart=true;
        bool cooldown = true;
        float cdc;

        public virtual void Update(GameTime gameTime) 
        {
            count += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            
            if(cooldown==true)
                cdc+=(float)gameTime.ElapsedGameTime.TotalSeconds;

            if (cdc >= 5 && cooldown==true) 
            {
                cdc = 0;
                cooldown = false;
                WaveStart = true;                
            }


            if (count >= 30 && cooldown == false) 
            {
                WaveNumber++;
                count = 0;
                cooldown = true;
            }

        }
        
       public int  SpawnRate() 
        {
           return( WaveNumber * 5);
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
        
            //spriteBatch.DrawString(time, count.ToString(), new Vector2(100, 300), Color.White);
            //spriteBatch.DrawString(time, "cool down= "+ cdc.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(time, " wave: " + WaveNumber.ToString(), new Vector2(625, 13), Color.White);
        }


    }
}
