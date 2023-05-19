using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        DateTime lastShotTime = DateTime.MinValue;
        DateTime cooldownTime = DateTime.MinValue;
        DateTime cooldownTimeBar = DateTime.MinValue;
        DateTime cooldownTimeAi = DateTime.MinValue;
        DateTime lastShotTimeAi = DateTime.MinValue;
        DateTime lastMeleeTimeAi = DateTime.MinValue;
        private SpriteBatch _spriteBatch;
        Player user;

      


        Texture2D rectangleTexture, AiSkelHitTexture, AiGoblinHitTexture,AiSkelMeleeRightTexture, AiSkelWalkingRight,wizardCrosshair, darkTreeTexture, grayRockTexture, darkerTreeTexture, healthGreenBarTexture, emptyGreenBarTexture,userSheildWalkTexture, userSheildIdleTexture, lightningTexture1, lightningTexture2, lightningTexture3, arrowTexture, AiArcherWalkingRight, AiArcherWalkingLeft, AiArcherMeleeRightTexture, stormtroperAimingLeft, AiWalkingRight, AiWalkingLeft, AiMeleeRightTexture, laserTexture, userWalkingRight, userWalkingLeft, userAttackRightTexture, userAttackLeftTexture, userIdleTexture, userIdleLeftTexture;
        Vector2 backroundSpeed;

        int mainGameWidth = 1400;
        int mainGameHeight = 900;

        int mainScreenHeight = 1000;
        int movedDistanceX = 0;
        int movedDistanceY = 0;
        int wave = 0;
       
        bool RespawnMethold = false;
        

        

        double damgaeMultiplyer = 1;
        float seconds;
        float startTime;

        private float playerRotation;
        private float enemyRotation;
        private Vector2 playerPosition;
        private Vector2 enemyPosition;
        private SpriteFont healthFont;
        private SpriteFont damageFont;


        List<Texture2D> userRightList;       
        List<Texture2D> userAttackList;     
        List<Texture2D> userIdleList;      

        List<Texture2D> userSheildWalkList;
        List<Texture2D> userSheildIdleList;

        List<Texture2D> AiRightList;       
        List<Texture2D> AiMeleeRightList;
        List<Texture2D> AiGoblinHitList;

        List<Texture2D> AiArcherRightList;  
        List<Texture2D> AiArcherMeleeRightList;
        List<Texture2D> AiArcherHitList;

        List<Texture2D> AiSkelRightList;
        List<Texture2D> AiSkelMeleeRightList;
        List<Texture2D> AiSkelHitList;

        List<Texture2D> LightningShotList1;
        List<Texture2D> LightningShotList2;
        List<Texture2D> LightningShotList3;

        List<Texture2D> ArrowShotList;

        List<Barriers> barriersList = new List<Barriers>();
        List<Player> stormtrooperlist = new List<Player>();

        List<LaserClass> laserList = new List<LaserClass>();
        List<LaserClass> enemyLaserList = new List<LaserClass>();
        Random rand = new Random();

        MouseState mouseState;
        KeyboardState keyboardState;
        enum Screen
        {
            TitleScreen,
            MainScreen,
            PauseScreen,
            OutroScreen
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }
        //-----------------------------------------------------------------------Initialize--------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.TitleScreen;

            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 600; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            base.Initialize();
            //Texture, x, y, width, health, heatup amount, firable shots
            user = new Player(new Rectangle(500, 500, 150, 130), 1000000, "melee", "normal", userRightList, userIdleList, userSheildWalkList, userSheildIdleList, userAttackList, AiRightList);


            //Top Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, 60), 100000));
            //Left Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, 60, mainGameHeight * 3), 100000));
            //Bottom Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, mainGameHeight * 2 - 60, mainGameWidth * 3, 60), 100000));
            //Right Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(mainGameWidth * 2 - 60, -mainGameHeight, 60, mainGameHeight * 3), 100000));


        }
        //----------------------------------------------------------------------LoadContent--------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");

            emptyGreenBarTexture = Content.Load<Texture2D>("EmptyGreenBar");
            healthGreenBarTexture = Content.Load<Texture2D>("HealhGreenBar");

            healthFont = Content.Load<SpriteFont>("HealthFont");
            damageFont = Content.Load<SpriteFont>("DamageText");

            wizardCrosshair = Content.Load<Texture2D>("WizardCrosshair");

            grayRockTexture = Content.Load<Texture2D>("NatureSprite-Gray-Rock");
            darkTreeTexture = Content.Load<Texture2D>("NatureSprite-Dark-Tree");
            darkerTreeTexture = Content.Load<Texture2D>("NatureSprite-Darker-Tree");

            userWalkingRight = Content.Load<Texture2D>("spritesheet (2)");
            userWalkingLeft = Content.Load<Texture2D>("spritesheet (2)");
            userAttackRightTexture = Content.Load<Texture2D>("spritesheet (5)");
            userAttackLeftTexture = Content.Load<Texture2D>("spritesheet (5)L");
            userIdleTexture = Content.Load<Texture2D>("spritesheet (10)");

            userSheildWalkTexture = Content.Load<Texture2D>("UserSheildWalkSprite");
            userSheildIdleTexture = Content.Load<Texture2D>("UserSheildIdleSprite");

            AiWalkingRight = Content.Load<Texture2D>("Goblin Running Right");            
            AiMeleeRightTexture = Content.Load<Texture2D>("Attack");
            AiGoblinHitTexture = Content.Load<Texture2D>("Take Hit_Goblin");

            AiArcherWalkingRight = Content.Load<Texture2D>("RunArcher_scaled");          
            AiArcherMeleeRightTexture = Content.Load<Texture2D>("AttackArcher_scaled");
            Texture2D AiArcherHitTexture = Content.Load<Texture2D>("Take Hit_Archer");

            AiSkelWalkingRight = Content.Load<Texture2D>("Walk_Skel");           
            AiSkelMeleeRightTexture = Content.Load<Texture2D>("Attack_Skel");
            AiSkelHitTexture = Content.Load<Texture2D>("Take Hit_Skel");


            lightningTexture1 = Content.Load<Texture2D>("lightning1");
            lightningTexture2 = Content.Load<Texture2D>("lightning2");
            lightningTexture3 = Content.Load<Texture2D>("lightning3");
            arrowTexture = Content.Load<Texture2D>("ArrowMove");

            static void ReapetingAnimation(GraphicsDevice graphicsDevice, Texture2D _texture, List<Texture2D> _textureList, int _diffImages)
            {


                Texture2D cropTexture;

                Rectangle sourceRect;

                int width = _texture.Width / _diffImages;
                int height = _texture.Height;


                for (int x = 0; x < _diffImages; x++)
                {
                    sourceRect = new Rectangle(x * width, 0, width, height);
                    cropTexture = new Texture2D(graphicsDevice, width, height);

                    Color[] data = new Color[width * height];
                    _texture.GetData(0, sourceRect, data, 0, data.Length);

                    cropTexture.SetData(data);

                    _textureList.Add(cropTexture);

                }


            }
            



            ReapetingAnimation(GraphicsDevice, userWalkingRight, userRightList = new List<Texture2D>(), 6);            
            ReapetingAnimation(GraphicsDevice, userAttackRightTexture, userAttackList = new List<Texture2D>(), 7);           
            ReapetingAnimation(GraphicsDevice, userIdleTexture, userIdleList = new List<Texture2D>(), 5);

            ReapetingAnimation(GraphicsDevice, userSheildWalkTexture, userSheildWalkList = new List<Texture2D>(), 6);
            ReapetingAnimation(GraphicsDevice, userSheildIdleTexture, userSheildIdleList = new List<Texture2D>(), 5);


            ReapetingAnimation(GraphicsDevice, AiWalkingRight, AiRightList = new List<Texture2D>(), 8);          
            ReapetingAnimation(GraphicsDevice, AiMeleeRightTexture, AiMeleeRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, AiGoblinHitTexture, AiGoblinHitList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, AiArcherWalkingRight, AiArcherRightList = new List<Texture2D>(), 8);         
            ReapetingAnimation(GraphicsDevice, AiArcherMeleeRightTexture, AiArcherMeleeRightList = new List<Texture2D>(), 6);
            ReapetingAnimation(GraphicsDevice, AiArcherHitTexture, AiArcherHitList = new List<Texture2D>(), 3);

            ReapetingAnimation(GraphicsDevice, AiSkelWalkingRight, AiSkelRightList = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, AiSkelMeleeRightTexture, AiSkelMeleeRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, AiSkelHitTexture, AiSkelHitList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, lightningTexture1, LightningShotList1 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningTexture2, LightningShotList2 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningTexture3, LightningShotList3 = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, arrowTexture, ArrowShotList = new List<Texture2D>(), 4);
        }
        //-----------------------------------------------------------------------Update--------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            static void AddGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 200, 100), 100, "goblin melee", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList));

            }
            static void AddFastGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 180, 90), 80, "goblin melee", "fast", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList));

            }

            static void AddSkel(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List< Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 200, 100), 130, "skel melee", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList));

            }
            static void AddArcher(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 200, 100), 95, "arrow", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList));

            }

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            if (screen == Screen.TitleScreen)
            {


                if (keyboardState.IsKeyDown(Keys.S))
                {
                    screen = Screen.MainScreen;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    _graphics.PreferredBackBufferWidth = mainGameWidth; // Sets the width of the window
                    _graphics.PreferredBackBufferHeight = mainScreenHeight; // Sets the height of the window
                    _graphics.ApplyChanges(); // Applies the new dimensions
                    //Texture, x, y, width, height, speed, health, weapon type
                    AddGoblin(stormtrooperlist, AiRightList, AiMeleeRightList, AiGoblinHitList);
                    AddSkel(stormtrooperlist, AiSkelRightList, AiSkelMeleeRightList, AiSkelHitList);
                    AddArcher(stormtrooperlist, AiArcherRightList, AiArcherMeleeRightList, AiArcherHitList);
                }



            }
            else if (screen == Screen.MainScreen)
            {

                if (wave == 0)
                {
                    for (int i = 0; barriersList.Count <= 30; i++)
                    {
                        barriersList.Add(new Barriers(grayRockTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 2), rand.Next(50, 80), rand.Next(50, 80)), 60));
                    }
                    for (int i = 0; barriersList.Count <= 50; i++)
                    {
                        barriersList.Add(new Barriers(darkTreeTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 2), rand.Next(120, 140), rand.Next(150, 180)), 80));
                    }
                    for (int i = 0; barriersList.Count <= 65; i++)
                    {
                        barriersList.Add(new Barriers(darkerTreeTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 2), rand.Next(120, 140), rand.Next(150, 180)), 100));
                    }
                    wave = 1;
                }


                if (stormtrooperlist.Count == 0)
                {
                    if (keyboardState.IsKeyDown(Keys.P))
                    {
                        wave += 1;
                        RespawnMethold = false;
                        for (int i = 0; stormtrooperlist.Count <= 1 + wave/4; i++)
                        {
                            AddArcher(stormtrooperlist, AiArcherRightList, AiArcherMeleeRightList,AiArcherHitList);
                            AddGoblin(stormtrooperlist, AiRightList, AiMeleeRightList, AiGoblinHitList);
                            AddFastGoblin(stormtrooperlist, AiRightList, AiMeleeRightList, AiGoblinHitList);
                        }

                    }
                }


                var distance = new Vector2(mouseState.X - playerPosition.X, mouseState.Y - playerPosition.Y);

                playerRotation = (float)Math.Atan2(distance.Y, distance.X);
                playerPosition = new Vector2(user.XLocationRight, user.YLocation);



                //Move user if barrier spawned in them
                while (RespawnMethold == false)
                {

                    for (int i = barriersList.Count - 1; i >= 0; i--)
                    {
                        Barriers barrier = barriersList[i];
                        if (user.Collide(barrier.GetBoundingBox()))
                        {
                            barriersList.RemoveAt(i);
                            break;
                        }
                    }
                    foreach (Player troops in stormtrooperlist)
                    {
                        for (int i = barriersList.Count - 1; i >= 0; i--)
                        {
                            Barriers barrier = barriersList[i];
                            if (troops.Collide(barrier.GetBoundingBox()))
                            {
                                barriersList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    RespawnMethold = true;
                }



                //Make user move

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if(user.Sheilding == "false")
                        user.HSpeed = 3;
                    else
                        user.HSpeed = 2;




                }

                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    

                    if (user.Sheilding == "false")
                        user.HSpeed = -3;
                    else
                        user.HSpeed = -2;

                }


                else
                {


                    user.HSpeed = 0;
                }


                if (keyboardState.IsKeyDown(Keys.W))
                {


                    if (user.Sheilding == "false")
                        user.VSpeed = -3;
                    else
                        user.VSpeed = -2;

                }
                
                

                else if (keyboardState.IsKeyDown(Keys.S))
                {


                    if (user.Sheilding == "false")
                        user.VSpeed = 3;
                    else
                        user.VSpeed = 2;
                }

                else
                {


                    user.VSpeed = 0;
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                    user.Sheild();
                else if (keyboardState.IsKeyUp(Keys.Space))
                    user.UnSheild();





                //Make ai move
                foreach (Player troops in stormtrooperlist)
                {

                    troops.TroopsSpeed(user.Hitbox());

                }

                backroundSpeed.X = 0;
                backroundSpeed.Y = 0;
                
                if (user.YLocationBottom >= mainGameHeight - mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.S))
                {
                    backroundSpeed.Y = user.VSpeed * -1;
                    if (movedDistanceY < mainGameHeight)
                        user.UndoMoveV();
                    else
                        backroundSpeed.Y = 0;
                }


                if (user.YLocation <= mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.W))
                {
                    backroundSpeed.Y = user.VSpeed * -1;
                    if (movedDistanceY > -mainGameHeight)
                        user.UndoMoveV();
                    else
                        backroundSpeed.Y = 0;
                }


                if (user.XLocationRight >= mainGameWidth - mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.D))
                {
                    backroundSpeed.X = user.HSpeed * -1;
                    if (movedDistanceX < mainGameWidth)
                        user.UndoMoveH();
                    else
                        backroundSpeed.X = 0;

                }


                if (user.XLocation <= mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.A))
                {
                    backroundSpeed.X = user.HSpeed * -1;
                    if (movedDistanceX > -mainGameWidth)
                        user.UndoMoveH();
                    else
                        backroundSpeed.X = 0;

                }
                

                movedDistanceX -= (int)backroundSpeed.X;
                movedDistanceY -= (int)backroundSpeed.Y;

                //Update User
                user.Update(new Vector2(0, 0), barriersList);

             
              

                foreach (Player troops in stormtrooperlist)
                {
                    troops.Update(backroundSpeed, barriersList);


                }
                foreach (Barriers barrier in barriersList)
                {
                    barrier.Update(backroundSpeed, barriersList);

                }
                //User Shots
                TimeSpan timeSinceLastShot = DateTime.Now - lastShotTime;

                if (timeSinceLastShot.TotalSeconds >= user.GunInterval && user.Sheilding == "false")
                {


                    if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        user.WeaponType = "melee";
                        lastShotTime = DateTime.Now; // update last shot time
                        user.UserAttackMelee(stormtrooperlist, user, barriersList);

                    }
                    else if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        lastShotTime = DateTime.Now; // update last shot time
                        user.WeaponType = "wizard ball";
                        if (user.HSpeed >= 0)
                            playerPosition = new Vector2(user.XLocationRight, user.YLocation);
                        else
                            playerPosition = new Vector2(user.XLocation, user.YLocation);


                        laserList.Add(new LaserClass(LightningShotList2, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 25, 20)));
                        user.Attack();

                    }
                }




                        //Ai Shots
                TimeSpan timeSinceLastShotAi = DateTime.Now - lastShotTimeAi;
                

                foreach (Player troops in stormtrooperlist)
                {

                    //Enemy Melee user
                    if (user.Sheilding == "false")
                        troops.EnemyAttackMelee(stormtrooperlist, user, barriersList);

                    //Enemy Melee Barrier
                    foreach (Barriers barrier in barriersList)
                    {
                        
                        
                        if (troops.Hitbox().Intersects(barrier.GetBoundingBox()))
                        {
                            troops.EnemyAttackMelee(null, null, barriersList);
                        }
                        
                            

                    }

                    //Enemy Shoot
                    if (timeSinceLastShotAi.TotalSeconds >= troops.GunInterval && user.Sheilding == "false")
                    {
                        

                        if (troops.WeaponType == "arrow")
                        {
                                if (troops.HSpeed > 0)
                                enemyPosition = new Vector2(troops.XLocationRight, troops.YLocation);
                            else
                                enemyPosition = new Vector2(troops.XLocation, troops.YLocation);

                            float missingRange = rand.Next(-75, 75);
                            var enemydistance = new Vector2(playerPosition.X + missingRange - enemyPosition.X, playerPosition.Y + missingRange - enemyPosition.Y);
                            enemyRotation = (float)Math.Atan2(enemydistance.Y, enemydistance.X);

                        

                            enemyLaserList.Add(new LaserClass(ArrowShotList, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 30, 8)));
                            troops.Attack();
                            lastShotTimeAi = DateTime.Now; // update last shot time 
                        }
                       
                    }
                     
                }
              //Update User Laser
                foreach (LaserClass bullet in laserList)
                {

                    bullet.Update(gameTime);

                }
                //Update Enemy Laser
                foreach (LaserClass bullet in enemyLaserList)
                {

                    bullet.Update(gameTime);

                }


                foreach (Player troops in stormtrooperlist)
                {


                    troops.ChoosingWeapon();
                }
                user.ChoosingWeapon();
                //If ai gets shot
                for (int i = laserList.Count - 1; i >= 0; i--)
                {

                    LaserClass laser = laserList[i];
                    foreach (Player troops in stormtrooperlist)
                    {
                        if (troops.Hitbox().Contains(laser.GetBoundingBox()))
                        {
                            damgaeMultiplyer = 1;
                            if (troops.HeadShotBox().Intersects(laser.GetBoundingBox()))
                            {
                                damgaeMultiplyer = 1.5f;

                            }

                            troops.Health -= user.WeaponDamage * (float)damgaeMultiplyer;
                            troops.EnemyHit();
                            laserList.RemoveAt(i);
                            break;
                        }




                    }

                }
                //If user gets Shot
                for (int i = enemyLaserList.Count - 1; i >= 0; i--)
                {
                    
                        foreach (LaserClass bullet in enemyLaserList)
                        {

                            if (bullet.GetBoundingBox().Intersects(user.Hitbox()) && user.Sheilding == "false")
                            {
                                foreach (Player troops in stormtrooperlist)
                                {
                                    user.Health -= troops.WeaponDamage;
                                }
                                enemyLaserList.RemoveAt(i);
                                break;
                            }



                        }

                    
                }
                //If laser hits barrier
                for (int i = laserList.Count - 1; i >= 0; i--)
                {
                    LaserClass t = laserList[i];
                    foreach (Barriers barrier in barriersList)
                    {
                        if (t.Collide(barrier.GetBoundingBox()))
                        {
                            barrier.Health -= (int)user.WeaponDamage;
                            laserList.RemoveAt(i);
                            break;
                        }
                    }
                }
                //If Enemy laser hits barrier
                for (int i = enemyLaserList.Count - 1; i >= 0; i--)
                {
                    LaserClass t = enemyLaserList[i];
                    foreach (Barriers barrier in barriersList)
                    {
                        if (t.Collide(barrier.GetBoundingBox()))
                        {
                            foreach (Player troops in stormtrooperlist)
                                barrier.Health -= (int)troops.WeaponDamage;
                            enemyLaserList.RemoveAt(i);
                            break;
                        }
                    }
                }

                //Detect user dealth
                if (user.Health <= 0)
                {
                    screen = Screen.OutroScreen;
                }
                //Detect ai dealth
                for (int i = stormtrooperlist.Count - 1; i >= 0; i--)
                {
                    Player troops = stormtrooperlist[i];

                    if (troops.Health <= 0)
                    {
                        stormtrooperlist.RemoveAt(i);

                    }
                }
                //Detect barrier dealth
                for (int i = barriersList.Count - 1; i >= 0; i--)
                {
                    Barriers barrier = barriersList[i];

                    if (barrier.Health <= 0)
                    {
                        barriersList.RemoveAt(i);

                    }
                }




            }
            else if (screen == Screen.PauseScreen)
            {

            }
            else if (screen == Screen.OutroScreen)
            {




            }
            base.Update(gameTime);
        }
        //-----------------------------------------------------------------Draw--------------------------------------------------------------------------------------
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.TitleScreen)
            {




            }
            else if (screen == Screen.MainScreen)
            {
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkOliveGreen);

              


                foreach (Barriers barrier in barriersList)
                    barrier.Draw(_spriteBatch);

                //Draw all the bullets

                foreach (LaserClass bullet in laserList)
                    bullet.Draw(_spriteBatch, laserTexture);

                foreach (Player ai in stormtrooperlist)
                {
                    ai.Draw(_spriteBatch, 100000);
                    ai.DrawHealth(_spriteBatch, emptyGreenBarTexture, healthGreenBarTexture);
                    ai.DrawDamage(_spriteBatch, damageFont, (int)user.WeaponDamage, damgaeMultiplyer, backroundSpeed, user.WeaponType);
                }
                user.Draw(_spriteBatch, mouseState.X);

                foreach (LaserClass bullet in enemyLaserList)
                    bullet.Draw(_spriteBatch, arrowTexture);


                //Hud               

                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.DrawString(healthFont, $"{user.Health}", new Vector2(550, 900), Color.White);
                _spriteBatch.DrawString(healthFont, $"{wave}", new Vector2(400, 900), Color.White);
                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X- 18, mouseState.Y, 50, 50), Color.White);

            }
            else if (screen == Screen.PauseScreen)
            {

            }
            else if (screen == Screen.OutroScreen)
            {




            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}