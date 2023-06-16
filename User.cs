using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Final_Project
{

    internal class Player
    {
        private Texture2D _texture;
        private Vector2 _location;
        private Vector2 _dimentions;
        private Vector2 _velocity;
        private Rectangle greenBar;
        private Rectangle redBar;
        private Rectangle userFullBar;
        private Rectangle userFullBarGray;
        private Rectangle userEmptyBar;
        private Rectangle _rectangle;

        private DateTime lastMeleeTime = DateTime.MinValue;
        private DateTime lastMeleeTimes = DateTime.MinValue;
        private double _speed;
        private double damgaeMultiplyer;
        private Vector2 _damageTextVector;
        private Vector2 playerPosition;
        private Vector2 enemyPosition;
        private Rectangle _damageTextLocation;
        private int _health;
        private double animationSpeed = 0.1;
        private int height;
        private int width;
        private int previousHealth;
        private int _AicooldownTime;
        private int _AiFireableShots;
        private float _projectileSpeed;
        private float _projectileSpeedBoost;
        private int _damage;
        private int _maxHealth;
        private int _killpoints = 0;
        
        private double _points = 3000;
        private double _totalPoints = 0;
        private double _sheildSeconds = 12;
        private double _boostSpeed;
        private int boostDamage;
        private double _meleeSpeed;
        private double _walkingSpeed;
        private double _standingSpeed;
        private double _hitSpeed;
        private double _guardSpeed;
        private double _transSpeed;
        private double _specialSpeed;

        private float _gunInterval;
        private float _gunIntervalBoost;
        private SpriteEffects direction;
        private float enemyRotation;
        private string _weapontype;
        private string _enemyType;
        private string _attack;
        private string playHitSound;
        private string playAttackSound;

        private string _hit;
        private string _trans;
        private string _special;
        private string _guard;
        private string _drawSheild;
        private string _drawingDamage;
        private string _targeted;
        private Color hitColor;
        private Color otherColor;

        private SoundEffect attackSound;
        private SoundEffect hitSound;
        private SoundEffectInstance attackSoundInsta;
        private SoundEffectInstance hitSoundInsta;
        List<Texture2D> _walkingTextures;
        List<Texture2D> _standingTextures;
        List<Texture2D> _transTextures;
        List<Texture2D> _walkingSheildTextures;
        List<Texture2D> _standingSheildTextures;

        List<Texture2D> _meleeTextures;
        List<Texture2D> _specialTextures;
        List<Texture2D> _hitTextures;
        List<Texture2D> _guardTextures;
        Random rand = new Random();
        public Player(Vector2 location, Vector2 dimentions, int health, string weapontype, double speed, List<Texture2D> walkingTextures, List<Texture2D> standingTextures, List<Texture2D> walkingSheildTextures, List<Texture2D> standingSheildTextures, List<Texture2D> meleeTextures, List<Texture2D> AiHitTextures, List<Texture2D> TransSheildTextures, List<Texture2D> specialTextures, List<Texture2D> guardTextures, SoundEffect _attackSound, SoundEffect _hitSound)
        {
            _dimentions = dimentions;
            _location = location;

            _speed = speed;
            _damageTextLocation = new Rectangle((int)_location.X, (int)_location.Y, 120, 120);
            _rectangle = new Rectangle((int)_location.X, (int)_location.Y, (int)_dimentions.X, (int)_dimentions.Y);
            _health = health;
            _maxHealth = health;
            _velocity = new Vector2(0, 0);

            _attack = "false";
            _hit = "false";
            _drawSheild = "false";
            _drawingDamage = "false";
            _weapontype = weapontype;
            _walkingTextures = walkingTextures;
            _standingTextures = standingTextures;
            _guardTextures = guardTextures;
            _walkingSheildTextures = walkingSheildTextures;
            _standingSheildTextures = standingSheildTextures;
            _meleeTextures = meleeTextures;
            _hitTextures = AiHitTextures;
            _transTextures = TransSheildTextures;
            _specialTextures = specialTextures;
            otherColor = Color.White;
            hitColor = Color.Gray;
            redBar = new Rectangle((int)_location.X, (int)_location.Y, (int)Health + (int)Health / 4, 10);
            greenBar = new Rectangle((int)_location.X, (int)_location.Y, (int)Health, redBar.Height - redBar.Height / 5);

            userEmptyBar = new Rectangle(200, 910, (int)Health / 2 + 30, 23);
            userFullBar = new Rectangle(210, 916, (int)Health / 2, 10);
            userFullBarGray = new Rectangle(210, 916, (int)Health / 2, 10);

            attackSound = _attackSound;
            hitSound = _hitSound;
            attackSoundInsta = attackSound.CreateInstance();
            hitSoundInsta = hitSound.CreateInstance();
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
            get { return _rectangle.Right; }
            set { _rectangle.X = (int)value; }
        }

        public float YLocationBottom
        {
            get { return _rectangle.Bottom; }
            set { _rectangle.Y = (int)value; }
        }

        public float HSpeed
        {
            get { return _velocity.X; }
            set { _velocity.X = value; }
        }
        public float VSpeed
        {
            get { return _velocity.Y; }
            set { _velocity.Y = value; }
        }
        public float Health
        {
            get { return _health; }
            set { _health = (int)value; }
        }
        public float MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = (int)value; }
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
        public string EnemyType
        {
            get { return _enemyType; }
            set { _enemyType = value; }
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
        public float GunIntervalBoost
        {
            get { return _gunIntervalBoost; }
            set { _gunIntervalBoost = value; }
        }
        public Double HeadShot
        {
            get { return damgaeMultiplyer; }
            set { damgaeMultiplyer = value; }
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
        public double Points
        {
            get { return _points; }
            set { _points = value; }
        }
        public double TotalPoints
        {
            get { return _totalPoints; }
            set { _totalPoints = value; }
        }
        public double SheildSeconds
        {
            get { return _sheildSeconds; }
            set { _sheildSeconds = value; }
        }
        public float ProjectileSpeed
        {
            get { return _projectileSpeed; }
            set { _projectileSpeed = value; }
        }
        public float ProjectileSpeedBoost
        {
            get { return _projectileSpeedBoost; }
            set { _projectileSpeedBoost = value; }
        }
        public double BoostSpeed
        {
            get { return _boostSpeed; }
            set { _boostSpeed = value; }
        }
        public double AnimationSpeed
        {
            get { return animationSpeed; }
            set { animationSpeed = value; }
        }
        public int BoostDamage
        {
            get { return boostDamage; }
            set { boostDamage = value; }
        }
        public string Target
        {
            get { return _targeted; }
            set { _targeted = value; }
        }

        public void ChoosingWeapon()
        {
            //User Traits
            if (_weapontype == "wizard ball")
            {
                _damage = 26 + boostDamage;
                _gunInterval = 2.6f + _gunIntervalBoost;
                _projectileSpeed = 4 + _projectileSpeedBoost;
            }

            else if (_weapontype == "melee")
            {
                _damage = 22 + boostDamage;
                _gunInterval = 1.9f + _gunIntervalBoost;

            }
            else if (_weapontype == "sheild melee")
            {
                _damage = 80 + boostDamage;
                _gunInterval = 0f;
            }
            else if (_weapontype == "special")
            {
                _damage = 28 + boostDamage;
                _gunInterval = 8.0f + _gunIntervalBoost;
                _projectileSpeed = 5 + _projectileSpeedBoost;
            }
            else if (_weapontype == "ally melee")
            {
                _damage = 12 + boostDamage;
                _gunInterval = 2.8f + _gunIntervalBoost;
                animationSpeed = 0.11;
            }

            //Enemy Traits
            else if (_weapontype == "arrow")
            {
                _damage = 29;
                _gunInterval = 2.1f;
                _killpoints = 5;
                animationSpeed = 0.12;
                _projectileSpeed = 5;
                _enemyType = "shooter";
            }
            else if (_weapontype == "guy arrow")
            {
                _damage = 41;
                _gunInterval = 3.4f;
                _killpoints = 6;
                animationSpeed = 0.09;
                _projectileSpeed = 6;
                _enemyType = "shooter";
            }
            else if (_weapontype == "fire ball")
            {
                _damage = 52;
                _gunInterval = 5.5f;
                _killpoints = 7;
                animationSpeed = 0.08;
                _enemyType = "shooter";
                _projectileSpeed = 4;
                if (_health <= 35)
                {
                    _damage = 68;
                    _gunInterval = 3.1f;                  
                    animationSpeed = 0.11;
                    _projectileSpeed = 8;
                    otherColor = Color.Red;
                    hitColor = Color.DarkRed;
                }
            }
            else if (_weapontype == "goblin melee")
            {
                _damage = 18;
                _gunInterval = 2.4f;
                _killpoints = 4;
                _enemyType = "melee";

            }
            else if (_weapontype == "fast goblin melee")
            {
                _damage = 9;
                _gunInterval = 1.2f;
                _killpoints = 4;
                animationSpeed = 0.14;
                _enemyType = "melee";
            }
            else if (_weapontype == "skel melee")
            {
                _damage = 40;
                _gunInterval = 5f;
                _killpoints = 6;
                animationSpeed = 0.08;
                _enemyType = "melee";
                if (_health <= 45)
                {
                    _damage = 95;
                    _gunInterval = 3f;
                    _speed = 2.5;
                    animationSpeed = 0.11;
                    otherColor = Color.Red;
                    hitColor = Color.DarkRed;
                }
            }

            else if (_weapontype == "bat")
            {
                _damage = 33;
                _gunInterval = 1.1f;
                _killpoints = 4;
                animationSpeed = 0.12;
                _enemyType = "melee";
            }
            else if (_weapontype == "slayer")
            {
                _damage = 54;
                _gunInterval = 3.6f;
                _killpoints = 7;
                animationSpeed = 0.1;
                _enemyType = "melee";
            }
            else if (_weapontype == "fantasy")
            {
                _damage = 42;
                _gunInterval = 2.3f;
                _killpoints = 8;
                animationSpeed = 0.1;
                _enemyType = "melee";
                if (_health <= 45)
                {
                    _damage = 54;
                    _gunInterval = 1.8f;
                    _speed = 3.0;
                    animationSpeed = 0.11;
                    otherColor = Color.Red;
                    hitColor = Color.DarkRed;
                }
            }
            //Boss Traits
            else if (_weapontype == "minotaur")
            {
                _damage = 160;
                _gunInterval = 2.0f;
                _killpoints = 40;
                _enemyType = "melee";
                if (_health <= 200)
                {
                    _damage = 225;
                    _gunInterval = 1.2f;
                    _speed = 3.7;
                    otherColor = Color.Red;
                    hitColor = Color.DarkRed;
                }
            }
            else if (_weapontype == "wizard")
            {
                _damage = 99;
                _gunInterval = 1.2f;
                _killpoints = 35;
                _enemyType = "melee";
                if (_health <= 150)
                {
                    _damage = 139;
                    _gunInterval = 0.6f;
                    _speed = 4;
                    otherColor = Color.Red;
                    hitColor = Color.DarkRed;
                }
            }
            else if (_weapontype == "death")
            {
                _damage = 205;
                _gunInterval = 2.1f;
                _killpoints = 125;

                _enemyType = "both";
                animationSpeed = 0.11;
                _projectileSpeed = 9;
                if (_health <= 150)
                {
                    _damage = 245;
                    _gunInterval = 1.6f;
                    _speed = 3.3;
                    otherColor = Color.Black;
                    hitColor = Color.Black;
                    _projectileSpeed = 15;
                }
            }
        }
        public void TroopsSpeed(Rectangle user)
        {
            Random rand = new Random();
            if (_hit != "true")
            {   
                if (_enemyType == "shooter")
                {
                    //Move Down
                    if (user.Y -300> _rectangle.Bottom)
                    {
                        _velocity.Y = (float)_speed;
                    }
                    //Move Up
                    if (user.Bottom +300 < _rectangle.Y)
                    {
                        _velocity.Y = -(float)_speed;
                    }
                    //Move Right
                    if (user.X -300> _rectangle.Right)
                    {
                        _velocity.X = (float)_speed;
                    }
                    //Move Left
                    if (user.Right +300 < _rectangle.X )
                    {
                        _velocity.X = -(float)_speed;
                    }
                }
                else
                {
                    //Move Down
                    if (user.Y + user.Height / 2 > _rectangle.Bottom + rand.Next(0, 100))
                    {
                        _velocity.Y = (float)_speed;
                    }
                    //Move Up
                    if (user.Bottom - user.Height / 2 < _rectangle.Y + rand.Next(-100, 0))
                    {
                        _velocity.Y = -(float)_speed;
                    }
                    //Move Right
                    if (user.X + user.Width / 2 > _rectangle.Right + rand.Next(0, 100))
                    {
                        _velocity.X = (float)_speed;
                    }
                    //Move Left
                    if (user.Right - user.Width / 2 < _rectangle.X + rand.Next(-100, 0))
                    {
                        _velocity.X = -(float)_speed;
                    }
                }
               
            }
        }
        private void Move(Vector2 backSpeed, List<Barriers> barriers, string screen, Rectangle Box, string type)
        {
            if (_drawingDamage == "false")
                previousHealth = _health;

            if (_attack == "true" && _enemyType == "shooter")
            {
                _location.X += (int)backSpeed.X;
                _location.Y += (int)backSpeed.Y;
            }
            else if (_hit == "false" || type == "user")
            {
                _location.X += _velocity.X + (int)backSpeed.X;
                _location.Y += _velocity.Y + (int)backSpeed.Y;
            }           
            else
            {
                _location.X += (int)backSpeed.X;
                _location.Y += (int)backSpeed.Y;
            }
            _rectangle.X = (int)Math.Round(_location.X);
            _rectangle.Y = (int)Math.Round(_location.Y);
            if (screen == "main game")
            {
                foreach (Barriers barrier in barriers)
                {
                    if (barrier.Blocking == "true")
                    {
                        if (Box.Intersects(barrier.GetBoundingBox()))
                            UndoMoveH();
                    }
                }
                foreach (Barriers barrier in barriers)
                {
                    if (barrier.Blocking == "true")
                    {
                        if (Box.Intersects(barrier.GetBoundingBox()))
                            UndoMoveV();
                    }

                }
            }

        }

        public void UndoMoveH()
        {
            _location.X -= _velocity.X;
        }
        public void UndoMoveV()
        {
            _location.Y -= _velocity.Y;
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

        public void UserAttackMelee(List<Player> enemys, List<Barriers> barriers, List<LaserClass> laserList, List<Texture2D> texture, float mouseStateX, float mouseStateY, List<Texture2D> shotTexture, List<Texture2D> fireList1, List<Texture2D> fireList2, List<Texture2D> iceList1, List<Texture2D> iceList2, List<Texture2D> thunderList1, List<Texture2D> thunderList2)
        {

            TimeSpan timeSinceLastAttack = DateTime.Now - lastMeleeTime;
            ChoosingWeapon();
            if (timeSinceLastAttack.TotalSeconds >= _gunInterval && _drawSheild == "false")
            {

                if (_weapontype == "special")
                    SpecialAttack();
                else
                    Attack();
                lastMeleeTime = DateTime.Now; // update last shot time
                if (_weapontype == "melee" || _weapontype == "sheild melee" || _weapontype == "ally melee")
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
                else if (_weapontype == "special")
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        switch (rand.Next(1, 4))
                        {
                            case 1: shotTexture = fireList2; break;
                            case 2: shotTexture = iceList2; break;
                            case 3: shotTexture = thunderList2; break;
                             
                        }
                        playerPosition = new Vector2(rand.Next(-200, 1600), -400);
                        var distance = new Vector2(Userbox().X - playerPosition.X + rand.Next(-600, 600), Userbox().Y - playerPosition.Y);
                        float playerRotation = (float)Math.Atan2(distance.Y, distance.X);                    
                        laserList.Add(new LaserClass(shotTexture, playerPosition, playerRotation, new Rectangle(Userbox().X, Userbox().Y, 40, 35), _damage));
                        
                    }
                }
                else
                {
                    if (direction == SpriteEffects.None)
                        playerPosition = new Vector2(Userbox().Right, Userbox().Y);
                    else
                        playerPosition = new Vector2(Userbox().X, Userbox().Y);
                    var distance = new Vector2(mouseStateX - playerPosition.X, mouseStateY - playerPosition.Y);
                    float playerRotation = (float)Math.Atan2(distance.Y, distance.X);
                    
                    
                  
                    if (_damage > 45)
                        laserList.Add(new LaserClass(fireList1, playerPosition, playerRotation, new Rectangle(Userbox().X, Userbox().Y, 30, 25), _damage));
                    else
                        laserList.Add(new LaserClass(texture, playerPosition, playerRotation, new Rectangle(Userbox().X, Userbox().Y, 30, 25), _damage));
                }
            }

        }
        public void DrawEnemyAttack(Player user)
        {
            TimeSpan timeSinceLastAttacks = DateTime.Now - lastMeleeTimes;

            if (timeSinceLastAttacks.TotalSeconds >= _gunInterval)
            {
                if (user != null)
                {
                    if (_rectangle.Intersects(user.GetBoundingBox()))
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
        public void EnemyAttackMelee(Player user, List<Barriers> barriers, List<Player> allylist, List<LaserClass> enemyLaserList, Vector2 playerPositions, List<Texture2D> texture1, List<Texture2D> texture2, List<Texture2D> texture3)
        {
            TimeSpan timeSinceLastAttack = DateTime.Now - lastMeleeTime;
            if (timeSinceLastAttack.TotalSeconds >= _gunInterval)
            {
               
                if (_enemyType == "melee" || _enemyType == "both")
                {
                    if (_rectangle.Intersects(user.GetBoundingBox()))
                    {
                        lastMeleeTime = DateTime.Now; // update last shot time 
                        if (LightSaberHitBoxRight().Intersects(user.Userbox()))
                        {
                            user.Health -= _damage;
                            user.UserHit();
                        }
                        else if (LightSaberHitBoxLeft().Intersects(user.Userbox()))
                        {
                            user.Health -= _damage;
                            user.UserHit();
                        }
                    }
                    foreach (Player ally in allylist)
                    {

                        if (GetBoundingBox().Intersects(ally.GetBoundingBox()))
                        {
                            lastMeleeTime = DateTime.Now; // update last shot time 
                            DrawEnemyAttack(null);
                            ally.Health -= _damage;
                            ally.UserHit();
                        }
                    }
                }
                if (_enemyType == "shooter" && new Rectangle(-100, -100, 1600, 1000).Contains(GetBoundingBox()) || _enemyType == "both" && !_rectangle.Intersects(user.GetBoundingBox()))
                {
                    lastMeleeTime = DateTime.Now; // update last shot time 
                    if (HSpeed > 0)
                        enemyPosition = new Vector2(XLocationRight, YLocation +height/2);
                    else
                        enemyPosition = new Vector2(XLocation, YLocation + height / 2);
                   
                    float missingRange = rand.Next(-100, 100);
                    var enemydistance = new Vector2(user.Userbox().X + missingRange - enemyPosition.X, user.Userbox().Y + user.Userbox().Height/2 +missingRange - enemyPosition.Y );
                    enemyRotation = (float)Math.Atan2(enemydistance.Y, enemydistance.X);

                    if (_weapontype == "fire ball")
                        enemyLaserList.Add(new LaserClass(texture1, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 60, 35), _damage));
                    else if (_weapontype == "arrow" )
                        enemyLaserList.Add(new LaserClass(texture2, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 26, 6), _damage));
                    else if (_weapontype == "guy arrow")
                        enemyLaserList.Add(new LaserClass(texture2, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 40, 12), _damage));
                    else
                    {
                        SpecialAttack();
                        enemyLaserList.Add(new LaserClass(texture3, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 120, 60), _damage));
                    }
                }
                
                foreach (Barriers barrier in barriers)
                {
                    if (GetBoundingBox().Intersects(barrier.GetBoundingBox()) && barrier.Breakable == "true")
                    {
                        lastMeleeTime = DateTime.Now; // update last shot time 
                        DrawEnemyAttack(null);
                        barrier.TakeHit((int)_damage);


                    }
                }
            }
        }
        public string Sheilding
        {
            get { return _drawSheild; }
            set { _drawSheild = value; }
        }
        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height);
        }
        public Rectangle LightSaberHitBoxRight()
        {
            return new Rectangle(Hitbox().X + Hitbox().Width / 2, Hitbox().Y - Hitbox().Height / 2, Hitbox().Width, Hitbox().Height * 2);
        }
        public Rectangle LightSaberHitBoxLeft()
        {
            return new Rectangle(Hitbox().X - Hitbox().Width / 2, Hitbox().Y - Hitbox().Height / 2, Hitbox().Width, Hitbox().Height * 2);
        }
        public Rectangle HeadShotBox()
        {
            return new Rectangle(_rectangle.X - 1, _rectangle.Y - 1, _rectangle.Width + 2, _rectangle.Height / 3);
        }
        public Rectangle Hitbox()
        {
            return new Rectangle(_rectangle.X + _rectangle.Width / 3, _rectangle.Y, _rectangle.Width - _rectangle.Width / 2, _rectangle.Height);
        }
        public Rectangle Userbox()
        {
            return new Rectangle(_rectangle.X + _rectangle.Width / 3, _rectangle.Y + _rectangle.Height / 2, _rectangle.Width - _rectangle.Width / 2, _rectangle.Height / 2);
        }

        public Rectangle Largebox()
        {
            return new Rectangle(_rectangle.X - _rectangle.Width / 2, _rectangle.Y - _rectangle.Height / 2, _rectangle.Width * 2, _rectangle.Height * 2);
        }


        public bool Collide(Rectangle item)
        {
            return _rectangle.Intersects(item);
        }


        public void Attack()
        {
            _attack = "true";
            playAttackSound = "true";
        }
        public void SpecialAttack()
        {
            _special = "true";
        }
        public void EnemyHit()
        {
            _drawingDamage = "true";
            _hit = "true";
            playHitSound = "true";
        }
        public void UserHit()
        {
            _hit = "true";
            playHitSound = "true";
        }

        public void ResetEnemyHit()
        {
            _drawingDamage = "false";

        }
        public void Respawn()
        {
            _rectangle.X = rand.Next(350, 1050 - width);
            _rectangle.Y = rand.Next(225, 675 - height);
        }

        public void Update(Vector2 backSpeed, List<Barriers> barriers, string screen, Rectangle Box, string type)
        {

            if (_attack == "true" && WeaponType == "fire worm")
                _meleeSpeed += 0.2;
            else if (_attack == "true")
                _meleeSpeed += animationSpeed;

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

            if (_special == "true")
                _specialSpeed += 0.12;

            if (_specialSpeed >= _specialTextures.Count - 0.5)
            {
                _specialSpeed = 0;
                _special = "false";
            }


            _guardSpeed += 0.08;

            if (_guardSpeed >= _guardTextures.Count - 0.5)
            {
                _guardSpeed = 0;

            }

            if (_drawSheild == "true")
                _walkingSpeed += 0.07;

            else
                _walkingSpeed += animationSpeed;

            if (_walkingSpeed >= _walkingTextures.Count - 0.5)
            {
                _walkingSpeed = 0;
            }

            _standingSpeed += 0.1;
            if (_standingSpeed >= _standingTextures.Count - 0.5)
            {
                _standingSpeed = 0;
            }
            redBar.X = _rectangle.X + _rectangle.Width / 2 - redBar.Width / 2;
            greenBar.X = _rectangle.X + _rectangle.Width / 2 - redBar.Width / 2 + redBar.Width / 10;

            redBar.Y = _rectangle.Y;
            greenBar.Y = _rectangle.Y;
            if (greenBar.Width != (int)Health)
                greenBar.Width -= 1;


            //Maxes
            userEmptyBar.Width = _maxHealth / 2 + 30;
            if (_health >= _maxHealth)
                _health = _maxHealth;
            if (SheildSeconds <= 5)
                SheildSeconds = 5;
            if (_points <= 0)
                _points = 0;


            if (userFullBar.Width < _health / 2)
                userFullBar.Width += 2;
            else if (userFullBar.Width > _health / 2)
                userFullBar.Width -= 2;

            if (userFullBarGray.Width < _health / 2)
                userFullBarGray.Width += 2;
            else if (userFullBarGray.Width > _health / 2)
                userFullBarGray.Width -= 1;


            Move(backSpeed, barriers, screen, Box, type);
        }
        public void LowerMaxHealth()
        {
            _maxHealth -= 45;
        }

        public void Draw(SpriteBatch spriteBatch, int mouseX, Vector2 guardLocation, string type)
        {
            if (new Rectangle(-100, -100, 1600, 1000).Intersects(GetBoundingBox()))
            {
                if (HSpeed < 0 && mouseX < _rectangle.X)

                    direction = SpriteEffects.FlipHorizontally;

                else if (HSpeed > 0)

                    direction = SpriteEffects.None;


                else if (HSpeed < 0)

                    direction = SpriteEffects.FlipHorizontally;

                else if (mouseX < _rectangle.X)
                    direction = SpriteEffects.FlipHorizontally;

                else
                    direction = SpriteEffects.None;
                if (type == "user")
                {
                    if (_hit == "true")
                        otherColor = Color.Gray;
                    else
                        otherColor = Color.White;
                }
               
                //Animations
                if (_drawSheild == "false")
                {
                    
                    if (hitSoundInsta.State == SoundState.Stopped && playHitSound == "true")
                    {                        
                        hitSoundInsta.Play();
                        playHitSound = "false";
                    }
                    if (_hit == "true" && type != "user")                                            
                        spriteBatch.Draw(_hitTextures[(int)Math.Round(_hitSpeed)], _rectangle, null, hitColor, 0f, new Vector2(0, 0), direction, 0f);
                    
                    else if (_special == "true")                   
                        spriteBatch.Draw(_specialTextures[(int)Math.Round(_specialSpeed)], _rectangle, null, otherColor, 0f, new Vector2(0, 0), direction, 0f);

                   
                    else if (_attack == "true")
                    {
                        if (attackSoundInsta.State == SoundState.Stopped && playAttackSound == "true")
                        {
                            attackSoundInsta.Play();
                            playAttackSound = "false";
                        }
                        spriteBatch.Draw(_meleeTextures[(int)Math.Round(_meleeSpeed)], _rectangle, null, otherColor, 0f, new Vector2(0, 0), direction, 0f);

                    }
                    else if (HSpeed != 0 || VSpeed != 0)
                    
                        spriteBatch.Draw(_walkingTextures[(int)Math.Round(_walkingSpeed)], _rectangle, null, otherColor, 0f, new Vector2(0, 0), direction, 0f);

                    else                   
                        spriteBatch.Draw(_standingTextures[(int)Math.Round(_standingSpeed)], _rectangle, null, otherColor, 0f, new Vector2(0, 0), direction, 0f);           
                }
                //Sheild Animations
                else
                {
                    if (_hit == "true")
                    {
                        if (hitSoundInsta.State == SoundState.Stopped && playHitSound == "true")
                        {
                            hitSoundInsta.Play();
                            playHitSound = "false";
                        }
                        spriteBatch.Draw(_guardTextures[(int)Math.Round(_guardSpeed)], new Rectangle((int)guardLocation.X, (int)guardLocation.Y, 24, 30), null, otherColor, 0f, new Vector2(0, 0), direction, 0f);
                    }
                    if (_trans == "true")
                        spriteBatch.Draw(_transTextures[(int)Math.Round(_transSpeed)], _rectangle, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);

                    else if (HSpeed != 0 || VSpeed != 0)                    
                        spriteBatch.Draw(_walkingSheildTextures[(int)Math.Round(_walkingSpeed)], _rectangle, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);
                    else
                    
                        spriteBatch.Draw(_standingSheildTextures[(int)Math.Round(_standingSpeed)], _rectangle, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);                
                }
            }
        }
        public void DrawDamage(SpriteBatch spriteBatch, SpriteFont FontText, Vector2 backSpeed, string userWeapon)
        {

            _damageTextLocation.X += (int)_damageTextVector.X + (int)backSpeed.X;
            _damageTextLocation.Y += (int)_damageTextVector.Y + (int)backSpeed.Y;
            if (!_damageTextLocation.Intersects(Hitbox()))
            {
                _damageTextVector = new Vector2(rand.Next(-2, 2), rand.Next(-2, -1));
                if (_damageTextVector.X == 0)
                    _damageTextVector.X = 2;
                HeadShot = 1;
                ResetEnemyHit();
                _damageTextLocation.X = Hitbox().X + 50;
                _damageTextLocation.Y = Hitbox().Y;
            }
            else if (_drawingDamage == "true")
            {

                _targeted = "not a target";

                if (damgaeMultiplyer != 1)
                    spriteBatch.DrawString(FontText, $"{(previousHealth - _health) * damgaeMultiplyer}", new Vector2(_damageTextLocation.X, _damageTextLocation.Y), Color.Yellow);
                else
                    spriteBatch.DrawString(FontText, $"{previousHealth - _health}", new Vector2(_damageTextLocation.X, _damageTextLocation.Y), Color.White);
            }

        }
        public void DrawHealth(SpriteBatch spriteBatch, Texture2D emptytTexture, Texture2D fullTextureRed, Texture2D grayTexture, bool bossBattle, string player)
        {
            if (bossBattle == true)
            {
                redBar.X = 1400 / 2 - redBar.Width / 2;
                redBar.Y = 80;
                greenBar.X = 1400 / 2 - redBar.Width / 2 + redBar.Width / 10;
                greenBar.Y = 82;
                redBar.Height = 18;
                greenBar.Height = redBar.Height - redBar.Height / 4;

            }
            if (player == "user")
            {
                spriteBatch.Draw(emptytTexture, userEmptyBar, Color.White);
                spriteBatch.Draw(grayTexture, userFullBarGray, Color.White);
                spriteBatch.Draw(fullTextureRed, userFullBar, Color.White);

            }
            else
            {
                spriteBatch.Draw(emptytTexture, redBar, Color.White);
                spriteBatch.Draw(fullTextureRed, greenBar, Color.White);
            }

        }
    }
}
