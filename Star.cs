using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Star : Sprite
    {
        public Star(float x, float y) : base("red star.png")
        {
            SetXY(x, y);
            SetScaleXY(0.05f, 0.05f); // Adjust the scale as needed
        }

        public void SetStarColor(byte red, byte green, byte blue)
        {
            // Optionally: Set the color of the star
            SetColor(red, green, blue);
        }
    }


}
