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

    public Action OnOpenSkillTree;
    public Action OnHideSkillTree;

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

    public void HandleUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            HideUI();
        }
    }

    private void LearnSkill(SkillSO skillSO)
    {
        SkillSystem.Instance.LearnSkill(skillSO);
    }

    public void OpenSkillTree()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        OnOpenSkillTree?.Invoke();

    }

    private void UpdateUI()
    {
        // Update UI to show when the skill is learnt
    }

    private void HideUI() {
        gameObject.SetActive(false);
        OnHideSkillTree?.Invoke();
    }
}
