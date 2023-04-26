using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project
{
    internal class HUD
    {
        private Texture2D _texture;
        private Vector2 _speed;
        private Rectangle rect;


        public HUD(Texture2D texture, Rectangle rect)
        {
            _texture = texture;
            
            
            
            
        }
        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float Width
        {
            get { return rect.Width; }
            set { rect.Width = (int)value; }
        }
        private void Move()
        {
            rect.Width += (int)_speed.X;
            
        }
        public void Draw(SpriteBatch sb)
        {


            sb.Draw(_texture, rect, Color.White);

        }
    }
}
