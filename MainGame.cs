using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
                
        private SpriteBatch _spriteBatch;
        Player user;

        Texture2D rectangleTexture, lightningExplodeTexture1, lightningExplodeTexture2, outroBackroundTexture,introBackroundTexture, storeBackroundTexture, hutBlueTexture,hutTexture,wizardCrosshair, healthGreenBarTexture, emptyGreenBarTexture,userSheildWalkTexture, userSheildIdleTexture, lightningTexture1, lightningTexture2, lightningTexture3, arrowTexture, AiArcherWalkingRight, AiArcherMeleeRightTexture, AiWalkingRight, AiMeleeRightTexture, userWalkingRight, userAttackRightTexture, userIdleTexture;
        Texture2D greenTreeTexture,redTreeTexture, brownTreeTexture, rock1Texture, rock2Texture, rock3Texture, grass1Texture, grass2Texture, grass3Texture, logTexture;
        Texture2D hoverTexture, UIStatsTexture ,notHoverTexture,UIHealthTexture, UIRedTexture, UIGrayFullTexture,UIRedEmptyTexture, UIHeartTexture,wizardHeadTexture;
        Vector2 backroundSpeed;
        Rectangle targetedEnemy = new Rectangle(750, 450, 50,50);
        Vector2 spawnPoint,guardLocation, explodLocation;
  
 
        int t = 0;
        int userspeed = 2;
        int Ratfolk = 0;
        int previousHealth = 0;
        int mainGameWidth = 1400;
        int mainGameHeight = 900;
        int enemyType = 0;
        int mainScreenHeight = 1000;
        int difficulty = 1;
        int difficultyChange = 1200;
        int randomNumber;

        int silverCoinGain = 15;
        int goldCoinGain = 25;
        int redCoinGain = 75;
        int greenCoinGain = 125;

        double totalSpent = 0;
        Color dimScreenColor;
        bool RespawnMethold = false;
        bool boost = false;

      
        bool Instruct = false;
        bool fading;
        bool toStore = false;
        bool bossBattle = false;
        bool wizardBattle = false;
        bool reaperBattle = false;
        bool startGame = false;
        bool press = false;
        //Sounds
        Song introMusic, bossMusic, mainMusic, storeMusic;
        SoundEffect buttonSound, staffSound, hitSound, fireSound, buySound, coinSound;
        SoundEffectInstance buttonSoundInsta;

        

        double damgaeMultiplyer = 1;
        float seconds;
        float sheildTime;

        
        private Vector2 playerPosition;
        
        private SpriteFont healthFont, dungeonFont, damageFont, titleFont;
  
    
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

        List<Texture2D> guyArcherRightList;
        List<Texture2D> guyArcherMeleeRightList;
        List<Texture2D> guyArcherHitList;

        List<Texture2D> AiWormRightList;
        List<Texture2D> AiWormMeleeRightList;
        List<Texture2D> AiWormHitList;

        List<Texture2D> BatRightList;
        List<Texture2D> BatMeleeRightList;
        List<Texture2D> BatHitList;

        List<Texture2D> SlayerRightList;
        List<Texture2D> SlayerMeleeRightList;
        List<Texture2D> SlayerHitList;

        List<Texture2D> FantasyRightList;
        List<Texture2D> FantasyMeleeRightList;
        List<Texture2D> FantasyHitList;

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
        List<Texture2D> MerchantList;
        List<Texture2D> shadyList;
        List<Texture2D> minoIdleList;
        List<Texture2D> evilIdleList;

        List<Texture2D> fireBallList;

        List<Texture2D> guardList;

        List<Texture2D> LightningShotList1;
        List<Texture2D> LightningShotList2;

        List<Texture2D> LightningExplodeList1;
        List<Texture2D> LightningExplodeList2;

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

        List<Texture2D> bushGifTextureList;

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
            user = new Player(new Vector2(500, 500),new Vector2( 150, 185), 800, "melee", 1.75, userRightList, userIdleList, userSheildWalkList, userSheildIdleList, userAttackList, userRightList, userSkillSheildList, userSpecialList, guardList, staffSound, hitSound);

            //Add Npc
            npcList.Add(new Npc(new Rectangle(150, 100, 70, 100), herbaleList, "right"));
            npcList.Add(new Npc(new Rectangle(900, 100, 70, 100), postionList, "right"));
            npcList.Add(new Npc(new Rectangle(180, 750, 90,100), blacksmithList, "right"));
            npcList.Add(new Npc(new Rectangle(950, 770, 40, 80), merchantList, "right"));
            npcList.Add(new Npc(new Rectangle(440, 830, 90, 110), shadyList, "right"));
            npcList.Add(new Npc(new Rectangle(620, 810, 110, 130), MerchantList, "right"));

            npcList.Add(new Npc(new Rectangle(1200, 150, 190, 170), minoIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(1200, 400, 230, 150), evilIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(1200, 650, 230, 150), deathRightList, "flip"));

            npcList.Add(new Npc(new Rectangle(450, 135, 100, 50), RatfolkIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(475, 150, 100, 50), RatfolkIdleList, "right"));
            npcList.Add(new Npc(new Rectangle(490, 160, 100, 50), RatfolkIdleList, "flip"));
            npcList.Add(new Npc(new Rectangle(496, 135, 100, 50), RatfolkIdleList, "right"));
            npcList.Add(new Npc(new Rectangle(475, 130, 100, 50), RatfolkIdleList, "right"));

            //Home Screen Buttons
            //Texture,  Rectangle, Color, Type, pro text, pro boost, con text, con boost, price
            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(5, 520, 345, 90), Color.White, "Start", "NONE", 0, "NONE", 0, 0));

            buttonList.Add(new Buttons(rectangleTexture, new Rectangle(5, 645, 545, 90), Color.White, "How to Play", "NONE", 0, "NONE", 0, 0));

            buttonList.Add(new Buttons(UIStatsTexture, new Rectangle(400, 0, 700, 900), Color.White, "Instructions", "NONE", 0, "NONE", 0, 0));

            //Store
            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(100, 150, 170, 170), Color.White, "Health Potion", "Health:+", 100,"NONE", 0, 60));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(850, 150, 170, 170), Color.White, "Sheild Recovery Time", "Sheild Cooldown:",-2, "Speed:", -0.1, 225));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(800, 750, 170, 170), Color.White, "Speed Boost", "Speed:+",0.70, "Max Health:", -40, 500));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(100, 750, 170, 170), Color.White, "Damage Boost", "Damage:+", 7, "Speed:", -0.20, 300));
         
            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(450, 150, 170, 170), Color.White, "Ratfolk (Ally)", "Allies:+", 1, "Sheild Cooldown:+", 0.50, 100));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(400, 750, 170, 170), Color.White, "Wizard Ball Speed", "Ball Speed:+", 1, "Damage:", -1, 275));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(600, 750, 170, 170), Color.White, "Attack Downtime", "Attack Cooldown:", -0.40, "Ball Speed:", -0.10, 325));

            //Bosses
            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(1100, 150, 160, 160), Color.SandyBrown, "Minotaur Battle", "NONE", 0, "NONE", 0, 0));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(1100, 400, 160, 160), Color.DarkRed, "Wizard Battle", "NONE", 0, "NONE", 0, 0));

            buttonList.Add(new Buttons(storeBackroundTexture, new Rectangle(1100, 650, 160, 160), Color.DarkGray, "Reaper Battle", "NONE", 0, "NONE", 0, 0));

            barriersList.Add(new Barriers(grass1Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass2Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass3Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass1Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass2Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass3Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass1Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass2Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
            barriersList.Add(new Barriers(grass3Texture, new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 20, 30), 80, Color.White, "false", "false"));
           
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 90, 45), grassGifTextureList, "grass", 0)); 
                      
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 90, 45), grassGif2TextureList, "grass", 0)); 
                        
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 90, 45), grassGif3TextureList, "grass", 0)); 
                      
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 90, 45), flowerGifTextureList, "grass", 0));
                     
            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 90, 45), flowerGif2TextureList, "grass", 0));

            grassList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 40, 20), bushGifTextureList, "bush", 0)); 

            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth/4 - 50, mainScreenHeight - mainScreenHeight / 4 - 62, 125,150), 145, Color.White, "true", "true"));
            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth /4- 50, mainScreenHeight / 4 - 62, 125, 150), 145, Color.White, "true", "true"));
            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth - mainGameWidth / 4-50, mainScreenHeight - mainScreenHeight / 4 - 62, 125, 150), 145, Color.White, "true", "true"));
            barriersList.Add(new Barriers(redTreeTexture, new Rectangle(mainGameWidth - mainGameWidth / 4-50, mainScreenHeight / 4 - 62, 125, 150), 145, Color.White, "true", "true"));

            coinList.Add(new Coins( new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 28, 35),silverCoinTextureList, "silver", silverCoinGain)); 
            coinList.Add(new Coins(new Rectangle(rand.Next(0, mainGameWidth), rand.Next(0, mainScreenHeight), 28, 35),goldCoinTextureList, "gold", goldCoinGain)); 
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
          
            hitSound = Content.Load<SoundEffect>("Effect_Hit");

             fireSound = Content.Load<SoundEffect>("Effect_Hit");

            coinSound = Content.Load<SoundEffect>("Effect_Collect_Coin");

            buySound = Content.Load<SoundEffect>("Effect_Buy_Item");
            


            rectangleTexture = Content.Load<Texture2D>("rectangle");
            outroBackroundTexture = Content.Load<Texture2D>("Art_Screen");
            introBackroundTexture = Content.Load<Texture2D>("Art_Intro");
            storeBackroundTexture = Content.Load<Texture2D>("Store_Backround");
            //Hud
            emptyGreenBarTexture = Content.Load<Texture2D>("Health_Empty");
            healthGreenBarTexture = Content.Load<Texture2D>("Health_Full");

            hoverTexture = Content.Load<Texture2D>("UI_Hover");
            notHoverTexture = Content.Load<Texture2D>("UI_Black");

            UIHealthTexture = Content.Load<Texture2D>("UI_Health");          

            UIRedTexture = Content.Load<Texture2D>("UI_Red_Full");
            UIRedEmptyTexture = Content.Load<Texture2D>("UI_Red_Empty");
            UIGrayFullTexture = Content.Load<Texture2D>("UI_Gray_Full");
            UIHeartTexture = Content.Load<Texture2D>("UI_Heart");

            UIStatsTexture = Content.Load<Texture2D>("UI_Stats");

            wizardHeadTexture = Content.Load<Texture2D>("Wizard_Head");

            
            damageFont = Content.Load<SpriteFont>("DamageText");
            dungeonFont = Content.Load<SpriteFont>("DungonFont");
            titleFont = Content.Load<SpriteFont>("TitleFont");

            wizardCrosshair = Content.Load<Texture2D>("WizardCrosshair");
            //Enviorment
             greenTreeTexture = Content.Load<Texture2D>("Green_Tree");
             redTreeTexture = Content.Load<Texture2D>("Red_Tree");
             brownTreeTexture = Content.Load<Texture2D>("Brown_tree");

             rock1Texture = Content.Load<Texture2D>("Rock1");
             rock2Texture = Content.Load<Texture2D>("Rock2");
             rock3Texture = Content.Load<Texture2D>("Rock3");

             grass1Texture = Content.Load<Texture2D>("Grass1");
             grass2Texture = Content.Load<Texture2D>("Grass2");
             grass3Texture = Content.Load<Texture2D>("Grass3");

        

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

            Texture2D guyArcherWalkingRight = Content.Load<Texture2D>("Guy_Archer_Move");
            Texture2D guyArcherMeleeRightTexture = Content.Load<Texture2D>("Guy_Archer_Attack");
            Texture2D guyArcherHitTexture = Content.Load<Texture2D>("Guy_Archer_Hit");

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

            Texture2D FantasyWalkingRight = Content.Load<Texture2D>("Fantasy_Move");
            Texture2D FantasyMeleeRightTexture = Content.Load<Texture2D>("Fantasy_Attack");
            Texture2D FantasyHitTexture = Content.Load<Texture2D>("Fantasy_Take_Hit");
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
            Texture2D MerchantTexture = Content.Load<Texture2D>("NPC_Merchant");
            Texture2D shadyTexture = Content.Load<Texture2D>("NPC_Shady_Guy");

            Texture2D minoIdleTexture = Content.Load<Texture2D>("Minotaur_Idle");
            Texture2D evilIdleTexture = Content.Load<Texture2D>("Evil_Idle");

            hutTexture = Content.Load<Texture2D>("Decorations_Hut");
            hutBlueTexture = Content.Load<Texture2D>("Decorations_Bluehut");
            //Projectiles
            Texture2D fireBallTexture = Content.Load<Texture2D>("Move_Fire_Ball");
            Texture2D guardTexture = Content.Load<Texture2D>("guard");
            lightningTexture1 = Content.Load<Texture2D>("lightning_Blue");
            lightningTexture2 = Content.Load<Texture2D>("lightning_Yellow");
            lightningExplodeTexture1 = Content.Load<Texture2D>("lightning3_blue");
            lightningExplodeTexture2 = Content.Load<Texture2D>("lightning3");


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

            Texture2D bushGifTexture = Content.Load<Texture2D>("Animated_Bush");

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

            ReapetingAnimation(GraphicsDevice, guyArcherWalkingRight, guyArcherRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, guyArcherMeleeRightTexture, guyArcherMeleeRightList = new List<Texture2D>(), 7);
            ReapetingAnimation(GraphicsDevice, guyArcherHitTexture, guyArcherHitList = new List<Texture2D>(), 4);

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

            ReapetingAnimation(GraphicsDevice, FantasyWalkingRight, FantasyRightList = new List<Texture2D>(), 8);
            ReapetingAnimation(GraphicsDevice, FantasyMeleeRightTexture, FantasyMeleeRightList = new List<Texture2D>(), 7);
            ReapetingAnimation(GraphicsDevice, FantasyHitTexture, FantasyHitList = new List<Texture2D>(), 3);
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
            ReapetingAnimation(GraphicsDevice, MerchantTexture, MerchantList = new List<Texture2D>(), 5);
            ReapetingAnimation(GraphicsDevice, shadyTexture, shadyList = new List<Texture2D>(), 5);
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

            ReapetingAnimation(GraphicsDevice, bushGifTexture, bushGifTextureList = new List<Texture2D>(), 6);

            ReapetingAnimation(GraphicsDevice, lightningTexture1, LightningShotList1 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningTexture2, LightningShotList2 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningExplodeTexture1, LightningExplodeList1 = new List<Texture2D>(), 4);
            ReapetingAnimation(GraphicsDevice, lightningExplodeTexture2, LightningExplodeList2 = new List<Texture2D>(), 4);


            ReapetingAnimation(GraphicsDevice, arrowTexture, ArrowShotList = new List<Texture2D>(), 4);

            ReapetingAnimation(GraphicsDevice, deathTexture, DeathShotList = new List<Texture2D>(), 4);
        }
        //-----------------------------------------------------------------------Update--------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            static void AddBaseEnemy(List<Player> enemys, int enemy, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, Vector2 spawnPoint, SoundEffect attackSound, SoundEffect hitSound)
            {
                switch (enemy) 
                {
                    case 1:
                        enemys.Add(new Player(spawnPoint, new Vector2(200, 100), 100, "goblin melee", 1.5, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 2:
                        enemys.Add(new Player(spawnPoint, new Vector2(180, 90), 80, "fast goblin melee", 2.6, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 3:
                        enemys.Add(new Player(spawnPoint, new Vector2(250, 125), 145, "skel melee", 0.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 4:
                        enemys.Add(new Player(spawnPoint, new Vector2(190, 95), 90, "arrow", 1.7, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 5:
                        enemys.Add(new Player(spawnPoint, new Vector2(210, 110), 115, "guy arrow", 0.9, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 6:
                        enemys.Add(new Player(spawnPoint, new Vector2(220, 100), 175, "fire ball", 1.3, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 7:
                        enemys.Add(new Player(spawnPoint, new Vector2(170, 90), 75, "bat", 2.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 8:
                        enemys.Add(new Player(spawnPoint, new Vector2(170, 90), 115, "slayer", 1.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                    case 9:
                        enemys.Add(new Player(spawnPoint, new Vector2(180, 110), 105, "fantasy", 2.2, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
                        break;
                } 
            }
            
            static void AddMinotaur(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, SoundEffect attackSound, SoundEffect hitSound)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(190, 170), 550, "minotaur", 2.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
            }
            static void AddWizard(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList, SoundEffect attackSound, SoundEffect hitSound)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(230, 150), 300, "wizard", 3.8, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
            }
            static void AddBringerOfDeath(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList , List<Texture2D> AiSpellRightList, SoundEffect attackSound, SoundEffect hitSound)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(230, 150), 800, "death", 1.2, AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList, AiRightList, AiRightList, AiSpellRightList, AiRightList, attackSound, hitSound));
            }
            static void AddAlly(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList, List<Texture2D> AiHitList,  List<Texture2D> AiIdleList, SoundEffect attackSound, SoundEffect hitSound)
            {
                Random rand = new Random();
                enemys.Add(new Player(new Vector2(rand.Next(0, 1000), rand.Next(0, 300)), new Vector2(100, 50), 120, "ally melee", 1.2 , AiRightList, AiIdleList, AiRightList, AiRightList, AiMeleeRightList, AiHitList, AiRightList, AiRightList, AiRightList, attackSound, hitSound));
            }
            static void MakeSpwanPoints(int mainGameWidth, int mainScreenHeight, ref Vector2 spawnPoint)
            {
                Random rand = new Random();
                switch (rand.Next(1, 5))
                {
                    case 1:
                        //Left
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, -150), rand.Next(-mainScreenHeight, mainScreenHeight * 3)); break;
                    case 2:
                        //Top
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, mainGameWidth * 3), rand.Next(-mainScreenHeight, -150)); break;
                    case 3:
                        //Right
                        spawnPoint = new Vector2(rand.Next(mainGameWidth + 150, mainGameWidth * 2), rand.Next(-mainScreenHeight, mainScreenHeight * 3)); break;
                    case 4:
                        //Bottom
                        spawnPoint = new Vector2(rand.Next(-mainGameWidth, mainGameWidth * 3), rand.Next(mainScreenHeight + 150, mainScreenHeight * 2)); break;
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
                            if (mouseState.LeftButton == ButtonState.Pressed && press == false)
                            {
                                press = true;
                                if (buttonSoundInsta.State == SoundState.Stopped)
                                    buttonSoundInsta.Play();
                                if (!Instruct)
                                    Instruct = true;
                                else
                                    Instruct = false;
                            }  
                            else if (mouseState.LeftButton == ButtonState.Released)
                                press = false;

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
                else if (enemylist.Count == 0)
                {                   
                        MediaPlayer.Stop();
                }
                    
                else
                {
                    if (MediaPlayer.State == MediaState.Stopped)
                        MediaPlayer.Play(mainMusic);                    
                }
                

                //Increase Difficulty
                if (user.TotalPoints >= difficultyChange)
                {
                    difficulty += 1;
                    difficultyChange += 1200;
                }
                foreach (Player troops in enemylist)
                {
                    if (troops.GetBoundingBox().Intersects(new Rectangle(0, 0, mainGameWidth, mainGameHeight)))
                        toStore = false;
                    else
                        toStore = true;
                }
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
                    coinList.Clear();
                    if (keyboardState.IsKeyDown(Keys.P) && enemylist.Count == 0)
                    {
                      
                        RespawnMethold = false;
                        if (wizardBattle == true)
                            AddWizard(enemylist, wizardRightList, wizardMeleeRightList, wizardHitList,  staffSound,  hitSound);
                        else if (reaperBattle == true)
                            AddBringerOfDeath(enemylist, deathRightList, deathMeleeRightList, deathSpellList, staffSound, hitSound);
                        else
                            AddMinotaur(enemylist, minoRightList, minoMeleeRightList, minoHitList, staffSound, hitSound);
                    }    
                }
                else
                {
                               
                    for (int i = 0; enemylist.Count <= difficulty + 2; i++)
                    {
                        enemyType = rand.Next(1, 10);
                        MakeSpwanPoints(mainGameWidth, mainScreenHeight, ref spawnPoint);
                        switch (enemyType)
                        {
                            case 1:
                                AddBaseEnemy(enemylist, enemyType, AiRightList, AiMeleeRightList, AiGoblinHitList, spawnPoint, staffSound, hitSound);
                                break;
                            case 2:
                                AddBaseEnemy(enemylist, enemyType, AiRightList, AiMeleeRightList, AiGoblinHitList, spawnPoint, staffSound, hitSound);
                                break;
                            case 3:
                                AddBaseEnemy(enemylist, enemyType, AiSkelRightList, AiSkelMeleeRightList, AiSkelHitList, spawnPoint, staffSound, hitSound);
                                break;
                            case 4:
                                AddBaseEnemy(enemylist, enemyType, AiArcherRightList, AiArcherMeleeRightList, AiArcherHitList, spawnPoint, staffSound, hitSound);
                                break;
                            case 5:
                                AddBaseEnemy(enemylist, enemyType, guyArcherRightList, guyArcherMeleeRightList, guyArcherHitList, spawnPoint, staffSound, hitSound);
                                break;                            
                            case 6:
                                AddBaseEnemy(enemylist, enemyType, AiWormRightList, AiWormMeleeRightList, AiWormHitList, spawnPoint, fireSound,hitSound);
                                break;                                                     
                            case 7:
                                AddBaseEnemy(enemylist, enemyType, BatRightList, BatMeleeRightList, BatHitList, spawnPoint, staffSound, hitSound);
                                break;
                            case 8:
                                AddBaseEnemy(enemylist, enemyType, SlayerRightList, SlayerMeleeRightList, SlayerHitList, spawnPoint, staffSound, hitSound);
                                break;
                            case 9:
                                AddBaseEnemy(enemylist, enemyType, FantasyRightList, FantasyMeleeRightList, FantasyHitList, spawnPoint, staffSound, hitSound);
                                break;
                        }
                    }
                }
                while (t < Ratfolk)
                {
                    AddAlly(allylist, RatfolkRightList, RatfolkMeleeRightList, RatfolkHitList, RatfolkIdleList, staffSound, hitSound);
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
                    seconds = (float)user.SheildSeconds;
                backroundSpeed.X = 0;
                backroundSpeed.Y = 0;
                for (int i = 0; barriersList.Count <= 150; i++)
                {
                    MakeSpwanPoints(mainGameWidth, mainScreenHeight, ref spawnPoint);
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
                    MakeSpwanPoints(mainGameWidth, mainScreenHeight, ref spawnPoint);
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
                for (int i = 0; grassList.Count <= 100; i++)
                {
                    MakeSpwanPoints(mainGameWidth, mainScreenHeight, ref spawnPoint);
                    switch (rand.Next(1,7))
                    {
                        case 1:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90,45 ), grassGifTextureList, "grass", 0)); break;
                        case 2:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), grassGif2TextureList, "grass", 0)); break;
                        case 3:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), grassGif3TextureList, "grass", 0)); break;
                        case 4:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), flowerGifTextureList, "grass", 0)); break;
                        case 5:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 90, 45), flowerGif2TextureList, "grass", 0)); break;
                        case 6:
                            grassList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 40, 20), bushGifTextureList, "bush", 0)); break;

                    }
                }
                if (!bossBattle)
                {
                    for (int i = 0; coinList.Count <= 23; i++)
                    {
                        
                        MakeSpwanPoints(mainGameWidth, mainScreenHeight, ref spawnPoint);
                        randomNumber = (rand.Next(0, 101));
                        if (randomNumber <= 50)
                            coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), silverCoinTextureList, "silver", silverCoinGain));
                        else if (randomNumber > 50 && randomNumber <= 75)
                            coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), goldCoinTextureList, "gold", goldCoinGain));
                        else if (randomNumber > 75 && randomNumber <= 92)
                            coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), redCoinTextureList, "red", redCoinGain));
                        else if (randomNumber > 92 && randomNumber <= 100)
                            coinList.Add(new Coins(new Rectangle((int)spawnPoint.X, (int)spawnPoint.Y, 28, 35), greenGemTextureList, "green", greenCoinGain));
                    }
                }
                for (int i = coinList.Count - 1; i >= 0; i--)
                {
                    Coins coins = coinList[i];
                    if (user.Userbox().Intersects(coins.GetBoundingBox()))
                    {
                        if (coins.CoinType != "grass")
                        {
                            coinSound.Play();
                            user.Points += coins.CoinPoints;
                            user.TotalPoints += coins.CoinPoints;  
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

                user.Timer.Start();
                user.Elapsed = user.Timer.Elapsed;
                //User Shots

                if (keyboardState.IsKeyDown(Keys.X))
                {
                    user.WeaponType = "melee";
                    explodLocation = new Vector2(-100, -100);
                    user.UserAttackMelee(enemylist, barriersList, null, null, mouseState.X, mouseState.Y, null, null, null, null, null, null, null);

                }

                else if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    user.WeaponType = "wizard ball";
                    user.UserAttackMelee(enemylist, barriersList, laserList, LightningShotList2, mouseState.X, mouseState.Y, shotTexture, LightningShotList1, fireList2, iceList1, iceList2, thunderList1, thunderList2);
                }
                else if (keyboardState.IsKeyDown(Keys.V))
                {
                    explodLocation = new Vector2(-100, -100);
                    user.WeaponType = "special";
                    user.UserAttackMelee(enemylist, barriersList, laserList, shotTexture, mouseState.X, mouseState.Y, shotTexture, fireList1, fireList2, iceList1, iceList2, thunderList1, thunderList2);
                }
                //Ai Shots

                foreach (Player troops in enemylist)
                {
                    //Enemy Melee user
                    if (troops.EnemyType == "melee" )
                    {
                        troops.DrawEnemyAttack(user,barriersList, allylist);
                        troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, playerPosition, fireBallList, ArrowShotList, DeathShotList);
                    }
                    //Enemy Shoot

                    else if (troops.EnemyType == "shooter" || troops.EnemyType == "both")
                    {
                        troops.DrawEnemyAttack(null, barriersList, allylist);                
                        troops.EnemyAttackMelee(user, barriersList, allylist, enemyLaserList, playerPosition, fireBallList, ArrowShotList, DeathShotList);
                    }
                }
                //Update User Laser
                foreach (LaserClass bullet in laserList)
                    bullet.Update(gameTime, backroundSpeed, user.ProjectileSpeed);

                //Update Enemy Laser             
                foreach (LaserClass bullet in enemyLaserList)
                    bullet.Update(gameTime, backroundSpeed, difficulty + 4);
                    
                
               
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
                            explodLocation = new Vector2(laser.GetBoundingBox().X - 30, laser.GetBoundingBox().Y - 30);
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
                        guardLocation = new Vector2(bullet.GetBoundingBox().X -20, bullet.GetBoundingBox().Y - 20);
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
                            explodLocation = new Vector2(t.GetBoundingBox().X - 30, t.GetBoundingBox().Y - 30);
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
                        if (bossBattle == true)
                        {
                            
                            bossBattle = false;
                            wizardBattle = false;
                            reaperBattle = false;
                                                    
                        }
                        for (int j = 0; j <= rand.Next(troops.PointsOnKill-2, troops.PointsOnKill); j++)
                        {
                            int randomLocationX = rand.Next(-55, 55);
                            int randomLocationY = rand.Next(-55, 55);
                            if (difficulty < 5)
                                coinList.Add(new Coins(new Rectangle(troops.Hitbox().X + randomLocationX, troops.Hitbox().Y + randomLocationY, 28, 35), silverCoinTextureList, "silver", silverCoinGain));
                            else
                                coinList.Add(new Coins(new Rectangle(troops.Hitbox().X + randomLocationX, troops.Hitbox().Y + randomLocationY, 28, 35), goldCoinTextureList, "gold", goldCoinGain));
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
                seconds = (float)user.SheildSeconds;

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
                                
                                if (button.Cost <= user.Points)
                                {
                                    
                                    totalSpent += button.Cost;
                                    button.TotalButtonCost += button.Cost;
                                    button.Bought = true;
                                    button.Boosts(user, ref Ratfolk, ref difficulty);
                                    buySound.Play();
                                    boost = false;
                                }
                                
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
                if (user.Health <= 0)
                    screen = Screen.OutroScreen;
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
                _spriteBatch.DrawString(titleFont, "Wizard Game", new Vector2(150, 700), Color.White);
                foreach (Buttons button in buttonList)
                {        
                    button.DrawHome(_spriteBatch, damageFont, hoverTexture, notHoverTexture);
                    if (Instruct == true)
                        button.InstructionsDraw(_spriteBatch,dungeonFont);
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
                    _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.DarkRed);
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
                user.Draw(_spriteBatch, mouseState.X, guardLocation, "user", LightningExplodeList1, backroundSpeed);
                
                foreach (Player ally in allylist)              
                    ally.Draw(_spriteBatch, 100000, new Vector2(0,0), "ally", LightningExplodeList1, backroundSpeed);
                   
                
                foreach (Player ai in enemylist)
                {
                    ai.Draw(_spriteBatch, 100000, explodLocation, "ai", LightningExplodeList2, backroundSpeed);
                    ai.DrawHealth(_spriteBatch, emptyGreenBarTexture, healthGreenBarTexture, UIGrayFullTexture, bossBattle, "ai", dungeonFont);                   
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
                if (keyboardState.IsKeyDown(Keys.Tab))
                {
                    if(toStore == false)
                        _spriteBatch.DrawString(dungeonFont, "To Many Monsters Near", new Vector2(500, 500), Color.White);
                }
                if (bossBattle == true && enemylist.Count == 0)
                    _spriteBatch.DrawString(dungeonFont, "Press 'P' to Start", new Vector2(550, 425), Color.White);
                _spriteBatch.Draw(UIHealthTexture, new Rectangle(100, mainGameHeight, 250, 100), Color.White);
                _spriteBatch.Draw(wizardHeadTexture, new Rectangle(115, mainGameHeight + 5, 70, 85), Color.White);
                _spriteBatch.Draw(UIHeartTexture, new Rectangle((int)user.MaxHealth/2 + 235, 910, 30, 30), Color.White);;
                user.DrawHealth(_spriteBatch, UIRedEmptyTexture, UIRedTexture,UIGrayFullTexture, false, "user", dungeonFont);
                _spriteBatch.DrawString(dungeonFont, $"{user.Points}", new Vector2(230, mainGameHeight +55), Color.White);
                _spriteBatch.DrawString(dungeonFont, $"Difficulty Level: {difficulty}", new Vector2(760, 950), Color.White);
              //  _spriteBatch.DrawString(dungeonFont, $" {user.GunInterval}", new Vector2(500, 500), Color.Red);

                _spriteBatch.DrawString(dungeonFont,$"SPACEBAR Ability {(seconds).ToString("0.0")}" , new Vector2(370, 950), Color.CornflowerBlue);


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
                    button.DrawText(_spriteBatch, damageFont, UIStatsTexture);
                    if (button.Bought)
                        button.DrawBuyingItem(user,_spriteBatch, damageFont);
                }            

                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
                user.Draw(_spriteBatch, mouseState.X, new Vector2(0, 0), "user", LightningExplodeList1, backroundSpeed);
                //Hud               
               
                _spriteBatch.Draw(UIHealthTexture, new Rectangle(100, mainGameHeight, 250, 100), Color.White);
                _spriteBatch.Draw(wizardHeadTexture, new Rectangle(115, mainGameHeight + 5, 70, 85), Color.White);
                _spriteBatch.Draw(UIHeartTexture, new Rectangle((int)user.MaxHealth/2 +235, 910, 30, 30), Color.White); ;
                user.DrawHealth(_spriteBatch, UIRedEmptyTexture, UIRedTexture, UIGrayFullTexture, false, "user", dungeonFont);
                _spriteBatch.DrawString(dungeonFont, $"{user.Points}", new Vector2(230, mainGameHeight + 55), Color.White);
                _spriteBatch.DrawString(dungeonFont, $"Difficulty Level: {difficulty}", new Vector2(760, 950), Color.White);
               // _spriteBatch.DrawString(dungeonFont, $" {user.GunInterval}", new Vector2(500, 500), Color.Red);
                _spriteBatch.DrawString(dungeonFont, $"SPACEBAR Ability {(seconds).ToString("0.0")}", new Vector2(370, 950), Color.CornflowerBlue);

                // - screen fade
                _spriteBatch.Draw(rectangleTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), dimScreenColor);
            }
           
            else if (screen == Screen.OutroScreen)
            {
                _spriteBatch.Draw(outroBackroundTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(wizardCrosshair, new Rectangle(mouseState.X - 18, mouseState.Y, 50, 50), Color.White);
                _spriteBatch.DrawString(damageFont, $"Total points spent ${Math.Round(totalSpent,2)}", new Vector2(600, 400), Color.White);
                _spriteBatch.DrawString(damageFont, $"Total points gained ${Math.Round(user.TotalPoints,2)}", new Vector2(600, 500), Color.White);


            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}