using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Star : Sprite
    {
        public Star(float x, float y, string fileName) : base(fileName)
        {
            SetXY(x, y);
            SetScaleXY(0.05f, 0.05f); // Adjust the scale as needed
        }
    }
}
