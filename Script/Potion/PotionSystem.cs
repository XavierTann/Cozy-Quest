using System;
using UnityEngine;

public class PotionSystem : MonoBehaviour
{
    public static PotionSystem Instance { get; private set; }
    public int HealthPotionCount
    {
        get => healthPotionCount;
        set => healthPotionCount = value;
    }
    public int ManaPotionCount
    {
        get => manaPotionCount;
        set => manaPotionCount = value;
    }

    [SerializeField]
    GameObject playerGameObject;

    [SerializeField]
    public PotionSO healthPotionSO;

    [SerializeField]
    public PotionSO manaPotionSO;

    private int healthPotionCount;
    private int manaPotionCount;

    private HealthSystem healthSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        healthSystem = playerGameObject.GetComponent<HealthSystem>();
    }

    public void HealingPotion()
    {
        healthSystem.Heal(50);
    }

    public void ManaPotion()
    {
        ManaSystem.Instance.GainMana(50);
    }

    public void UsePotion(string potionType)
    {
        // Depending on what kind of potion
        if (potionType == "HealthPotion")
        {
            if (healthPotionCount > 0)
            {
                healthSystem.Heal(50);
                healthPotionCount -= 1;
                PotionUI.Instance.UpdatePotionUI("HealthPotion", healthPotionCount);
                Debug.Log("Used Health Potion");
            }
        }
        if (potionType == "ManaPotion")
        {
            if (manaPotionCount > 0)
            {
                ManaSystem.Instance.GainMana(50);
                manaPotionCount -= 1;
                PotionUI.Instance.UpdatePotionUI("ManaPotion", manaPotionCount);
                Debug.Log("Used Mana Potion");
            }
        }
    }

    public void ObtainPotion(string potionType)
    {
        // Depending on what kind of potion
        if (potionType == "HealthPotion")
        {
            healthPotionCount += 1;
            PotionUI.Instance.UpdatePotionUI("HealthPotion", healthPotionCount);
            InventorySystem.Instance.AddItem(healthPotionSO);
        }
        if (potionType == "ManaPotion")
        {
            manaPotionCount += 1;
            PotionUI.Instance.UpdatePotionUI("ManaPotion", manaPotionCount);
            InventorySystem.Instance.AddItem(manaPotionSO);
        }
    }
}
