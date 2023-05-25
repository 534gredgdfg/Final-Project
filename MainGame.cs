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

      


        Texture2D rectangleTexture, hutBlueTexture,hutTexture, grassTexture, AiSkelHitTexture, AiGoblinHitTexture,AiSkelMeleeRightTexture, AiSkelWalkingRight,wizardCrosshair, darkTreeTexture, grayRockTexture, darkerTreeTexture, healthGreenBarTexture, emptyGreenBarTexture,userSheildWalkTexture, userSheildIdleTexture, lightningTexture1, lightningTexture2, lightningTexture3, arrowTexture, AiArcherWalkingRight, AiArcherWalkingLeft, AiArcherMeleeRightTexture, stormtroperAimingLeft, AiWalkingRight, AiWalkingLeft, AiMeleeRightTexture, laserTexture, userWalkingRight, userWalkingLeft, userAttackRightTexture, userAttackLeftTexture, userIdleTexture, userIdleLeftTexture;
        Vector2 backroundSpeed;
        int userspeed = 2;
        int damageBoost = 0;
        int boostSpeed = 0;
        int mainGameWidth = 1400;
        int mainGameHeight = 900;
        int enemyType = 0;
        int mainScreenHeight = 1000;
        int movedDistanceX = 0;
        int movedDistanceY = 0;
        int wave = 0;
        Color dimScreenColor;
        bool RespawnMethold = false;
        bool boost = false;
        bool fading;

        

        double damgaeMultiplyer = 1;
        float seconds;
        float sheildTime;

        private float playerRotation;
        
        private Vector2 playerPosition;
        
        private SpriteFont healthFont;
        private SpriteFont damageFont;
        

        List<Texture2D> userRightList;       
        List<Texture2D> userAttackList;     
        List<Texture2D> userIdleList;
        List<Texture2D> userSkillSheildList;
        List<Texture2D> userSheildWalkList;
        List<Texture2D> userSheildIdleList;

        List<Texture2D> AiRightList;       
        List<Texture2D> AiMeleeRightList;
        List<Texture2D> AiGoblinHitList;

        List<Texture2D> AiArcherRightList;  
        List<Texture2D> AiArcherMeleeRightList;
        List<Texture2D> AiArcherHitList;

        List<Texture2D> AiWormRightList;
        List<Texture2D> AiWormMeleeRightList;
        List<Texture2D> AiWormHitList;

        List<Texture2D> AiSkelRightList;
        List<Texture2D> AiSkelMeleeRightList;
        List<Texture2D> AiSkelHitList;
        
        List<Texture2D> blacksmithList;
        List<Texture2D> postionList;
        List<Texture2D> herbaleList;
        List<Texture2D> merchantList;

        List<Texture2D> fireBallList;

        List<Texture2D> LightningShotList1;
        List<Texture2D> LightningShotList2;
        List<Texture2D> LightningShotList3;

        List<Texture2D> ArrowShotList;

        List<Barriers> barriersList = new List<Barriers>();
        List<Player> stormtrooperlist = new List<Player>();
        List<Npc> npcList = new List<Npc>();

        List<LaserClass> laserList = new List<LaserClass>();
        List<LaserClass> enemyLaserList = new List<LaserClass>();
        List<Buttons> buttonList = new List<Buttons> { };
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

            _graphics.PreferredBackBufferWidth = mainGameWidth; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = mainScreenHeight; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions
            dimScreenColor = new Color(0, 0, 0, 0);
            base.Initialize();
            //Texture, x, y, width, health, heatup amount, firable shots
            user = new Player(new Rectangle(500, 500, 150, 130), 1000, "melee", "normal", userRightList, userIdleList, userSheildWalkList, userSheildIdleList, userAttackList, AiRightList, userSkillSheildList);

            //Add Npc
            npcList.Add(new Npc(new Rectangle(150, 100, 70, 100), herbaleList));
            npcList.Add(new Npc(new Rectangle(950, 100, 70, 100), postionList));
            npcList.Add(new Npc(new Rectangle(180, 750, 90,100), blacksmithList));
            npcList.Add(new Npc(new Rectangle(990, 770, 50, 90), merchantList));

            //Buttons
            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(100, 150, 150, 150), Color.DarkSlateGray, "health"));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(900, 150, 150, 150), Color.DarkSlateGray, "sheild time"));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(900, 750, 150, 150), Color.DarkSlateGray, "speed boost"));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(100, 750, 150, 150), Color.DarkSlateGray, "damage boost"));

            //Top Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, 60), 10000, Color.Black, "false", "true"));
            //Left Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, 60, mainGameHeight * 3), 10000, Color.Black, "false", "true"));
            //Bottom Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, mainGameHeight * 2 - 60, mainGameWidth * 3, 60), 10000, Color.Black, "false", "true"));
            //Right Wall
            barriersList.Add(new Barriers(rectangleTexture, new Rectangle(mainGameWidth * 2 - 60, -mainGameHeight, 60, mainGameHeight * 3), 10000, Color.Black, "false", "true"));



        }
        //----------------------------------------------------------------------LoadContent--------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            //Hud
            emptyGreenBarTexture = Content.Load<Texture2D>("EmptyGreenBar");
            healthGreenBarTexture = Content.Load<Texture2D>("HealhGreenBar");

            healthFont = Content.Load<SpriteFont>("Good");
            damageFont = Content.Load<SpriteFont>("DamageText");

            wizardCrosshair = Content.Load<Texture2D>("WizardCrosshair");
            //Enviorment
            grayRockTexture = Content.Load<Texture2D>("NatureSprite-Gray-Rock");
            darkTreeTexture = Content.Load<Texture2D>("NatureSprite-Dark-Tree");
            darkerTreeTexture = Content.Load<Texture2D>("NatureSprite-Darker-Tree");
            //User
            userWalkingRight = Content.Load<Texture2D>("spritesheet (2)");
            userWalkingLeft = Content.Load<Texture2D>("spritesheet (2)");
            userAttackRightTexture = Content.Load<Texture2D>("spritesheet (5)");
            userAttackLeftTexture = Content.Load<Texture2D>("spritesheet (5)L");
            userIdleTexture = Content.Load<Texture2D>("spritesheet (10)");
            Texture2D userSkillSheildTexture = Content.Load<Texture2D>("Wizard_Skill_1");
            userSheildWalkTexture = Content.Load<Texture2D>("UserSheildWalkSprite");
            userSheildIdleTexture = Content.Load<Texture2D>("UserSheildIdleSprite");
            //Ai
            AiWalkingRight = Content.Load<Texture2D>("Goblin Running Right");            
            AiMeleeRightTexture = Content.Load<Texture2D>("Attack");
            AiGoblinHitTexture = Content.Load<Texture2D>("Take Hit_Goblin");

            AiArcherWalkingRight = Content.Load<Texture2D>("RunArcher_scaled");          
            AiArcherMeleeRightTexture = Content.Load<Texture2D>("AttackArcher_scaled");
            Texture2D AiArcherHitTexture = Content.Load<Texture2D>("Take Hit_Archer");

            AiSkelWalkingRight = Content.Load<Texture2D>("Walk_Skel");           
            AiSkelMeleeRightTexture = Content.Load<Texture2D>("Attack_Skel");
            AiSkelHitTexture = Content.Load<Texture2D>("Take Hit_Skel");

            Texture2D AiWormWalkingRight = Content.Load<Texture2D>("Walk_Worm");
            Texture2D AiWormMeleeRightTexture = Content.Load<Texture2D>("Attack_Worm"); 
            Texture2D AiWormHitTexture = Content.Load<Texture2D>("Take Hit_Worm");

            //Npc's
            Texture2D blacksmithTexture = Content.Load<Texture2D>("Blacksmith_Working"); 
            Texture2D postionTexture = Content.Load<Texture2D>("Postion_Maker");
            Texture2D herbaleTexture = Content.Load<Texture2D>("Herbale_Maker");
            Texture2D merchantTexture = Content.Load<Texture2D>("Speed_Maker");
            grassTexture = Content.Load<Texture2D>("Grass_Sample");
            hutTexture = Content.Load<Texture2D>("Decorations_Hut");
            hutBlueTexture = Content.Load<Texture2D>("Decorations_Bluehut");
            //Projectiles
            Texture2D fireBallTexture = Content.Load<Texture2D>("Move_Fire_Ball");
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

            ReapetingAnimation(GraphicsDevice, userSkillSheildTexture, userSkillSheildList = new List<Texture2D>(), 7);
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

            ReapetingAnimation(GraphicsDevice, AiWormWalkingRight, AiWormRightList = new List<Texture2D>(), 9);
            ReapetingAnimation(GraphicsDevice, AiWormMeleeRightTexture, AiWormMeleeRightList = new List<Texture2D>(), 16);
            ReapetingAnimation(GraphicsDevice, AiWormHitTexture, AiWormHitList = new List<Texture2D>(), 3);
            //Npc
            ReapetingAnimation(GraphicsDevice, blacksmithTexture, blacksmithList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, postionTexture, postionList = new List<Texture2D>(), 19);
            ReapetingAnimation(GraphicsDevice, herbaleTexture, herbaleList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, merchantTexture, merchantList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, fireBallTexture, fireBallList = new List<Texture2D>(), 6);

            ReapetingAnimation(GraphicsDevice, lightningTexture1, LightningShotList1 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningTexture2, LightningShotList2 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningTexture3, LightningShotList3 = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, arrowTexture, ArrowShotList = new List<Texture2D>(), 4);
        }
        //-----------------------------------------------------------------------Update--------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            static void AddGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 200, 100), 100, "goblin melee", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList));

            }
            static void AddFastGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 180, 90), 80, "fast goblin melee", "fast", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList));

            }

            static void AddSkel(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 250, 125), 130, "skel melee", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList));

            }
            static void AddArcher(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 200, 100), 95, "arrow", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList));

            }
            static void AddWorm(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Rectangle(rand.Next(0, 1000), rand.Next(0, 300), 220, 100), 175, "fire ball", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList));

            }
            static void MoveingUser(Player user, int userspeed, KeyboardState keyboardState)
            {
                //Make user move

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (user.Sheilding == "false")
                        user.HSpeed = userspeed + user.BoostSpeed;
                    else
                        user.HSpeed = 2;
                }

                else if (keyboardState.IsKeyDown(Keys.A))
                {


                    if (user.Sheilding == "false")
                        user.HSpeed = -userspeed - user.BoostSpeed;
                    else
                        user.HSpeed = -2;

                }

                else
                    user.HSpeed = 0;



                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (user.Sheilding == "false")
                        user.VSpeed = -userspeed - user.BoostSpeed;
                    else
                        user.VSpeed = -2;

                }

                else if (keyboardState.IsKeyDown(Keys.S))
                {


                    if (user.Sheilding == "false")
                        user.VSpeed = userspeed + user.BoostSpeed;
                    else
                        user.VSpeed = 2;
                }

                else

                    user.VSpeed = 0;

            }
            static void DimingScreen(ref bool fading, KeyboardState keyboardState, ref Color dimScreenColor, ref Player user, ref Screen screen)
            {
                if (keyboardState.IsKeyDown(Keys.Tab))               
                    fading = true;
                
                if (fading)                
                    dimScreenColor.A = (byte)Math.Min(dimScreenColor.A + 5, 255);
                
                if (dimScreenColor.A >= 255)
                {
                    if (screen == Screen.TitleScreen)                   
                        screen = Screen.MainScreen;
                    
                    else if (screen == Screen.MainScreen)                    
                        screen = Screen.PauseScreen;
                    
                    else if (screen == Screen.PauseScreen)                  
                        screen = Screen.MainScreen;
                    

                    fading = false;
                    dimScreenColor.A = 0;
                    user.XLocation = 600;
                    user.YLocation =450;
                    
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            if (screen == Screen.TitleScreen)
            {
                DimingScreen(ref fading,  keyboardState, ref dimScreenColor, ref user, ref screen);

            }
            else if (screen == Screen.MainScreen)
            {

                if (wave == 0)
                {
                    for (int i = 0; barriersList.Count <= 30; i++)
                    {
                        barriersList.Add(new Barriers(grayRockTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 2), rand.Next(50, 80), rand.Next(50, 80)), 60, Color.White, "true", "true"));
                    }
                    for (int i = 0; barriersList.Count <= 50; i++)
                    {
                        barriersList.Add(new Barriers(darkTreeTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 2), rand.Next(120, 140), rand.Next(150, 180)), 80, Color.White, "true", "true"));
                    }
                    for (int i = 0; barriersList.Count <= 65; i++)
                    {
                        barriersList.Add(new Barriers(darkerTreeTexture, new Rectangle(rand.Next(-mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 2), rand.Next(120, 140), rand.Next(150, 180)), 100, Color.White, "true", "true"));
                    }
                    wave = 0;
                }
                
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - sheildTime;
                if (stormtrooperlist.Count == 0)
                {
                    
                        
                    if (keyboardState.IsKeyDown(Keys.P))
                    {
                        wave += 1;
                        RespawnMethold = false;
                        for (int i = 0; stormtrooperlist.Count <= rand.Next(0, wave); i++)
                        {
                            enemyType = rand.Next(0, 6);
                            switch (enemyType)
                            {
                                case 1:
                                    AddArcher(stormtrooperlist, AiArcherRightList, AiArcherMeleeRightList, AiArcherHitList);
                                    break;
                                case 2:
                                    AddFastGoblin(stormtrooperlist, AiRightList, AiMeleeRightList, AiGoblinHitList);
                                    break;
                                case 3:
                                    AddGoblin(stormtrooperlist, AiRightList, AiMeleeRightList, AiGoblinHitList);
                                    break;
                                case 4:
                                    AddWorm(stormtrooperlist, AiWormRightList, AiWormMeleeRightList, AiWormHitList);
                                    break;
                                case 5:
                                    AddSkel(stormtrooperlist, AiSkelRightList, AiSkelMeleeRightList, AiSkelHitList);
                                    break;
                            }
                        }
                    }
                    else
                        DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen);




                }

                var distance = new Vector2(mouseState.X - playerPosition.X, mouseState.Y - playerPosition.Y);
                
                playerRotation = (float)Math.Atan2(distance.Y, distance.X);
                playerPosition = new Vector2(user.XLocationRight, user.YLocation);



                //Delte barrier is user spawns in it
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
                MoveingUser(user, userspeed, keyboardState);

                
                
                    
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (seconds >= user.SheildSeconds && user.Sheilding == "false")
                    {

                        user.WeaponType = "sheild melee";
                        
                        user.UserAttackMelee(stormtrooperlist, barriersList, null, null, 0);
                        user.Sheild();
                    }

                }

                else if (keyboardState.IsKeyUp(Keys.Space))
                {
                    if (user.Sheilding == "true")
                        sheildTime = (float)gameTime.TotalGameTime.TotalSeconds;

                    user.UnSheild();
                }
                if (seconds >= user.SheildSeconds)
                    seconds = user.SheildSeconds;





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
                user.Update(new Vector2(0, 0), barriersList, "main game");




                foreach (Player troops in stormtrooperlist)
                {
                    troops.Update(backroundSpeed, barriersList, "main game");


                }
                foreach (Barriers barrier in barriersList)
                {
                    barrier.Update(backroundSpeed, barriersList);

                }

                //User Shots
               

                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    user.WeaponType = "melee";

                    user.UserAttackMelee(stormtrooperlist, barriersList, null, null, 0);

                }

                else if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    user.WeaponType = "wizard ball";


                    user.UserAttackMelee(stormtrooperlist, barriersList, laserList, LightningShotList2, playerRotation);

                }
                //Ai Shots

                foreach (Player troops in stormtrooperlist)
                {
                 //Enemy Melee user
                    if (user.Sheilding == "false" && troops.WeaponType == "goblin melee" || troops.WeaponType == "fast goblin melee" || troops.WeaponType == "skel melee")
                    {
                        troops.DrawEnemyAttackMelee(user);
                        if (troops.Attacking == "false")
                            troops.EnemyAttackMelee(user, barriersList, null, new Vector2(0, 0), null, null);
                    }
                    //Enemy Shoot

                    else if (troops.WeaponType == "arrow" || troops.WeaponType == "fire ball")
                    {
                        troops.DrawEnemyAttackMelee(null);
                        if (troops.Attacking == "false")
                            troops.EnemyAttackMelee(user, barriersList, enemyLaserList, playerPosition, fireBallList, ArrowShotList);

                    }


                    //Enemy Melee Barrier
                    foreach (Barriers barrier in barriersList)
                    {


                        if (troops.Hitbox().Intersects(barrier.GetBoundingBox()))
                        {
                            
                            if (troops.Attacking == "false")
                                troops.EnemyAttackMelee(null, barriersList, null, new Vector2(0, 0), null, null);
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
                            barrier.TakeHit((int)user.WeaponDamage);
                            if (barrier.Blocking == "true")
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
                                barrier.TakeHit((int)troops.WeaponDamage);
                            if (barrier.Blocking == "true")
                            {
                                enemyLaserList.RemoveAt(i);
                                break;
                            }
                                
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
                        user.Points += troops.PointsOnKill;
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
                seconds = user.SheildSeconds;
                
                
                 DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen);

                

                MoveingUser(user, userspeed, keyboardState);

                foreach(Npc npc in npcList)
                    npc.Update();
                user.Update(new Vector2(0,0), barriersList, "not main game");
                if (keyboardState.IsKeyUp(Keys.B))
                {
                    boost = true;
                }
                foreach ( Buttons button in buttonList)
                {
                    if (keyboardState.IsKeyDown(Keys.B))
                    {
                        if (boost == true)
                        {

                            if (button.Contains(user.Hitbox()))
                            {
                                button.Boosts(user, (int)user.WeaponDamage);
                                boost = false;
                            }
                            

                        }
                    }
                   

                }
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


                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkOrange);
                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
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
                _spriteBatch.DrawString(healthFont, $"{user.Points}", new Vector2(800, 900), Color.White);
                _spriteBatch.DrawString(healthFont, (seconds).ToString("0.0"), new Vector2(1000, 900), Color.CornflowerBlue);


                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
            }
            else if (screen == Screen.PauseScreen)
            {
                //Backround
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkOliveGreen);
                //Buttons
                foreach (Buttons button in buttonList)
                {
                    button.Draw(_spriteBatch);
                }
                
                //Structures Far
                _spriteBatch.Draw(hutBlueTexture, new Rectangle(66  , 0, 240, 200), Color.White);
                foreach (Npc npc in npcList)
                    npc.Draw(_spriteBatch);

                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
                user.Draw(_spriteBatch, mouseState.X);

                //Structures Close
                _spriteBatch.Draw(hutTexture, new Rectangle(162, 660, 200,200), Color.White);
                

                //Hud               
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.DrawString(healthFont, $"{user.Health}", new Vector2(550, 900), Color.White);
                _spriteBatch.DrawString(healthFont, $"{wave}", new Vector2(400, 900), Color.White);
                _spriteBatch.DrawString(healthFont, $"{user.Points}", new Vector2(800, 900), Color.White);
                _spriteBatch.DrawString(healthFont, (seconds).ToString("0.0"), new Vector2(1000, 900), Color.CornflowerBlue);

                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
            }
            else if (screen == Screen.OutroScreen)
            {
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkOrange);



            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}