using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Enemy3 : Enemy
    {
        private PlayableArea playableArea;

        private int speed = 0;
        private int shootTime = 100;
        private int timer = 0;
        public Enemy3(float x, float y, int enemyHealth, int enemyShootTime, PlayableArea playableArea) : base(x, y, enemyHealth, enemyShootTime, playableArea)
        {
            this.x = x;
            this.y = y;
            this.enemyHealth = enemyHealth;
            this.enemyShootTime = enemyShootTime;
            this.playableArea = playableArea;
        }

        public override void Update()
        {
            base.Update();
            enemyShoot3();
        }

        public override void enemyMove()
        {
            y++;
            /* angle -= 0.1f;
             y = y + Mathf.Sin(angle) * radius;*/
        }

        public void enemyShoot3()
        {
            timer++;
            if (timer % shootTime == 0)
            {
                generateCircleOfBullets(12,50);

                //azzyGenerateSpiralOfBullets(12, 50, timer / shootTime / 14 * Mathf.PI);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is PlayerBullet)
            {
                base.OnCollision(other); // Call the base class implementation if needed
            }
        }



        void generateCircleOfBullets(int nr, int radius)
        {
            float angle = 0;
            float delta_angle = Mathf.PI * 2.0f / (nr + 1);
            for (int i = 0; i <= nr; i++)
            {
                float enemyBulletSpawnX = x + radius * Mathf.Cos(angle); // Spawn at the right side of the screen
                float enemyBulletSpawnY = y + radius * Mathf.Sin(angle); // Adjust as needed
                if (IsInsidePlayableArea(enemyBulletSpawnX, enemyBulletSpawnY))
                {
                    EnemyBullet enemybullet = new EnemyBullet(enemyBulletSpawnX, enemyBulletSpawnY, playableArea, angle);
                    game.AddChild(enemybullet);
                }
                angle += delta_angle;
            }
        }

        bool IsInsidePlayableArea(float x, float y)
        {
            // Replace these values with the actual boundaries of the playable area
            float playableLeft = playableArea.x;
            float playableRight = playableArea.x + playableArea.Width;
            float playableTop = playableArea.y;
            float playableBottom = playableArea.y + playableArea.Height;

            // Check if the position is inside the playable area
            return x >= playableLeft && x <= playableRight &&
                   y >= playableTop && y <= playableBottom;
        }

        /*    void azzyGenerateSpiralOfBullets(int nr, float radius, float startingangle)
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
            }*/
    }
}
