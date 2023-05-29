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
        private string hovering;
        private string bought;
        private int spent;
        private int _cost;

        public Buttons(Texture2D texture, Rectangle rect, Color color, string type, int cost)
        {
            _texture = texture;
            _rect = rect;
            _color = color;
            _type = type;
            _cost = cost;
            hovering = "false";
        }
        
        public void Boosts(Player user, ref int crusaders)
        {
            
            if (_type == "Health Potion" && user.Points >= _cost)
            {
                user.Health += 100;
                user.Points -= _cost;
                
            }
                
            else if (_type == "Sheild Recovery Time Decrease" && user.Points >= _cost)
            {
                user.Points -= _cost;
                user.SheildSeconds -= 1;
               
            }
                
            else if (_type == "Speed Boost" && user.Points >= _cost)
            {
                user.Points -= _cost;
                user.BoostSpeed += 0.5;
               

            }
            else if (_type == "Damage Boost" && user.Points >= _cost)
            {
                user.Points -= _cost;
                user.BoostDamage += 4;
              

            }
            else if (_type == "Add Crusader" && user.Points >= _cost)
            {
                user.Points -= _cost;
                crusaders += 1;
              

            }
            spent += _cost;

        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Hovering
        {
            get { return hovering; }
            set { hovering = value; }
        }



        public bool Contains(Rectangle item)
        {
            return _rect.Contains(item);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rect, _color);
        }
        public void DrawText(SpriteBatch spriteBatch, SpriteFont healthFont)
        {
            if (hovering == "true" && _cost >0)
            {
                spriteBatch.DrawString(healthFont, $"{_type}", new Vector2(_rect.X, _rect.Y - 50), Color.White);
            }
            else if (_cost == 0)
            {
                spriteBatch.DrawString(healthFont, $"{_type}", new Vector2(_rect.X, _rect.Y - 50), Color.White);
            }
            if (_cost >0)
                spriteBatch.DrawString(healthFont, $"${_cost}", new Vector2(_rect.X - 60, _rect.Y), Color.White);
            else 
                spriteBatch.DrawString(healthFont, "FREE", new Vector2(_rect.X - 60, _rect.Y), Color.White);

        }

    }
}
           