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
       

        Texture2D rectangleTexture, lukeStillRight, stormtroperAimingLeft;
        Vector2 backroundSpeed;

        int mainGameWidth = 1400;
        int mainGameHeight = 925;
        int mainScreenHeight = 1000;
        int movedDistanceX = 0;
        int movedDistanceY = 0;
        float userGunInterval = 0.5f; 
        float enemyGunInterval = 0.8f;
        

        bool userCollideWithObject = false;
        bool cooldownTimer = false;
        int firedShotsAi = 0;

        bool initialRespawn = true;
        float seconds; 
        float startTime;
        
        private float playerRotation;
        private float enemyRotation;
        private Vector2 playerPosition; 
        private Vector2 enemyPosition;

        

        
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

          
            

            base.Initialize();
            //Texture, x, y, width, health, heatup amount, firable shots
            user = new Player(lukeStillRight, 500, 500, 64, 124, 10000,  8, "minigun");
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200))));

            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200))));

            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200))));

            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200))));


            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-300-mainGameWidth/4,  -300-mainGameHeight/4, 600 +mainGameWidth, 60)));

            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-300 - mainGameWidth / 4, -300 - mainGameHeight / 4, 60, 600 + mainGameHeight)));

            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-300 - mainGameWidth / 4, mainGameHeight, 600 + mainGameWidth, 60)));

            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(300 + mainGameWidth, -300 - mainGameHeight / 4, 60, 600 + mainGameHeight)));



        }
        //----------------------------------------------------------------------LoadContent--------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            stormtroperAimingLeft = Content.Load<Texture2D>("stormtroperAimingLeft");
            lukeStillRight = Content.Load<Texture2D>("lukeStandingRight");
            
            
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
                    //Texture, x, y, width, health, cooldown time, firable shots
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 100, 100, 64, 124, 100,  8 ,"pistol"));
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 500, 300, 84, 144, 150,  8 ,"pistol"));
                }



            }
            else if (screen == Screen.MainScreen)
            {


                var distance = new Vector2(mouseState.X - playerPosition.X, mouseState.Y - playerPosition.Y);

                playerRotation = (float)Math.Atan2(distance.Y, distance.X);
                playerPosition = new Vector2(user.XLocationRight, user.YLocation);
                
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                //Move user if barrier spawned in them
                if (initialRespawn == true)
                {
                    initialRespawn = false;
                    foreach (Barriers barrier in barriersList)
                        if (user.Collide(barrier.GetBoundingBox()))
                            user.Respawn();
                }
                //Make user move
                
                if (keyboardState.IsKeyDown(Keys.D))
                    user.HSpeed = 3;
                else if (keyboardState.IsKeyDown(Keys.A))
                    user.HSpeed = -3;
                else
                    user.HSpeed = 0;

                if (keyboardState.IsKeyDown(Keys.W))
                    user.VSpeed = -3;
                else if (keyboardState.IsKeyDown(Keys.S))
                    user.VSpeed = 3;
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
                        troops.HSpeed = 1;
                    if (user.XLocationRight < troops.XLocation)
                        troops.HSpeed = -1;

                }

              


                foreach (Player troops in stormtrooperlist)
                {
                    foreach (Barriers barrier in barriersList)
                    {
                        
                        
                            backroundSpeed.X = 0;
                            backroundSpeed.Y = 0;
                            if (user.YLocationBottom >= mainGameHeight - mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.S))
                            {
                                backroundSpeed.Y = user.VSpeed * -1;
                            }
                                

                            if (user.YLocation <= mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.W))
                            {
                                backroundSpeed.Y = user.VSpeed * -1;
                            }
                                

                            if (user.XLocationRight >= mainGameWidth - mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.D))
                            {
                                backroundSpeed.X = user.HSpeed * -1;
                            }
                                

                            if (user.XLocation <= mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.A))
                            {
                                backroundSpeed.X = user.HSpeed * -1;
                            }                        

                    }

                }

                if (movedDistanceY < 300 )
                {
                    if (user.YLocationBottom >= mainGameHeight - mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.S))
                        user.VSpeed = 0;
                    
                }
                if (movedDistanceY > -300)
                {
                    if (user.YLocation <= mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.W))
                        user.VSpeed = 0;
                }


                if (movedDistanceX < 300)
                {
                    if (user.XLocationRight >= mainGameWidth - mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.D))
                        user.HSpeed = 0;
                }
                if (movedDistanceX > -300)
                {
                    if (user.XLocation <= mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.A))
                        user.HSpeed = 0;
                }
                    

                user.Update(new Vector2(0,0));

                //Make barriers for user
                foreach (Barriers barrier in barriersList)
                    if (user.Collide(barrier.GetBoundingBox()))
                    {
                        backroundSpeed.X = 0;
                        backroundSpeed.Y = 0;
                        user.UndoMove();
                    }
                movedDistanceX -= (int)backroundSpeed.X;
                movedDistanceY -= (int)backroundSpeed.Y;


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
                        laserList.Add(new LaserClass(rectangleTexture, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 30, 8)));
                        cooldownBarRed.Width += cooldownBarWhite.Width / (int)user.fireableShots;
                        lastShotTime = DateTime.Now; // update last shot time
                        
                        cooldownBarRed.Width += (int)user.HeatUpAmount;
                        
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
                }


                


                //Ai Shots
                TimeSpan timeSinceLastShotAi = DateTime.Now - lastShotTimeAi;
                TimeSpan TimeSinceCooldownAi = DateTime.Now - cooldownTimeAi;
                
                foreach (Player troops in stormtrooperlist)
                {

                    if (firedShotsAi <= troops.fireableShots * stormtrooperlist.Count)
                    {

                        if (timeSinceLastShotAi.TotalSeconds >= enemyGunInterval && TimeSinceCooldownAi.TotalSeconds >= troops.AICooldownTime)
                        {
                            if (troops.HSpeed > 0)
                                enemyPosition = new Vector2(troops.XLocationRight, troops.YLocation);
                            else
                                enemyPosition = new Vector2(troops.XLocation, troops.YLocation);
                            float missingRange = rand.Next(-50, 50);
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

                        if (troops.Collide(laser.GetBoundingBox()))
                        {
                            laserList.RemoveAt(i);
                            troops.Health -= user.WeaponDamage;
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

                    bullet.Draw(_spriteBatch);

                }
                foreach (Player ai in stormtrooperlist)
                {

                    ai.Draw(_spriteBatch);

                }
                foreach (LaserClass bullet in enemyLaserList)
                {

                    bullet.Draw(_spriteBatch);

                }
                //Hud
                //Draw cooldown bar

                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, _graphics.PreferredBackBufferHeight - 75, _graphics.PreferredBackBufferWidth, 75), Color.Gray);
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