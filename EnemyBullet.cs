using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class EnemyBullet : Sprite
    {
        public float speed = 3;
        float angle;
        private PlayableArea playableArea;

        public EnemyBullet(float x, float y, PlayableArea playableArea, float angle = 180) : base("enemybullet.png")
        {
            SetOrigin(width / 2, height / 2); // Set the origin to the center of the bullet
            SetXY(x, y);
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.playableArea = playableArea;
        }

        public virtual void Update()
        {
            x += speed * Mathf.Cos(angle);
            y += speed * Mathf.Sin(angle);

            // Check if the bullet is off the screen, and remove it if needed
            if (x < 0+10 || x > playableArea.Width-10 || y > playableArea.Height-10 || y < 0+10)
            
            {
                LateRemove();
            }
        }


        public virtual void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                LateRemove();
            }
        }
    }
}
