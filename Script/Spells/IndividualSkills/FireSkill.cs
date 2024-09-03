using System;
using Unity;
using UnityEngine;

public class FireSkill : MonoBehaviour, ISkill
{
    [SerializeField]
    GameObject firePrefab;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float distance = 3;

    public void ActivateSkill()
    {
        Vector3 spawnPosition =
            player.GetComponent<PlayerController>().TargetPos
            + player.GetComponent<PlayerController>().FaceDirection * distance;

        Debug.Log("Activated fire skill!");

        Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        // Activate animation
    }
}
