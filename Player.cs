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
    public Boolean playerAlive = true;

    public Player() : base("character_run.png", 1, 8)
    {
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
        // I like to move it, move it.
        if (Input.GetKey(Key.S))
        {
            y += speed;
        }
        else if (Input.GetKey(Key.W))
        {
            y -= speed;
        }

        if (Input.GetKey(Key.A))
        {
            x -= speed;
        }
        else if (Input.GetKey(Key.D))
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
            playerHealth--;
            //touhouHUD.SetPlayerHealth(playerHealth);
            Console.WriteLine(" hit " + playerHealth);
        }
    }







}