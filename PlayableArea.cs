using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class PlayableArea : GameObject
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private EasyDraw background;

        public PlayableArea(int width, int height, string backgroundImagePath)
        {
            Width = width;
            Height = height;

            // Create an EasyDraw instance for the background
            background = new EasyDraw(width, height);
            AddChild(background);

            // Load and draw the background image
            Bitmap backgroundImage = new Bitmap("Background.png");
            background.DrawSprite(new Sprite(backgroundImage));
        }
    }

}
