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
        private DateTime lastMeleeTimes = DateTime.MinValue;
        private Vector2 _speed;
        private Vector2 _damageTextVector;
        private Vector2 playerPosition;
        private Vector2 enemyPosition;
        private Rectangle _damageTextLocation;
        private int _health;
        private int height;
        private int width;
        private int _AicooldownTime;
        private int _AiFireableShots;
        private int _damage;
        private int _killpoints = 0;
        private int _points = 1000;
        private int _sheildSeconds = 10;
        private int _boostSpeed;
        private int boostDamage;
        private double _meleeSpeed;
        private double _walkingSpeed;
        private double _standingSpeed;
        private double _hitSpeed;
        private double _transSpeed;

        private double _gunInterval;

        private float enemyRotation;
        private string _weapontype;
        private string _enemyType;
        private string _attack;
    
        private string _hit;
        private string _trans;
        private string _drawSheild;
        private string _drawingDamage;
       
        List<Texture2D> _walkingTextures;
        List<Texture2D> _standingTextures;
        List<Texture2D> _transTextures;
        List<Texture2D> _walkingSheildTextures;
        List<Texture2D> _standingSheildTextures;

        List<Texture2D> _meleeTextures;

        List<Texture2D> _hitTextures;
        Random rand = new Random();
        public Player(Rectangle location, int health, string weapontype, string enemyType, List<Texture2D> walkingTextures, List<Texture2D> standingTextures, List<Texture2D> walkingSheildTextures, List<Texture2D> standingSheildTextures, List<Texture2D> meleeTextures, List<Texture2D> AiHitTextures, List<Texture2D> TransSheildTextures)
        {

            _location = location;
            _speed = new Vector2();
            _damageTextLocation = new Rectangle(_location.X, location.Y, 120, 120);
            _health = health;
            _enemyType = enemyType;

            _attack = "false";
            _hit = "false";
            _drawSheild = "false";
            _drawingDamage = "false";
        
            _damageTextVector = new Vector2(rand.Next(-2, 2), rand.Next(-2, -1));

            _weapontype = weapontype;
            _walkingTextures = walkingTextures;
            _standingTextures = standingTextures;
            
            _walkingSheildTextures = walkingSheildTextures;
            _standingSheildTextures = standingSheildTextures;          
            _meleeTextures = meleeTextures;
            _hitTextures = AiHitTextures;
            _transTextures = TransSheildTextures;

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
      
        public string Attacking
        {
            get { return _attack; }
            set { _attack = value; }
        }
        public int PointsOnKill
        {
            get { return _killpoints; }
            set { _killpoints = value; }
        }
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }
        public int SheildSeconds
        {
            get { return _sheildSeconds; }
            set { _sheildSeconds = value; }
        }
        public int BoostSpeed
        {
            get { return _boostSpeed; }
            set { _boostSpeed = value; }
        }
        public int BoostDamage
        {
            get { return boostDamage; }
            set { boostDamage = value; }
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
            if (_weapontype == "wizard ball")
            {
                _damage = 34 + boostDamage;
                _gunInterval = 1.4f;

            }

            else if (_weapontype == "melee")
            {
                _damage = 26 + boostDamage;
                _gunInterval = 1.0f;
            }
            else if (_weapontype == "sheild melee")
            {
                _damage = 89 + boostDamage;
                _gunInterval = 0f;
            }
            else if (_weapontype == "arrow")
            {
                _damage = 38;
                _gunInterval = 2f;
                _killpoints = 60;
            }
            else if (_weapontype == "goblin melee")
            {
                _damage = 17;
                _gunInterval = 2.3f;
                _killpoints = 50;
            }
            else if (_weapontype == "fast goblin melee")
            {
                _damage = 13;
                _gunInterval = 1.9f;
                _killpoints = 55;
            }
            else if (_weapontype == "skel melee")
            {
                _damage = 26;
                _gunInterval = 3.5f;
                _killpoints = 45;
            }
            else if (_weapontype == "fire ball")
            {
                _damage = 54;
                _gunInterval = 6f;
                _killpoints = 70;
            }
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_location.X, _location.Y, _location.Width, _location.Height);
        }
        private void Move(Vector2 backSpeed, List<Barriers> barriers, string screen)
        {
            _location.X += (int)_speed.X + (int)backSpeed.X ;
            _location.Y += (int)_speed.Y + (int)backSpeed.Y ;
            if (screen == "main game")
            {
                foreach (Barriers barrier in barriers)
                {
                    if (barrier.Blocking == "true")
                    {
                        if (Hitbox().Intersects(barrier.GetBoundingBox()))
                            UndoMoveH();
                    }


                }

                foreach (Barriers barrier in barriers)
                {
                    if (barrier.Blocking == "true")
                    {
                        if (Hitbox().Intersects(barrier.GetBoundingBox()))
                            UndoMoveV();
                    }




                }
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
            _trans = "true";

        }
        public void UnSheild()
        {
            _drawSheild = "false";
            _trans = "false";
            _transSpeed = 0;
        }
        

        public void UserAttackMelee(List<Player> enemys, List<Barriers> barriers,  List<LaserClass> laserList, List<Texture2D>  texture, float playerRotation)
        {

            TimeSpan timeSinceLastAttack = DateTime.Now - lastMeleeTime;

            ChoosingWeapon();
            if (timeSinceLastAttack.TotalSeconds >= GunInterval && _drawSheild == "false")
            {
                Attack();
                lastMeleeTime = DateTime.Now; // update last shot time
                if (WeaponType == "melee" || WeaponType == "sheild melee")
                {
                    foreach (Player troops in enemys)
                    {

                        if (LightSaberHitBoxRight().Intersects(troops.Hitbox()))
                        {
                            troops.Health -= _damage;
                            troops.EnemyHit();
                        }



                        else if (LightSaberHitBoxLeft().Intersects(troops.Hitbox()))
                        {
                            troops.Health -= _damage;
                            troops.EnemyHit();
                        }

                    }
                    foreach (Barriers barrier in barriers)
                    {


                        if (LightSaberHitBoxRight().Intersects(barrier.Rect()))
                            barrier.Health -= (int)_damage;


                        else if (LightSaberHitBoxLeft().Intersects(barrier.Rect()))
                            barrier.Health -= (int)_damage;


                    }
                }

                else
                {

                    if (HSpeed >= 0)
                        playerPosition = new Vector2(XLocationRight, YLocation);
                    else
                        playerPosition = new Vector2(XLocation, YLocation);
                    laserList.Add(new LaserClass(texture, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 25, 20)));
                }
            }
               
                




        }
        public void DrawEnemyAttackMelee(Player user)
        {
            TimeSpan timeSinceLastAttacks = DateTime.Now - lastMeleeTimes;

            if (timeSinceLastAttacks.TotalSeconds >= GunInterval)
            {
                if (user != null)
                {
                    
                    if (_location.Intersects(user.GetBoundingBox()))
                    {
                        Attack();
                        lastMeleeTimes = DateTime.Now; // update last shot time 
                    }
                    
                }
                else
                {
                    Attack();
                    lastMeleeTimes = DateTime.Now; // update last shot time 
                }
                
                        
            }
           
        }
        public void EnemyAttackMelee(List<Player> enemys, Player user, List<Barriers> barriers, List<LaserClass> enemyLaserList, Vector2 playerPositions, List<Texture2D> texture1, List<Texture2D> texture2)
        {
            TimeSpan timeSinceLastAttack = DateTime.Now - lastMeleeTime;

            if (timeSinceLastAttack.TotalSeconds >= GunInterval)
            {
                
                if (WeaponType == "arrow" || WeaponType == "fire ball")
                {
                    if (HSpeed > 0)
                        enemyPosition = new Vector2(XLocationRight, YLocation);
                    else
                        enemyPosition = new Vector2(XLocation, YLocation);

                    float missingRange = rand.Next(-90, 90);
                    var enemydistance = new Vector2(playerPositions.X + missingRange - enemyPosition.X, playerPositions.Y + missingRange - enemyPosition.Y);
                    enemyRotation = (float)Math.Atan2(enemydistance.Y, enemydistance.X);


                    if (WeaponType == "fire ball")
                        enemyLaserList.Add(new LaserClass(texture1, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 60, 35)));
                    else
                        enemyLaserList.Add(new LaserClass(texture2, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 30, 8)));
             
                    lastMeleeTime = DateTime.Now; // update last shot time  
                }
                
                else if (_location.Intersects(user.GetBoundingBox()))
                {
                   
                    lastMeleeTime = DateTime.Now; // update last shot time 

                    if (LightSaberHitBoxRight().Intersects(user.Hitbox()))
                        user.Health -= _damage;


                    else if (LightSaberHitBoxLeft().Intersects(user.Hitbox()))
                        user.Health -= _damage;
                    
                }
                else
                {
                    
                    foreach (Barriers barrier in barriers)
                    {
                        
                        if (_location.Intersects(barrier.GetBoundingBox()))
                        {
                            DrawEnemyAttackMelee(null);
                            barrier.TakeHit((int)_damage);

                            lastMeleeTime = DateTime.Now; // update last shot time 
  
                        }

                    }
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
            _hit = "true";
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

        public void Update(Vector2 backSpeed, List<Barriers> barriers, string screen)
        {

            if (_attack == "true" && WeaponType == "fire worm")
                _meleeSpeed += 0.2;
            else if (_attack == "true")
                _meleeSpeed += 0.1;

            if (_meleeSpeed >= _meleeTextures.Count - 0.5)
            {
                _meleeSpeed = 0;
                _attack = "false";
                
            }
           

            if (_hit == "true")
                _hitSpeed += 0.1;

            if (_hitSpeed >= _hitTextures.Count - 0.5)
            {
                _hitSpeed = 0;
                _hit = "false";
            }

            if (_trans == "true")
                _transSpeed += 0.12;

            if (_transSpeed >= _transTextures.Count - 0.5)
            {
                _transSpeed = 0;
                _trans = "false";
            }

            if (_drawSheild == "true")
                _walkingSpeed += 0.07;
            else if (WeaponType == "fast goblin melee")
                _walkingSpeed += 0.15;
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



            Move(backSpeed, barriers, screen);
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
                if (_hit == "true")
                {
                    spriteBatch.Draw(_hitTextures[(int)Math.Round(_hitSpeed)], _location, null, Color.Gray, 0f, new Vector2(0, 0), direction, 0f);
                }

                else if (_attack == "true")
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
                if (_trans == "true")
                    spriteBatch.Draw(_transTextures[(int)Math.Round(_transSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);

                else if (HSpeed != 0 || VSpeed != 0)
                {
                    spriteBatch.Draw(_walkingSheildTextures[(int)Math.Round(_walkingSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);

                }

                else
                {
                    spriteBatch.Draw(_standingSheildTextures[(int)Math.Round(_standingSpeed)], _location, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);

                }
            }


        }
        public void DrawDamage(SpriteBatch spriteBatch, SpriteFont FontText, int userDamage, double headshotMultiplyer, Vector2 backSpeed, string userWeapon)
        {


            if (!_damageTextLocation.Intersects(Hitbox()))
            {
                ResetEnemyHit();
                _damageTextLocation.X = Hitbox().X + 50 ;
                _damageTextLocation.Y = Hitbox().Y + 50;
            }
            else if (_drawingDamage == "true")
            {
                _damageTextLocation.X += (int)_damageTextVector.X + (int)backSpeed.X;
                _damageTextLocation.Y += (int)_damageTextVector.Y + (int)backSpeed.Y;
                if (userWeapon == "wizard ball")
                    spriteBatch.DrawString(FontText, $"{userDamage * headshotMultiplyer}", new Vector2(_damageTextLocation.X, _damageTextLocation.Y), Color.Yellow);
                else
                    spriteBatch.DrawString(FontText, $"{userDamage}", new Vector2(_damageTextLocation.X, _damageTextLocation.Y), Color.Yellow);
            }

        }
        public void DrawHealth(SpriteBatch spriteBatch, Texture2D emptytTexture, Texture2D fullTexture)
        {

            spriteBatch.Draw(emptytTexture, redBar, Color.White);
            spriteBatch.Draw(fullTexture, greenBar, Color.White);

        }


    }
}
