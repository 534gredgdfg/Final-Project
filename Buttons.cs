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
        private string _changing;
        private string _changingCon;
        private string hovering;       
      
        private double _cost;
        private double _totalCost;
        private double _boostChange;
        private double _boostChangeCon;
        private bool bought;
        private bool poor;
   

        public Buttons(Texture2D texture, Rectangle rect, Color color, string type, string changing, double boostchange , string changingCon, double boostchangeCon,double cost)
        {
       
            _texture = texture;
            _rect = rect;
            _color = color;
            _type = type;
            _cost = cost;
            hovering = "false";
            _changing = changing;
            _boostChange = boostchange;
            _changingCon = changingCon;
            _boostChangeCon = boostchangeCon;
        }
        
        public void Boosts(Player user, ref int crusaders, ref int difficulty)
        {


            if (_type == "Health Potion" && user.Points >= _cost)
            {
                user.Health += (int)_boostChange;

            }

            else if (_type == "Sheild Recovery Time" && user.Points >= _cost && user.SheildSeconds >= 5)
            {

                user.SheildSeconds += _boostChange;
                user.BoostSpeed += _boostChangeCon;
            }

            else if (_type == "Speed Boost" && user.Points >= _cost)
            {
                user.BoostSpeed += _boostChange;
                user.LowerMaxHealth();

            }
            else if (_type == "Damage Boost" && user.Points >= _cost)
            {
                user.BoostDamage += (int)_boostChange;
                user.BoostSpeed += _boostChangeCon;

            }
            else if (_type == "Ratfolk (Ally)" && user.Points >= _cost)
            {
                crusaders += (int)_boostChange;
                user.SheildSeconds += _boostChangeCon;
            }
            else if (_type == "Wizard Ball Speed" && user.Points >= _cost)
            {
                user.ProjectileSpeedBoost += (float)_boostChange;
                user.BoostDamage += (int)_boostChangeCon;
            }
            else if (_type == "Attack Downtime" && user.Points >= _cost)
            {
                user.GunIntervalBoost += (float)_boostChange;
                user.ProjectileSpeedBoost += (float)_boostChangeCon;
            }
         
           
                

            if (user.Points <= _cost)
            {
                poor = true;
            }
            if (!poor)
            {
                user.Points -= (int)_cost;
                _cost *= 1.10;
            }
                

        }
        
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
      
        public double TotalButtonCost
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
        
        public void DrawHome(SpriteBatch spriteBatch, SpriteFont Font, Texture2D hoverTexture, Texture2D notHoverTexture)
        {
            if (_type == "Start" || _type == "How to Play")
            {
                 

                if (hovering == "true")
                {
                    spriteBatch.Draw(hoverTexture, _rect, Color.Gray);
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X + 60, _rect.Y + _rect.Height / 4), Color.Black);
                }

                else
                {
                    spriteBatch.Draw(notHoverTexture, _rect, Color.White);
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X + 60, _rect.Y + _rect.Height / 4), Color.Gray);
                }
                   
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_type != "Start" && _type != "How to Play" && _type != "Instructions")
                spriteBatch.Draw(_texture, _rect, _color);
        }
        public void DrawBuyingItem(Player user, SpriteBatch spriteBatch, SpriteFont Font)
        {
           
            if (_type != "Start" && _type != "How to Play" && _type != "Instructions" && user.Points >= _cost)
                spriteBatch.DrawString(Font, $"Bought {_type}, -${Math.Round(_totalCost, 2)}", new Vector2(500, 425), Color.White);
        }
       
        public void DrawText(SpriteBatch spriteBatch, SpriteFont Font, Texture2D texture)
        {
            if (_type != "Start" && _type != "How to Play" && _type != "Instructions")
            {
                if (hovering == "true" && _cost > 0)
                {
                    spriteBatch.Draw(texture, new Rectangle(-60, 320, 500, 350), Color.White);
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(50, 375), Color.White);
                    if (_changing != "NONE")
                        spriteBatch.DrawString(Font, $"{_changing} {_boostChange}", new Vector2(10, 425), Color.DarkGreen);
                    if (_changingCon != "NONE")
                        spriteBatch.DrawString(Font, $"{_changingCon} {_boostChangeCon}", new Vector2(10, 475), Color.DarkRed);
                }
                else if (_cost == 0)
                {
                    spriteBatch.DrawString(Font, $"{_type}", new Vector2(_rect.X - 50, _rect.Y - 50), Color.White);
                }
                if (_cost > 0)
                    spriteBatch.DrawString(Font, $"${Math.Round(_cost, 2)}", new Vector2(_rect.X - 60, _rect.Y + 30), Color.White);
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
           