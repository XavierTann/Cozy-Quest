using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExperienceSystem : MonoBehaviour {
    /* 
        Methods in Experience System:
        Increase experience
        Decrease experience
    */
    
    public static ExperienceSystem Instance {get; private set;}

    // Event to invoke update of UI
    public Action OnGainExperience;

    private int experience;
    public int Experience {
        get {return experience;} private set {}
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public int GetLevelUpXP() {
        int level = LevelSystem.Instance.Level;
        return (int) Math.Pow(level, 2.0);
    }

    // For updating of experience bar UI
    public int GetLevelUpXP(int level) {
        return (int) Math.Pow(level, 2.0);
    }

    public void IncreaseXP(int experience) {
        this.experience += experience;

        while (this.experience >= GetLevelUpXP()) {
            LevelSystem.Instance.LevelUp();
        }

        OnGainExperience?.Invoke();

    }

    private void DecreaseXP(int experience) {
        this.experience -= experience;
    }


}