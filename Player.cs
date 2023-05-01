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
        private int _AicooldownTime;
        private int _AiFireableShots;
        private int _damage;
        private float _heatUpAmount;
        private float _gunInterval;
        private string _weapontype;
        private string _enemyType;
        private string _direction;
        Random rand = new Random();
        public Player(Texture2D texture, int x, int y, int width, int height,  int health,string weapontype, string enemyType, string direction)
        {
            _texture = texture;
            _location = new Rectangle(x, y, width, height);
            _speed = new Vector2();
            _health = health;
            _enemyType = enemyType;
            _direction = direction;
            _weapontype = weapontype;
            
        }
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
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
        public float AiFireableShots
        {
            get { return _AiFireableShots; }
            set { _AiFireableShots = (int)value; }
        }
        public float AICooldownTime
        {
            get { return _AicooldownTime; }
            set { _AicooldownTime = (int)value; }
        }
        public string WeaponType
        {
            get { return _weapontype; }
            set { _weapontype = value; }
        }
        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public float WeaponDamage
        {
            get { return _damage; }
            set { _damage = (int)value; }
        }
        public float GunInterval
        {
            get { return _gunInterval; }
            set { _gunInterval = value; }
        }
        public float HeatUpAmount
        {
            get { return _heatUpAmount; }
            set { _heatUpAmount = value; }
        }
        public void TroopsSpeed(Rectangle user)
        {
            if (user.Y + user.Height / 2 > _location.Bottom)
            {
                if (_enemyType == "fast")
                    _speed.Y = 2;
                else
                    _speed.Y = 1;

            }
            if (user.Bottom - user.Height / 2 < _location.Y)
            {
                if (_enemyType == "fast")
                    _speed.Y = -2;
                else
                    _speed.Y = -1;
            }
            if (user.X + user.Width / 2 > _location.Right)
            {
                if (_enemyType == "fast")
                    _speed.X = 2;
                else
                    _speed.X = 1;
                _direction = "right";
            }
            if (user.Right - user.Width / 2 < _location.X)
            {
                if (_enemyType == "fast")
                    _speed.X = -2;
                else
                    _speed.X = -1;
                _direction = "left";
            }



        }

            public void ChoosingWeapon()
        {
            if (_weapontype == "pistol")
            {
                _damage = 24;
                _gunInterval = 0.4f;
                _heatUpAmount = 50;
                _AicooldownTime = 5;
                _AiFireableShots = 7;
            }
               
            else if (_weapontype == "projectile")
            {
                _damage = 30;
                _gunInterval = 0.6f;
                _heatUpAmount = 65;
                _AicooldownTime = 5;
                _AiFireableShots = 12;
            }
                
            else if (_weapontype == "melee")
            {
                _damage = 30;
                _gunInterval = 0.8f;
                _heatUpAmount = 20;
                _AicooldownTime = 5;
                _AiFireableShots = 3;
            }
            
            else if (_weapontype == "lightsaber")
            {
                _damage = 50;
                _gunInterval = 0.4f;
                _heatUpAmount = 210;
                
            }




        }
        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_location.X, _location.Y, _location.Width, _location.Height);
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
        public Rectangle LightSaberHitBoxRight()
        {
            return new Rectangle(_location.X +_location.Width/2 , _location.Y, _location.Width, _location.Height );
        }
        public Rectangle LightSaberHitBoxLeft()
        {
            return new Rectangle(_location.X - _location.Width/2, _location.Y , _location.Width, _location.Height );
        }
        public Rectangle HeadShotBox()
        {
            return new Rectangle(_location.X - 1, _location.Y - 1, _location.Width + 2, _location.Height / 3);
        }
        public Rectangle Hitbox()
        {
            return new Rectangle(_location.X + 60, _location.Y, _location.Width -110, _location.Height);
        }
        public Rectangle Largebox()
        {
            return new Rectangle(_location.X -30, _location.Y -30, _location.Width + 60, _location.Height +60);
        }


        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }        
        public void Respawn()
        {
            _location.X = rand.Next(350, 1050- width);
            _location.Y = rand.Next(225, 675 - height);
        }
        public void Update(Vector2 backSpeed)
        {
            Move(backSpeed);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
        public void DrawAI(SpriteBatch spriteBatch, Texture2D _texture)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
