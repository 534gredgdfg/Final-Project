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


       
        public Animation(GraphicsDevice graphicsDevice, int diffImages, double index,Texture2D texture, List<Texture2D> textureList)
        {
            _texture = texture;
            _diffImages = diffImages;
            _index = index;
            _textureList = textureList;
            _graphicsDevice = graphicsDevice;
            _draw = "true";


        }
         public void ReapetingAnimation(GraphicsDevice graphicsDevice)
        {

            _textureList = new List<Texture2D>();
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
        public void OneTimeAnimation(GraphicsDevice graphicsDevice)
        {
            _textureList = new List<Texture2D>();
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
        public void ChangeImage()
        {
            _animationIndex += _index;
            if (_animationIndex >= _textureList.Count - 0.5)
            {
                _draw = "false";
                _animationIndex = 0;
            }
            




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
        public void DrawOneTime(SpriteBatch spriteBatch, Rectangle rect)
        {

            if (_draw == "true")
                spriteBatch.Draw(_textureList[(int)Math.Round(_animationIndex)], rect, Color.White);
            

        }





    }
}
