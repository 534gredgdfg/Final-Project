using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Project
{
    
    internal class Animation
    {
        
        private Texture2D _texture;
        
        private SpriteBatch _spriteBatch;

        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _graphicsDevice; // Add a reference to GraphicsDevice
        private int _diffImages;
        private double _animationIndex;
        private double _index;
        private string _draw;
        private List<Texture2D> _textureList;


       
        public Animation(List<Texture2D> textureList)
        {

            
            _textureList = textureList;
           
           


        }
        
       
        public string drawAnimation
        {
            get { return _draw; }
            set { _draw = value; }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rect )
        {


            spriteBatch.Draw(_textureList[(int)Math.Round(_animationIndex)], rect, Color.White);

        }
        public void DrawOneTime(SpriteBatch spriteBatch, Texture2D _texture, Rectangle rect)
        {

            if (_draw == "true")
                spriteBatch.Draw(_textureList[(int)Math.Round(_animationIndex)], rect, Color.White);
            

        }

        
    }
}
