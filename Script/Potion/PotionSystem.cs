using System;
using UnityEngine;

public class PotionSystem : MonoBehaviour
{
    public static PotionSystem Instance { get; private set; }

    [SerializeField]
    GameObject playerGameObject;

    [SerializeField]
    public PotionSO potionSO;

    private HealthSystem healthSystem;

    public Action OnPotionObtained;
    public Action OnPotionUsed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        OnPotionObtained += ObtainPotion;
        OnPotionUsed += UsePotion;

        potionSO.Count = 0;
    }

    private void Start()
    {
        healthSystem = playerGameObject.GetComponent<HealthSystem>();
    }

    public void HealingPotion()
    {
        healthSystem.Heal(50);
    }

    public void UsePotion()
    {
        if (potionSO.Count > 0)
        {
            potionSO.Count -= 1;
            PotionUI.Instance.UpdatePotionUI(potionSO.Count);

            // Decrease quantity of both dictionaries
            InventorySystem.Instance.DecreaseItemQuantity(potionSO);

            // Depending on what kind of potion
            HealingPotion();
        }
    }

    public void ObtainPotion()
    {
        potionSO.Count += 1;
        PotionUI.Instance.UpdatePotionUI(potionSO.Count);
        InventorySystem.Instance.AddItem(potionSO);
    }
}
