using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointUI : MonoBehaviour
{
    [SerializeField]
    GameObject skillPointCounter;

    private void Awake()
    {
        skillPointCounter.GetComponentInChildren<Text>().text = 0.ToString();
        SkillTreeUI.Instance.OnOpenSkillTree += UpdateUI;
        SpellSystem.Instance.OnLearnSpell += UpdateUI;
    }

    private void UpdateUI()
    {
        int skillPoints = SkillSystem.Instance.SkillPoints;
        skillPointCounter.GetComponentInChildren<Text>().text = skillPoints.ToString();
    }
}
