using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats {
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyAttackDamage;
    [SerializeField] private float enemyAttackRange;

    public float GetHealth() {
        return enemyHealth;
    }

    public void SetHealth(float enemyHealth) {
        this.enemyHealth = enemyHealth;
    }

    public float GetAttackDamage() {
        return enemyAttackDamage;
    }

    public float GetAttackRange() {
        return enemyAttackRange;
    }
}