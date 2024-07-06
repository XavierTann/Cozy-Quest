using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class CoinDropSystem : MonoBehaviour {

    public static CoinDropSystem Instance {get; private set;}

    Vector3 randomPosition;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void SetRandomPosition(GameObject entity, int maxDropDistance) {
        Vector3 circleOrigin = entity.transform.position;
        Vector3 randomPointInCircle = UnityEngine.Random.insideUnitCircle * maxDropDistance;
        randomPosition = circleOrigin + randomPointInCircle;
        
    }

    private void SpawnCoin(GameObject coinPrefab) {
        Instantiate(coinPrefab, randomPosition, Quaternion.identity);
    }

    public void DropCoins( GameObject entity, int numberOfCoinsDropped, int maxDropDistance, GameObject coinPrefab) {
        for (int i = 0; i < numberOfCoinsDropped; i++) {
            SetRandomPosition(entity, maxDropDistance);
            SpawnCoin(coinPrefab);
        }
    }



}