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

        Texture2D rectangleTexture, introBackroundTexture, hutBlueTexture,hutTexture,AiSkelMeleeRightTexture, AiSkelWalkingRight,wizardCrosshair, darkTreeTexture, grayRockTexture, darkerTreeTexture, healthGreenBarTexture, emptyGreenBarTexture,userSheildWalkTexture, userSheildIdleTexture, lightningTexture1, lightningTexture2, lightningTexture3, arrowTexture, AiArcherWalkingRight, AiArcherMeleeRightTexture, AiWalkingRight, AiMeleeRightTexture, userWalkingRight, userWalkingLeft, userAttackRightTexture, userAttackLeftTexture, userIdleTexture;
        Vector2 backroundSpeed;
        Rectangle targetedEnemy;
        Vector2 spawnPoint;
        int t = 0;
        int userspeed = 2;
        int crusaders = 0;
        int previousHealth = 0;
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
        bool toStore;
        bool bossBattle = false;
        bool wizardBattle = false;
        bool reaperBattle = false;
       

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
        List<Texture2D> userSpecialList;

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

        List<Texture2D> BatRightList;
        List<Texture2D> BatMeleeRightList;
        List<Texture2D> BatHitList;

        List<Texture2D> AiSkelRightList;
        List<Texture2D> AiSkelMeleeRightList;
        List<Texture2D> AiSkelHitList;
        
        List<Texture2D> KnightRightList;
        List<Texture2D> KnightMeleeRightList;
        List<Texture2D> KnightIdleList;
        List<Texture2D> KnightHitList;

        List<Texture2D> minoRightList;
        List<Texture2D> minoMeleeRightList;
        List<Texture2D> minoHitList;

        List<Texture2D> wizardRightList;
        List<Texture2D> wizardMeleeRightList;
        List<Texture2D> wizardHitList;

        List<Texture2D> deathRightList;
        List<Texture2D> deathMeleeRightList;
       
        List<Texture2D> deathSpellList;

        List<Texture2D> blacksmithList;
        List<Texture2D> postionList;
        List<Texture2D> herbaleList;
        List<Texture2D> merchantList;

        List<Texture2D> fireBallList;

        List<Texture2D> LightningShotList1;
        List<Texture2D> LightningShotList2;
        List<Texture2D> LightningShotList3;

        List<Texture2D> ArrowShotList;
        List<Texture2D> DeathShotList;

        List<Barriers> barriersList = new List<Barriers>();
        List<Player> enemylist = new List<Player>();
        List<Player> allylist = new List<Player>();
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
            StoreScreen,
            
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
            user = new Player(new Vector2(500, 500),new Vector2( 150, 185), 1000, "melee", 2, userRightList, userIdleList, userSheildWalkList, userSheildIdleList, userAttackList, AiRightList, userSkillSheildList, userSpecialList);

            //Add Npc
            npcList.Add(new Npc(new Rectangle(150, 100, 70, 100), herbaleList));
            npcList.Add(new Npc(new Rectangle(950, 100, 70, 100), postionList));
            npcList.Add(new Npc(new Rectangle(180, 750, 90,100), blacksmithList));
            npcList.Add(new Npc(new Rectangle(990, 770, 50, 90), merchantList));

            //Buttons
            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(100, 150, 150, 150), Color.DarkSlateGray, "Health Potion", 50));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(900, 150, 150, 150), Color.DarkSlateGray, "Sheild Recovery Time Decrease", 250));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(900, 750, 150, 150), Color.DarkSlateGray, "Speed Boost", 500));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(100, 750, 150, 150), Color.DarkSlateGray, "Damage Boost", 300));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(1100, 350, 160, 160), Color.DarkRed, "Minotaur Battle", 0));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(1100, 600, 160, 160), Color.DarkBlue, "Wizard Battle" ,0));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(1100, 850, 160, 160), Color.Black, "Reaper Battle", 0));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(450, 150, 160, 160), Color.DarkBlue, "Add Crusader", 300));

            //Top Wall
            //barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, 60), 10000, Color.Black, "false", "true"));
            //Left Wall
            //barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, -mainGameHeight, 60, mainGameHeight * 3), 10000, Color.Black, "false", "true"));
            //Bottom Wall
            //barriersList.Add(new Barriers(rectangleTexture, new Rectangle(-mainGameWidth, mainGameHeight * 2 - 60, mainGameWidth * 3, 60), 10000, Color.Black, "false", "true"));
            //Right Wall
            //barriersList.Add(new Barriers(rectangleTexture, new Rectangle(mainGameWidth * 2 - 60, -mainGameHeight, 60, mainGameHeight * 3), 10000, Color.Black, "false", "true"));



        }
        //----------------------------------------------------------------------LoadContent--------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            
            rectangleTexture = Content.Load<Texture2D>("rectangle");
            introBackroundTexture = Content.Load<Texture2D>("Intro_Final_Project");
            //Hud
            emptyGreenBarTexture = Content.Load<Texture2D>("Health_Empty");
            healthGreenBarTexture = Content.Load<Texture2D>("Health_Full");

            healthFont = Content.Load<SpriteFont>("Good");
            damageFont = Content.Load<SpriteFont>("DamageText");

            wizardCrosshair = Content.Load<Texture2D>("WizardCrosshair");
            //Enviorment
            grayRockTexture = Content.Load<Texture2D>("NatureSprite-Gray-Rock");
            darkTreeTexture = Content.Load<Texture2D>("NatureSprite-Dark-Tree");
            darkerTreeTexture = Content.Load<Texture2D>("NatureSprite-Darker-Tree");
            //User
            userWalkingRight = Content.Load<Texture2D>("Wizard_Move");
            
            userAttackRightTexture = Content.Load<Texture2D>("Wizard_Attack");
            
            userIdleTexture = Content.Load<Texture2D>("Wizard_Idle");
            Texture2D userSkillSheildTexture = Content.Load<Texture2D>("Wizard_Explode");
            Texture2D userSpecialTexture = Content.Load<Texture2D>("Wizard_Special");
            userSheildWalkTexture = Content.Load<Texture2D>("Wizard_Move_Blue");
            userSheildIdleTexture = Content.Load<Texture2D>("Wizard_Idle_Blue");
            //Ai
            Texture2D AiWalkingRight = Content.Load<Texture2D>("Goblin Running Right");
            Texture2D AiMeleeRightTexture = Content.Load<Texture2D>("Attack");
            Texture2D AiGoblinHitTexture = Content.Load<Texture2D>("Take Hit_Goblin");

            Texture2D AiArcherWalkingRight = Content.Load<Texture2D>("RunArcher_scaled");
            Texture2D AiArcherMeleeRightTexture = Content.Load<Texture2D>("AttackArcher_scaled");
            Texture2D AiArcherHitTexture = Content.Load<Texture2D>("Take Hit_Archer");

            Texture2D AiSkelWalkingRight = Content.Load<Texture2D>("Walk_Skel");
            Texture2D AiSkelMeleeRightTexture = Content.Load<Texture2D>("Attack_Skel");
            Texture2D AiSkelHitTexture = Content.Load<Texture2D>("Take Hit_Skel");

            Texture2D AiWormWalkingRight = Content.Load<Texture2D>("Walk_Worm");
            Texture2D AiWormMeleeRightTexture = Content.Load<Texture2D>("Attack_Worm"); 
            Texture2D AiWormHitTexture = Content.Load<Texture2D>("Take Hit_Worm");

            Texture2D BatWalkingRight = Content.Load<Texture2D>("Bat_Fly");
            Texture2D BatMeleeRightTexture = Content.Load<Texture2D>("Bat_Attack");
            Texture2D BatHitTexture = Content.Load<Texture2D>("Bat_Take_Hit");
            //Ally
            Texture2D knightWalkingRight = Content.Load<Texture2D>("Crusader_Run");
            Texture2D knightMeleeRightTexture = Content.Load<Texture2D>("Crusader_Attack");
            Texture2D knightIdleTexture = Content.Load<Texture2D>("Crusader_Idle");
            Texture2D knightHitTexture = Content.Load<Texture2D>("Crusader_Take_Hit");
            //Bosses
            Texture2D minoWalkTexture = Content.Load<Texture2D>("Minotaur_Walk");
            Texture2D minoMeleeTexture = Content.Load<Texture2D>("Minotaur_Attack");
            Texture2D minoHitTexture = Content.Load<Texture2D>("Minotaur_Hit");

            Texture2D wizardWalkTexture = Content.Load<Texture2D>("Evil_Move");
            Texture2D wizardMeleeTexture = Content.Load<Texture2D>("Evil_Attack");
            Texture2D wizardHitTexture = Content.Load<Texture2D>("Evil_Take_Hit");

            Texture2D deathWalkTexture = Content.Load<Texture2D>("Reaper_Walk");
            Texture2D deathMeleeTexture = Content.Load<Texture2D>("Reaper_Attack");           
            Texture2D deathSpellTexture = Content.Load<Texture2D>("Reaper_Shot");
            
            //Npc's
            Texture2D blacksmithTexture = Content.Load<Texture2D>("Blacksmith_Working"); 
            Texture2D postionTexture = Content.Load<Texture2D>("Postion_Maker");
            Texture2D herbaleTexture = Content.Load<Texture2D>("Herbale_Maker");
            Texture2D merchantTexture = Content.Load<Texture2D>("Speed_Maker");
            
            hutTexture = Content.Load<Texture2D>("Decorations_Hut");
            hutBlueTexture = Content.Load<Texture2D>("Decorations_Bluehut");
            //Projectiles
            Texture2D fireBallTexture = Content.Load<Texture2D>("Move_Fire_Ball");
            lightningTexture1 = Content.Load<Texture2D>("lightning1");
            lightningTexture2 = Content.Load<Texture2D>("lightning2");
            lightningTexture3 = Content.Load<Texture2D>("lightning3");
            arrowTexture = Content.Load<Texture2D>("ArrowMove");
            Texture2D deathTexture = Content.Load<Texture2D>("Reaper_Summon");

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
            ReapetingAnimation(GraphicsDevice, userSpecialTexture, userSpecialList = new List<Texture2D>(), 16);

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

            ReapetingAnimation(GraphicsDevice, BatWalkingRight, BatRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, BatMeleeRightTexture, BatMeleeRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, BatHitTexture, BatHitList = new List<Texture2D>(), 4);
            //Ally
            ReapetingAnimation(GraphicsDevice, knightWalkingRight, KnightRightList = new List<Texture2D>(), 6);
            ReapetingAnimation(GraphicsDevice, knightMeleeRightTexture, KnightMeleeRightList = new List<Texture2D>(), 13);
            ReapetingAnimation(GraphicsDevice, knightIdleTexture, KnightIdleList = new List<Texture2D>(), 6);
            ReapetingAnimation(GraphicsDevice, knightHitTexture, KnightHitList = new List<Texture2D>(), 3);
            //Bosses
            ReapetingAnimation(GraphicsDevice, minoWalkTexture, minoRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, minoMeleeTexture, minoMeleeRightList = new List<Texture2D>(), 9);
            ReapetingAnimation(GraphicsDevice, minoHitTexture, minoHitList = new List<Texture2D>(), 3);

            ReapetingAnimation(GraphicsDevice, wizardWalkTexture, wizardRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, wizardMeleeTexture, wizardMeleeRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, wizardHitTexture, wizardHitList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, deathWalkTexture, deathRightList = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, deathMeleeTexture, deathMeleeRightList = new List<Texture2D>(), 6);            
            ReapetingAnimation(GraphicsDevice, deathSpellTexture, deathSpellList = new List<Texture2D>(), 4);

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

            ReapetingAnimation(GraphicsDevice, deathTexture, DeathShotList = new List<Texture2D>(), 4);
        }
        //-----------------------------------------------------------------------Update--------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            static void AddGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint,new Vector2( 200, 100), 100, "goblin melee", 1.5, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));

            }
            static void AddFastGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(180, 90), 80, "fast goblin melee", 2.3, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));

            }

            static void AddSkel(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(250, 125), 130, "skel melee", 0.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));

            }
            static void AddArcher(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(200, 100), 95, "arrow", 1.2, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));

            }
            static void AddWorm(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(220, 100), 175, "fire ball", 1.4, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));

            }
            static void AddBat(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(170, 90), 75, "bat", 2.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));

            }
            static void AddMinotaur(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(190, 170), 550, "minotaur", 2.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));
            }
            static void AddWizard(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(230, 150), 300, "wizard", 3.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));
            }
            static void AddBringerOfDeath(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList , List<Texture2D> AiSpellRightList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(230, 150), 800, "death", 1, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiRightList, AiRightList, AiSpellRightList));
            }
            static void AddAlly(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList,  List<Texture2D> AiIdleList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(120, 150), 250, "ally melee", 0.9 , AiRightList, AiIdleList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList));
            }
            static void MakeSpwanPoints(int mainGameWidth, int mainGameHeight, ref Vector2 spawnPoint)
            {
                Random rand = new Random();
                switch (rand.Next(0, 5))
                {
                    case 1:
                        //Left
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, 0), rand.Next(-mainGameHeight, mainGameHeight * 3)); break;
                    case 2:
                        //Top
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, mainGameWidth * 3), rand.Next(-mainGameHeight, 0)); break;
                    case 3:
                        //Right
                        spawnPoint = new Vector2(rand.Next(mainGameWidth, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 3)); break;
                    case 4:
                        //Bottom
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, mainGameWidth * 3), rand.Next(mainGameHeight, mainGameHeight * 2)); break;
                }
            }
            static void MoveingUser(Player user, int userspeed, KeyboardState keyboardState)
            {
                //Make user move

                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (user.Sheilding == "false")
                        user.HSpeed = userspeed + (float)user.BoostSpeed;
                    else
                        user.HSpeed = (userspeed + (float)user.BoostSpeed )/ 2;
                }

                else if (keyboardState.IsKeyDown(Keys.A))
                {


                    if (user.Sheilding == "false")
                        user.HSpeed = -userspeed - (float)user.BoostSpeed;
                    else
                        user.HSpeed = (-userspeed - (float)user.BoostSpeed) / 2;

                }

                else
                    user.HSpeed = 0;



                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (user.Sheilding == "false")
                        user.VSpeed = -userspeed - (float)user.BoostSpeed;
                    else
                        user.VSpeed = (-userspeed - (float)user.BoostSpeed) / 2;

                }

                else if (keyboardState.IsKeyDown(Keys.S))
                {


                    if (user.Sheilding == "false")
                        user.VSpeed = userspeed + (float)user.BoostSpeed;
                    else
                        user.VSpeed = (userspeed + (float)user.BoostSpeed) / 2;
                }

                else

                    user.VSpeed = 0;

            }
            static void DimingScreen(ref bool fading, KeyboardState keyboardState, ref Color dimScreenColor, ref Player user, ref Screen screen, string type)
            {
                                
                if (keyboardState.IsKeyDown(Keys.Tab))
                    fading = true;

                if (fading == true)                
                    dimScreenColor.A = (byte)Math.Min(dimScreenColor.A + 5, 255);
                
                if (dimScreenColor.A >= 255)
                {
                    fading = false;
                    dimScreenColor.A = 0;
                    user.XLocation = 600;
                    user.YLocation = 450;
                    if (screen == Screen.TitleScreen)                   
                        screen = Screen.MainScreen;
                    
                    else if (screen == Screen.MainScreen)                    
                        screen = Screen.StoreScreen;
                    
                    else if (screen == Screen.StoreScreen)                                       
                           screen = Screen.MainScreen;
                                    
                }
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            if (screen == Screen.TitleScreen)
            {
                DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen, "title");

            }
            //------------------------------------------Main Screen Update------------------------------------------------
            else if (screen == Screen.MainScreen)
            {
                
                foreach (Player troops in enemylist)
                    if (troops.GetBoundingBox().Intersects(new Rectangle(0, 0, mainGameWidth, mainGameHeight)))
                        toStore = false;
                    else toStore = true;
                if (toStore == true)
                {
                    DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen, "main");
                    for (int i = enemylist.Count - 1; i >= 0; i--)
                    {
                        Player t = enemylist[i];
                        enemylist.RemoveAt(i);
                        break;
                    }
                }

                seconds = (float)gameTime.TotalGameTime.TotalSeconds - sheildTime;
                if (bossBattle == true)
                {
                    if (keyboardState.IsKeyDown(Keys.P) && enemylist.Count == 0)
                    {
                        wave += 1;
                        RespawnMethold = false;


                        if (wizardBattle == true)
                            AddWizard(enemylist, wizardRightList, wizardMeleeRightList, wizardHitList);
                        else if (reaperBattle == true)
                            AddBringerOfDeath(enemylist, deathRightList, deathMeleeRightList, deathSpellList);
                        else
                            AddMinotaur(enemylist, minoRightList, minoMeleeRightList, minoHitList);

                    }

                }
                else
                {
                    for (int i = 0; enemylist.Count <= 3; i++)
                    {
                        enemyType = rand.Next(1, 7);
                        MakeSpwanPoints(mainGameWidth, mainGameHeight, ref spawnPoint);
                        switch (enemyType)
                        {
                            case 1:
                                AddArcher(enemylist, AiArcherRightList, AiArcherMeleeRightList, AiArcherHitList, spawnPoint);
                                break;
                            case 2:
                                AddFastGoblin(enemylist, AiRightList, AiMeleeRightList, AiGoblinHitList, spawnPoint);
                                break;
                            case 3:
                                AddGoblin(enemylist, AiRightList, AiMeleeRightList, AiGoblinHitList, spawnPoint);
                                break;
                            case 4:
                                AddWorm(enemylist, AiWormRightList, AiWormMeleeRightList, AiWormHitList, spawnPoint);
                                break;
                            case 5:
                                AddSkel(enemylist, AiSkelRightList, AiSkelMeleeRightList, AiSkelHitList, spawnPoint);
                                break;
                            case 6:
                                AddBat(enemylist, BatRightList, BatMeleeRightList, BatHitList, spawnPoint);
                                break;

                        }
                    }
                }
    
                while (t < crusaders)
                {
                    AddAlly(allylist, KnightRightList, KnightMeleeRightList, KnightHitList,KnightIdleList);
                    t++;
                }

                //Make ai move
                foreach (Player troops in enemylist)
                    troops.TroopsSpeed(user.Userbox());

                foreach (Player ally in allylist)
                    ally.TroopsSpeed(targetedEnemy);
                               
                previousHealth = (int)user.Health;
                playerPosition = new Vector2(user.Userbox().X, user.Userbox().Y);
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
                    foreach (Player troops in enemylist)
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

                //Define Ally Target

                foreach (Player troops in enemylist)
                {
                    if (mouseState.RightButton == ButtonState.Pressed)
                    {
                        if (troops.Collide(new Rectangle(mouseState.X, mouseState.Y, 50, 50)))
                            troops.Target = "target";
                    }
                    if (troops.Target == "target")
                    {
                        targetedEnemy = new Rectangle((int)troops.XLocation, (int)troops.YLocation, 100, 100);
                        foreach (Player ally in allylist)
                        {
                            foreach (Barriers barrier in barriersList)
                                if (ally.Collide(barrier.GetBoundingBox()))
                                    ally.UserAttackMelee(enemylist, barriersList, null, null, mouseState.X, mouseState.Y);
                                if (ally.Collide(troops.Hitbox()))
                                    ally.UserAttackMelee(enemylist, barriersList, null, null,  mouseState.X, mouseState.Y);
                        }
                    }
                }

                //Make user move
                MoveingUser(user, userspeed, keyboardState);

                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (seconds >= user.SheildSeconds && user.Sheilding == "false")
                    {

                        user.WeaponType = "sheild melee";
                        
                        user.UserAttackMelee(enemylist, barriersList, null, null,  mouseState.X, mouseState.Y);
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

                backroundSpeed.X = 0;
                backroundSpeed.Y = 0;
                
                for (int i = 0; barriersList.Count <= 80; i++)
                {

                    MakeSpwanPoints(mainGameWidth, mainGameHeight, ref spawnPoint);
                    switch (rand.Next(1, 4))
                    {
                        case 1:
                            barriersList.Add(new Barriers(grayRockTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(50, 80), rand.Next(50, 80)), 60, Color.White, "true", "true")); break;                        
                        case 2:
                            barriersList.Add(new Barriers(darkTreeTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(120, 140), rand.Next(150, 180)), 80, Color.White, "true", "true")); break;
                        case 3:
                            barriersList.Add(new Barriers(darkerTreeTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(120, 140), rand.Next(150, 180)), 100, Color.White, "true", "true")); break;
                        
                    }
                    
                }
               

                for (int i = barriersList.Count - 1; i >= 0; i--)
                {
                    Barriers t = barriersList[i];
                    if (!t.GetBoundingBox().Intersects(new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, mainGameHeight * 3)))
                    {
                        barriersList.RemoveAt(i);
                        break;
                    }
                }
                for (int i = enemylist.Count - 1; i >= 0; i--)
                {
                    Player t = enemylist[i];
                    if (!t.GetBoundingBox().Intersects(new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, mainGameHeight * 3)))
                    {
                        enemylist.RemoveAt(i);
                        break;
                    }
                }
                if (user.YLocationBottom >= mainGameHeight - mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.S))
                {
                    backroundSpeed.Y = user.VSpeed * -1;                 
                     user.UndoMoveV();                   
                }


                if (user.YLocation <= mainGameHeight / 4 && keyboardState.IsKeyDown(Keys.W))
                {
                    backroundSpeed.Y = user.VSpeed * -1;                   
                      user.UndoMoveV();                 
                }


                if (user.XLocationRight >= mainGameWidth - mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.D))
                {
                    backroundSpeed.X = user.HSpeed * -1;                   
                     user.UndoMoveH();                    
                }


                if (user.XLocation <= mainGameWidth / 4 && keyboardState.IsKeyDown(Keys.A))
                {
                    backroundSpeed.X = user.HSpeed * -1;
                   
                    user.UndoMoveH();
                    

                }
                movedDistanceX -= (int)backroundSpeed.X;
                movedDistanceY -= (int)backroundSpeed.Y;

                
               

                //Update User
                user.Update(new Vector2(0, 0), barriersList, "main game", user.Userbox());

                foreach (Player ally in allylist)
                
                    ally.Update(backroundSpeed, barriersList, "main game", ally.Hitbox());
                
                foreach (Player troops in enemylist)
                
                    troops.Update(backroundSpeed, barriersList, "main game", troops.Hitbox());
                
                foreach (Barriers barrier in barriersList)
                
                    barrier.Update(backroundSpeed, barriersList);

                //User Shots

                if (keyboardState.IsKeyDown(Keys.X))                    
                {
                    user.WeaponType = "melee";

                    user.UserAttackMelee(enemylist, barriersList, null, null,  mouseState.X, mouseState.Y);

                }

                else if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    user.WeaponType = "wizard ball";


                    user.UserAttackMelee(enemylist, barriersList, laserList, LightningShotList2, mouseState.X, mouseState.Y);

                }
                else if (keyboardState.IsKeyDown(Keys.V))
                {

                    user.WeaponType = "special";
                    user.UserAttackMelee(enemylist, barriersList, laserList, LightningShotList2, mouseState.X, mouseState.Y);

                }
                //Ai Shots

                foreach (Player troops in enemylist)
                {
                 //Enemy Melee user
                    
                    
                    if (troops.EnemyType == "melee" || troops.EnemyType == "both")
                    {
                        troops.DrawEnemyAttack(user);
                        if (troops.Attacking == "false")
                            troops.EnemyAttackMelee(user, barriersList, allylist,enemyLaserList, playerPosition, fireBallList, ArrowShotList, DeathShotList);
                    }
                    else if ((troops.WeaponType == "minotaur" || troops.WeaponType == "wizard") && troops.Attacking == "false")
                    {
                        troops.DrawEnemyAttack(user);

                        troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, new Vector2(0, 0), null, null, null);
                    }

                    //Enemy Shoot

                    else if (troops.EnemyType == "shoter" && new Rectangle(0, 0, mainGameWidth, mainGameHeight).Contains(troops.GetBoundingBox()))
                    {
                        troops.DrawEnemyAttack(null);
                        if (troops.Attacking == "false")
                            troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, playerPosition, fireBallList, ArrowShotList, DeathShotList);

                    }
                    
                }
                //Update User Laser
                foreach (LaserClass bullet in laserList)
                {

                    bullet.Update(gameTime, user.ProjectileSpeed);

                }
                //Update Enemy Laser
                foreach (LaserClass bullet in enemyLaserList)
                {
                    foreach (Player troops in enemylist)
                        bullet.Update(gameTime,troops.ProjectileSpeed);

                }


                foreach (Player troops in enemylist)
                {
                    troops.ChoosingWeapon();
                }

                //If ai gets shot
                for (int i = laserList.Count - 1; i >= 0; i--)
                {

                    LaserClass laser = laserList[i];
                    foreach (Player troops in enemylist)
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
                        if (bullet.GetBoundingBox().Intersects(user.Userbox()))
                        {
                            foreach (Player troops in enemylist)
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
                            foreach (Player troops in enemylist)
                                barrier.TakeHit((int)troops.WeaponDamage);
                            if (barrier.Blocking == "true")
                            {
                                enemyLaserList.RemoveAt(i);
                                break;
                            }
                                
                        }
                    }
                    if (!t.GetBoundingBox().Intersects(new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth*3, mainGameHeight * 3)))
                    {
                        enemyLaserList.RemoveAt(i);
                        break;
                    }
                }
                if (user.Sheilding == "true")
                    user.Health = previousHealth;
                //Detect user dealth
                if (user.Health <= 0)               
                    screen = Screen.OutroScreen;

                //Detect ally dealth
                for (int i = allylist.Count - 1; i >= 0; i--)
                {
                    Player ally = allylist[i];
                    if (ally.Health <= 0)                    
                        allylist.RemoveAt(i);                                         
                }

                //Detect ai dealth
                for (int i = enemylist.Count - 1; i >= 0; i--)
                {
                    Player troops = enemylist[i];

                    if (troops.Health <= 0)
                    {
                        enemylist.RemoveAt(i);
                        user.Points += troops.PointsOnKill;
                        
                       
                        bossBattle = false;
                        wizardBattle = false;
                        reaperBattle = false;

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
            else if (screen == Screen.StoreScreen)
            {
                seconds = user.SheildSeconds;
 
                MoveingUser(user, userspeed, keyboardState);

                foreach(Npc npc in npcList)
                    npc.Update();
                user.Update(new Vector2(0,0), barriersList, "not main game", user.Userbox());
                if (keyboardState.IsKeyUp(Keys.B))
                {
                    boost = true;
                }
                foreach ( Buttons button in buttonList)
                {
                    
                    if (button.Contains(user.Userbox()))
                    {
                        button.Hovering = "true";
                        if (keyboardState.IsKeyDown(Keys.B))
                        {
                            if (boost == true)
                            {
                                button.Boosts(user, ref crusaders);
                                boost = false;
                            }
                        }

                        if (button.Type == "Minotaur Battle")
                        {
                            bossBattle = true;
                            
                        }
                        else if (button.Type == "Wizard Battle")
                        {
                            bossBattle = true;
                            wizardBattle = true;                           
                        }
                        else if (button.Type == "Reaper Battle")
                        {
                            bossBattle = true;
                            reaperBattle = true;
                        }
                        else
                        {
                            bossBattle = false;
                            reaperBattle = false;
                            wizardBattle = false;
                        }


                    }
                    else
                    {
                        button.Hovering = "false";
                    }
                        
                }
                DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen, "store");
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


                _spriteBatch.Draw(introBackroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0,0,_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
            }
            else if (screen == Screen.MainScreen)
            { 
                if (reaperBattle == true)
                    _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkSlateGray);
                else if (wizardBattle == true)
                    _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.OrangeRed);
                else if (bossBattle == true)
                    _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.SandyBrown);
                else
                    _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkOliveGreen);


                foreach (Barriers barrier in barriersList)
                    barrier.Draw(_spriteBatch);

                //Draw all the bullets
                foreach (LaserClass bullet in laserList)
                    bullet.Draw(_spriteBatch, lightningTexture2);
                user.Draw(_spriteBatch, mouseState.X);
                
                foreach (Player ally in allylist)
                {
                    ally.Draw(_spriteBatch, 100000);
                   
                }
                foreach (Player ai in enemylist)
                {
                    ai.Draw(_spriteBatch, 100000);
                    ai.DrawHealth(_spriteBatch, emptyGreenBarTexture, healthGreenBarTexture, bossBattle, "false");                   
                    ai.DrawDamage(_spriteBatch, damageFont,damgaeMultiplyer, backroundSpeed, user.WeaponType);
                }
               

                foreach (LaserClass bullet in enemyLaserList)
                    bullet.Draw(_spriteBatch, arrowTexture);

                if (wizardBattle == true)
                    _spriteBatch.DrawString(damageFont, "Fire Wizard", new Vector2(mainGameWidth / 2 - 80, 0), Color.White);
                else if (reaperBattle == true)
                    _spriteBatch.DrawString(damageFont, "The Reaper", new Vector2(mainGameWidth / 2 - 80, 0), Color.White);
                else if (bossBattle == true)
                    _spriteBatch.DrawString(damageFont, "Minotaur", new Vector2(mainGameWidth / 2 - 80, 0), Color.White);
                //Hud               

                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.DrawString(healthFont, $"{user.Health}", new Vector2(550, 900), Color.White);
                _spriteBatch.DrawString(healthFont, $"{wave}", new Vector2(400, 900), Color.White);
                _spriteBatch.DrawString(healthFont, $"{user.Points}", new Vector2(800, 900), Color.White);
                _spriteBatch.DrawString(healthFont, (seconds).ToString("0.0"), new Vector2(1000, 900), Color.CornflowerBlue);
                user.DrawHealth(_spriteBatch, emptyGreenBarTexture, healthGreenBarTexture, bossBattle, "true");

                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
            }
            else if (screen == Screen.StoreScreen)
            {
                //Backround
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkOliveGreen);
                //Buttons
                foreach (Buttons button in buttonList)                
                    button.Draw(_spriteBatch);
                
                //Structures Far
                _spriteBatch.Draw(hutBlueTexture, new Rectangle(66, 0, 240, 200), Color.White);
                foreach (Npc npc in npcList)
                    npc.Draw(_spriteBatch);

                //Button text
                foreach (Buttons button in buttonList)             
                    button.DrawText(_spriteBatch, damageFont);
                
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
                _spriteBatch.Draw(introBackroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);


            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}