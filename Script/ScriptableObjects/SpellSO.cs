using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "SpellSO")]
public class SpellSO : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int skillPointsRequired;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int manaCost;

    [SerializeField]
    private float areaOfEffect;

    [SerializeField]
    private float range;

    [NonSerialized]
    private bool hasLearnt = false;

    // Properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    public GameObject Prefab
    {
        get { return prefab; }
    }

    public int SkillPointsRequired
    {
        get { return skillPointsRequired; }
        set { skillPointsRequired = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int ManaCost
    {
        get { return manaCost; }
        set { manaCost = value; }
    }

    public bool HasLearnt
    {
        get { return hasLearnt; }
        set { hasLearnt = value; }
    }

    public float AreaOfEffect
    {
        get { return areaOfEffect; }
    }

    public float Range
    {
        get { return range; }
    }
}
