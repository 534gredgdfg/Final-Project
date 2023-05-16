using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Final_Project
{
    internal class Waves
    {
        private Texture2D _texture;
        private Rectangle _rect;
        private int waveNumber;
        private string newEnemys;
      
        public Waves(Texture2D texture, Rectangle rect)
        {
            _texture = texture;
            _rect = rect;
            waveNumber = 0;
            newEnemys = "false";
        }
        public int Wave
        {
            get { return waveNumber; }
            set { waveNumber = (int)value; }
        }
       
      /*
        public void AddGoblin(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList)
        {
            enemys.Add(new Player(new Rectangle(100, 100, 200, 100), 100, "goblin melee", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList));

        }
        public void AddArcher(List<Player> enemys, List<Texture2D> AiRightList, List<Texture2D> AiMeleeRightList)
        {
            enemys.Add(new Player(new Rectangle(100, 100, 200, 100), 100, "arrow", "slow", AiRightList, AiRightList, AiRightList, AiRightList, AiMeleeRightList));

        }
      */
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, Color.White);
        }

    }
}