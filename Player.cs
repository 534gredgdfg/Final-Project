using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace Final_Project
{
    
    internal class Player
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;
        private int _health;
        private int height;
        private int width;
        private int _cooldownTime;
        private int _fireableShots;

        Random rand = new Random();
        public Player(Texture2D texture, int x, int y, int width, int height, int health, int cooldownTime, int fireableShots)
        {
            _texture = texture;
            _location = new Rectangle(x, y, width, height);
            _speed = new Vector2();
            _health = health;
            _cooldownTime = cooldownTime;
            _fireableShots = fireableShots;
        }
        public float XLocation
        {
            get { return _location.X; }
            set { _location.X = (int)value; }
        }
        public float YLocation
        {
            get { return _location.Y; }
            set { _location.Y = (int)value; }
        }
        public float XLocationRight
        {
            get { return _location.Right; }
            set { _location.X = (int)value; }
        }
        public float Width
        {
            get { return width; }
            set { width = (int)value; }
        }
        public float Height
        {
            get { return height; }
            set { height = (int)value; }
        }
        public float YLocationBottom
        {
            get { return _location.Bottom; }
            set { _location.Y = (int)value; }
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
        public float Health
        {
            get { return _health; }
            set { _health = (int)value; }
        }
        public float fireableShots
        {
            get { return _fireableShots; }
            set { _fireableShots = (int)value; }
        }
        public float cooldownTime
        {
            get { return _cooldownTime; }
            set { _cooldownTime = (int)value; }
        }
        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_location.X, _location.Y, width, height);
        }
        private void Move(Vector2 backSpeed)
        {
            _location.X += (int)_speed.X + (int)backSpeed.X;
            _location.Y += (int)_speed.Y +(int)backSpeed.Y;
        }

        public void UndoMove()
        {
            _location.X -= (int)_speed.X;
            _location.Y -= (int)_speed.Y ;
        }
        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }        
        public void Respawn()
        {
            _location.X = rand.Next(0, 1000);
            _location.Y = rand.Next(0, 800);
        }
        public void Update(Vector2 backSpeed)
        {
            Move(backSpeed);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
