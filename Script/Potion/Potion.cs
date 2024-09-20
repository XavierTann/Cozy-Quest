using UnityEngine;

public class Potion : MonoBehaviour, ICollectible
{
    public PotionSO potionData;

    public void Collect()
    {
        // Use the data from the ScriptableObject
        switch (potionData.potionType)
        {
            case PotionType.HealthPotion:
                PotionSystem.Instance.ObtainPotion("HealthPotion");
                Debug.Log("Collected health pot");
                break;

            case PotionType.ManaPotion:
                Debug.Log("Collected mana pot");
                PotionSystem.Instance.ObtainPotion("ManaPotion");

                // To make UI for mana potion and link logic to there.
                break;
        }
    }
}
