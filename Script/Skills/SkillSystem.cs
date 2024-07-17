using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public static SkillSystem Instance { get; private set; }

    private int skillPoints;
    public int SkillPoints
    {
        get { return skillPoints; }
    }

    [SerializeField]
    private List<SkillSO> availableSkills = new();

    public List<SkillSO> learntSkillsList = new();

    public Action OnLearnSkill; // This event should pass in the skill that you learnt and enable that particular skill in the UI.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        skillPoints = 0;
    }

    private void Start()
    {
        LevelSystem.Instance.OnLevelUp += GainSkillPoint;
    }

    private void GainSkillPoint()
    {
        skillPoints += 1;
        Debug.Log("Gained a skill point!");
    }

    private void LoseSkillPoint(int number)
    {
        skillPoints -= number;
    }

    public void LearnSkill(SkillSO skill)
    {
        if (skill.SkillPointsRequired <= skillPoints)
        {
            LoseSkillPoint(skill.SkillPointsRequired);
            learntSkillsList.Add(skill);
            OnLearnSkill?.Invoke();
            Debug.Log($"Learnt {skill.Name} Skill!");
        }
        else
        {
            Debug.Log("Not enough points!");
        }
    }
}
