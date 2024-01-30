using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
    int lastShootTime;
    int shootIntervalMs = 100;
    bool gameStart;

    Player player;
    EnemySpawner enemySpawner;
    TouhouHUD touhouHUD;
    EasyDraw canvas;
    EasyDraw background;
    //EasyDraw playableArea;
    PlayableArea playableArea;

    public int playableWidth = 600;  // Set your desired width
    public int playableHeight = 500; // Set your desired height

    Button startButton;
    Button exitButton;

    bool gameStarted;
    MainMenu mainMenu;



    public MyGame() : base(1200, 600, false, false)
    {
            gameStarted = false;
            mainMenu = new MainMenu();

            startButton = new Button("Start", 100, 50);
            startButton.SetXY(width/2, height / 2 - 150);
            startButton.OnButtonClick += StartButton_OnButtonClick;
            AddChild(startButton);

            // Create exit button
            exitButton = new Button("Exit", 100, 50);
            exitButton.SetXY(width / 2, height / 2 +150);
            exitButton.OnButtonClick += ExitButton_OnButtonClick;
            AddChild(exitButton);
    }


    void StartButton_OnButtonClick()
    {
        // Handle start button click
        // Add code to start the game
        Console.WriteLine("Start button clicked");
        gameStart = true;

        // Set up the background
        background = new EasyDraw(width, height);
            AddChild(background);
            background.Fill(0, 0, 0); // Black background

            // Set up the playable area


            playableArea = new PlayableArea(playableWidth, playableHeight, "Background.png");
            SetXY((width - playableWidth) / 10, (height - playableHeight) / 2);
            AddChild(playableArea);

            // Set up the player
            player = new Player();
            AddChild(player);

            // Set up the enemy spawner
            enemySpawner = new EnemySpawner(this, playableArea);
            AddChild(enemySpawner);

            // Set up the HUD
            touhouHUD = new TouhouHUD(player);
            AddChild(touhouHUD);
            Start();

        // Handle start button click
        // Add code to start the game
        Console.WriteLine("Start button clicked");
        gameStart = true;
    }





    void ExitButton_OnButtonClick()
    {
        // Handle exit button click
        // Add code to exit the program
        Environment.Exit(0);
    }


  
    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        mainMenu.Update();
        if (gameStart == true)
        {
            touhouHUD.UpdateHealthBar(player.playerHealth);
            //  enemy.spawnEnemy();
            Shoot();
            touhouHUD.SetPlayerHealth(player.playerHealth);


            if (player.playerAlive == false)
            {
                Environment.Exit(0);

            }


        }

    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }


    public void Shoot()
    {
        // Create and spawn a PlayerBullet in front of the player
        float bulletSpawnX = player.x + player.width; // Adjust as needed
        float bulletSpawnY = player.y + player.height / 2; // Adjust as needed

        // Check for shooting action, for example, when Space key is pressed
        if (Input.GetMouseButton(0) && (Time.now - lastShootTime) > shootIntervalMs)
        {
            lastShootTime = Time.now;


            PlayerBullet bullet = new PlayerBullet(bulletSpawnX, bulletSpawnY, playableArea);
            bullet.scaleX = scaleX; // Inherit the player's orientation

            //Console.WriteLine("  " +  bulletSpawnX + " , " + bulletSpawnY + " & " + player.x + "," + player.y);
            // Add the bullet to the game or perform any other actions
            AddChild(bullet);
        }

        if (Input.GetMouseButtonUp(1))
        {
            PlayerBomb bomb = new PlayerBomb(bulletSpawnX, bulletSpawnY, playableArea);
            AddChild(bomb);
            Console.WriteLine("BOMB");
             lateRemoveAllEnemies(this);


        }
    }
    

    void lateRemoveAllEnemies( GameObject obj )
    {
        List<GameObject> children = obj.GetChildren();
        foreach (GameObject child in children)
        {
            if (child is Enemy)
            {
                Enemy enemy = child as Enemy;
                enemy.enemyHealth--;
                Console.WriteLine("Object processed: " + child);
                
            } else if (child is EnemyBullet)
            {
                child.LateRemove();

            } else
            {
                lateRemoveAllEnemies(child);
            }
        }
    }

}

