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


    private void Awake()
    {
        InitializeHealthBarUI();
    }

    public void TakeDamage(float damage)
    {
        playerStats.SetHealth(playerStats.GetHealth() - damage);
        Debug.Log(playerStats.GetHealth());

        OnDamageTaken?.Invoke(this, EventArgs.Empty);

        UpdateHealthUI();
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
