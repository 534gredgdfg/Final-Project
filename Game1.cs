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
        DateTime cooldownTimeAi = DateTime.MinValue;
        DateTime lastShotTimeAi = DateTime.MinValue;
        private SpriteBatch _spriteBatch;
        Player user;
        
        

        Texture2D rectangleTexture, lukeStillRight, stormtroperAimingLeft;
        

        int mainGameWidth = 1200;
        int mainGameHeight = 1000;
        float userGunInterval = 0.5f; 
        float enemyGunInterval = 0.8f;
        int avalibleShots = 8;
        int firedShots = 0;
        int firedShotsAi = 0;

        bool initialRespawn = true;
        float seconds;
        float startTime;
        private float playerRotation;
        private float enemyRotation;
        private Vector2 playerPosition; 
        private Vector2 enemyPosition;

        Rectangle  cooldownBarGreen, cooldownBarRed;

        List<Rectangle> barriersList;
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

            cooldownBarGreen = new Rectangle(160, 160, 160, 50);
            cooldownBarRed = new Rectangle(160, 160, 0, 50);

            barriersList = new List<Rectangle>();
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));

            

            base.Initialize();
            user = new Player(lukeStillRight, 500, 500, 64, 124, 200);
            
            
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
                    _graphics.PreferredBackBufferHeight = mainGameHeight; // Sets the height of the window
                    _graphics.ApplyChanges(); // Applies the new dimensions
                    //Texture, x, y, width, health
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 100, 100, 64, 124, 100));
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 500, 300, 84, 144, 150));
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
                    foreach (Rectangle barrier in barriersList)
                        if (user.Collide(barrier))
                            user.Respawn();
                }
                //Make user move
                user.HSpeed = 0;
                user.VSpeed = 0;
                if (keyboardState.IsKeyDown(Keys.D))
                    user.HSpeed = 3;
                else if (keyboardState.IsKeyDown(Keys.A))
                    user.HSpeed = -3;
                if (keyboardState.IsKeyDown(Keys.W))
                    user.VSpeed = -3;
                else if (keyboardState.IsKeyDown(Keys.S))
                    user.VSpeed = 3;

                user.Update();


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
                    troops.Update();
                }
                

                //Make time between shots
                TimeSpan timeSinceLastShot = DateTime.Now - lastShotTime;
                TimeSpan timeSinceCooldown = DateTime.Now - cooldownTime;
                if (firedShots <= avalibleShots)
                {
                    if(timeSinceCooldown.TotalSeconds >= 5)
                    {
                        if (timeSinceLastShot.TotalSeconds >= userGunInterval && mouseState.LeftButton == ButtonState.Pressed)
                        {
                            laserList.Add(new LaserClass(rectangleTexture, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 30, 8)));
                            firedShots += 1;
                            cooldownBarRed.Width += cooldownBarGreen.Width / avalibleShots;

                            lastShotTime = DateTime.Now; // update last shot time
                        }
                        
                    }
                    else
                    {
                        if (cooldownBarRed.Width <= cooldownBarGreen.Width)
                        {
                            cooldownBarRed.Width -= cooldownBarGreen.Width / 5;
                        }
                    }
                    
                }
                else
                {
                    cooldownTime = DateTime.Now;
                    firedShots = 0;
                }
                

                //Make time between shots
                TimeSpan timeSinceLastShotAi = DateTime.Now - lastShotTimeAi;
                TimeSpan TimeSinceCooldownAi = DateTime.Now - cooldownTimeAi;
                if (firedShotsAi <= avalibleShots * stormtrooperlist.Count)
                {

                    if (timeSinceLastShotAi.TotalSeconds >= enemyGunInterval && TimeSinceCooldownAi.TotalSeconds >= 5)
                    {

                        foreach (Player troops in stormtrooperlist)
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


                }
                else
                {
                    cooldownTimeAi = DateTime.Now;
                    firedShotsAi = 0;
                    
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
                //Make barriers for user
                foreach (Rectangle barrier in barriersList)
                    if (user.Collide(barrier))
                        user.UndoMove();
                //Make barriers for ai
                foreach (Player troops in stormtrooperlist)
                {

                    foreach (Rectangle barrier in barriersList)
                    {
                        if (troops.Collide(barrier))
                        {
                            troops.UndoMove();
                            break;
                        }
                    }
                }
                //If ai gets shot
                for (int i = laserList.Count - 1; i >= 0; i--)
                { 
                                       
                   LaserClass laser = laserList[i];
                    foreach (Player troops in stormtrooperlist)
                    {

                        if (troops.Collide(laser.GetBoundingBox()))
                        {
                            laserList.RemoveAt(i);
                            troops.Health -= 32;
                            break;
                        }
                    }
                    
                }
                //If laser hits barrier
                for (int i = laserList.Count - 1; i >= 0; i--)
                {
                    LaserClass t = laserList[i];
                    foreach (Rectangle barrier in barriersList)
                    {
                        if (t.Collide(barrier))
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
                    foreach (Rectangle barrier in barriersList)
                    {
                        if (t.Collide(barrier))
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
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferWidth), Color.SandyBrown);
                user.Draw(_spriteBatch);
                foreach (Rectangle barrier in barriersList)
                    _spriteBatch.Draw(rectangleTexture, barrier, Color.Black);

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
                
                _spriteBatch.Draw(rectangleTexture, cooldownBarGreen, Color.Green);
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