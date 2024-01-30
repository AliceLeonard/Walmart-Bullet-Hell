using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using TiledMapParser;

public class TouhouHUD : GameObject
{
    private int playerHealth;
    private int playerScore;
    private int playerBombs;

    private EasyDraw hudBackground;
    private EasyDraw healthBar;
    private EasyDraw scoreDisplay;
    private EasyDraw bombDisplay;

    Sound _music;
    public List<Star> stars;

    public TouhouHUD(Player player)
    {
        playerHealth = player.playerHealth;
        playerScore = 0;
        playerBombs = 3; // Initial number of bombs

        hudBackground = new EasyDraw(game.width, 40);
        healthBar = new EasyDraw(game.width, game.height);
        scoreDisplay = new EasyDraw(200, 20);
        bombDisplay = new EasyDraw(40, 40);

        AddChild(hudBackground);
        AddChild(healthBar);
        AddChild(scoreDisplay);
        AddChild(bombDisplay);

        stars = new List<Star>();
        CreateHealthStars(10);
        playMusic();
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        UpdateHealthBar(playerHealth);
        UpdateScoreDisplay();
        UpdateBombDisplay();
   
    }

   /* public void UpdateHealthBar()
    {
        float barWidth = Map(playerHealth, 0, 10, 0, 200);
        float x = game.width/4*3; // Set your desired x-coordinate
        float y = game.height/8; // Set your desired y-coordinate

        healthBar.Clear(Color.Transparent);
        healthBar.Fill(255, 0, 0);
        healthBar.Rect(x, y, barWidth, 20);
        
        Console.WriteLine(playerHealth);
        //Console.WriteLine("Health Bar Coordinates: x = " + x + ", y = " + y);

    }*/

    private void UpdateScoreDisplay()
    {
        scoreDisplay.Clear(Color.Transparent);
        scoreDisplay.Fill(255);
        scoreDisplay.Text("Score: " + playerScore, game.width/4*2, game.height/8);
    }

    private void UpdateBombDisplay()
    {
        bombDisplay.Clear(Color.Transparent);
        bombDisplay.Fill(255, 255, 0); // Yellow color for bombs
        bombDisplay.Text("B", 0, 0); // Display a "B" for bombs
        bombDisplay.Text(playerBombs.ToString(), 15, 20); // Display bomb count
    }

    public void IncreaseScore(int points)
    {
        playerScore += points;
        UpdateScoreDisplay();
    }

    public void UseBomb()
    {
        if (playerBombs > 0)
        {
            playerBombs--;
            UpdateBombDisplay();
            // Implement bomb effect logic here
        }
    }

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
        UpdateHealthBar(playerHealth);
    }

    public void CreateHealthStars(int initialHealth)
    {
        int starSpacing = 50;  // Adjust the spacing value as needed - really should use width but can't

        for (int i = 0; i < initialHealth; i++)
        {
            Star star = new Star(game.width / 4 * 2 + 100 + (i * starSpacing), game.height / 5);
            stars.Add(star);
            AddChild(star);
        }
    }


    public void UpdateHealthBar(int playerhealth)
    {
        
        if (playerHealth < stars.Count)
        {
            // Remove stars beyond the current health
            int starsToRemove = stars.Count - playerHealth;
            for (int i = 0; i < starsToRemove; i++)
            {
                Star star = stars[stars.Count - 1];
                star.LateRemove();
                stars.RemoveAt(stars.Count - 1);
                Console.WriteLine(stars.Count);
            }
        }
    }

    void playMusic()
    {
        _music = new Sound("music.mp3",false,true);
        _music.Play();
        Console.WriteLine();
        hello;
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}