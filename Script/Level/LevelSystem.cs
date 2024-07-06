using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LevelSystem : MonoBehaviour {
    /* 
        Methods in Level System:
        Check Level
        Level Up
        Calculate how much xp to next level
        Define and invoke Level Up Event to notify subscibers like Skill tree system
    */

    [SerializeField] private GameObject levelUI;

    public static LevelSystem Instance {get; private set;}

    public Action OnLevelUp;

    private int level = 1;
    public int Level {
        get {return level;} 
        set {level = value;}
        }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
    }


    
    public void LevelUp() {
        level += 1;
        OnLevelUp?.Invoke();
        Debug.Log("Level up!");

        // Update UI
        levelUI.GetComponent<LevelUI>().SetLevel(level);
    }

}