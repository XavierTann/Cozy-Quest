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

    private List<SpellSO> learntSpellsList;

    private void Start()
    {
        SpellSystem.Instance.OnLearnSpell += OnLearntSpell;
    }

    private void OnLearntSpell()
    {
        UpdateUI();
        UpdateButtonListener();
    }

    private void UpdateUI()
    {
        learntSpellsList = SpellSystem.Instance.LearntSpellsList;

        for (int i = 0; i < skillSlotList.Count; i++)
        {
            int index = i;
            if (learntSpellsList.Count >= index + 1)
            {
                skillSlotList[index].SetActive(true);
                skillSlotList[index].transform.GetChild(1).GetComponent<Image>().sprite =
                    learntSpellsList[index].Sprite;
            }
        }
    }

    private void UpdateButtonListener()
    {
        learntSpellsList = SpellSystem.Instance.LearntSpellsList;

        for (int i = 0; i < skillSlotList.Count; i++)
        {
            int index = i;
            if (learntSpellsList.Count >= index + 1)
            {
                skillSlotList[index]
                    .GetComponent<Button>()
                    .onClick.AddListener(() =>
                    {
                        SpellSystem.Instance.ActivateSpell(learntSpellsList[index]);
                    });
            }
        }
    }
}
