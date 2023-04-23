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
        public float XLocation
        {
            get { return rect.X; }
            set { rect.X = (int)value; }
        }
        public float YLocation
        {
            get { return rect.Y; }
            set { rect.Y = (int)value; }
        }
        public void Update(GameTime gt)
            {

                velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 5f;

                position += velocity;
                rect.X = (int)position.X;
                rect.Y = (int)position.Y;




        }
        public bool Collide(Rectangle item)
        {
            return rect.Intersects(item);
        }

        public void Remove()
        {
            rect.X = 0;
            rect.Y = 0;
        }

        public void Draw(SpriteBatch sb)
            { 

                    sb.Draw(texture, rect , Color.Red);

            }
        






    }
    

}