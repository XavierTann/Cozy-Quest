using System;
using Unity;
using UnityEngine;

public class LightningSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    GameObject lightningPrefab;

    [SerializeField]
    GameObject player;

    [SerializeField]
    private float distance = 3;

    public void ActivateSkill()
    {
        Vector3 spawnPosition =
            player.GetComponent<PlayerController>().TargetPos
            + player.GetComponent<PlayerController>().FaceDirection * distance;

        Debug.Log("Activated lightning skill!");

        Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);
        // Activate animation
    }
}
