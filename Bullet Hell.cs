using GXPEngine;
using System;
using System.Collections.Generic;

public class MyGame : Game
{
    int lastShootTime;
    int shootIntervalMs = 150;
    bool gameStart = false;

    Player player;
    Boss boss;
    EnemySpawner enemySpawner;
    TouhouHUD touhouHUD;
    EasyDraw background;
    PlayableArea playableArea;
    public bool gameWin = false;

    private EasyDraw winScreen;

    public int playableWidth = 600;
    public int playableHeight = 500;

    Button startButton;
    Button restartButton;
    Button exitButton;

    Sound attack;

    enum GameState
    {
        MainMenu,
        Playing,
        RestartMenu
    }

    GameState gameState;

    public MyGame() : base(1200, 600, false, false)
    {
        InitializeMainMenu();
    }
    static void Main()
    {
        new MyGame().Start();
    }

    void InitializeMainMenu()
    {
        x = 0; y = 0;
        gameState = GameState.MainMenu;

        startButton = new Button("Start", 100, 50);
        startButton.SetXY(width / 2, height / 2 - 150);
        startButton.OnButtonClick += StartButton_OnButtonClick;
        AddChild(startButton);

        exitButton = new Button("Exit", 100, 50);
        exitButton.SetXY(width / 2, height / 2 + 150);
        exitButton.OnButtonClick += ExitButton_OnButtonClick;
        AddChild(exitButton);
    }

    void StartButton_OnButtonClick()
    {
        gameState = GameState.Playing;
        gameStart = true;

        RemoveMainMenu();

        SetupGame();
    }

    void RemoveMainMenu()
    {
        RemoveChild(startButton);
        RemoveChild(exitButton);
    }

    void SetupGame()
    {

        background = new EasyDraw(width, height);
        AddChild(background);
        background.Fill(0, 0, 0); // Black background

        playableArea = new PlayableArea(playableWidth, playableHeight, "Background.png");
        SetXY((width - playableWidth) / 10, (height - playableHeight) / 2);
        AddChild(playableArea);

        player = new Player(playableArea);
        AddChild(player);

        enemySpawner = new EnemySpawner(this, playableArea);
        AddChild(enemySpawner);

        touhouHUD = new TouhouHUD(player);
        AddChild(touhouHUD);

        gameState = GameState.Playing;
        gameStart = true;

        LateAddChild(new RestartMenu(this));

        // Other initialization code as needed
    }

    void ExitButton_OnButtonClick()
    {
        Environment.Exit(0);
    }

    void Update()
    {
        if (gameState == GameState.Playing)
        {
            // Update and draw other game elements
            touhouHUD.UpdateHUD();
            touhouHUD.UpdateHealthBar(player.playerHealth);
            touhouHUD.UpdatePlayerBomb(touhouHUD.playerBombs);
            Shoot();
            touhouHUD.SetPlayerHealth(player.playerHealth);
            CheckBossWin(); // Add this line
            WinGameCheck();

            if (player.playerAlive == false)
            {
                gameState = GameState.RestartMenu;
                // Remove all existing children
                foreach (var child in GetChildren())
                {
                    RemoveChild(child);
                }
                ResetGame();
            }
            else if (gameState == GameState.RestartMenu)
            {
                if (Input.GetMouseButton(0) && restartButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    ResetGame();
                }
            }
        }
    }

    public void Shoot()
    {
        float bulletSpawnX = player.x + player.width;
        float bulletSpawnY = player.y + player.height / 2;

        if (Input.GetMouseButton(0) && (Time.now - lastShootTime) > shootIntervalMs)
        {
            lastShootTime = Time.now;

            PlayerBullet bullet = new PlayerBullet(bulletSpawnX, bulletSpawnY, playableArea);
            bullet.scaleX = scaleX;
            AddChild(bullet);
            playAttack();
        }

        if (Input.GetMouseButtonUp(1) && touhouHUD.playerBombs > 0)
        {
            //PlayerBomb bomb = new PlayerBomb(bulletSpawnX, bulletSpawnY, playableArea);
            //AddChild(bomb);
            Console.WriteLine("BOMB");
            touhouHUD.UseBomb();
            lateRemoveAllEnemies(this);
        }
    }

    void lateRemoveAllEnemies(GameObject obj)
    {
        List<GameObject> children = obj.GetChildren();
        foreach (GameObject child in children)
        {
            if (child is Enemy)
            {
                Enemy enemy = child as Enemy;
                enemy.enemyHealth--;
                Console.WriteLine("Object processed: " + child);
            }
            else if (child is EnemyBullet)
            {
                child.LateRemove();
            }
            else
            {
                lateRemoveAllEnemies(child);
            }
        }
    }

    void playAttack()
    {
        attack = new Sound("attack.wav", false, true);
    }

    void ResetGame()
    {
        x = 0; y = 0;
        gameState = GameState.MainMenu;

        startButton = new Button("Restart?", 100, 50);
        startButton.SetXY(width / 2, height / 2 - 150);
        startButton.OnButtonClick += StartButton_OnButtonClick;
        AddChild(startButton);

        exitButton = new Button("Exit", 100, 50);
        exitButton.SetXY(width / 2, height / 2 + 150);
        exitButton.OnButtonClick += ExitButton_OnButtonClick;
        AddChild(exitButton);

    }


    void CheckBossWin()
    {
        // If the boss is not instantiated, create an instance for the check
        if (boss == null)
        {
            boss = new Boss(x, y, 30, playableArea);
        }
    }


    void WinGameCheck()
    {
        if (boss.gameWin)
        {
            Console.WriteLine("WINNNNNNNNNNNNNNNNNNNN");

            foreach (var child in GetChildren())
            {
                RemoveChild(child);
            }

            x = 0; y = 0;
            gameState = GameState.MainMenu;

            winScreen = new EasyDraw(width, height);
            AddChild(winScreen);
            winScreen.Text("VICTORY!:", game.width / 2, game.height / 10); // Display a "B" for bombs

            exitButton = new Button("Exit", 100, 50);
            exitButton.SetXY(width / 2, height / 2 + 150);
            exitButton.OnButtonClick += ExitButton_OnButtonClick;
            AddChild(exitButton);
        }
    }
}

