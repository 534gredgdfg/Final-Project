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
        private SpriteBatch _spriteBatch;
        Player user;
        Rectangle cooldownBarRed, cooldownBarWhite;
       

        Texture2D rectangleTexture, lukeStillRight, lukeStillLeft, stormtroperAimingLeft, stormtrooperWalkingRight, stormtrooperWalkingLeft, laserTexture;
        Vector2 backroundSpeed;

        int mainGameWidth = 1400;
        int mainGameHeight = 900;

        int mainScreenHeight = 1000;
        int movedDistanceX = 0;
        int movedDistanceY = 0;
      
        bool UserFacingRight = true;

        bool trooperGoingRight = true;

        bool cooldownTimer = false;
        int firedShotsAi = 0;

        float damgaeMultiplyer = 0.0f;
        float seconds; 
        float startTime;
        
        private float playerRotation;
        private float enemyRotation;
        private Vector2 playerPosition; 
        private Vector2 enemyPosition;



        List<Texture2D> stormtropperTexturesRight;
        List<Texture2D> stormtropperTexturesLeft;
        double trooperIndexRight;
        double trooperIndexLeft;
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

            
            stormtropperTexturesRight = new List<Texture2D>();
            stormtropperTexturesLeft = new List<Texture2D>();
            trooperIndexRight = 0;
            trooperIndexLeft = 0;






            base.Initialize();
            //Texture, x, y, width, health, heatup amount, firable shots
            user = new Player(lukeStillRight, 500, 500, 64, 124, 10000, "blaster");
            
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
            stormtroperAimingLeft = Content.Load<Texture2D>("stormtroperAimingLeft");
            lukeStillRight = Content.Load<Texture2D>("lukeStandingRight");
            lukeStillLeft = Content.Load<Texture2D>("lukeStandingLeft");
            stormtrooperWalkingRight =Content.Load<Texture2D>("DriodWalkingRight");
            stormtrooperWalkingLeft = Content.Load<Texture2D>("DriodWalkingLeft");
            laserTexture = Content.Load<Texture2D>("Small Laser");

            Texture2D cropTexture1;
            Texture2D cropTexture2;
            Rectangle sourceRect1;
            Rectangle sourceRect2;
            int width = stormtrooperWalkingRight.Width / 3;
            int height = stormtrooperWalkingRight.Height;


            for (int x = 0; x < 3; x++)
            {
                sourceRect1 = new Rectangle(x * width, 0, width, height);
                cropTexture1 = new Texture2D(GraphicsDevice, width, height);

                Color[] data = new Color[width * height];
                stormtrooperWalkingRight.GetData(0, sourceRect1, data, 0, data.Length);

                cropTexture1.SetData(data);

                stormtropperTexturesRight.Add(cropTexture1);
            }


            int width1 = stormtrooperWalkingLeft.Width / 3;
            int height1 = stormtrooperWalkingLeft.Height;

            for (int x = 0; x < 3; x++)
            {
                sourceRect2 = new Rectangle(x * width1, 0, width1, height1);
                cropTexture2 = new Texture2D(GraphicsDevice, width1, height1);

                Color[] data2 = new Color[width1 * height1];
                stormtrooperWalkingLeft.GetData(0, sourceRect2, data2, 0, data2.Length);

                cropTexture2.SetData(data2);

                stormtropperTexturesLeft.Add(cropTexture2);
            }


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
                    //Texture, x, y, width, health, weapon type
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 100, 100, 64, 124, 100 , "sniper"));
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 500, 300, 84, 144, 150 , "sniper"));
                }



            }
            else if (screen == Screen.MainScreen)
            {
                
                if (trooperGoingRight == true)
                {
                    trooperIndexRight += 0.05;
                    if (trooperIndexRight >= stormtropperTexturesRight.Count - 0.5)
                        trooperIndexRight = 0;
                }
                else
                {
                    trooperIndexLeft += 0.05;
                    if (trooperIndexLeft >= stormtropperTexturesLeft.Count - 0.5)
                        trooperIndexLeft = 0;
                }
                


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
                    user.HSpeed = 4;
                    UserFacingRight = true;
                    user.Texture = lukeStillRight;
                }
                    
                else if (keyboardState.IsKeyDown(Keys.A))
                {
                    user.HSpeed = -4;
                    UserFacingRight = false;
                    user.Texture = lukeStillLeft;
                }
                    
                
                else
                    user.HSpeed = 0;

                if (keyboardState.IsKeyDown(Keys.W))
                    user.VSpeed = -4;
                else if (keyboardState.IsKeyDown(Keys.S))
                    user.VSpeed = 4;
                else
                    user.VSpeed = 0;




                //Make ai move
                foreach (Player troops in stormtrooperlist)
                {

                    if (user.YLocation > troops.YLocationBottom)
                        troops.VSpeed = 1;
                    if (user.YLocationBottom < troops.YLocation)
                        troops.VSpeed = -1;

                    if (user.XLocation > troops.XLocationRight)
                    {
                        trooperGoingRight = true;
                        troops.HSpeed = 1;
                    }
                        
                    if (user.XLocationRight < troops.XLocation)
                    {
                        trooperGoingRight = false;
                        troops.HSpeed = -1;
                    }
                        

                }

              


                
                    
                        
                        
                backroundSpeed.X = 0;
                backroundSpeed.Y = 0;
                if (user.YLocationBottom >= mainGameHeight - mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.S))
                {
                    backroundSpeed.Y = user.VSpeed * -1;
                    if (movedDistanceY < mainGameHeight)
                        user.VSpeed = 0;
                    else
                        backroundSpeed.Y = 0;
                }
                                

                if (user.YLocation <= mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.W))
                {
                    backroundSpeed.Y = user.VSpeed * -1;
                    if (movedDistanceY > -mainGameHeight)                                           
                            user.VSpeed = 0;                    
                    else
                        backroundSpeed.Y = 0;
                }
                                

                if (user.XLocationRight >= mainGameWidth - mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.D))
                {
                    backroundSpeed.X = user.HSpeed * -1;
                    if (movedDistanceX < mainGameWidth)                                           
                            user.HSpeed = 0;
                    else
                        backroundSpeed.X = 0;

                }
                                

                if (user.XLocation <= mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.A))
                {
                    backroundSpeed.X = user.HSpeed * -1;
                    if (movedDistanceX > -mainGameWidth)                                           
                            user.HSpeed = 0;                   
                    else
                        backroundSpeed.X = 0;

                }



                movedDistanceX -= (int)backroundSpeed.X;
                movedDistanceY -= (int)backroundSpeed.Y;
                
               
                
                


                
             
               
               
                user.Update(new Vector2(0,0));

                //Make barriers for user
                foreach (Barriers barrier in barriersList)
                    if (user.Collide(barrier.GetBoundingBox()))
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
                
                       
                if (cooldownTimer == false)
                {
                    
                    if (timeSinceLastShot.TotalSeconds >= user.GunInterval && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        cooldownBarRed.Width += (int)user.HeatUpAmount;
                        lastShotTime = DateTime.Now; // update last shot time
                        if (user.WeaponType == "lightsaber")
                        {
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
                        
                }
                
               
                
                cooldownBarRed.Width -= 1;
                
                
                if (cooldownBarRed.Width >= cooldownBarWhite.Width)
                {
                    cooldownBarRed.Width = cooldownBarWhite.Width;
                    cooldownTimer = true;
                }
                if (cooldownBarRed.Width <= 0)
                {
                    cooldownBarRed.Width = 0;
                    cooldownTimer = false;
                    cooldownBarRed.Height = 25;
                    cooldownBarWhite.Height = 25;
                    cooldownBarRed.Y = mainScreenHeight - 50;
                    cooldownBarWhite.Y = mainScreenHeight - 50;
                }
                if (cooldownTimer == true)
                {
                    cooldownBarRed.Height = 50;
                    cooldownBarWhite.Height = 50;
                    cooldownBarRed.Y = mainScreenHeight - 57;
                    cooldownBarWhite.Y = mainScreenHeight - 57;

                }
               


                


                //Ai Shots
                TimeSpan timeSinceLastShotAi = DateTime.Now - lastShotTimeAi;
                TimeSpan TimeSinceCooldownAi = DateTime.Now - cooldownTimeAi;
                
                foreach (Player troops in stormtrooperlist)
                {

                    if (firedShotsAi <= troops.AiFireableShots * stormtrooperlist.Count)
                    {

                        if (timeSinceLastShotAi.TotalSeconds >= troops.GunInterval && TimeSinceCooldownAi.TotalSeconds >= troops.AICooldownTime)
                        {
                            if (troops.HSpeed > 0)
                                enemyPosition = new Vector2(troops.XLocationRight, troops.YLocation);
                            else
                                enemyPosition = new Vector2(troops.XLocation, troops.YLocation);
                            float missingRange = rand.Next(-75, 75);
                            var enemydistance = new Vector2(playerPosition.X + missingRange - enemyPosition.X, playerPosition.Y + missingRange - enemyPosition.Y);
                            enemyRotation = (float)Math.Atan2(enemydistance.Y, enemydistance.X);

                            enemyLaserList.Add(new LaserClass(rectangleTexture, enemyPosition, enemyRotation, new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, 30, 8)));
                            firedShotsAi += 1;
                            
                            lastShotTimeAi = DateTime.Now; // update last shot time
                        }

                    }
                    else
                    {
                        cooldownTimeAi = DateTime.Now;
                        firedShotsAi = 0;

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
                        if (troops.Collide(barrier.GetBoundingBox()))
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

                        if (user.Collide(bullet.GetBoundingBox()))
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
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.SandyBrown);
                
                user.Draw(_spriteBatch);
                foreach (Barriers barrier in barriersList)
                    barrier.Draw(_spriteBatch);

                //Draw all the bullets

                foreach (LaserClass bullet in laserList)
                {

                    bullet.Draw(_spriteBatch, laserTexture);

                }
                foreach (Player ai in stormtrooperlist)
                {
                    if (trooperGoingRight == true)
                        ai.DrawAI(_spriteBatch, stormtropperTexturesRight[(int)Math.Round(trooperIndexRight)]);
                    else
                        ai.DrawAI(_spriteBatch, stormtropperTexturesLeft[(int)Math.Round(trooperIndexLeft)]);

                }
              
                foreach (LaserClass bullet in enemyLaserList)
                {

                    bullet.Draw(_spriteBatch, laserTexture);

                }
                //Hud
                //Draw cooldown bar

                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.Draw(rectangleTexture, cooldownBarWhite, Color.White);
                _spriteBatch.Draw(rectangleTexture, cooldownBarRed, Color.Red);
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