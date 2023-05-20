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
        private int _health;

        public Barriers(Texture2D texture, Rectangle rect, int health)
        {
            _texture = texture;
            _rect = rect;
            _speed = new Vector2();
            _health = rect.Width + rect.Height;
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
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public Rectangle Rect()
        {
            return new Rectangle(_rect.X , _rect.Y , _rect.Width, _rect.Height);
        }
        private void Move(Vector2 backSpeed, List<Barriers> barriers)
        {
            _rect.X += (int)_speed.X + (int)backSpeed.X;
            
            _rect.Y += (int)_speed.Y + (int)backSpeed.Y;
            
          
        }
        public void UndoMoveH()
        {
            _rect.X -= (int)_speed.X;
        }
        public void UndoMoveV()
        {
            _rect.Y -= (int)_speed.Y;
        }
        public void Update(Vector2 backSpeed, List<Barriers> barriers)
        {
            Move(backSpeed, barriers);
        }
        public void BarrierHit(int damage)
        {
            _health -= damage;
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_rect.X, _rect.Y, _rect.Width, _rect.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_health > (_rect.Width + _rect.Height)/2)
                spriteBatch.Draw(_texture, _rect, Color.White);
            else if (_health < (_rect.Width + _rect.Height)/2 && _health > (_rect.Width + _rect.Height) / 4)
                spriteBatch.Draw(_texture, _rect, Color.LightGray);
            else
                spriteBatch.Draw(_texture, _rect, Color.Gray);
        }
    }
}
