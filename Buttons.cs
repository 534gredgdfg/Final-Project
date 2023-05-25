using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace Final_Project
{
    internal class Buttons
    {
        private Texture2D _texture;

        private Rectangle _rect;

        private Color _color;
        private string _type;
      
    
        public Buttons(Texture2D texture, Rectangle rect, Color color, string type)
        {
            _texture = texture;
            _rect = rect;
            _color = color;
            _type = type;
        }
        
        public void Boosts(Player user, int userDamage)
        {
           
            if (_type == "health" && user.Points >= 200)
            {
                user.Health += 100;
                user.Points -= 200;
            }
                
            else if (_type == "sheild time" && user.Points >= 150)
            {
                user.Points -= 150;
                user.SheildSeconds -= 1;
            }
                
            else if (_type == "speed boost" && user.Points >= 600)
            {
                user.Points -= 600;
                user.BoostSpeed += 1;
               
            }
            else if (user.Points >= 300)
            {
                user.Points -= 300;
                user.BoostDamage += 4;
                
            }
                
        }
       
    
       
        public bool Contains(Rectangle item)
        {
            return _rect.Intersects(item);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, _color);
        }

    }
}
           