using System;
using Unity.VisualScripting;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    public static ManaSystem Instance { get; private set; }
    private float maxMana;
    private float currentMana;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject manaBarUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        maxMana = player.GetComponent<PlayerStats>().MaxMana;
        currentMana = maxMana;
    }

    public void UseMana(float mana)
    {
        if (currentMana > mana)
        {
            currentMana -= mana;
            Debug.Log($"Used {mana} mana!");
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough mana");
        }
    }

    public void GainMana(float mana)
    {
        currentMana += mana;
        Debug.Log($"Gained {mana} mana!");
        UpdateUI();
    }

    public void UpdateUI()
    {
        manaBarUI.GetComponent<ManaBarUI>().SetMana(currentMana, maxMana);
        Debug.Log("Updated UI");
    }
}
