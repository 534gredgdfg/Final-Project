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

        private double _meleeSpeed;
        private double _walkingSpeed;
        private double _standingSpeed;

        private float _heatUpAmount;
        private float _gunInterval;

        private string _weapontype;
        private string _enemyType;
        private string _attack;
        private string _drawingDamage;

        List<Texture2D> _walkingTextures;
        List<Texture2D> _standingTextures;
        List<Texture2D> _meleeTextures;

        Random rand = new Random();
        public Player(Rectangle location,  int health,string weapontype, string enemyType, List<Texture2D> walkingTextures, List<Texture2D> standingTextures, List<Texture2D> meleeTextures)
        {
           
            _location = location;
            _speed = new Vector2();
            _health = health;
            _enemyType = enemyType;
            _attack = "false";
            _drawingDamage = "false";
            _weapontype = weapontype;
            _walkingTextures = walkingTextures;
            _standingTextures = standingTextures;
            _meleeTextures = meleeTextures;
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
        public string Attacking
        {
            get { return _attack; }
            set { _attack = value; }
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
               
            }
            if (user.Right - user.Width / 2 < _location.X)
            {
                if (_enemyType == "fast")
                    _speed.X = -2;
                else
                    _speed.X = -1;
                
            }
        }

        public void ChoosingWeapon()
        {
            
               
            if (_weapontype == "arrow")
            {
                _damage = 30;
                _gunInterval = 2f;
                
            }
            else if (_weapontype == "wizard ball")
            {
                _damage = 50;
                _gunInterval = 1f;
                
            }


            else if (_weapontype == "melee")
            {
                _damage = 34;
                _gunInterval = 2.5f;              
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
        public void UndoMoveH()
        {
            _location.X -= (int)_speed.X;         
        }
        public void UndoMoveV()
        {
            _location.Y -= (int)_speed.Y;
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
            return new Rectangle(_location.X + _location.Width/3, _location.Y, _location.Width - _location.Width/2, _location.Height );
        }
        public Rectangle EnemyHitbox()
        {
            return new Rectangle(_location.X + _location.Width / 3, _location.Y, _location.Width - _location.Width / 3 * 2, _location.Height);
        }
        public Rectangle Largebox()
        {
            return new Rectangle(_location.X -30, _location.Y -30, _location.Width + 60, _location.Height +60);
        }


        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }
      
        
        public void Attack()
        {
                _attack = "true";   
        }
        public void ArrowAttack()
        {
            _attack = "true";
        }
        public void EnemyHit()
        {
            _drawingDamage = "true";
        }
        public void ResetEnemyHit()
        {
            _drawingDamage = "false";
        }
        public void Respawn()
        {
            _location.X = rand.Next(350, 1050- width);
            _location.Y = rand.Next(225, 675 - height);
        }       

        public void Update(Vector2 backSpeed)
        {
            _meleeSpeed += 0.1;
            if (_meleeSpeed >= _meleeTextures.Count - 0.5)
            {
                _meleeSpeed = 0;
                _attack = "false";
            }

            _walkingSpeed += 0.1;
            if (_walkingSpeed >= _walkingTextures.Count - 0.5)
            {
                _walkingSpeed = 0;
            }
            _standingSpeed += 0.1;
            if (_standingSpeed >= _standingTextures.Count - 0.5)
            {
                _standingSpeed = 0;
            }

            Move(backSpeed);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            
            if (_attack == "true")
                spriteBatch.Draw(_meleeTextures[(int)Math.Round(_meleeSpeed)], _location, Color.White);
            else if (HSpeed > 0)
                spriteBatch.Draw(_walkingTextures[(int)Math.Round(_walkingSpeed)], _location, Color.White);
          
            else
                spriteBatch.Draw(_standingTextures[(int)Math.Round(_standingSpeed)], _location, Color.White);

            
        }
        public void DrawDamage(SpriteBatch spriteBatch, SpriteFont FontText, int userDamage)
        {
            if (_drawingDamage == "true")
                 spriteBatch.DrawString(FontText, $"{userDamage}",new Vector2 (_location.X, _location.Y), Color.White);
        }


    }
}
