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
        
        private SpriteBatch _spriteBatch;
        Player user;
        
        

        Texture2D rectangleTexture, lukeStillRight;
        

        int mainGameWidth = 1200;
        int mainGameHeight = 1000;
        int fired = 0;
        bool initialRespawn = true;
        float seconds;
        float startTime;
        private float playerRotation;
        private Vector2 playerPosition;
        private Vector2 playerOrigin;

        List<Rectangle> barriersList;
        

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
            lukeStillRight = Content.Load<Texture2D>("lukeStandingRight");
            playerOrigin = new Vector2(32, 62);
            
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



                //Shoot
                
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    laserList.Add(new LaserClass(rectangleTexture, playerPosition, playerRotation, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 10, 10)));
                    
                }


                //Loop through the list of bullets and update their position

                foreach (LaserClass bullet in laserList)
                {

                    bullet.Update(gameTime);

                }

                foreach (Rectangle barrier in barriersList)
                    if (user.Collide(barrier))
                        user.UndoMove();

                if (laserList.Count > 0)
                {
                    foreach (LaserClass t in laserList)
                    {
                        foreach (Rectangle barrier in barriersList)
                        {
                            if (t.Collide(barrier))
                            {

                                laserList.Remove(t);
                                


                            }

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