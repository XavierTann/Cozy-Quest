using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpellSystem : MonoBehaviour
{
    public static SpellSystem Instance { get; private set; }

    [SerializeField]
    private GameObject firePrefab;

    [SerializeField]
    private GameObject waterPrefab;

    [SerializeField]
    private GameObject lightningPrefab;

    [SerializeField]
    private GameObject player;

    private List<SpellSO> learntSpellsList = new();
    public List<SpellSO> LearntSpellsList
    {
        get { return learntSpellsList; }
    }

    public Action OnLearnSpell; // This event should pass in the skill that you learnt and enable that particular skill in the UI.

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void LearnSpell(SpellSO spell)
    {
        if (spell.SkillPointsRequired <= SkillSystem.Instance.SkillPoints)
        {
            SkillSystem.Instance.LoseSkillPoint(spell.SkillPointsRequired);
            learntSpellsList.Add(spell);
            spell.HasLearnt = true;
            OnLearnSpell?.Invoke();
            Debug.Log($"Learnt {spell.Name} Spell!");
        }
        else
        {
            Debug.Log("Not enough points!");
        }
    }

    public void ActivateSpell(SpellSO spell)
    {
        bool enoughMana = ManaSystem.Instance.UseMana(spell.ManaCost);

        if (enoughMana)
        {
            Vector3 spawnPosition =
                player.GetComponent<PlayerController>().TargetPos
                + player.GetComponent<PlayerController>().FaceDirection * spell.Range;

            // Damage enemies
            AttackSystem.Instance.DetectSpell(spell, spawnPosition);

            switch (spell.Name)
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
