using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int skillPointsRequired;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int manaCost;

    private bool hasLearnt;

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
}
