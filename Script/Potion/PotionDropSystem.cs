using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PotionDropSystem : MonoBehaviour
{
    public static PotionDropSystem Instance { get; private set; }

    Vector3 randomPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void SetRandomPosition(GameObject entity, int maxDropDistance)
    {
        Vector3 circleOrigin = entity.transform.position;
        Vector3 randomPointInCircle = UnityEngine.Random.insideUnitCircle * maxDropDistance;
        randomPosition = circleOrigin + randomPointInCircle;
    }

    private void SpawnPotion(GameObject potionPrefab)
    {
        Instantiate(potionPrefab, randomPosition, Quaternion.identity);
    }

    public void DropPotion(GameObject entity, int maxDropDistance, GameObject potionPrefab)
    {
        for (int i = 0; i < 1; i++)
        {
            SetRandomPosition(entity, maxDropDistance);
            SpawnPotion(potionPrefab);
        }
    }
}
