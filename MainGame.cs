using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
                
        private SpriteBatch _spriteBatch;
        Player user;

        Texture2D rectangleTexture, outroBackroundTexture,introBackroundTexture, hutBlueTexture,hutTexture,wizardCrosshair, darkTreeTexture, grayRockTexture, darkerTreeTexture, healthGreenBarTexture, emptyGreenBarTexture,userSheildWalkTexture, userSheildIdleTexture, lightningTexture1, lightningTexture2, lightningTexture3, arrowTexture, AiArcherWalkingRight, AiArcherMeleeRightTexture, AiWalkingRight, AiMeleeRightTexture, userWalkingRight, userWalkingLeft, userAttackRightTexture, userAttackLeftTexture, userIdleTexture;
        Texture2D greenTreeTexture, bossBuyTexture, boostBuyTexture,redTreeTexture, brownTreeTexture, rock1Texture, rock2Texture, rock3Texture, grass1Texture, grass2Texture, grass3Texture, logTexture;
        Texture2D UIHealthTexture, UIBlackTexture, UIHoverTexture, UIRedTexture, UIRedEmptyTexture, UIHeartTexture,UIBlueTexture, UIBlueEmptyTexture,wizardHeadTexture;
        Vector2 backroundSpeed;
        Rectangle targetedEnemy = new Rectangle(750, 450, 50,50);
        Vector2 spawnPoint,guardLocation;
  
 
        int t = 0;
        int userspeed = 2;
        int Ratfolk = 0;
        int previousHealth = 0;
        int mainGameWidth = 1400;
        int mainGameHeight = 900;
        int enemyType = 0;
        int mainScreenHeight = 1000;
        int difficulty = 3;
        int randomNumber;
        Color dimScreenColor;
        bool RespawnMethold = false;
        bool boost = false;
       
        bool Instruct = false;
        bool fading;
        bool toStore;
        bool bossBattle = false;
        bool wizardBattle = false;
        bool reaperBattle = false;
        bool startGame = false;

        //Sounds
        Song introMusic, bossMusic, mainMusic, storeMusic;
        SoundEffect buttonSound, staffSound, hitSound;
        SoundEffectInstance buttonSoundInsta, staffSoundInsta, hitSoundInsta;

        double damgaeMultiplyer = 1;
        float seconds;
        float sheildTime;

        
        private Vector2 playerPosition;
        
        private SpriteFont healthFont, dungeonFont, damageFont;
  
    
        List<Texture2D> shotTexture;
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

        List<Texture2D> SlayerRightList;
        List<Texture2D> SlayerMeleeRightList;
        List<Texture2D> SlayerHitList;

        List<Texture2D> AiSkelRightList;
        List<Texture2D> AiSkelMeleeRightList;
        List<Texture2D> AiSkelHitList;
        
        List<Texture2D> RatfolkRightList;
        List<Texture2D> RatfolkMeleeRightList;
        List<Texture2D> RatfolkIdleList;
        List<Texture2D> RatfolkHitList;

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
        List<Texture2D> minoIdleList;
        List<Texture2D> evilIdleList;

        List<Texture2D> fireBallList;

        List<Texture2D> guardList;

        List<Texture2D> LightningShotList1;
        List<Texture2D> LightningShotList2;
        List<Texture2D> LightningShotList3;

        List<Texture2D> fireList1;
        List<Texture2D> fireList2;

        List<Texture2D> iceList1;
        List<Texture2D> iceList2;

        List<Texture2D> thunderList1;
        List<Texture2D> thunderList2;

        List<Texture2D> goldCoinTextureList;
        List<Texture2D> redCoinTextureList;
        List<Texture2D> silverCoinTextureList;
        List<Texture2D> greenGemTextureList;

        List<Texture2D> grassGifTextureList;
        List<Texture2D> grassGif2TextureList;
        List<Texture2D> grassGif3TextureList;

        List<Texture2D> flowerGifTextureList;
        List<Texture2D> flowerGif2TextureList;

        List<Texture2D> ArrowShotList;
        List<Texture2D> DeathShotList;

        List<Coins> coinList = new List<Coins>();
        List<Coins> grassList = new List<Coins>();
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
            user = new Player(new Vector2(500, 500),new Vector2( 150, 185), 1000, "melee", 1.75, userRightList, userIdleList, userSheildWalkList, userSheildIdleList, userAttackList, userRightList, userSkillSheildList, userSpecialList, guardList);

            //Add Npc
            npcList.Add(new Npc(new Rectangle(150, 100, 70, 100), herbaleList, "right"));
            npcList.Add(new Npc(new Rectangle(950, 100, 70, 100), postionList, "right"));
            npcList.Add(new Npc(new Rectangle(180, 750, 90,100), blacksmithList, "right"));
            npcList.Add(new Npc(new Rectangle(990, 770, 50, 90), merchantList, "right"));

            npcList.Add(new Npc(new Rectangle(1200, 150, 190, 170), minoIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(1200, 400, 230, 150), evilIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(1200, 650, 230, 150), deathRightList, "flip"));

            npcList.Add(new Npc(new Rectangle(450, 135, 100, 50), RatfolkIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(475, 150, 100, 50), RatfolkIdleList, "right"));
            npcList.Add(new Npc(new Rectangle(490, 160, 100, 50), RatfolkIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(496, 135, 100, 50), RatfolkIdleList, "right"));
            npcList.Add(new Npc(new Rectangle(475, 130, 100, 50), RatfolkIdleList, "right"));
            
            //Home Screen Buttons
            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(5, 520, 345, 90), Color.White, "Start", 0));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(5, 645, 545, 90), Color.White, "How to Play", 0));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(500, 80, 545, 500), Color.White, "Instructions", 0));

            //Buttons
            buttonList.Add(new Buttons(boostBuyTexture, new Rectangle(100, 150, 160, 160), Color.White, "Health Potion", 50));

            buttonList.Add(new Buttons(boostBuyTexture, new Rectangle(900, 150, 160, 160), Color.White, "Sheild Recovery Time Decrease", 250));

            buttonList.Add(new Buttons(boostBuyTexture, new Rectangle(900, 750, 160, 160), Color.White, "Speed Boost", 500));

            buttonList.Add(new Buttons(boostBuyTexture, new Rectangle(100, 750, 160, 160), Color.White, "Damage Boost", 300));

            buttonList.Add(new Buttons(bossBuyTexture, new Rectangle(1100, 150, 160, 160), Color.SandyBrown, "Minotaur Battle", 0));

            buttonList.Add(new Buttons(bossBuyTexture, new Rectangle(1100, 400, 160, 160), Color.DarkRed, "Wizard Battle" ,0));

            buttonList.Add(new Buttons(bossBuyTexture, new Rectangle(1100, 650, 160, 160), Color.Black, "Reaper Battle", 0));

            buttonList.Add(new Buttons(bossBuyTexture, new Rectangle(450, 150, 160, 160), Color.White, "Ratfolk (Ally)", 125));

            buttonList.Add(new Buttons(boostBuyTexture, new Rectangle(100, 400, 160, 160), Color.White, "Increase Difficuly (+$250)", 250));

            barriersList.Add(new Barriers(grass1Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass2Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass3Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass1Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass2Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass3Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass1Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass2Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass3Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 20, 30), 80, Color.White, "false", "false"));

           
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 90, 45), grassGifTextureList, "grass")); 
                      
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 90, 45), grassGif2TextureList, "grass")); 
                        
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 90, 45), grassGif3TextureList, "grass")); 
                      
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 90, 45), flowerGifTextureList, "grass"));
                     
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 90, 45), flowerGif2TextureList, "grass"));

            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth/4 - 50, mainGameHeight-mainGameHeight/4 - 62, 125,150), 145, Color.White, "true", "true"));
            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth /4- 50, mainGameHeight /4 - 62, 125, 150), 145, Color.White, "true", "true"));
            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth - mainGameWidth / 4-50, mainGameHeight - mainGameHeight / 4 - 62, 125, 150), 145, Color.White, "true", "true"));
            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth - mainGameWidth / 4-50, mainGameHeight/4 - 62, 125, 150), 145, Color.White, "true", "true"));

            coinList.Add(new Coins( new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 28, 35),silverCoinTextureList, "silver")); 
            coinList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainGameHeight), 28, 35),goldCoinTextureList, "gold")); 
        }
        //----------------------------------------------------------------------LoadContent--------------------------------------------------------------------------------------
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            //Sounds
            introMusic = Content.Load<Song>("introMusic");
            bossMusic = Content.Load<Song>("MusicBoss");
            mainMusic = Content.Load<Song>("MusicMainGame");
            storeMusic = Content.Load<Song>("MusicStore");

            buttonSound = Content.Load<SoundEffect>("Effect_Button");
            buttonSoundInsta = buttonSound.CreateInstance();

            staffSound = Content.Load<SoundEffect>("Effect_Staff");
            staffSoundInsta = buttonSound.CreateInstance();

            hitSound = Content.Load<SoundEffect>("Effect_Hit");
            hitSoundInsta = buttonSound.CreateInstance();

            rectangleTexture = Content.Load<Texture2D>("rectangle");
            outroBackroundTexture = Content.Load<Texture2D>("Art_Screen");
            introBackroundTexture = Content.Load<Texture2D>("Art_Intro");
            //Hud
            emptyGreenBarTexture = Content.Load<Texture2D>("Health_Empty");
            healthGreenBarTexture = Content.Load<Texture2D>("Health_Full");

            UIHealthTexture = Content.Load<Texture2D>("UI_Health");
            UIBlackTexture = Content.Load<Texture2D>("UI_Black");
            UIHoverTexture = Content.Load<Texture2D>("UI_Hover");

            UIBlueTexture = Content.Load<Texture2D>("UI_Blue_Full");
            UIBlueEmptyTexture = Content.Load<Texture2D>("UI_Blue_Empty");

            UIRedTexture = Content.Load<Texture2D>("UI_Red_Full");
            UIRedEmptyTexture = Content.Load<Texture2D>("UI_Red_Empty");
            UIHeartTexture = Content.Load<Texture2D>("UI_Heart");

            wizardHeadTexture = Content.Load<Texture2D>("Wizard_Head");

            healthFont = Content.Load<SpriteFont>("Good");
            damageFont = Content.Load<SpriteFont>("DamageText");
            dungeonFont = Content.Load<SpriteFont>("DungonFont");

            wizardCrosshair = Content.Load<Texture2D>("WizardCrosshair");
            //Enviorment
            grayRockTexture = Content.Load<Texture2D>("NatureSprite-Gray-Rock");
            darkTreeTexture = Content.Load<Texture2D>("NatureSprite-Dark-Tree");
            darkerTreeTexture = Content.Load<Texture2D>("NatureSprite-Darker-Tree");

             greenTreeTexture = Content.Load<Texture2D>("Green_Tree");
             redTreeTexture = Content.Load<Texture2D>("Red_Tree");
             brownTreeTexture = Content.Load<Texture2D>("Brown_tree");

             rock1Texture = Content.Load<Texture2D>("Rock1");
             rock2Texture = Content.Load<Texture2D>("Rock2");
             rock3Texture = Content.Load<Texture2D>("Rock3");

             grass1Texture = Content.Load<Texture2D>("Grass1");
             grass2Texture = Content.Load<Texture2D>("Grass2");
             grass3Texture = Content.Load<Texture2D>("Grass3");

            bossBuyTexture = Content.Load<Texture2D>("Boss_Buy");
            boostBuyTexture = Content.Load<Texture2D>("Boost_Buy");

            logTexture = Content.Load<Texture2D>("Log");
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

            Texture2D SlayerWalkingRight = Content.Load<Texture2D>("Slayer_Move");
            Texture2D SlayerMeleeRightTexture = Content.Load<Texture2D>("Slayer_Attack");
            Texture2D SlayerHitTexture = Content.Load<Texture2D>("Slayer_Hit");
            //Ally
            Texture2D RatfolkWalkingRight = Content.Load<Texture2D>("Ratfolk_Move");
            Texture2D RatfolkMeleeRightTexture = Content.Load<Texture2D>("Ratfolk_Attack");
            Texture2D RatfolkIdleTexture = Content.Load<Texture2D>("Ratfolk_Idle");
            Texture2D RatfolkHitTexture = Content.Load<Texture2D>("Ratfolk_Hit");
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
            Texture2D minoIdleTexture = Content.Load<Texture2D>("Minotaur_Idle");
            Texture2D evilIdleTexture = Content.Load<Texture2D>("Evil_Idle");

            hutTexture = Content.Load<Texture2D>("Decorations_Hut");
            hutBlueTexture = Content.Load<Texture2D>("Decorations_Bluehut");
            //Projectiles
            Texture2D fireBallTexture = Content.Load<Texture2D>("Move_Fire_Ball");
            Texture2D guardTexture = Content.Load<Texture2D>("guard");
            lightningTexture1 = Content.Load<Texture2D>("lightning1");
            lightningTexture2 = Content.Load<Texture2D>("lightning2");
            lightningTexture3 = Content.Load<Texture2D>("lightning3");

            Texture2D fire1Texture = Content.Load<Texture2D>("fire1");
            Texture2D fire2Texture = Content.Load<Texture2D>("fire2");

            Texture2D ice1Texture = Content.Load<Texture2D>("ice1");
            Texture2D ice2Texture = Content.Load<Texture2D>("ice2");

            Texture2D thunder1Texture = Content.Load<Texture2D>("thunder1");
            Texture2D thunder2Texture = Content.Load<Texture2D>("thunder2");

            Texture2D goldCoinTexture = Content.Load<Texture2D>("Coin_Gold");
            Texture2D silverCoinTexture = Content.Load<Texture2D>("Coin_Silver");
            Texture2D redCoinTexture = Content.Load<Texture2D>("Coin_Red");

            Texture2D grassGifTexture = Content.Load<Texture2D>("Animated_Grass");
            Texture2D grassGif2Texture = Content.Load<Texture2D>("Animated_Grass2");
            Texture2D grassGif3Texture = Content.Load<Texture2D>("Animated_Grass3");

            Texture2D flowerGifTexture = Content.Load<Texture2D>("Animated_Flowers");
            Texture2D flowerGif2Texture = Content.Load<Texture2D>("Animated_Flowers2");

            Texture2D greenGemTexture = Content.Load<Texture2D>("Gem_Green");
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

            ReapetingAnimation(GraphicsDevice, SlayerWalkingRight, SlayerRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, SlayerMeleeRightTexture, SlayerMeleeRightList = new List<Texture2D>(), 5);
            ReapetingAnimation(GraphicsDevice, SlayerHitTexture,SlayerHitList = new List<Texture2D>(), 4);
            //Ally
            ReapetingAnimation(GraphicsDevice, RatfolkWalkingRight, RatfolkRightList = new List<Texture2D>(), 7);
            ReapetingAnimation(GraphicsDevice, RatfolkMeleeRightTexture, RatfolkMeleeRightList = new List<Texture2D>(), 12);
            ReapetingAnimation(GraphicsDevice, RatfolkIdleTexture, RatfolkIdleList = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, RatfolkHitTexture, RatfolkHitList = new List<Texture2D>(), 4);
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
            ReapetingAnimation(GraphicsDevice, minoIdleTexture, minoIdleList = new List<Texture2D>(), 5);
            ReapetingAnimation(GraphicsDevice, evilIdleTexture, evilIdleList = new List<Texture2D>(), 8);

            //Projectiles
            ReapetingAnimation(GraphicsDevice, fireBallTexture, fireBallList = new List<Texture2D>(), 6);

            ReapetingAnimation(GraphicsDevice, guardTexture, guardList = new List<Texture2D>(), 4);
            
            ReapetingAnimation(GraphicsDevice, fire1Texture, fireList1 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, fire2Texture, fireList2 = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, ice1Texture, iceList1 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, ice2Texture, iceList2 = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, thunder1Texture, thunderList1 = new List<Texture2D>(), 2);
            ReapetingAnimation(GraphicsDevice, thunder2Texture, thunderList2 = new List<Texture2D>(), 2);

            //Coins
            ReapetingAnimation(GraphicsDevice, goldCoinTexture, goldCoinTextureList = new List<Texture2D>(), 5);
            ReapetingAnimation(GraphicsDevice, silverCoinTexture, silverCoinTextureList = new List<Texture2D>(), 5);
            ReapetingAnimation(GraphicsDevice, redCoinTexture, redCoinTextureList = new List<Texture2D>(), 5);
            ReapetingAnimation(GraphicsDevice, greenGemTexture, greenGemTextureList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, grassGifTexture, grassGifTextureList = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, grassGif2Texture, grassGif2TextureList = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, grassGif3Texture, grassGif3TextureList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, flowerGifTexture, flowerGifTextureList = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, flowerGif2Texture, flowerGif2TextureList = new List<Texture2D>(), 4);

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
                enemys.Add(new Player(spawnPoint,new Vector2( 200, 100), 100, "goblin melee", 1.5, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList,AiRightList));
            }
            static void AddFastGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(180, 90), 80, "fast goblin melee", 2.3, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }

            static void AddSkel(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(250, 125), 145, "skel melee", 0.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddArcher(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(200, 100), 95, "arrow", 1.2, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddWorm(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(220, 100), 175, "fire ball", 1.4, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddBat(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(170, 90), 75, "bat", 2.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddSlayer(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint)
            {
                Random rand = new Random();
                enemys.Add(new Player(spawnPoint, new Vector2(170, 90), 105, "slayer", 2.2, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddMinotaur(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(190, 170), 550, "minotaur", 2.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddWizard(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(230, 150), 300, "wizard", 3.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void AddBringerOfDeath(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList , List<Texture2D> AiSpellRightList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(230, 150), 800, "death", 1.2, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiRightList, AiRightList, AiSpellRightList, AiRightList));
            }
            static void AddAlly(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList,  List<Texture2D> AiIdleList)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(100, 50), 120, "ally melee", 1.2 , AiRightList, AiIdleList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList));
            }
            static void MakeSpwanPoints(int mainGameWidth, int mainGameHeight, ref Vector2 spawnPoint)
            {
                Random rand = new Random();
                switch (rand.Next(1, 5))
                {
                    case 1:
                        //Left
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, -150), rand.Next(-mainGameHeight, mainGameHeight * 3)); break;
                    case 2:
                        //Top
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, mainGameWidth * 3), rand.Next(-mainGameHeight, -150)); break;
                    case 3:
                        //Right
                        spawnPoint = new Vector2(rand.Next(mainGameWidth + 150, mainGameWidth * 2), rand.Next(-mainGameHeight, mainGameHeight * 3)); break;
                    case 4:
                        //Bottom
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, mainGameWidth * 3), rand.Next(mainGameHeight +150, mainGameHeight * 2)); break;
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
                    MediaPlayer.Stop();
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
                //Play intro song on loop
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(introMusic);

                foreach (Buttons button in buttonList)
                {

                    if (button.Contains(new Rectangle(mouseState.X, mouseState.Y, 20, 20)))
                    {
                        
                        
                        if (button.Type == "Start")
                        {
                            fading = true;
                            button.Hovering = "true";
                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                if (buttonSoundInsta.State == SoundState.Stopped)
                                    buttonSoundInsta.Play();
                                startGame = true;
                            }
                                
                            
                        }
                        else if(button.Type == "How to Play")
                        {
                            button.Hovering = "true";
                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                if (buttonSoundInsta.State == SoundState.Stopped)
                                    buttonSoundInsta.Play();
                                Instruct = true;
                            }
                                
                            else
                                Instruct = false;
                        }
                       
                    }
                    else
                    {
                       
                        button.Hovering = "false";
                    }
                   

                }
                if (startGame)
                    DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen, "title");
            }
            //------------------------------------------Main Screen Update------------------------------------------------
            else if (screen == Screen.MainScreen)
            {
                //Play intro song on loop
                if (bossBattle)
                {
                    if (MediaPlayer.State == MediaState.Stopped)
                        MediaPlayer.Play(bossMusic);
                }
                else
                {
                    if (MediaPlayer.State == MediaState.Stopped)
                        MediaPlayer.Play(mainMusic);
                }
                

                foreach (Player troops in enemylist)
                    if (troops.GetBoundingBox().Intersects(new Rectangle(0, 0, mainGameWidth, mainGameHeight)))
                        toStore = false;
                    else
                        toStore = true;
                if (toStore)
                    DimingScreen(ref fading, keyboardState, ref dimScreenColor, ref user, ref screen, "main");

                seconds = (float)gameTime.TotalGameTime.TotalSeconds - sheildTime;
                if (bossBattle == true)
                {
                    for (int i = enemylist.Count - 1; i >= 0; i--)
                    {
                        Player t = enemylist[i];
                        if (t.WeaponType != "minotaur" && t.WeaponType != "wizard" && t.WeaponType != "death")
                        {
                            enemylist.Clear();
                            break;
                        }
                    }
                    if (keyboardState.IsKeyDown(Keys.P) && enemylist.Count == 0)
                    {
                        difficulty += 1;
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
                    for (int i = 0; enemylist.Count <= difficulty; i++)
                    {
                        enemyType = rand.Next(1, 8);
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
                            case 7:
                                AddSlayer(enemylist, SlayerRightList, SlayerMeleeRightList, SlayerHitList, spawnPoint);
                                break;

                        }
                    }
                }
                while (t < Ratfolk)
                {
                    AddAlly(allylist, RatfolkRightList, RatfolkMeleeRightList, RatfolkHitList, RatfolkIdleList);
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
                        
                    }
                    foreach (Player ally in allylist)
                    {
                        foreach (Barriers barrier in barriersList)
                            if (ally.Collide(barrier.GetBoundingBox()) && barrier.Breakable == "true")
                                ally.UserAttackMelee(enemylist, barriersList, null, null, mouseState.X, mouseState.Y, null, null, null, null, null, null, null);
                        if (ally.Collide(troops.Hitbox()))
                            ally.UserAttackMelee(enemylist, barriersList, null, null, mouseState.X, mouseState.Y, null, null, null, null, null, null, null);
                    }
                }                     
                //Make user move
                MoveingUser(user, userspeed, keyboardState);

                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (seconds >= user.SheildSeconds && user.Sheilding == "false")
                    {

                        user.WeaponType = "sheild melee";

                        user.UserAttackMelee(enemylist, barriersList, null, null, mouseState.X, mouseState.Y, null, null, null, null, null, null, null);
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

                for (int i = 0; barriersList.Count <= 150; i++)
                {

                    MakeSpwanPoints(mainGameWidth, mainGameHeight, ref spawnPoint);
                    switch (rand.Next(1, 8))
                    {
                        case 1:
                            barriersList.Add(new Barriers(rock1Texture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(50, 80), rand.Next(50, 80)), 60, Color.White, "true", "true")); break;
                        case 2:
                            barriersList.Add(new Barriers(rock2Texture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(50, 80), rand.Next(50, 80)), 60, Color.White, "true", "true")); break;
                        case 3:
                            barriersList.Add(new Barriers(rock3Texture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(50, 80), rand.Next(50, 80)), 60, Color.White, "true", "true")); break;
                        case 4:
                            barriersList.Add(new Barriers(greenTreeTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(120, 140), rand.Next(150, 180)), 80, Color.White, "true", "true")); break;
                        case 5:
                            barriersList.Add(new Barriers(redTreeTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(120, 140), rand.Next(150, 180)), 80, Color.White, "true", "true")); break;
                        case 6:
                            barriersList.Add(new Barriers(brownTreeTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, rand.Next(120, 140), rand.Next(150, 180)), 80, Color.White, "true", "true")); break;
                        case 7:
                            barriersList.Add(new Barriers(logTexture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 50, 55), 80, Color.White, "true", "true")); break;

                    }
                    MakeSpwanPoints(mainGameWidth, mainGameHeight, ref spawnPoint);
                    switch (rand.Next(1, 4))
                    {

                        case 1:
                            barriersList.Add(new Barriers(grass1Texture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 20, 30), 80, Color.White, "false", "false")); break;
                        case 2:
                            barriersList.Add(new Barriers(grass2Texture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 20, 30), 80, Color.White, "false", "false")); break;
                        case 3:
                            barriersList.Add(new Barriers(grass3Texture, new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 20, 30), 80, Color.White, "false", "false")); break;
                        
                    }

                }
                for (int i = 0; grassList.Count <= 95; i++)
                {
                    MakeSpwanPoints(mainGameWidth, mainGameHeight, ref spawnPoint);
                    switch (rand.Next(1, 6))
                    {

                        case 1:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90,45 ), grassGifTextureList, "grass")); break;
                        case 2:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), grassGif2TextureList, "grass")); break;
                        case 3:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), grassGif3TextureList, "grass")); break;
                        case 4:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), flowerGifTextureList, "grass")); break;
                        case 5:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), flowerGif2TextureList, "grass")); break;

                    }

                }
                 for (int i = 0; coinList.Count <= 23; i++)
                {
                    randomNumber = (rand.Next(0, 101));
                    MakeSpwanPoints(mainGameWidth, mainGameHeight, ref spawnPoint);
                    if (randomNumber <=50)
                        coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), silverCoinTextureList, "silver")); 
                    else if (randomNumber > 50 && randomNumber <= 75)
                        coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), goldCoinTextureList, "gold"));
                    else if (randomNumber > 75 && randomNumber <= 92)
                        coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), redCoinTextureList, "red"));
                    else if (randomNumber > 92 && randomNumber <=100)
                        coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), greenGemTextureList, "green")); 
                }


                for (int i = coinList.Count - 1; i >= 0; i--)
                {
                    Coins coins = coinList[i];
                    if (user.Userbox().Intersects(coins.GetBoundingBox()))
                    {
                        if (coins.CoinType != "grass")
                        {
                            if (coins.CoinType == "silver")
                                user.Points += 25;
                            else if (coins.CoinType == "gold")
                                user.Points += 45;
                            else if (coins.CoinType == "red")
                                user.Points += 125;
                            else if (coins.CoinType == "green")
                                user.Points += 200;
                            coinList.RemoveAt(i);
                            break;
                        }
                        
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
                for (int i =coinList.Count - 1; i >= 0; i--)
                {
                    Coins t = coinList[i];
                    if (!t.GetBoundingBox().Intersects(new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, mainGameHeight * 3)))
                    {
                        coinList.RemoveAt(i);
                        break;
                    }
                }
                for (int i = grassList.Count - 1; i >= 0; i--)
                {
                    Coins t = grassList[i];
                    if (!t.GetBoundingBox().Intersects(new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, mainGameHeight * 3)))
                    {
                        grassList.RemoveAt(i);
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
        

                //Update User
                user.Update(new Vector2(0, 0), barriersList, "main game", user.Userbox(), "user");

                foreach (Player ally in allylist)

                    ally.Update(backroundSpeed, barriersList, "main game", ally.Hitbox(), "ai");

                foreach (Player troops in enemylist)

                    troops.Update(backroundSpeed, barriersList, "main game", troops.Hitbox(), "ai");
                foreach (Coins coins in coinList)

                    coins.Update(backroundSpeed);
                foreach (Coins grass in grassList)

                    grass.Update(backroundSpeed);

                foreach (Barriers barrier in barriersList)

                    barrier.Update(backroundSpeed, barriersList);

                //User Shots

                if (keyboardState.IsKeyDown(Keys.X))
                {
                    user.WeaponType = "melee";

                    user.UserAttackMelee(enemylist, barriersList, null, null, mouseState.X, mouseState.Y, null, null, null, null, null, null, null);

                }

                else if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    user.WeaponType = "wizard ball";


                    user.UserAttackMelee(enemylist, barriersList, laserList, LightningShotList2, mouseState.X, mouseState.Y, null, null, null, null, null, null, null);

                }
                else if (keyboardState.IsKeyDown(Keys.V))
                {

                    user.WeaponType = "special";


                    user.UserAttackMelee(enemylist, barriersList, laserList, shotTexture, mouseState.X, mouseState.Y, shotTexture, fireList1, fireList2, iceList1, iceList2, thunderList1, thunderList2);



                }
                //Ai Shots

                foreach (Player troops in enemylist)
                {
                    //Enemy Melee user


                    if (troops.EnemyType == "melee" || troops.EnemyType == "both")
                    {
                        troops.DrawEnemyAttack(user);
                        if (troops.Attacking == "false")
                            troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, playerPosition, fireBallList, ArrowShotList, DeathShotList);
                    }
                    //Enemy Shoot

                    else if (troops.EnemyType == "shoter")
                    {
                        troops.DrawEnemyAttack(null);
                        if (troops.Attacking == "false")
                            troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, playerPosition, fireBallList, ArrowShotList, DeathShotList);

                    }
                    /*
                    else if ((troops.WeaponType == "minotaur" || troops.WeaponType == "wizard") && troops.Attacking == "false")
                    {
                        troops.DrawEnemyAttack(user);

                        troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, new Vector2(0, 0), null, null, null);
                    }
                    */


                }
                //Update User Laser
                foreach (LaserClass bullet in laserList)
                    bullet.Update(gameTime, user.ProjectileSpeed);

                //Update Enemy Laser
                foreach (LaserClass bullet in enemyLaserList)
                {
                    foreach (Player troops in enemylist)
                        bullet.Update(gameTime, troops.ProjectileSpeed);

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
                        if (troops.Hitbox().Intersects(laser.GetBoundingBox()))
                        {
                            
                            if (troops.HeadShotBox().Intersects(laser.GetBoundingBox()))
                                troops.HeadShot = 1.5f;

                            
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
                    LaserClass bullet = enemyLaserList[i];

                    if (bullet.GetBoundingBox().Intersects(user.Userbox()))
                    {

                        user.Health -= bullet.WeaponDamage;
                        guardLocation = new Vector2(bullet.GetBoundingBox().X, bullet.GetBoundingBox().Y);
                        user.UserHit();
                        enemyLaserList.RemoveAt(i);
                        break;

                    }
                    else
                        guardLocation = new Vector2(user.Userbox().X, user.Userbox().Y);

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

                            barrier.TakeHit(t.WeaponDamage);
                            if (barrier.Blocking == "true")
                            {
                                enemyLaserList.RemoveAt(i);
                                break;
                            }

                        }
                    }
                    if (!t.GetBoundingBox().Intersects(new Rectangle(-mainGameWidth, -mainGameHeight, mainGameWidth * 3, mainGameHeight * 3)))
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
                        if (bossBattle == true)
                        {
                            bossBattle = false;
                            wizardBattle = false;
                            reaperBattle = false;
                            fading = true;
                            
                        }
                        

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
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(storeMusic);
                seconds = user.SheildSeconds;

                MoveingUser(user, userspeed, keyboardState);

                foreach (Npc npc in npcList)
                    npc.Update();
                user.Update(new Vector2(0, 0), barriersList, "not main game", user.Userbox(), "user");
                if (keyboardState.IsKeyUp(Keys.Enter))
                {
                    boost = true;
                    
                }
                foreach (Buttons button in buttonList)
                {

                    if (button.Contains(user.Userbox()))
                    {
                        button.Hovering = "true";
                        if (keyboardState.IsKeyDown(Keys.Enter))
                        {
                            
                            if (boost == true)
                            {
                                button.Bought = true;
                                button.TotalButtonCost += button.Cost;
                                button.Boosts(user, ref Ratfolk, ref difficulty);
                                boost = false;
                            }
                            if (bossBattle)
                                fading = true;
                        }

                        if (button.Type == "Minotaur Battle")
                        {
                            bossBattle = true;
                            reaperBattle = false;
                            wizardBattle = false;
                        }
                        else if (button.Type == "Wizard Battle")
                        {
                            bossBattle = true;
                            wizardBattle = true;
                            reaperBattle = false;
                        }
                        else if (button.Type == "Reaper Battle")
                        {
                            bossBattle = true;
                            reaperBattle = true;
                            wizardBattle = false;
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
                        button.Poor = false;
                        button.TotalButtonCost = 0;
                        button.Bought = false;
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
                foreach (Buttons button in buttonList)
                {
                    button.DrawHome(_spriteBatch, damageFont);
                    if (Instruct == true)
                        button.InstructionsDraw(_spriteBatch, damageFont);
                }
                    
                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
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
                foreach (Coins coins in coinList)
                    coins.Draw(_spriteBatch);

                foreach (Coins grass in grassList)
                    grass.Draw(_spriteBatch);

                foreach (Barriers barrier in barriersList)
                    barrier.Draw(_spriteBatch);

                

          

                //Draw all the bullets
                foreach (LaserClass bullet in laserList)
                    bullet.Draw(_spriteBatch, lightningTexture2);
                user.Draw(_spriteBatch, mouseState.X, guardLocation, staffSoundInsta, hitSoundInsta);
                
                foreach (Player ally in allylist)
                {
                    ally.Draw(_spriteBatch, 100000, new Vector2(0,0), staffSoundInsta, hitSoundInsta);
                   
                }
                foreach (Player ai in enemylist)
                {
                    ai.Draw(_spriteBatch, 100000, new Vector2(0, 0), staffSoundInsta, hitSoundInsta);
                    ai.DrawHealth(_spriteBatch, emptyGreenBarTexture, healthGreenBarTexture, bossBattle, "ai");                   
                    ai.DrawDamage(_spriteBatch, damageFont, backroundSpeed, user.WeaponType);
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

               // _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.Draw(UIHealthTexture, new Rectangle(100, mainGameHeight, 250, 100), Color.White);
                _spriteBatch.Draw(wizardHeadTexture, new Rectangle(115, mainGameHeight + 5, 70, 85), Color.White);
                _spriteBatch.Draw(UIHeartTexture, new Rectangle(735, 910, 30, 30), Color.White);
                user.DrawHealth(_spriteBatch, UIRedEmptyTexture, UIRedTexture, false, "user");
                _spriteBatch.DrawString(dungeonFont, $"{user.Points}", new Vector2(230, mainGameHeight +55), Color.White);             
                _spriteBatch.DrawString(dungeonFont, $"{difficulty-2}", new Vector2(600, 940), Color.White);               
                _spriteBatch.DrawString(dungeonFont, (seconds).ToString("0.0"), new Vector2(1000, 900), Color.CornflowerBlue);


                //user.DrawHealth(_spriteBatch, emptyGreenBarTexture, healthGreenBarTexture, bossBattle, "true");

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
                //Barriers
                foreach (Barriers barrier in barriersList)
                {
                    if (barrier.Blocking == "false")
                        barrier.Draw(_spriteBatch);
                }
                    
                //Structures Far
                _spriteBatch.Draw(hutBlueTexture, new Rectangle(66, 0, 240, 200), Color.White);
                foreach (Npc npc in npcList)
                    npc.Draw(_spriteBatch);
                //Structures Close
                _spriteBatch.Draw(hutTexture, new Rectangle(162, 660, 200, 200), Color.White);
                //Button text
                foreach (Buttons button in buttonList)
                {
                    button.DrawText(_spriteBatch, damageFont);
                    if (button.Bought)
                        button.DrawBuyingItem(user,_spriteBatch, damageFont);
                }            
         
                   

                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
                user.Draw(_spriteBatch, mouseState.X, new Vector2(0, 0), staffSoundInsta, hitSoundInsta);




                //Hud               
               // _spriteBatch.Draw(rectangleTexture, new Rectangle(0, mainGameHeight, _graphics.PreferredBackBufferWidth, 100), Color.Gray);
                _spriteBatch.Draw(UIHealthTexture, new Rectangle(100, mainGameHeight, 250, 100), Color.White);
                _spriteBatch.Draw(wizardHeadTexture, new Rectangle(115, mainGameHeight + 5, 70, 85), Color.White);
                _spriteBatch.Draw(UIHeartTexture, new Rectangle(735, 910, 30, 30), Color.White);
                user.DrawHealth(_spriteBatch, UIRedEmptyTexture, UIRedTexture, false, "user");
                _spriteBatch.DrawString(dungeonFont, $"{user.Points}", new Vector2(230, mainGameHeight + 55), Color.White);
                _spriteBatch.DrawString(dungeonFont, $"{difficulty - 2}", new Vector2(600, 940), Color.White);
                _spriteBatch.DrawString(dungeonFont, (seconds).ToString("0.0"), new Vector2(1000, 900), Color.CornflowerBlue);

                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
            }
           
            else if (screen == Screen.OutroScreen)
            {
                _spriteBatch.Draw(outroBackroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}