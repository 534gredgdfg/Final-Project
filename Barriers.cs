using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Final_Project
{
    internal class Barriers
    {
        private Texture2D _texture;
        private Vector2 _speed;
        private Rectangle _rect;


        public Barriers(Texture2D texture, Rectangle rect)
        {
            _texture = texture;
            _rect = rect;
            _speed = new Vector2();
        }
        public float HSpeed
        {
            get { return _speed.X; }
            set { _speed.X = value; }
        }
        public float VSpeed
        {
            get { return _speed.Y; }
            set { _speed.Y = value; }
        }
        private void Move(Vector2 backSpeed)
        {
            _rect.X += (int)_speed.X + (int)backSpeed.X;
            _rect.Y += (int)_speed.Y + (int)backSpeed.Y;
        }
        public void Update(Vector2 backSpeed)
        {
            Move(backSpeed);
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_rect.X, _rect.Y, _rect.Width, _rect.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.Black);
        }
    }
}
