using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    public void Start()
    {
        currentHealth = maxHealth;
        HealthBarUI.instance.SetHealthBarUI(maxHealth);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        HealthBarUI.instance.SetHealthBar(currentHealth);
    }

    private void Update()
    {
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        if(currentHealth <= 0){
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("You died");
    }

}
