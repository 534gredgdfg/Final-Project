using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Final_Project
{

    public class LaserClass
    {

        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Rectangle rect;
        private float rotation;
        

        public LaserClass(Texture2D texture, Vector2 position, float rotation, Rectangle rect)
        {

            this.texture = texture;

            this.position = position;

            this.rotation = rotation;


            this.rect = rect;
          
        }
       


        public void Update(GameTime gt)
        {

            velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f;
            velocity = velocity * 4;

            position += velocity;
            rect.X = (int)position.X ;
            rect.Y = (int)position.Y + 40 ;




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


            sb.Draw(texture, rect, null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0f);

        }






    }
}
    

