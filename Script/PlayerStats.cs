using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerAttackDamage;
    [SerializeField] private float playerAttackRange;

    public float GetPlayerHealth() {
        return playerHealth;
    }

    public void SetPlayerHealth(float playerHealth) {
        this.playerHealth = playerHealth;
    }

    public float GetPlayerAttackDamage() {
        return playerAttackDamage;
    }

    public float GetPlayerAttackRange() {
        return playerAttackRange;
    }
}