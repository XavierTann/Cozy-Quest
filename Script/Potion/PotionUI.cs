using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour 
{

    public static PotionUI Instance {get; private set;}

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start() {
        UpdatePotionUI(0);
    }

    public void UpdatePotionUI(int potionCount) {
        GetComponentInChildren<Text>().text = potionCount.ToString();   
    }

}