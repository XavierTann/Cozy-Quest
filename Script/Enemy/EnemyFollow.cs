using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyFollow : MonoBehaviour {
    // Will be attached to each enemy

    [SerializeField] private LayerMask playerLayer;

    private Vector2 followDirection;

    private new Collider2D collider;

    public bool DetectedPlayer() {
        float enemyDetectRange = GetComponent<IStats>().DetectRange;
        collider = Physics2D.OverlapCircle(transform.position, enemyDetectRange, playerLayer);
        if (collider != null) {
            Debug.Log("Detected!");
            return true;
        }

        return false;

    }

    public void FollowPlayer() {
        followDirection = (collider.transform.position - gameObject.transform.position).normalized;
        Debug.Log(followDirection);
        float enemySpeed = gameObject.GetComponent<EnemyStats>().MovementSpeed;
        transform.Translate(followDirection * enemySpeed * Time.deltaTime, Space.World);
    }

    public Vector2 GetFollowDirection {get {return followDirection;}}
}