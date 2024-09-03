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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        skillPoints = 10;
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

    public void LoseSkillPoint(int number)
    {
        skillPoints -= number;
    }
}
