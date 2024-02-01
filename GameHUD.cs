﻿using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using TiledMapParser;

public class TouhouHUD : GameObject
{
    private int playerHealth;
    private int playerScore;
    public int playerBombs;

    private EasyDraw hudBackground;
    private EasyDraw healthBar;
    private EasyDraw scoreDisplay;
    private EasyDraw bombDisplay;

    private EasyDraw healthLabel;
    private EasyDraw bombLabel;

    Sound _music;
    public List<Star> stars;
    public List<Star> starbombs;

    bool gameIntro = true;
    int bombReload;
    int bombReloadTimer = 500;


    public TouhouHUD(Player player)
    {
        playerHealth = player.playerHealth;
        playerScore = 0;
        playerBombs = 5; // Initial number of bombs



        hudBackground = new EasyDraw(game.width, 40);
        healthBar = new EasyDraw(game.width, game.height);
        scoreDisplay = new EasyDraw(200, 20);
        bombDisplay = new EasyDraw(game.width, game.height);

        AddChild(hudBackground);
        AddChild(healthBar);
        AddChild(scoreDisplay);
        AddChild(bombDisplay);

        healthLabel = new EasyDraw(200, 20);
        bombLabel = new EasyDraw(200, 20);
        AddChild(healthLabel);
        AddChild(bombLabel);



        stars = new List<Star>();
        starbombs = new List <Star>();
        CreateHealthStars(10);
        CreateBombStars(5);
        playMusic();
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        reloadingBomb();
        UpdateHealthBar(playerHealth);
        UpdateScoreDisplay();
        UpdateBombDisplay();
        UpdatePlayerBomb(5);
    }



private void UpdateScoreDisplay()
    {
        scoreDisplay.Clear(Color.Transparent);
        scoreDisplay.Fill(255);
        scoreDisplay.Text("Score: " + playerScore, game.width / 4 * 2, game.height / 8);
    }

    private void UpdateBombDisplay()
    {
        bombDisplay.Clear(Color.Transparent);
        bombDisplay.Fill(255, 255, 255); // Yellow color for bombs
        bombDisplay.Text("Bombs:", game.width / 4 * 2 + 30, game.height / 5 * 2 - 30); // Display a "B" for bombs
        bombDisplay.Text("Health:", game.width / 4 * 2 + 30, game.height / 5 - 10); // Display a "B" for bombs


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
        }
    }

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
        UpdateHealthBar(playerHealth);
    }

    public void CreateHealthStars(int initialHealth)
    {
        int starSpacing = 50; // Adjust the spacing value as needed

        for (int i = 0; i < initialHealth; i++)
        {
            Star star = new Star(game.width / 4 * 2 + 30 + (i * starSpacing), game.height / 5);
            stars.Add(star);
            AddChild(star);
        }
    }

    public void CreateBombStars(int initialBombs)
    {
        int starSpacing = 50; // Adjust the spacing value as needed

        for (int i = 0; i < initialBombs; i++)
        {
            Star starbomb = new Star(game.width / 4 * 2 + 30 + (i * starSpacing), game.height / 5 * 2);
            starbombs.Add(starbomb);
            AddChild(starbomb);
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
            }
        }
        else if (playerHealth == 0)
        {
            // Handle other conditions if needed
        }
    }

    public void UpdatePlayerBomb(int playerBombs)
    {
        if (playerBombs < starbombs.Count)
        {
            // Remove stars beyond the current bomb count
            int starsToRemove = starbombs.Count - playerBombs;
            for (int i = 0; i < starsToRemove; i++)
            {
                Star star = starbombs[starbombs.Count - 1];
                star.LateRemove();
                starbombs.RemoveAt(starbombs.Count - 1);
            }
        }
        else if (playerBombs > starbombs.Count)
        {
            // Add stars for additional bombs
            int starsToAdd = playerBombs - starbombs.Count;
            for (int i = 0; i < starsToAdd; i++)
            {
                Star star = new Star(game.width / 4 * 2 + 30 + ((starbombs.Count + i) * 50), game.height / 5 * 2);
                starbombs.Add(star);
                AddChild(star);
            }
        }
        // Handle other conditions if needed
    }

    void reloadingBomb()
    {
        if (playerBombs <= 4)
        {
            bombReload++;

            if (bombReload == bombReloadTimer)
            {
                playerBombs++;
                bombReload = 0;

            }
        }
      
    }

    void playMusic()
    {
        _music = new Sound("music.ogg", false, true);
        //_music.Play();
    }



    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}
