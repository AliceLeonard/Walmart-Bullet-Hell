using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TiledMapParser;

public class Enemy : AnimationSprite
{
    int moveTimer;
    float radius = 10;
    float angle = 90;
    public int enemyHealth;
    public int enemyShootTime;
    private PlayableArea playableArea;


    public Enemy(float x, float y, int enemyHealth, int enemyShootTime, PlayableArea playableArea) : base("character_run.png", 1, 8)
    {
        this.x = x;
        this.y = y;
        this.enemyHealth = enemyHealth;
        this.enemyShootTime = enemyShootTime;
        this.playableArea = playableArea;
    }

    //have enemy1 be a basic enemy that goes from right to left side of screen
    //move the arc movement to enemy2

    public virtual void Update()
    {
        moveTimer++;
        if (moveTimer > 2)
        {
            moveTimer = 0;
            enemyMove();
        }
        healthUpdate();
        enemyShoot();
        IsOutsidePlayableArea();
    }

    void IsOutsidePlayableArea()
    {
        // Replace these values with the actual boundaries of the playable area
        float playableLeft = 0;
        // playableRight = playableArea.Width;
        float playableTop = 0;
        float playableBottom = playableArea.Height - 20;

        // Check if the enemy is outside the horizontal boundaries
        if (x < playableLeft)
        {
            LateRemove();
        }
        else if (y < playableTop || y > playableBottom)

        {
            LateRemove();
        }

    }

    public virtual void enemyMove()
    {
        x--;

        /*        angle -= 0.1f;
                y = y + Mathf.Sin(angle) * radius;*/
    }


    public virtual void OnCollision(GameObject other)
    // had to use public virtual, or else bullet won't work
    {
        if (other is PlayerBullet)
        {
            //LateRemove();
            //enemyCount--;
            enemyHealth--;
            Console.WriteLine("Enemy hit! Remaining health: " + enemyHealth);
        }
    }

    void healthUpdate()
    {
        if (enemyHealth <= 0)
        {
            LateRemove();
            //Console.WriteLine(enemyHealth);
        }
    }

    public virtual void enemyShoot()
    {
        if (enemyShootTime > 0)
        {
            enemyShootTime--;
        }

        if (enemyShootTime == 0)
        {
            enemyShootTime = 200; // Set a fixed cooldown time
            enemy1Shoot();

            //generateSpiralOfBullets(12, 50);
        }
    }

    void enemy1Shoot()
    {
        float enemyBulletSpawnX = x;
        float enemyBulletSpawnY = y + width / 2;
        if (IsInsidePlayableArea(enemyBulletSpawnX, enemyBulletSpawnY))
        {
            EnemyBullet enemybullet = new EnemyBullet(enemyBulletSpawnX, enemyBulletSpawnY, playableArea, angle);
            game.AddChild(enemybullet);
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
}