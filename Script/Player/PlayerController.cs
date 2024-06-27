using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private PlayerInputActionMap playerInputActionMap;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator animator;

    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask collectiblesLayer;

    [SerializeField] private float pickUpRange = 1f;

    private Vector3 targetPos;
    private Vector3 interactPos;
    private Vector3 faceDirection;
    
    private void Awake() {
        // Movement
        playerInputActionMap = new PlayerInputActionMap();
        rb = GetComponent<Rigidbody2D>();

        // Animator
        animator = GetComponent<Animator>();
        playerInputActionMap.Enable();
    }

   
    public void HandleUpdate() {
        Move();
        MoveAnimation();

        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Attack();
            AttackAnimation();
        }

        if (Input.GetKeyUp(KeyCode.F)){
            animator.SetBool("isAttacking", false);
        }

        if (CoinsDetected() != null) {

            // Logic to remove coin prefab
            GameObject coin = CoinsDetected().gameObject;
            Destroy(coin);

            // Logic to add coins to player coins system
            CoinSystem.Instance.EarnCoins(1);

            Debug.Log("Coins Collected!");
        }

    }


    

    private void Move() {
        movement = playerInputActionMap.Movement.Move.ReadValue<Vector2>();
        targetPos = rb.position + movement * moveSpeed * Time.deltaTime;
        faceDirection = new Vector3(movement.x, movement.y);

        if (IsWalkable(targetPos)) {
            rb.MovePosition(targetPos);
        }
    }

    private void MoveAnimation() {
        if (movement != Vector2.zero) {
            // Moving
            if (movement.x != 0) movement.y = 0;

            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }
        else {
            // Not Moving
            animator.SetBool("isMoving", false);

        }
        
    }

    private void Interact()
    {
        interactPos = transform.position + faceDirection;
        
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }


    private void Attack()
    {
        GameObject attackee = AttackSystem.Instance.DetectAttack(gameObject, enemyLayer, faceDirection);
        if (attackee != null) {
            AttackSystem.Instance.PerformAttack(gameObject, attackee);
        }
        
    }

    private void AttackAnimation() {
        animator.SetBool("isAttacking", true);
        if (movement != Vector2.zero) {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }

       
    }


    private bool IsWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, collisionLayer | interactableLayer | waterLayer) != null) {
            return false;
        }
        return true;
    }

    public Vector3 TargetPos {
        get {return targetPos;}
    }

    private Collider2D CoinsDetected() {
        var collider = Physics2D.OverlapCircle(gameObject.transform.position, pickUpRange, collectiblesLayer);
        if (collider != null) {
            return collider;
        } 
        return null;
    }

}
