using UnityEngine;
using System;

public class PotionSystem : MonoBehaviour
{
    public static PotionSystem Instance {get; private set;}


    [SerializeField] GameObject playerGameObject;
    private HealthSystem healthSystem;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start() {
        healthSystem = playerGameObject.GetComponent<HealthSystem>();
    }

    public void HealingPotion (){
        healthSystem.Heal(50);
    }

}