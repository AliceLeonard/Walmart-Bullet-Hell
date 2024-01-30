/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Enemy3 : Enemy
    {
        private int speed = 0;
        private int shootTime = 10;
        private int timer = 0;
        public Enemy3(float x, float y, int enemyHealth, int enemyShootTime, PlayableArea playableArea) : base(x, y, enemyHealth, enemyShootTime, playableArea)
        {
        }

        public override void Update()
        {
            base.Update();
            movementOverride();
            enemyShoot2();
        }

        private void movementOverride()
        {
            x-=speed;
        }

        public void enemyShoot2()
        {
            timer++;
            if (timer % shootTime == 0)
            {
                azzyGenerateSpiralOfBullets(12, 50, timer / shootTime / 14 * Mathf.PI);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is PlayerBullet)
            {
                base.OnCollision(other); // Call the base class implementation if needed
            }
        }
        void azzyGenerateSpiralOfBullets(int nr, float radius, float startingangle)
        {

            float angle = 0;
            float delta_angle = Mathf.PI * 2.0f / (nr + 1);
            for (int i = 0; i <= nr; i++)
            {
                float enemyBulletSpawnX = x + radius * Mathf.Cos(angle + startingangle); // Spawn at the right side of the screen
                float enemyBulletSpawnY = y + radius * Mathf.Sin(angle + startingangle); // Adjust as needed

                EnemyBullet enemybullet = new EnemyBullet(enemyBulletSpawnX, enemyBulletSpawnY, angle + startingangle);
                game.AddChild(enemybullet);
                angle += delta_angle;
            }
        }
    }
}
*/