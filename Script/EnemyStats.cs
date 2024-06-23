using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyAttackDamage;
    [SerializeField] private float enemyAttackRange;

    public float GetEnemyHealth() {
        return enemyHealth;
    }

    public void SetEnemyHealth(float enemyHealth) {
        this.enemyHealth = enemyHealth;
    }

    public float GetEnemyAttackDamage() {
        return enemyAttackDamage;
    }

    public float GetEnemyAttackRange() {
        return enemyAttackRange;
    }
}