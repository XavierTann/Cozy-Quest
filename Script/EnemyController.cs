using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : MonoBehaviour {

    private RandomMovement randomMovement;
    private Animator animator;
    private EnemyStats enemyStats;

    private void Start() {
        randomMovement = GetComponent<RandomMovement>();
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update() {
        Move();
        Animate();
    }

     private void Move()
    {
        randomMovement.HandleUpdate();
    }

    private void Animate()
    {
        Vector2 movement = randomMovement.GetMoveDirection;

        if (movement != Vector2.zero) {
            animator.SetBool("isMoving", true);
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }

        else {
            animator.SetBool("isMoving", false);
        }
    }
   
}