using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Final_Project
{

    public class LaserClass
    {

        List<Texture2D> lightningTextures;
        private double updateSpeed;

        private Vector2 position;
        private Vector2 velocity;
        private Rectangle rect;
        private float rotation;
        private int _damage;

        public LaserClass(List<Texture2D> ligthningTextures, Vector2 position, float rotation, Rectangle rect, int damage)
        {
            this.lightningTextures = ligthningTextures;
            this.position = position;

            this.rotation = rotation;
            _damage = damage;
            this.rect = rect; 
        }
        public int WeaponDamage
        {
            get { return _damage; }
            set { _damage = value; }
        }
        public void Update(GameTime gt, Vector2 backSpeed, double projectileSpeed)
        {

            velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            velocity *= (float)projectileSpeed;
            position += velocity;

            position.X += (int)backSpeed.X;
            position.Y += (int)backSpeed.Y;
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;


            updateSpeed += 0.11;
            if (updateSpeed >= lightningTextures.Count - 0.5)
                updateSpeed = 0;                          
        }
        public Rectangle GetBoundingBox()
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
        
        public bool Collide(Rectangle item)
        {
            return rect.Intersects(item);
        }

        public void Draw(SpriteBatch sb, Texture2D texture)
        {
            sb.Draw(lightningTextures[(int)Math.Round(updateSpeed)], rect, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);
        }

    }
}
    

