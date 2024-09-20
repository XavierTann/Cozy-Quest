using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPotionUI;

    [SerializeField]
    private GameObject manaPotionUI;

    [SerializeField]
    public static PotionUI Instance { get; private set; }

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
        UpdatePotionUI("ManaPotion", 0);
        UpdatePotionUI("HealthPotion", 0);
    }

    public void UpdatePotionUI(string potionType, int potionCount)
    {
        if (potionType == "HealthPotion")
        {
            healthPotionUI.GetComponentInChildren<Text>().text = potionCount.ToString();
        }
        if (potionType == "ManaPotion")
        {
            manaPotionUI.GetComponentInChildren<Text>().text = potionCount.ToString();
        }
        // GetComponentInChildren<Text>().text = potionCount.ToString();
    }
}
