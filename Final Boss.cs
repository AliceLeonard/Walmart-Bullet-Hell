namespace GXPEngine
{
    using System;

    public class Boss : AnimationSprite
    {
        private int health;
        private PlayableArea playableArea;


        public int bossStage = 1;
        float moveSpeed;

        int bossHealth = 100;
        bool bossMove = true;

        private Random random = new Random();
        bool locationPicking = true;
        float storedTargetX;
        float storedTargetY;
        float moveTimer = 3000;  // Set the initial timer value

        int previousRandomValue = 0;
        int previousRandomXValue = 0;

        int bossShootTime;
        private int timer = 0;
        private int shootTime = 50;
        private int waveTime = 10;

        public Boss(float x, float y, int health, PlayableArea playableArea)
            : base("boss.png", 1, 9)
        {
            SetOrigin(width / 2, height / 2);
            this.x = x;
            this.y = y;
            this.health = health;
            this.playableArea = playableArea;
        }

        public void Update()
        {
            MoveToNextStage();
            PerformBossActions();
            bossHealthUpdate();

            if (bossStage >= 4)
            {
                Environment.Exit(0);
            }
        }

        private void MoveToNextStage()
        {
            if (bossMove == true)
            {
                if (locationPicking == true)
                {
                    storedTargetX = GetTargetXForCurrentStage();
                    storedTargetY = GetTargetYForCurrentStage();
                    locationPicking = false;
                }

                float angle = Mathf.Atan2(storedTargetY - y, storedTargetX - x);
                // moveSpeed = 3;
                x += moveSpeed * Mathf.Cos(angle);
                y += moveSpeed * Mathf.Sin(angle);

                // Check if the boss has reached the target point
                float distanceToTarget = Mathf.Sqrt((x - storedTargetX) * (x - storedTargetX) + (y - storedTargetY) * (y - storedTargetY));

                float threshold = 3; // note - anything below 3 can jitter

                if (distanceToTarget <= threshold)
                {
                    // Set the boss's position to the exact target point to avoid jittering
                    x = storedTargetX;
                    y = storedTargetY;
                    moveSpeed = 0;
                    //bossMove = false;
                    CountdownTimer();

                    // Optionally, you can perform additional actions when the boss reaches the target point
                }
                else
                {
                    moveSpeed = 3;
                }
            }
        }

        private void PerformBossActions()
        {
            switch (bossStage)
            {
                case 1:
                    FireBullets();
                    break;
                case 2:
                    timer++;
                    if (timer % waveTime == 0)
                    {
                        generateCircleOfBullets(12, 50);
                    }
                    break;
                case 3:
                    FireBullets();
                    timer++;
                    if (timer % waveTime == 0)
                    {
                        generateSpiralOfBullets(12, 50);
                    }
                    break;
            }
        }
        private float GetTargetXForCurrentStage()
        {
            switch (bossStage)
            {
                case 1:
                    return playableArea.Width / 2;
                case 2:
                    return playableArea.Width / 5 * 4;
                case 3:
                    int randomXValue;
                    do
                    {
                        randomXValue = random.Next(3, 5); // Change upper bound to 6
                    } while (randomXValue == previousRandomXValue);

                    Console.WriteLine(randomXValue);
                    // Update the previous value for the next call to GetTargetYForCurrentStage
                    previousRandomXValue = randomXValue;

                    return playableArea.Width / 6 * randomXValue;
                default: return playableArea.Width - 100;
            }
        }

        private float GetTargetYForCurrentStage()
        {
            switch (bossStage)
            {
                case 1:
                    return playableArea.Height / 2;
                case 2:
                    int randomValue;
                    do
                    {
                        randomValue = random.Next(1, 5); // Change upper bound to 6
                    } while (randomValue == previousRandomValue);

                    Console.WriteLine(randomValue);
                    // Update the previous value for the next call to GetTargetYForCurrentStage
                    previousRandomValue = randomValue;

                    return playableArea.Height / 5 * randomValue;

                case 3:
                    do
                    {
                        randomValue = random.Next(1, 5); // Change upper bound to 6
                    } while (randomValue == previousRandomValue);

                    Console.WriteLine(randomValue);
                    // Update the previous value for the next call to GetTargetYForCurrentStage
                    previousRandomValue = randomValue;

                    return playableArea.Height / 5 * randomValue;

                default:
                    return playableArea.Height / 2;
            }
        }
        private void FireBullets()
        {
            // Find the player's current position
            Player player = FindPlayer();

            if (bossShootTime > 0)
            {
                bossShootTime--;
            }

            if (bossShootTime == 0)
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
                bossShootTime = 10; // Set a fixed cooldown time
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

        void OnCollision(GameObject other)
        {
            if (other is PlayerBullet)
            {
                bossHealth--;
            }
        }
        void bossHealthUpdate()
        {
            if (bossHealth <= 0)
            {
                bossStage++;
                bossHealth = 100;
                bossMove = true;
            }
        }

        private void CountdownTimer()
        {
            if (locationPicking == false)
            {

                if (moveTimer > 0)
                {
                    moveTimer -= Time.deltaTime;

                    if (moveTimer <= 0)
                    {
                        bossMove = true;
                        locationPicking = true;
                        moveTimer = 3000; //move in seconds/1000
                    }
                }
            }
        }

        void generateCircleOfBullets(int nr, int radius)
        {
            float angle = 0;
            float delta_angle = Mathf.PI * 2.0f / (nr + 1);
            for (int i = 0; i <= nr; i++)
            {
                float enemyBulletSpawnX = x + radius * Mathf.Cos(angle);
                float enemyBulletSpawnY = y + radius * Mathf.Sin(angle);
                if (IsInsidePlayableArea(enemyBulletSpawnX, enemyBulletSpawnY))
                {
                    EnemyBullet enemybullet = new EnemyBullet(enemyBulletSpawnX, enemyBulletSpawnY, playableArea, angle);
                    game.AddChild(enemybullet);
                }
                angle += delta_angle;
            }
        }
        void generateSpiralOfBullets(int nr, float radius)
        {

            float angle = 0;
            float delta_angle = Mathf.PI * 2.0f / (nr + 1);
            float length = radius / nr;

            for (int i = 0; i <= nr; i++)
            {
                float enemyBulletSpawnX = x + length * Mathf.Cos(angle);
                float enemyBulletSpawnY = y + length * Mathf.Sin(angle);

                // Check if the spawn position is inside the playableArea
                if (IsInsidePlayableArea(enemyBulletSpawnX, enemyBulletSpawnY))
                {
                    EnemyBullet enemybullet = new EnemyBullet(enemyBulletSpawnX, enemyBulletSpawnY, playableArea, angle);
                    game.AddChild(enemybullet);
                }
                else
                {
                    //debug
                    //Console.WriteLine("Bullet not spawned. Position outside playable area.");
                }
                angle += delta_angle;
                length += radius / nr;
            }
        }

        bool IsInsidePlayableArea(float x, float y)
        {
            float playableLeft = playableArea.x;
            float playableRight = playableArea.x + playableArea.Width;
            float playableTop = playableArea.y;
            float playableBottom = playableArea.y + playableArea.Height;

            return x >= playableLeft && x <= playableRight &&
                   y >= playableTop && y <= playableBottom;
        }
    }
}