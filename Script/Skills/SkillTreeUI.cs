using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
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
    List<SpellSO> spellSOList;

    public Action OnOpenSkillTree;
    public Action OnHideSkillTree;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        for (int i = 0; i < math.min(skillButtonList.Count, spellSOList.Count); i++)
        {
            int index = i; // index is needed because index allows the integer to be stored in local scope
            skillButtonList[index]
                .GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    SpellSystem.Instance.LearnSpell(spellSOList[index]);
                });

            skillButtonList[index].transform.GetChild(1).GetComponent<Image>().sprite = spellSOList[
                index
            ].Sprite;
        }
    }

    private void Start()
    {
        SpellSystem.Instance.OnLearnSpell += UpdateUI;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUI();
        }
    }

    public void OpenSkillTree()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        OnOpenSkillTree?.Invoke();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < math.min(skillButtonList.Count, spellSOList.Count); i++)
        {
            if (spellSOList[i].HasLearnt == true)
            {
                // Grey out the sprite
                skillButtonList[i].transform.GetChild(1).GetComponent<Image>().color = Color.gray;

                // Disable onclick
                skillButtonList[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }

    private void HideUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        OnHideSkillTree?.Invoke();
    }
}
