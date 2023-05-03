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
        Animation AnimationClass;
        Rectangle cooldownBarRed, cooldownBarWhite;
       

        Texture2D rectangleTexture, stormtroperAimingLeft, AiWalkingRight, AiWalkingLeft, AiMeleeRightTexture,laserTexture, userWalkingRight, userWalkingLeft, userAttackRightTexture, userAttackLeftTexture, userIdleTexture, userIdleLeftTexture;
        Vector2 backroundSpeed;

        int mainGameWidth = 1400;
        int mainGameHeight = 900;

        int mainScreenHeight = 1000;
        int movedDistanceX = 0;
        int movedDistanceY = 0;
      
        bool UserFacingRight = true;
        bool userSwingAttack = false;
        string enemyDirection;

        bool goingUp = false;
        bool goingDown = false;
        bool cooldownTimer = false;
        int firedShotsAi = 0;

        float damgaeMultiplyer = 0.0f;
        float seconds; 
        float startTime;
        
        private float playerRotation;
        private float enemyRotation;
        private Vector2 playerPosition; 
        private Vector2 enemyPosition;
        private SpriteFont healthFont;


        List<Texture2D> AiRightList;
        List<Texture2D> AiLeftList;
        List<Texture2D> AiMeleeRightList;
        List<Texture2D> userRightList;
        List<Texture2D> userLeftList;
        List<Texture2D> userAttackList;
        List<Texture2D> userAttackLeftList;
        List<Texture2D> userIdleList;
        List<Texture2D> userIdleLeftList;
        

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
            IsMouseVisible = true;
        }
        //-----------------------------------------------------------------------Initialize--------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.TitleScreen;
            
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 600; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            cooldownBarRed = new Rectangle(160, mainScreenHeight - 50, 0, 25);
            cooldownBarWhite = new Rectangle(160, mainScreenHeight - 50, 200, 25);

           
           
            userRightList = new List<Texture2D>();
            userLeftList = new List<Texture2D>();
            userAttackList = new List<Texture2D>();
            userAttackLeftList = new List<Texture2D>();
            userIdleList = new List<Texture2D>();
            userIdleLeftList = new List<Texture2D>();

            AiRightList = new List<Texture2D>();
            AiLeftList = new List<Texture2D>();
            AiMeleeRightList = new List<Texture2D>();



            base.Initialize();
            //Texture, x, y, width, health, heatup amount, firable shots
            user = new Player(new Rectangle( 500, 500, 170, 150), 1000000, "melee", "normal", userRightList, userIdleList, userAttackList);
           

            //Top Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth *3,60)));
            //Left Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight,60, mainGameHeight*3)));
            //Bottom Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, mainGameHeight*2- 60, mainGameWidth*3, 60)));
            //Right Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(mainGameWidth*2 - 60, -mainGameHeight, 60, mainGameHeight*3)));


        }
        //----------------------------------------------------------------------LoadContent--------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");

            healthFont = Content.Load<SpriteFont>("HealthFont");

            
            userWalkingRight = Content.Load<Texture2D>("spritesheet (2)");
            userWalkingLeft = Content.Load<Texture2D>("spritesheet (6)");
            userAttackRightTexture = Content.Load<Texture2D>("spritesheet (5)");
            userAttackLeftTexture = Content.Load<Texture2D>("spritesheet (5)L");
            userIdleTexture = Content.Load<Texture2D>("spritesheet (1)");

            AiWalkingRight = Content.Load<Texture2D>("Goblin Running Right");
            AiWalkingLeft = Content.Load<Texture2D>("Goblin Running Left");
            AiMeleeRightTexture = Content.Load<Texture2D>("Attack");

            userIdleLeftTexture = Content.Load<Texture2D>("NightBorne IdleLeft Scaled");
            laserTexture = Content.Load<Texture2D>("Ice Shot"); 

          
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
          
            ReapetingAnimation(GraphicsDevice, userWalkingRight, userRightList,6);
            ReapetingAnimation(GraphicsDevice, userWalkingLeft, userLeftList, 6);
            ReapetingAnimation(GraphicsDevice, userAttackRightTexture, userAttackList, 7);
            ReapetingAnimation(GraphicsDevice, userAttackLeftTexture, userAttackLeftList, 7);
            ReapetingAnimation(GraphicsDevice, userIdleTexture, userIdleList, 5);

            ReapetingAnimation(GraphicsDevice, AiWalkingRight, AiRightList, 8);
            ReapetingAnimation(GraphicsDevice, AiWalkingLeft, AiLeftList, 8);
            ReapetingAnimation(GraphicsDevice, AiMeleeRightTexture, AiMeleeRightList, 8);
            
    

        }
        //-----------------------------------------------------------------------Update--------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
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
                    stormtrooperlist.Add(new Player( new Rectangle(100, 100, 200, 100), 100 , "melee", "slow", AiRightList, AiRightList, AiMeleeRightList));
                    stormtrooperlist.Add(new Player(new Rectangle(500, 300, 200, 100), 150 , "melee", "fast", AiRightList, AiRightList, AiMeleeRightList));
                }



            }
            else if (screen == Screen.MainScreen)
            {


                for (int i = 0; barriersList.Count <= 35; i++)
                {
                    barriersList.Add(new Barriers(rectangleTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth*2), rand.Next(-mainGameHeight, mainGameHeight*2), rand.Next(50, 200), rand.Next(50, 200))));
                }
                    

              

                var distance = new Vector2(mouseState.X - playerPosition.X, mouseState.Y - playerPosition.Y);

                playerRotation = (float)Math.Atan2(distance.Y, distance.X);
                playerPosition = new Vector2(user.XLocationRight, user.YLocation);
                
                seconds = (float)gameTime.TotalGameTime.TotalSeconds;
                
                //Move user if barrier spawned in them
                if (seconds <= 1)
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
                       
                }
                
                //Make user move

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    user.HSpeed = 3;
                    UserFacingRight = true;



                }
                    
                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    user.HSpeed = -3;
                    UserFacingRight = false;

                }


                else
                {
                 
                    
                    user.HSpeed = 0;
                }
                    

                if (keyboardState.IsKeyDown(Keys.W))
                {
                    
                    
                    user.VSpeed = -3;
                }
                    
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                   
                    
                    user.VSpeed = 3;
                }
                    
                else
                {
                   
                    
                    user.VSpeed = 0;
                }
             




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
                
               
                
                


                
             
               
               
                user.Update(new Vector2(0,0));

                //Make barriers for user
                foreach (Barriers barrier in barriersList)
                    if (barrier.GetBoundingBox().Intersects(user.Hitbox()))
                    {
                        backroundSpeed.X = 0;
                        backroundSpeed.Y = 0;
                        user.UndoMove();
                    }
                


                foreach (Player troops in stormtrooperlist)
                {
                    troops.Update(backroundSpeed);

                }
                foreach (Barriers barrier in barriersList)
                {
                    barrier.Update(backroundSpeed);

                }
                

                //User Shots
                TimeSpan timeSinceLastShot = DateTime.Now - lastShotTime;

                 

                    if (timeSinceLastShot.TotalSeconds >= user.GunInterval && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        
                        
                        lastShotTime = DateTime.Now; // update last shot time
                        if (user.WeaponType == "lightsaber")
                        {

                            userSwingAttack = true;
                            foreach (Player troops in stormtrooperlist)
                            {
                                
                                
                                    if (UserFacingRight == true)
                                    {
                                        if (troops.Collide(user.LightSaberHitBoxRight()))
                                            troops.Health -= user.WeaponDamage;
                                    }
                                    else
                                    {
                                        if (troops.Collide(user.LightSaberHitBoxLeft()))
                                            troops.Health -= user.WeaponDamage;
                                    }

                            }

                        }
                        else
                        {
                            laserList.Add(new LaserClass(rectangleTexture, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 30, 8)));
                            
                            

                        }
                    }
                        
              

                //Ai Shots
                TimeSpan timeSinceLastShotAi = DateTime.Now - lastShotTimeAi;
                
                
                foreach (Player troops in stormtrooperlist)
                {

                    

                    if (timeSinceLastShotAi.TotalSeconds >= troops.GunInterval)
                    {
                        if (troops.HSpeed > 0)
                            enemyPosition = new Vector2(troops.XLocationRight, troops.YLocation);
                        else
                            enemyPosition = new Vector2(troops.XLocation, troops.YLocation);

                        float missingRange = rand.Next(-75, 75);
                        var enemydistance = new Vector2(playerPosition.X + missingRange - enemyPosition.X, playerPosition.Y + missingRange - enemyPosition.Y);
                        enemyRotation = (float)Math.Atan2(enemydistance.Y, enemydistance.X);

                        if (troops.WeaponType == "arrow")
                        {
                            
                            enemyLaserList.Add(new LaserClass(rectangleTexture, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 30, 8)));

                        }
                        else if (troops.WeaponType == "wizard ball")
                        {

                            enemyLaserList.Add(new LaserClass(rectangleTexture, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 30, 30)));

                        }
                        lastShotTimeAi = DateTime.Now; // update last shot time 
                           


                    }
                    
                  

                }
                // AI Melee
                TimeSpan timeSinceLastMelee= DateTime.Now - lastMeleeTimeAi;
                foreach (Player troops in stormtrooperlist)
                {
                    
                    if (timeSinceLastMelee.TotalSeconds >= troops.GunInterval)
                    {
                        if (troops.WeaponType == "melee")
                        {
                            troops.AiMelee(user.GetBoundingBox(), troops.GunInterval, timeSinceLastMelee.TotalSeconds);
                            if (troops.MeleeCollide(user.GetBoundingBox()))
                            {
                                
                                lastMeleeTimeAi = DateTime.Now; // update last shot time 

                                if (troops.LightSaberHitBoxRight().Intersects(user.Hitbox()))
                                    user.Health -= troops.WeaponDamage;


                                else if (troops.LightSaberHitBoxLeft().Intersects(user.Hitbox()))
                                    user.Health -= troops.WeaponDamage;

                            }
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



                //Make barriers for ai
                foreach (Player troops in stormtrooperlist)
                {

                    foreach (Barriers barrier in barriersList)
                    {
                        if (barrier.GetBoundingBox().Intersects(troops.Hitbox()))
                        {
                            troops.UndoMove();
                            break;
                        }
                    }
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
                        if (troops.GetBoundingBox().Contains(laser.GetBoundingBox()))
                        {
                            damgaeMultiplyer = 1f;
                            if (troops.HeadShotBox().Contains(laser.GetBoundingBox()))
                            {
                                damgaeMultiplyer = 1.5f;
                                
                            }
                            
                            troops.Health -= user.WeaponDamage * damgaeMultiplyer;
                            laserList.RemoveAt(i);
                            break;
                        }

                        
                                
                        
                    }
                    
                }
                //If user gets Shot
                foreach (Player troops in stormtrooperlist)
                {
                    foreach (LaserClass bullet in enemyLaserList)
                    {

                        if (bullet.GetBoundingBox().Intersects(user.Hitbox()))
                            user.Health -= troops.WeaponDamage;

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
                


            }
            else if (screen == Screen.PauseScreen)
            {

            }
            else if (screen == Screen.OutroScreen)
            {




            }
            base.Update(gameTime);
        }
        //---------------------------------------------------------------Draw--------------------------------------------------------------------------------------
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
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.BurlyWood);

                user.Draw(_spriteBatch);

                foreach (Barriers barrier in barriersList)
                    barrier.Draw(_spriteBatch);

                //Draw all the bullets

                foreach (LaserClass bullet in laserList)
                    bullet.Draw(_spriteBatch, laserTexture);              
                
                foreach (Player ai in stormtrooperlist)                
                    ai.Draw(_spriteBatch);
                                                 
                foreach (LaserClass bullet in enemyLaserList)                
                    bullet.Draw(_spriteBatch, laserTexture);

                
                //Hud
                //Draw cooldown bar

                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.DrawString(healthFont,  $"{user.Health}", new Vector2(550, 900), Color.White);
               
               
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