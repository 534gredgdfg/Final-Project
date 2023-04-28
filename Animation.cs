using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project
{
    
    internal class Animation
    {
        private Texture2D _texture;
        private Rectangle _location;
        
        private int height;
        private int width;
        public Animation(Texture2D texture, int x, int y, int width, int height)
        {
            _texture = texture;
            _location = new Rectangle(x, y, width, height);
            


        }
        public float XLocation
        {
            get { return _location.X; }
            set { _location.X = (int)value; }
        }
        public float YLocation
        {
            get { return _location.Y; }
            set { _location.Y = (int)value; }
        }
        
        public float Width
        {
            get { return width; }
            set { width = (int)value; }
        }
        public float Height
        {
            get { return height; }
            set { height = (int)value; }
        }
    }
}
