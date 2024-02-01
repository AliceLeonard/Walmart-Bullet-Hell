using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class TrackingEnemy : Enemy
    {
        private PlayableArea playableArea;

        private int tEnemyShootTime;
        private int tEnemyShootTimer;

        public TrackingEnemy(float x, float y, int enemyHealth, int tEnemyShootTime, PlayableArea playableArea) : base(x, y, enemyHealth, tEnemyShootTime, playableArea)
        {
            this.x = x;
            this.y = y;
            this.enemyHealth = enemyHealth;
            this.tEnemyShootTime = tEnemyShootTime;
            this.playableArea = playableArea;
        }

        public override void Update()
        {
            enemyShoot();
            base.Update();
        }
        public override void OnCollision(GameObject other)
        {
            if (other is PlayerBullet)
            {
                base.OnCollision(other); // Call the base class implementation if needed
            }
        }

        private Player FindPlayer()
        {
            foreach (var child in game.GetChildren())
            {
                if (child is Player)
                {
                    return (Player)child;
                }
            }

            return null; // Return null if player is not found
        }

        public override void enemyShootTimeUpdate()
        {
            tEnemyShootTimer++;            
            if (tEnemyShootTimer == tEnemyShootTime)
            {
                enemyShoot();
                tEnemyShootTimer = 0; // Set a fixed cooldown time
            }
        }

        public override void enemyShoot()
        {
            // Find the player's current position
            Player player = FindPlayer();

            if (tEnemyShootTimer == tEnemyShootTime)
            {
                if (player != null)
                {
                    // Calculate the direction towards the player
                    float directionX = player.x - x;
                    float directionY = player.y - y;
                    float angle = Mathf.Atan2(directionY, directionX);

                    // Spawn bullets in the direction of the player
                    float bulletSpawnX = x;
                    float bulletSpawnY = y;

                    // Create a bullet instance with the adjusted trajectory and add it to the game
                    EnemyBullet bullet = new EnemyBullet(bulletSpawnX, bulletSpawnY, playableArea, angle);
                    game.AddChild(bullet);
                }
                tEnemyShootTimer = 0;
            }
        }
    }
}
