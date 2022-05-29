using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace ZombieApocalypse
{
    public class Sprite: ICloneable
    {
        protected Texture2D _Texture;

        public int points=5000;
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;
        public float _rotation;
        public Vector2 Direction;
        public Rectangle hitbox;
        public Vector2 Position;
        public float LinearVelocity = 4f;
        public float Speed = 5f;
        public Vector2 Origin;
        public Color color=Color.White;

        public Vector2 PlayerPosition { get; set; }
        public object Rectangle { get; internal set; }

        public float RotationVelocity = 3f;

        public int WeaponType=3;

        public float LifeSpan = 0f;
        public bool IsRemoved = false;
        public float damage;
        public int HealthPoints=  10;

        public int magazine, recharge=100;

        public Sprite(Texture2D texture)
        {
            _Texture = texture;
            Origin = new Vector2(_Texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
           

            spriteBatch.Draw(_Texture, Position, null, color, _rotation, Origin, 1, SpriteEffects.None, 0f);
           
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
