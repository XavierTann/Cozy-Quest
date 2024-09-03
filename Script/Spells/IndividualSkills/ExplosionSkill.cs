using System;
using Unity;
using UnityEngine;

public class ExplosionSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    GameObject explosionPrefab;

    [SerializeField]
    GameObject player;

    [SerializeField]
    private float distance = 3;

    public void ActivateSkill()
    {
        Vector3 spawnPosition =
            player.GetComponent<PlayerController>().TargetPos
            + player.GetComponent<PlayerController>().FaceDirection * distance;

        Debug.Log("Activated water skill!");

        Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);
        // Activate animation
    }
}
