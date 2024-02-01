using System;
using System.Drawing.Imaging;
using System.Runtime.Remoting.Activation;
using GXPEngine;

public class Player : AnimationSprite
{
    int counter;
    int frame;
    int speed = 3;
    public int playerHealth = 10;
    public int playerBombs = 5;
    public Boolean playerAlive = true;
    private PlayableArea playableArea;


    public Player(PlayableArea playableArea) : base("character_run.png", 1, 8)
    {
        this.playableArea = playableArea;
        scale = 1;
        //SetOrigin(width / 2, height / 2); // Set the origin to the center of the png

    }

    void Update()
    {
        counter++;
        if (counter > 10)
        {
            counter = 10;
            frame++;
            if (frame == frameCount)
            {
                frame = 0;
            }

            SetCycle(1, 8);
            Animate(0.08f);
        }

        MoveCharacter();
        playerHealthUpdate();

    }

    void MoveCharacter()
    {
        if (Input.GetKey(Key.S) && IsInsidePlayableArea(x, y + speed))
        {
            y += speed;
        }

        if (Input.GetKey(Key.W) && IsInsidePlayableArea(x, y - speed))
        {
            y -= speed;
        }

        if (Input.GetKey(Key.A) && IsInsidePlayableArea(x - speed, y))
        {
            x -= speed;
        }

        if (Input.GetKey(Key.D) && IsInsidePlayableArea(x + speed, y))
        {
            x += speed;
        }
    }






    void playerHealthUpdate()
    {
        if (playerHealth <= 0)
        {
            playerAlive = false;

        }
    }


    void OnCollision(GameObject other)
    {
        if (other is EnemyBullet || other is Enemy)
        {
            //playerHealth--;
            //touhouHUD.SetPlayerHealth(playerHealth);
            Console.WriteLine(" hit " + playerHealth);
        }
    }

    bool IsInsidePlayableArea(float x, float y)
    {
        // Replace these values with the actual boundaries of the playableArea
        float playableLeft = 0;
        float playableRight = playableArea.Width-width;
        float playableTop = 0;
        float playableBottom = playableArea.Height-height;

        // Check if the position is inside the playable area
        return x >= playableLeft && x <= playableRight &&
               y >= playableTop && y <= playableBottom;
    }





}