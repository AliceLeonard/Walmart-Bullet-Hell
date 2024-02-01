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
  
    public int enemyHealth;
    public int enemyShootTimer = 0;
    public int enemyShootTime;
    private PlayableArea playableArea;
    private EnemySpawner spawner;

    public Enemy(float x, float y, int enemyHealth, int enemyShootTime, PlayableArea playableArea) : base("enemy.png", 1, 8)
    {
        SetOrigin(width / 2, height / 2);
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
        enemyShootTimeUpdate();
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
    }


    public virtual void OnCollision(GameObject other)
    // had to use public virtual, or else bullet won't work
    {
        if (other is PlayerBullet) 
        {
            enemyHealth--;
            Console.WriteLine("Enemy hit! Remaining health: " + enemyHealth);
        }

        if (other is Player)
        {
            LateRemove();
        }
    }

    public virtual void healthUpdate()
    {
        if (enemyHealth <= 0)
        {
            LateRemove();
        }
    }

    public virtual void enemyShootTimeUpdate()
    {
            enemyShootTimer++;

        if (enemyShootTimer == enemyShootTime)
        {
            enemyShoot();
            enemyShootTimer = 0; // Set a fixed cooldown time
            //generateSpiralOfBullets(12, 50);
        }
    }

    public virtual void enemyShoot()
    {
        float enemyBulletSpawnX = x - width;
        float enemyBulletSpawnY = y + height / 4;
       
        EnemyBulletSimple enemybullet = new EnemyBulletSimple(enemyBulletSpawnX, enemyBulletSpawnY, playableArea);
        game.AddChild(enemybullet);
    }
}