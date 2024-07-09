using UnityEngine;
using System;

public class PotionSystem : MonoBehaviour
{
    public static PotionSystem Instance {get; private set;}


    [SerializeField] GameObject playerGameObject;
    [SerializeField] PotionSO potionSO;

    private HealthSystem healthSystem;
    public int potionCount;

    public Action OnPotionObtained;
    public Action OnPotionUsed;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;

        OnPotionObtained += ObtainPotion;
        OnPotionUsed += UsePotion;
        
    }

    private void Start() {
        healthSystem = playerGameObject.GetComponent<HealthSystem>();
    }

    public void HealingPotion (){
        healthSystem.Heal(50);
    }

    public void UsePotion() {
        potionCount -= 1;
        PotionUI.Instance.UpdatePotionUI(potionCount);

        // Depending on what kind of potion
        HealingPotion();

        // Remove from Inventory
        InventorySystem.Instance.RemoveItems(potionSO);
        
    }

    public void ObtainPotion() {
        potionCount += 1;
        PotionUI.Instance.UpdatePotionUI(potionCount);

        // Add to Inventory
        InventorySystem.Instance.AddItems(potionSO);
    }

}