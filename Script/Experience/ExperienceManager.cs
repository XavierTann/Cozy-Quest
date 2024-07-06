using System;
using UnityEngine;

public class ExperienceManager : MonoBehaviour 
{

    [SerializeField] GameObject experienceBar;

    int currentLevel;
    int currentExperience;
    int experienceToPreviousLevel;
    int levelUpExperience;


    private void Start() {
        
        ExperienceSystem.Instance.OnGainExperience += UpdateUI;
    }


    private void UpdateUI() {
        ExperienceBarUI experienceBarUI = experienceBar.GetComponent<ExperienceBarUI>();

        currentLevel = LevelSystem.Instance.Level;
        currentExperience = ExperienceSystem.Instance.Experience;

        if (currentLevel != 0) {
            experienceToPreviousLevel = ExperienceSystem.Instance.GetLevelUpXP(currentLevel - 1);
        }
        else {
            experienceToPreviousLevel = 0;
            }

        levelUpExperience = ExperienceSystem.Instance.GetLevelUpXP(currentLevel);

        int experience = currentExperience - experienceToPreviousLevel;
        int maxExperience = levelUpExperience - experienceToPreviousLevel;

        experienceBarUI.SetExperience(experience, maxExperience);
    }

}