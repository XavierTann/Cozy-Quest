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
    private GameObject firePrefab;

    [SerializeField]
    private GameObject waterPrefab;

    [SerializeField]
    private GameObject lightningPrefab;

    [SerializeField]
    private GameObject player;

    private int distance = 3;

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

    public void ActivateSkill(SkillSO skillSO)
    {
        Debug.Log($"{skillSO.Name} is activated!");
        bool enoughMana = ManaSystem.Instance.UseMana(skillSO.ManaCost);

        if (enoughMana)
        {
            Vector3 spawnPosition =
                player.GetComponent<PlayerController>().TargetPos
                + player.GetComponent<PlayerController>().FaceDirection * distance;

            switch (skillSO.Name)
            {
                case "Fire":
                    GameObject firePrefabInstance = Instantiate(
                        firePrefab,
                        spawnPosition,
                        Quaternion.identity
                    );
                    Destroy(firePrefabInstance, 1);
                    break;

                case "Water":
                    GameObject waterPrefabInstance = Instantiate(
                        waterPrefab,
                        spawnPosition,
                        Quaternion.identity
                    );
                    Destroy(waterPrefabInstance, 1);
                    break;

                case "Lightning":
                    GameObject lightningPrefabInstance = Instantiate(
                        lightningPrefab,
                        spawnPosition,
                        Quaternion.identity
                    );
                    Destroy(lightningPrefabInstance, 1);
                    break;
            }
        }
    }
}
