using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> skillSlotList;

    private List<SkillSO> learntSkillsList;

    private void Start()
    {
        SkillSystem.Instance.OnLearnSkill += OnLearntSkill;
    }

    private void OnLearntSkill()
    {
        UpdateUI();
        UpdateButtonListener();
    }

    private void UpdateUI()
    {
        learntSkillsList = SkillSystem.Instance.learntSkillsList;

        for (int i = 0; i < skillSlotList.Count; i++)
        {
            int index = i;
            if (learntSkillsList.Count >= index + 1)
            {
                skillSlotList[index].SetActive(true);
                skillSlotList[index].GetComponentInChildren<TextMeshProUGUI>().text = learntSkillsList[index].Name;
                skillSlotList[index].GetComponent<Image>().sprite = learntSkillsList[index].Sprite;
            }
        }
        Debug.Log("Updated UI!");
    }

    private void UpdateButtonListener()
    {
        learntSkillsList = SkillSystem.Instance.learntSkillsList;

        for (int i = 0; i < skillSlotList.Count; i++)
        {
            int index = i;
            if (learntSkillsList.Count >= index + 1)
            {
                skillSlotList[index]
                    .GetComponent<Button>()
                    .onClick.AddListener(() =>
                    {
                        ActivateSkill(learntSkillsList[index]);
                    });
            }
        }
    }

    private void ActivateSkill(SkillSO skillSO)
    {
        Debug.Log($"{skillSO.Name} is activated!");
        ManaSystem.Instance.UseMana(skillSO.ManaCost);
    }
}
