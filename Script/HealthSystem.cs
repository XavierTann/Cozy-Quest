using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private GameObject healthBar;
    
    private HealthBarUI healthBarUI;
    private IStats playerStats;

    // Event to notify when damage is taken
    public event EventHandler OnDamageTaken;
    public event EventHandler OnDeath;


    private void Awake()
    {
        InitializeHealthBarUI();
        OnDeath += On_Death;
    }

    private void On_Death(object sender, EventArgs e) {
        Debug.Log("Player died");
    }

    public void TakeDamage(float damage)
    {
        float newHealth = playerStats.GetHealth() - damage;

        if (newHealth <= 0) 
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
        
        else 
        {
            playerStats.SetHealth(newHealth);

            OnDamageTaken?.Invoke(this, EventArgs.Empty);

            UpdateHealthUI();
        }
        
    }

    private void InitializeHealthBarUI() {
        playerStats = gameObject.GetComponent<IStats>();
        healthBar.SetActive(true);
        healthBarUI = healthBar.GetComponent<HealthBarUI>();
        healthBarUI.SetMaxHealth();
    }

    public void UpdateHealthUI() {
        healthBarUI.SetHealth(playerStats.GetHealth(), playerStats.GetMaxHealth());
    }
}
