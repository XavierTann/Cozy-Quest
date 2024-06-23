using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : MonoBehaviour {

    [SerializeField] private LayerMask playerLayer;

    private RandomMovement randomMovement;
    private Animator animator;

    private bool canAttack = true;
    private float attackCooldown = 2.0f;


    private void Start() {
        randomMovement = GetComponent<RandomMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        Move();
        Animate();

        if (canAttack) {
            Attack();
        }
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

    private void Attack()
    {
        GameObject attackee = AttackSystem.Instance.DetectAttack(gameObject, playerLayer);
        if (attackee != null) {
            AttackSystem.Instance.PerformAttack(gameObject, attackee);
            StartCoroutine(CanAttack());
        }
    }

    private IEnumerator CanAttack() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}