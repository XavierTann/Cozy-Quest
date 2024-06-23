using UnityEngine;

interface IStats {

    public float GetMaxHealth();
    
    public float GetHealth();

    public void SetHealth(float health);

    public float GetAttackDamage();

    public float GetAttackRange();
    
}