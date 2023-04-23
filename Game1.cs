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
        private SpriteBatch _spriteBatch;
        Player user;
        
        

        Texture2D rectangleTexture, lukeStillRight, stormtroperAimingLeft;
        

        int mainGameWidth = 1200;
        int mainGameHeight = 1000;
        float userGunInterval = 0.5f;

        bool initialRespawn = true;
        float seconds;
        float startTime;
        private float playerRotation;
        private Vector2 playerPosition;
        

        List<Rectangle> barriersList;
        List<Player> stormtrooperlist = new List<Player>();

        List<LaserClass> laserList = new List<LaserClass>();
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

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.TitleScreen;
            
            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 600; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            barriersList = new List<Rectangle>();
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));
            barriersList.Add(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), rand.Next(10, 200), rand.Next(10, 200)));

            

            base.Initialize();
            user = new Player(lukeStillRight, 500, 500);
            
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            stormtroperAimingLeft = Content.Load<Texture2D>("stormtroperAimingLeft");
            lukeStillRight = Content.Load<Texture2D>("lukeStandingRight");
            
            
        }

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
                    stormtrooperlist.Add(new Player(stormtroperAimingLeft, 100, 100));
                }



            }
            else if (screen == Screen.MainScreen)
            {
                

                var distance = new Vector2(mouseState.X - playerPosition.X, mouseState.Y - playerPosition.Y);

                playerRotation = (float)Math.Atan2(distance.Y, distance.X);
                playerPosition = new Vector2(user.XLocationRight , user.YLocation  );
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

                if (initialRespawn == true)
                {
                    initialRespawn = false;
                    foreach (Rectangle barrier in barriersList)
                        if (user.Collide(barrier))
                            user.Respawn();
                }
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

                

                 foreach (Player troops in stormtrooperlist)
                 {

                    if (user.YLocation > troops.YLocationBottom)
                        troops.VSpeed = 1;
                    if (user.YLocationBottom< troops.YLocation)
                        troops.VSpeed = -1;

                    if (user.XLocation > troops.XLocationRight)
                        troops.HSpeed = 1;
                    if (user.XLocationRight < troops.XLocation)
                        troops.HSpeed = -1;
                    troops.Update();
                 }



                TimeSpan timeSinceLastShot = DateTime.Now - lastShotTime;
                if (timeSinceLastShot.TotalSeconds >= userGunInterval && mouseState.LeftButton == ButtonState.Pressed)
                {
                    laserList.Add(new LaserClass(rectangleTexture, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 30, 8)));
                    lastShotTime = DateTime.Now; // update last shot time
                }



                //Loop through the list of bullets and update their position

                foreach (LaserClass bullet in laserList)
                {

                    bullet.Update(gameTime);

                }

                foreach (Rectangle barrier in barriersList)
                    if (user.Collide(barrier))
                        user.UndoMove();
                
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
                /*
                for (int i = laserList.Count - 1; i >= 0; i--)
                {
                    LaserClass t = laserList[i];
                    foreach (Player troops in stormtrooperlist)
                    {
                        if (troops.Collide(t))
                        {
                            laserList.RemoveAt(i);
                            break;
                        }
                    }
                }
                */
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



            }
            else if (screen == Screen.PauseScreen)
            {

            }
            else if (screen == Screen.OutroScreen)
            {




            }
            base.Update(gameTime);
        }

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