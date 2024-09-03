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
        // For convenience
        maxMana = 10000;
        currentMana = maxMana;
    }

    public bool UseMana(float mana)
    {
        if (currentMana > mana)
        {
            currentMana -= mana;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough mana");
            return false;
        }
    }

    public void GainMana(float mana)
    {
        currentMana += mana;
        UpdateUI();
    }

    public void UpdateUI()
    {
        manaBarUI.GetComponent<ManaBarUI>().SetMana(currentMana, maxMana);
    }
}
