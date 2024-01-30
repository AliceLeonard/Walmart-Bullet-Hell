/*using GXPEngine;
using System.Collections.Generic;

public class Hud : GameObject
{
    private List<Star> stars;

    public Hud(Player player)
    {
        stars = new List<Star>();
        CreateHealthStars(player.playerHealth);
    }

    private void CreateHealthStars(int initialHealth)
    {
        for (int i = 0; i < initialHealth; i++)
        {
            Star star = new Star(i * 30, 30);
            stars.Add(star);
            AddChild(star);
        }
    }

    public void UpdateHealthBar(int currentHealth)
    {
        if (currentHealth < stars.Count)
        {
            // Remove stars beyond the current health
            int starsToRemove = stars.Count - currentHealth;
            for (int i = 0; i < starsToRemove; i++)
            {
                Star star = stars[stars.Count - 1];
                star.LateRemove();
                stars.RemoveAt(stars.Count - 1);
            }
        }

        for (int i = 0; i < stars.Count; i++)
        {
            Star star = stars[i];
            if (i < currentHealth)
            {
                // Optionally: Set star color for remaining health
                star.SetStarColor(255, 255, 0); // Yellow
            }
            else
            {
                // Optionally: Set star color for lost health
                star.SetStarColor(128, 128, 128); // Gray
            }
        }
    }
}*/