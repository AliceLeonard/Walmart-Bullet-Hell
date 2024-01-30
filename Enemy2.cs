using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Enemy2 : Enemy
        //wave enemy 
    {
        int speed = 3;

        public Enemy2(float x, float y, int enemyHealth, int enemyShootTime, PlayableArea playableArea) : base(x, y, enemyHealth, enemyShootTime, playableArea)
        {
            // SetXY(x, y);  // This line is not needed, as the base constructor already sets the position
            // Set a specific image for WaveEnemy if needed
        }

        public override void Update()
        {
            base.Update();
            // Update logic specific to wave enemy
            // Add any other specific behavior for WaveEnemy
        }

        public override void enemyMove()
        {
            y--;
           /* angle -= 0.1f;
            y = y + Mathf.Sin(angle) * radius;*/
        }

        public override void OnCollision(GameObject other)
        {
            if (other is PlayerBullet)
            {
                // Optionally, provide custom behavior for WaveEnemy collision
                // For example, you may want to have different effects or no health reduction
                base.OnCollision(other); // Call the base class implementation if needed
                //Console.WriteLine("WaveEnemy hit! Custom collision behavior.");
            }
        }
    }
}
