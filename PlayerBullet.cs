using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

class PlayerBullet : Sprite
{
    float speed = 5;
    private PlayableArea playableArea;


    public PlayerBullet(float x, float y, PlayableArea playableArea) : base("bullet.png")
    {
        SetXY(x, y);
        this.playableArea = playableArea;
    }

    public void Update()
    {
        // Move the bullet independently (modify as needed)
        x += speed;
/*            * (scaleX > 0 ? 1 : -1);
*/
        // Check if the bullet is off the screen, and remove it if needed
        if (x > playableArea.Width - 20 || x < 0)
        {
            LateRemove();
/*            Console.WriteLine(" remove");
*/        }
    }


    void OnCollision(GameObject other)
    {
        if (other is Enemy)
        {
            LateRemove();
        }
    }
}









