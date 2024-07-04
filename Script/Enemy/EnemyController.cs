using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class EnemyController : MonoBehaviour {

    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int maxDropDistance;
    [SerializeField] private GameObject coinPrefab;

    private RandomMovement randomMovement;
    private Animator animator;

    private bool canAttack = true;
    private float attackCooldown = 2.0f;

    private Vector2 faceDirection;

    private void Start() {
        randomMovement = GetComponent<RandomMovement>();
        animator = GetComponent<Animator>();
        // GetComponent<HealthSystem>().OnDeath += DropCoinsOnDeath;
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
        EnemyFollow enemyFollowScript = GetComponent<EnemyFollow>();
        if (enemyFollowScript.DetectedPlayer()) {
            enemyFollowScript.FollowPlayer(); 
            faceDirection = enemyFollowScript.GetFollowDirection;    
        }

        else {
            // Random movement
            randomMovement.HandleUpdate();
            faceDirection = randomMovement.MoveDirection;
        }
        
    }


    private void Animate()
    {

        if (faceDirection != Vector2.zero) {
            animator.SetBool("isMoving", true);
            animator.SetFloat("MoveX", faceDirection.x);
            animator.SetFloat("MoveY", faceDirection.y);
        }

        else {
            animator.SetBool("isMoving", false);
        }
    }

    private void Attack()
    {
        GameObject attackee = AttackSystem.Instance.DetectAttack(gameObject, playerLayer, faceDirection);
        if (attackee != null) {
            AttackSystem.Instance.PerformAttack(gameObject, attackee);
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void DropCoinsOnDeath(object sender, EventArgs e) {
        CoinDropSystem.Instance.DropCoins(gameObject, coinsDroppedOnDeath, maxDropDistance, coinPrefab);
    }
}