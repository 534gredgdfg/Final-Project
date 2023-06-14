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
        private Color _color;
        private double _grassSpeed;
        private double _flowerSpeed;
        private string _brakable;
        private string _blocking;
       
        public Barriers(Texture2D texture, Rectangle rect, int health, Color color, string brakable, string blocking)
        {
            _texture = texture;
            _rect = rect;
            _speed = new Vector2();
            _health = rect.Width + rect.Height;
            _color = color;
            _brakable = brakable;
            _blocking = blocking;
           
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
        public string Breakable
        {
            get { return _brakable; }
            set { _brakable = value; }
        }
        public string Blocking
        {
            get { return _blocking; }
            set { _blocking = value; }
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
        public void TakeHit(int damage)
        {
            if (_brakable == "true" )            
                _health -= damage;          
        }
        public void UndoMoveV()
        {
            _rect.Y -= (int)_speed.Y;
        }
        public void Update(Vector2 backSpeed, List<Barriers> barriers)
        {
            Move(backSpeed, barriers);
            
        }
        
        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_rect.X, _rect.Y + _rect.Height/5, _rect.Width, _rect.Height - _rect.Height / 5*2);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int startinghealth = _rect.Width + _rect.Height;

            if (_health >= startinghealth - startinghealth / 4)
                spriteBatch.Draw(_texture, _rect, Color.White);

            else if (_health < startinghealth - startinghealth / 4 && _health >= startinghealth / 2)
                spriteBatch.Draw(_texture, _rect, Color.LightGray);

            else if (_health < startinghealth - startinghealth / 2 && _health >= startinghealth / 4)
                spriteBatch.Draw(_texture, _rect, Color.Gray);

            else
                spriteBatch.Draw(_texture, _rect, Color.DarkSlateGray);



        }
    }
}
