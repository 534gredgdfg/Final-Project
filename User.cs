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
        private Rectangle greenBar;
        private Rectangle redBar;
        private Rectangle grayBar;
        private Rectangle whiteBar;
        private DateTime lastMeleeTime = DateTime.MinValue;
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
        private double _gunInterval;

        private string _weapontype;
        private string _enemyType;
        private string _attack;
        private string _drawSheild;
        private string _drawingDamage;

        List<Texture2D> _walkingTextures;
        List<Texture2D> _standingTextures;

        List<Texture2D> _walkingSheildTextures;
        List<Texture2D> _standingSheildTextures;

        List<Texture2D> _meleeTextures;

        Random rand = new Random();
        public Player(Rectangle location, int health, string weapontype, string enemyType, List<Texture2D> walkingTextures, List<Texture2D> standingTextures, List<Texture2D> walkingSheildTextures, List<Texture2D> standingSheildTextures, List<Texture2D> meleeTextures)
        {

            _location = location;
            _speed = new Vector2();
            _health = health;
            _enemyType = enemyType;
            _attack = "false";
            _drawSheild = "false";
            _drawingDamage = "false";
            _weapontype = weapontype;
            _walkingTextures = walkingTextures;
            _standingTextures = standingTextures;

            _walkingSheildTextures = walkingSheildTextures;
            _standingSheildTextures = standingSheildTextures;

            _meleeTextures = meleeTextures;

            redBar = new Rectangle(_location.X, _location.Y, (int)Health + 9, 10);
            greenBar = new Rectangle(_location.X, _location.Y, (int)Health, 10);

            grayBar = new Rectangle(_location.X, _location.Y + 50, 100, 10);
            whiteBar = new Rectangle(_location.X, _location.Y + 50, 100, 10);

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
            get { return (float)_gunInterval; }
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
                _damage = 38;
                _gunInterval = 2f;

            }

            else if (_weapontype == "wizard ball")
            {
                _damage = 34;
                _gunInterval = 1.4f;

            }

            else if (_weapontype == "melee")
            {
                _damage = 26;
                _gunInterval = 1.0f;
            }
            else if (_weapontype == "goblin melee")
            {
                _damage = 17;
                _gunInterval = 2.3f;
            }
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_location.X, _location.Y, _location.Width, _location.Height);
        }
        private void Move(Vector2 backSpeed, List<Barriers> barriers)
        {
            _location.X += (int)_speed.X + (int)backSpeed.X;
            foreach (Barriers barrier in barriers)
            {
                if (Hitbox().Intersects(barrier.GetBoundingBox()))
                    UndoMoveH();



            }

            _location.Y += (int)_speed.Y + (int)backSpeed.Y;

            foreach (Barriers barrier in barriers)
            {
                if (Hitbox().Intersects(barrier.GetBoundingBox()))
                    UndoMoveV();



            }

        }

        public void UndoMove()
        {
            _location.X -= (int)_speed.X;
            _location.Y -= (int)_speed.Y;
        }
        public void UndoMoveH()
        {
            _location.X -= (int)_speed.X;
        }
        public void UndoMoveV()
        {
            _location.Y -= (int)_speed.Y;
        }
        public void Sheild()
        {
            _drawSheild = "true";
        }
        public void UnSheild()
        {
            _drawSheild = "false";
        }


        public void UserAttackMelee(List<Player> enemys, Player user, List<Barriers> barriers)
        {


            Attack();
            foreach (Player troops in enemys)
            {

                if (LightSaberHitBoxRight().Intersects(troops.Hitbox()))
                    troops.Health -= WeaponDamage;


                else if (LightSaberHitBoxLeft().Intersects(troops.Hitbox()))
                    troops.Health -= WeaponDamage;
            }
            foreach (Barriers barrier in barriers)
            {


                if (LightSaberHitBoxRight().Intersects(barrier.Rect()))
                    barrier.Health -= (int)WeaponDamage;


                else if (LightSaberHitBoxLeft().Intersects(barrier.Rect()))
                    barrier.Health -= (int)WeaponDamage;


            }




        }
        public void EnemyAttackMelee(List<Player> enemys, Player user, List<Barriers> barriers)
        {
            TimeSpan timeSinceLastAttack = DateTime.Now - lastMeleeTime;


            if (timeSinceLastAttack.TotalSeconds >= GunInterval)
            {
                if (enemys == null)
                {

                    foreach (Barriers barrier in barriers)
                    {
                        if (_location.Intersects(barrier.GetBoundingBox()))
                        {
                            Attack();
                            lastMeleeTime = DateTime.Now; // update last shot time 

                            if (LightSaberHitBoxRight().Intersects(barrier.Rect()))
                                barrier.BarrierHit((int)WeaponDamage);


                            else if (LightSaberHitBoxLeft().Intersects(barrier.Rect()))
                                barrier.BarrierHit((int)WeaponDamage);
                        }

                    }
                }
                else if (_location.Intersects(user.GetBoundingBox()))
                {
                    Attack();
                    lastMeleeTime = DateTime.Now; // update last shot time 


                    if (LightSaberHitBoxRight().Intersects(user.Hitbox()))
                        user.Health -= WeaponDamage;


                    else if (LightSaberHitBoxLeft().Intersects(user.Hitbox()))
                        user.Health -= WeaponDamage;


                }

            }


        }

        public string Sheilding
        {
            get { return _drawSheild; }
            set { _drawSheild = value; }
        }

        public Rectangle LightSaberHitBoxRight()
        {
            return new Rectangle(_location.X + _location.Width / 2, _location.Y, _location.Width, _location.Height);
        }
        public Rectangle LightSaberHitBoxLeft()
        {
            return new Rectangle(_location.X - _location.Width / 2, _location.Y, _location.Width, _location.Height);
        }
        public Rectangle HeadShotBox()
        {
            return new Rectangle(_location.X - 1, _location.Y - 1, _location.Width + 2, _location.Height / 3);
        }
        public Rectangle Hitbox()
        {
            return new Rectangle(_location.X + _location.Width / 3, _location.Y, _location.Width - _location.Width / 2, _location.Height);
        }
        public Rectangle Barrierbox()
        {
            return new Rectangle(_location.X + _location.Width / 3, _location.Y + _location.Height / 3, _location.Width - 2 * _location.Width / 3, _location.Height - 2 * _location.Height / 3);
        }

        public Rectangle Largebox()
        {
            return new Rectangle(_location.X - 30, _location.Y - 30, _location.Width + 60, _location.Height + 60);
        }


        public bool Collide(Rectangle item)
        {
            return _location.Intersects(item);
        }


        public void Attack()
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
            _location.X = rand.Next(350, 1050 - width);
            _location.Y = rand.Next(225, 675 - height);
        }

        public void Update(Vector2 backSpeed, List<Barriers> barriers)
        {

            if (_attack == "true")
                _meleeSpeed += 0.1;

            if (_meleeSpeed >= _meleeTextures.Count - 0.5)
            {
                _meleeSpeed = 0;
                _attack = "false";
            }

            if (_drawSheild == "true")
                _walkingSpeed += 0.07;
            else
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

            redBar.X = _location.X + _location.Width / 2 - redBar.Width / 2;
            greenBar.X = _location.X + _location.Width / 2 - redBar.Width / 2 + 9;

            redBar.Y = _location.Y;
            greenBar.Y = _location.Y;
            if (greenBar.Width != (int)Health)
                greenBar.Width -= 1;


            Move(backSpeed, barriers);
        }

        public void Draw(SpriteBatch spriteBatch, int mouseX)
        {
            SpriteEffects direction;

            
            if (HSpeed < 0 && mouseX < _location.X)
            {
                direction = SpriteEffects.FlipHorizontally;


            }
            else if (HSpeed > 0)
            {
                direction = SpriteEffects.None;

            }
            else if (HSpeed < 0)
            {
                direction = SpriteEffects.FlipHorizontally;


            }
            else if (mouseX < _location.X)
            {
                
                direction = SpriteEffects.FlipHorizontally;


            }


            else 
                direction = SpriteEffects.None;


            if (_drawSheild == "false")
            {
                if (_attack == "true")
                {
                    spriteBatch.Draw(_meleeTextures[(int)Math.Round(_meleeSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);

                }


                else if (HSpeed != 0 || VSpeed != 0)
                {
                    spriteBatch.Draw(_walkingTextures[(int)Math.Round(_walkingSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);
                    
                }



                else
                {
                    spriteBatch.Draw(_standingTextures[(int)Math.Round(_standingSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);
                   
                }
            }
            else
            {
                if (HSpeed != 0 || VSpeed != 0)
                {
                    spriteBatch.Draw(_walkingSheildTextures[(int)Math.Round(_walkingSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);
                   
                }



                else
                {
                    spriteBatch.Draw(_standingSheildTextures[(int)Math.Round(_standingSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);
                 
                }
            }


        }
        public void DrawDamage(SpriteBatch spriteBatch, SpriteFont FontText, int userDamage, double headshotMultiplyer)
        {
            if (_drawingDamage == "true")
                spriteBatch.DrawString(FontText, $"{userDamage * headshotMultiplyer}", new Vector2(_location.X, _location.Y), Color.White);
        }
        public void DrawHealth(SpriteBatch spriteBatch, Texture2D emptytTexture, Texture2D fullTexture)
        {

            spriteBatch.Draw(emptytTexture, redBar, Color.White);
            spriteBatch.Draw(fullTexture, greenBar, Color.White);

        }


    }
}
