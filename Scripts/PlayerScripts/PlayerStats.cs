using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private readonly float maxHealth;
    private float currentHealth;
    private int kills = 0;

    public PlayerStats(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int GetKills() { return kills; }

    public void SetKills(int kills) { this.kills = kills; }

    public void ConfigureStats() // start
    {
        currentHealth = maxHealth;
        UISystem.Instance.GetHealthBarUI().SetHealthBarUI(maxHealth);
    }

    public void UpdateHealth() // Update
    {
        if(currentHealth <= 0){
            Die();
        }

        UISystem.Instance.GetHealthBarUI().SetHealthBar(currentHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }


    private void Die()
    {
        Debug.Log("You died");
        GameState.Instance.Respawn();
        currentHealth = maxHealth;
    }

}
