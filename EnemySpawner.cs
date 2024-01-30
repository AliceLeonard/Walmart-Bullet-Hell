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
        private GameObject enemyParent;
        private PlayableArea playableArea; // Assuming PlayableArea is the class containing width and height
        public int gameTime = 600;
        int spawnTimer = 500; // Increase if you want fewer enemies per second
        int spawnTime;
        private Random random = new Random();

        private int currentStage = 1;

        float elapsedTime;
        float interval = 5 * 1000; // we're using millisecond so *1000
        bool timerReached = false;

        public EnemySpawner(GameObject enemyParent, PlayableArea playableArea)
        {
            this.enemyParent = enemyParent;
            this.playableArea = playableArea;
        }

        public void Update()
        {
            gameStage();
            //Console.WriteLine(elapsedTime);
            if ((Time.now - spawnTime) > spawnTimer)
            {
                //GetChildCount() <= 5 &&
                spawnTime = Time.now;
                spawnEnemy();
            }
        }

        public void gameStage()
        {
            // Update the timer
            elapsedTime += Time.deltaTime;

            // Check if the interval is reached
            if (elapsedTime >= interval)
            {
                // Set the boolean to true
                timerReached = true;

                // Increment the current stage
                currentStage++;


                // Additional actions you want to perform when the interval is reached
                Console.WriteLine($"Timer reached! Current Stage: {currentStage}");

                // Adjust elapsedTime to keep track of the remaining time
                elapsedTime %= interval;
            }
        }


        public void spawnEnemy()
        {
           
              /*  float enemySpawnX = playableArea.Width - 20; // Adjust as needed
                float enemySpawnY = (float)(random.NextDouble() * playableArea.Height);
                //Console.WriteLine(enemySpawnY);
                Enemy enemy = new Enemy(enemySpawnX, enemySpawnY, 3, 3, playableArea);
                enemy.SetXY(enemySpawnX, enemySpawnY);
                AddChild(enemy);*/


            float enemySpawnX = playableArea.Width - 20; // Adjust as needed
            float enemySpawnY = (float)(random.NextDouble() * playableArea.Height);

            // Determine the enemy type based on the current stage
            Enemy enemy;



            switch (currentStage)
            {
                case 1:
                    // Only spawn Enemy type for stage 1
                    enemy = new Enemy(enemySpawnX, enemySpawnY, 3, 3, playableArea);
                    enemy.SetXY(enemySpawnX, enemySpawnY);
                    AddChild(enemy);
                    break;
                case 2:
                    // Mix of Enemy and Enemy2 for stage 2
                    if (random.Next(2) == 1)
                    {
                        enemy = new Enemy(enemySpawnX, enemySpawnY, 3, 3, playableArea);
                    }
                    else
                    {
                        float enemy2SpawnX = playableArea.Width / 2 + (float)(random.NextDouble() * playableArea.Width / 2); // Spawns only in the right half
                        float enemy2SpawnY =  playableArea.Height - 40;
                        enemy = new Enemy2(enemy2SpawnX, enemy2SpawnY, 3, 3, playableArea);
                        enemy.SetXY(enemy2SpawnX, enemy2SpawnY);
                        AddChild(enemy);
                    }
                    break;
                // Add cases for other stages as needed
                case 3:
                    // ...
                    break;
                case 4:
                    // ...
                    break;
                case 5:
                    // ...
                    break;
                default:
                    // Default to spawning Enemy if the current stage is not handled
                    enemy = new Enemy(enemySpawnX, enemySpawnY, 3, 3, playableArea);
                    break;
            }

            // Set the position and add the enemy to the scene
            
        }
    }
}