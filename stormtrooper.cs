using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace Final_Project
{
    internal class stormtrooper
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;

        Random rand = new Random();
        public stormtrooper(Texture2D texture, int x, int y)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 64, 124);
            _speed = new Vector2();
        }
    }
}
