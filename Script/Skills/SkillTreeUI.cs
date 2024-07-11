using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SkillTreeUI : MonoBehaviour
{
    public static SkillTreeUI Instance { get; private set; }

    [SerializeField]
    List<Button> skillButtonList;

    [SerializeField]
    List<SkillSO> skillSOList;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        for (int i = 0; i < skillButtonList.Count; i++)
        {
            int index = i;
            skillButtonList[index]
                .GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    LearnSkill(skillSOList[index]);
                });
        }
    }

    private void LearnSkill(SkillSO skillSO)
    {
        SkillSystem.Instance.LearnSkill(skillSO);
    }

    public void OpenSkillTree()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void UpdateUI()
    {
        // Update UI to show when the skill is learnt
    }
}
