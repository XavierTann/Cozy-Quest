using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : MonoBehaviour {

    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask waterLayer;
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
        Vector2 currentPos = new(transform.position.x, transform.position.y);
        Vector2 newPos = currentPos + (randomMovement.GetMoveDirection * randomMovement.GetMoveSpeed * Time.deltaTime);
        if (IsWalkable(newPos))
        {   
            randomMovement.HandleUpdate();
        }
        
    }

    private bool IsWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, collisionLayer | interactableLayer | waterLayer) != null) {
            return false;
        }
        return true;
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