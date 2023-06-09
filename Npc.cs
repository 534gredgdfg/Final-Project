using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Final_Project
{
    internal class Npc
    {
        
        List<Texture2D> _texturesList;
        private Rectangle _rect;
        private SpriteEffects direction;
        private double _updateSpeed;
        private string type;
        


        public Npc(Rectangle rect, List<Texture2D> texturesList, string type)
        {
            _texturesList = texturesList;
            _rect = rect;
            
            
        }
        public void Update()
        {
            _updateSpeed += 0.1;

            if (_updateSpeed >= _texturesList.Count - 0.5)            
                _updateSpeed = 0;                           
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            direction = SpriteEffects.FlipHorizontally;
            if (type == "right")
                spriteBatch.Draw(_texturesList[(int)Math.Round(_updateSpeed)], _rect, Color.White);
            else
                spriteBatch.Draw(_texturesList[(int)Math.Round(_updateSpeed)], _rect, null, Color.White, 0f, new Vector2(0, 0), direction, 0f);
        }
    }
}