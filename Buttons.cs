using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GXPEngine.MouseHandler;

namespace GXPEngine
{
    // Button class
    public class Button : EasyDraw
    {
        public event Action OnButtonClick;

        public Button(string text, int width, int height) : base(width, height)
        {
            Fill(100, 100, 100); // Button color
            Stroke(0); // Border color
            Rect(0, 0, width, height);

            // Draw text in the center of the button
            Fill(255, 255, 255); // Text color
            TextAlign(CenterMode.Center, CenterMode.Center);
            TextFont(new Font("Arial", 18, FontStyle.Regular));
            Text(text, width / 2, height / 2);
        }

        public void Update()
        {
            // Check for mouse click inside the button
            if (Input.GetMouseButtonDown(0) && HitTestPoint(Input.mouseX, Input.mouseY))
            {
                OnButtonClick?.Invoke();
            }
        }
    }

}