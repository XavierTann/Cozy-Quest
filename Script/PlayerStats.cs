using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats {
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerAttackDamage;
    [SerializeField] private float playerAttackRange;

    public float GetHealth() {
        return playerHealth;
    }

    public void SetHealth(float playerHealth) {
        this.playerHealth = playerHealth;
    }

    public float GetAttackDamage() {
        return playerAttackDamage;
    }

    public float GetAttackRange() {
        return playerAttackRange;
    }
}