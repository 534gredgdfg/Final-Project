using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Final_Project
{
    internal class Coins
    {
     
        private Rectangle _rect;
        private string _type;
        private double _coinSpeed;
        List<Texture2D> _coinTextureList;
        public Coins(Rectangle rect, List<Texture2D> coinTextureList, string type)
        { 
            _rect = rect;        
            _type = type;
            _coinTextureList = coinTextureList;
        }
        public string CoinType
        {
            get { return _type; }
            set { _type = value; }
        }

        public Rectangle Rect()
        {
            return new Rectangle(_rect.X, _rect.Y, _rect.Width, _rect.Height);
        }
        private void Move(Vector2 backSpeed)
        {
            _rect.X +=  (int)backSpeed.X;
            _rect.Y += (int)backSpeed.Y;
        }
       
        public void Update(Vector2 backSpeed)
        {
            Move(backSpeed);
            _coinSpeed += 0.11;
            if (_coinSpeed >= _coinTextureList.Count - 0.5)
                _coinSpeed = 0;
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle(_rect.X, _rect.Y, _rect.Width, _rect.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_coinTextureList[(int)Math.Round(_coinSpeed)], _rect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
