using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class EnemySpawner : GameObject
    {
        bool bossSpawned = false;
        private GameObject enemyParent;
        private PlayableArea playableArea;
        private Enemy enemy;


        private int spawnTimer = 80;
        private int spawnTime;
        private Random random = new Random();
        private int currentStage = 1;
        private bool bossStage = false;
        private float elapsedTime;
        private float interval = 30 * 1000;

        private EnemyConfig[] enemyConfigs; // Array to store configurations for different enemy types


        public EnemySpawner(GameObject enemyParent, PlayableArea playableArea)
        {
            this.enemyParent = enemyParent;
            this.playableArea = playableArea;

            // Initialize enemy configurations
            InitializeEnemyConfigs();
        }

        private void InitializeEnemyConfigs()
        {
            spawnTimer = 60;

            // Define configurations for different enemy types
            enemyConfigs = new EnemyConfig[]
            {
            new EnemyConfig("Enemy", 1, 50),
            new EnemyConfig("Enemy2", 3, 25),
            new EnemyConfig("Enemy3", 5, 30),
            // Basic Enemies!
            //Change their health and attack delay!

            new EnemyConfig("CircleEnemy", 3, 70, 10, 15),
            //Circle of bullets enemies!
            //Change their health, attack delay, bullets in the circle, radius of the circle!

            new EnemyConfig("TrackingEnemy", 2, 40),
            //Enemy that shoots at where you are!
            //Change their health and attack delay!

            new EnemyConfig("Boss",100, 10),
            };
        }


        private string GetEnemyType()
        {

            switch (currentStage)
            //currentStage is set at 30 seconds interval. Add / remove enemy types to your content!
            {
                case 1:
                    return "Enemy";
                case 2:
                    return (random.Next(2) == 0) ? "Enemy2" : "Enemy3";
                case 3:
                    return "CircleEnemy";
                case 4:
                    return "TrackingEnemy";
                case 5:
                    return "Boss";
                default:
                    return "Enemy";
            }
        }

        public void Update()
        {

            //Console.WriteLine(currentStage);
            timerChange();
            gameStage();

            if ((spawnTime - spawnTimer) == 0)
            {
                spawnEnemy();
                spawnTime = 0;
            }
        }

        public void gameStage()
        {
            if(currentStage <= 5)
            {
            // Update the timer
            elapsedTime += Time.deltaTime;

                // Check if the interval is reached
                if (elapsedTime >= interval)
                {
                    // Increment the current stage
                    currentStage++;

                    // Adjust elapsedTime to keep track of the remaining time
                    elapsedTime %= interval;
                }
            }
        }

        void timerChange()
        {
            spawnTime++;
        }

        public void spawnEnemy()
        {
            // If the boss has already been spawned, do not spawn any more enemies
            if (bossSpawned)
            {
                return;
            }

            float enemySpawnX = playableArea.Width - 50;
            float enemySpawnY = (float)(random.NextDouble() * (playableArea.Height + 50));

            // Determine the enemy type based on the current stage
            string enemyType = GetEnemyType();

            // Find the configuration for the selected enemy type
            EnemyConfig selectedConfig = enemyConfigs.FirstOrDefault(config => config.Type == enemyType);

            // If a valid configuration is found, spawn the enemy
            if (selectedConfig != null && IsInsidePlayableArea(enemySpawnX, enemySpawnY) && currentStage <= 4)
            {
                switch (enemyType)
                {
                    case "CircleEnemy":
                        // Spawn CircleEnemy
                        CircleEnemy circleEnemy = new CircleEnemy(enemySpawnX, enemySpawnY, selectedConfig.Health, selectedConfig.Damage, selectedConfig.CircleBulletCount, selectedConfig.CircleBulletRadius, playableArea);
                        circleEnemy.SetXY(enemySpawnX, enemySpawnY);
                        AddChild(circleEnemy);
                        break;
                    case "TrackingEnemy":
                        // Spawn TrackingEnemy
                        TrackingEnemy trackingEnemy = new TrackingEnemy(enemySpawnX, enemySpawnY, selectedConfig.Health, selectedConfig.Damage, playableArea);
                        trackingEnemy.SetXY(enemySpawnX, enemySpawnY);
                        AddChild(trackingEnemy);
                        break;
                        
                    default:
                        Enemy Enemy = new Enemy(enemySpawnX, enemySpawnY, selectedConfig.Health, selectedConfig.Damage, playableArea);
                        Enemy.SetXY(enemySpawnX, enemySpawnY);
                        AddChild(Enemy);
                        break;
                }
            }

            if (selectedConfig != null && currentStage == 5 && !bossSpawned)
            {
                Boss boss = new Boss(enemySpawnX, enemySpawnY, selectedConfig.Health, playableArea);
                    boss.SetXY(enemySpawnX, enemySpawnY);
                    AddChild(boss);
                    bossSpawned = true;
            }


        }

        public bool IsInsidePlayableArea(float x, float y)
        {
            float playableLeft = 0;
            float playableRight = playableArea.Width;
            float playableTop = 0;
            float playableBottom = playableArea.Height - 50;

            return x >= playableLeft && x <= playableRight &&
                   y >= playableTop && y <= playableBottom;
        }

        

        private class EnemyConfig
        {
            public string Type { get; }
            public int Health { get; }
            public int Damage { get; }
            public int CircleBulletCount { get; } // Additional parameter for CircleEnemy
            public int CircleBulletRadius { get; } // Additional parameter for CircleEnemy
            public int TrackingSpeed { get; } // Additional parameter for TrackingEnemy

            public EnemyConfig(string type, int health, int damage)
            {
                Type = type;
                Health = health;
                Damage = damage;
            }

            public EnemyConfig(string type, int health, int damage, int circleBulletCount, int circleBulletRadius)
                : this(type, health, damage)
            {
                CircleBulletCount = circleBulletCount;
                CircleBulletRadius = circleBulletRadius;
            }

            public EnemyConfig(string type, int health, int damage, int trackingSpeed)
                : this(type, health, damage)
            {
                TrackingSpeed = trackingSpeed;
            }

        }
    }
}