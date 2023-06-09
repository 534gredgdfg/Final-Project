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
        private int spent;
        private int _cost;
        private int _totalCost;
        private bool bought;
        private bool poor;
        public Buttons(Texture2D texture, Rectangle rect, Color color, string type, int cost)
        {
       
            _texture = texture;
            _rect = rect;
            _color = color;
            _type = type;
            _cost = cost;
            hovering = "false";
        }
        
        public void Boosts(Player user, ref int crusaders, ref int difficulty)
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
            else if (_type == "Ratfolk (Ally)" && user.Points >= _cost)
            {
                user.Points -= _cost;
                crusaders += 1;
              

            }
            else if (_type == "Increase Difficuly (+$250)" && user.Points >= _cost)
            {
                user.Points += _cost;
                difficulty += 1;
            }
            else
            {
                poor = true;
            }
            spent += _cost;

        }
        
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public int TotalButtonCost
        {
            get { return _totalCost; }
            set { _totalCost = value; }
        }
        public string Hovering
        {
            get { return hovering; }
            set { hovering = value; }
        }
        public bool Bought
        {
            get { return bought; }
            set { bought = value; }
        }
        public bool Poor
        {
            get { return poor; }
            set { poor = value; }
        }


        public void InstructionsDraw(SpriteBatch spriteBatch, SpriteFont Font)
        {
            if (_type == "Instructions")
            {
                spriteBatch.Draw(_texture, _rect, _color);
                spriteBatch.DrawString(Font, "-Play on the infinite map", new Vector2(_rect.X, _rect.Y), Color.Black);
                spriteBatch.DrawString(Font, "-Move using W,A,S & D", new Vector2(_rect.X, _rect.Y + 75), Color.Black);
                spriteBatch.DrawString(Font, "-Left Click to Shoot", new Vector2(_rect.X, _rect.Y + 150), Color.Black);
                spriteBatch.DrawString(Font, "-Press 'X' to Melee", new Vector2(_rect.X, _rect.Y + 225), Color.Black);
                spriteBatch.DrawString(Font, "-Press 'SPACE' for special attack", new Vector2(_rect.X, _rect.Y + 300), Color.Black);
                spriteBatch.DrawString(Font, "which leads into invicability", new Vector2(_rect.X, _rect.Y + 350), Color.Black);
                spriteBatch.DrawString(Font, "-Press 'TAB' to enter Store", new Vector2(_rect.X, _rect.Y + 425), Color.Black);
            }
               

        }

        public bool Contains(Rectangle item)
        {
            return _rect.Contains(item);
        }
        
        public void DrawHome(SpriteBatch spriteBatch, SpriteFont Font)
        {
            if (_type == "Start" || _type == "How to Play")
            {
                spriteBatch.Draw(_texture, _rect, _color);
                if (hovering == "true")
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X, _rect.Y + _rect.Height/2), Color.Gray);
                else
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X, _rect.Y + _rect.Height / 2), Color.Black);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_type != "Start" && _type != "How to Play" && _type != "Instructions")
                spriteBatch.Draw(_texture, _rect, _color);
        }
        public void DrawBuyingItem(Player user, SpriteBatch spriteBatch, SpriteFont Font)
        {
            if (_type == "Increase Difficuly (+$250)" && user.Points >= _cost)
                spriteBatch.DrawString(Font, $"Bought {_type}, +${_totalCost}", new Vector2(450, 425), Color.White);
            else if (_type != "Start" && _type != "How to Play" && _type != "Instructions" && user.Points >= _cost)
                spriteBatch.DrawString(Font, $"Bought {_type}, -${_totalCost}", new Vector2(500, 425), Color.White);
        }
       
        public void DrawText(SpriteBatch spriteBatch, SpriteFont Font)
        {
            if (_type != "Start" && _type != "How to Play" && _type != "Instructions")
            {
                if (hovering == "true" && _cost > 0)
                {
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X- 50, _rect.Y - 50), Color.White);
                }
                else if (_cost == 0)
                {
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X - 50, _rect.Y - 50), Color.White);
                }
                if (_cost > 0)
                    spriteBatch.DrawString(Font, $"${_cost}", new Vector2(_rect.X - 60, _rect.Y), Color.White);
                else
                    spriteBatch.DrawString(Font, "BOSS", new Vector2(_rect.X - 60, _rect.Y), Color.White);
                if (poor)
                {
                    spriteBatch.DrawString(Font, "You need more money for this!", new Vector2(450, 425), Color.White);
                }
            }

        }

    }
}
           