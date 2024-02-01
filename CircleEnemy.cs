using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
  
    public class CircleEnemy : Enemy
    {
        int radius;
        int nr;
       
        private EnemySpawner enemySpawner;
        private PlayableArea playableArea;

        private int cEnemyShootTime;
        private int cEnemyShootTimer;

        public CircleEnemy(float x, float y, int enemyHealth, int enemyShootTime, int nr, int radius,PlayableArea playableArea) : base(x, y, enemyHealth, enemyShootTime, playableArea)
        {
            this.nr = nr;
            this.radius = radius;
            this.enemyHealth = enemyHealth;
            this.enemyShootTime = enemyShootTime;
            this.playableArea = playableArea;
        }

        public override void Update()
        {
            //base.Update();
            enemyMove();
            healthUpdate();
            cEnemyShootTimer++;
            if (cEnemyShootTimer == enemyShootTime)
            {
                generateCircleOfBullets(nr, radius);
                cEnemyShootTimer = 0;
                //azzyGenerateSpiralOfBullets(12, 50, timer / shootTime / 14 * Mathf.PI);
            }
            if (x < 0)
            {
                LateRemove();
            }
            else if (y < 0 || y > playableArea.Height)

            {
                LateRemove();
            }
        }


        public override void OnCollision(GameObject other)
        {
           base.OnCollision(other);
        }

        public override void healthUpdate()
        {
            base.healthUpdate();
        }

        public void generateCircleOfBullets(int nr, int radius)
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

        public bool IsInsidePlayableArea(float x, float y)
        {
            float playableLeft = 0;
            float playableRight = playableArea.Width;
            float playableTop = 0;
            float playableBottom = playableArea.Height-50;

            // Check if the position is inside the playable area
            return x >= playableLeft && x <= playableRight &&
                   y >= playableTop && y <= playableBottom;
        }
    }
}
