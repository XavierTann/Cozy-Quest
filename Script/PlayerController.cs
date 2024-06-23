using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private PlayerInputActionMap playerInputActionMap;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator animator;

    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask enemyLayer;

    private Vector3 targetPos;
    private Vector3 interactPos;
    private Vector3 faceDirection;
    
    private float enemyRange = 0.5f;
    private float enemyDamage = 10f;
    public event EventHandler<EnemyAttackEventArgs> EnemyAttack;


    private void Awake() {
        // Movement
        playerInputActionMap = new PlayerInputActionMap();
        rb = GetComponent<Rigidbody2D>();

        // Animator
        animator = GetComponent<Animator>();
        playerInputActionMap.Enable();

        // Listen to enemy attack event
        EnemyAttack += OnEnemyAttack;
    }

   
    public void HandleUpdate() {
        Move();
        Animate();

        if (Input.GetKeyDown(KeyCode.E)) {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("Attacking");
            Attack();
        }
    }


    

    private void Move() {
        movement = playerInputActionMap.Movement.Move.ReadValue<Vector2>();
        targetPos = rb.position + movement * moveSpeed * Time.deltaTime;

        if (IsWalkable(targetPos)) {
            rb.MovePosition(targetPos);
        }

        if (EnemyAttacks(enemyRange)) {
            EnemyAttack?.Invoke(this, new EnemyAttackEventArgs(enemyRange, enemyDamage));
        }
    }

    private void Animate() {
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
        faceDirection = new Vector3(movement.x, movement.y);
        interactPos = transform.position + faceDirection;
        
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }


    private void Attack()
    {
        GameObject playerGameObject = gameObject;
        if (playerGameObject == null) {
            Debug.Log ("No object detected");
        }
        else {
            Debug.Log(playerGameObject);
            AttackSystem.Instance.PerformAttack(playerGameObject);
        }
        
    }



    private bool IsWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, collisionLayer | interactableLayer) != null) {
            return false;
        }
        return true;
    }

    public Vector3 TargetPos {
        get {return targetPos;}
    }

    /* Enemy Code */
    private bool EnemyAttacks(float enemyRange)
    {
        if (Physics2D.OverlapCircle(targetPos, enemyRange, enemyLayer)) {
            return true;
        }
        return false;
    }

     private void OnEnemyAttack(object sender, EnemyAttackEventArgs e)
    {
        GetComponent<HealthSystem>().TakeDamage(e.EnemyDamage);
    }


    // Event Args for Enemy Attack Event
    public class EnemyAttackEventArgs : EventArgs
{
    public float EnemyDamage { get; }
    public float EnemyRange { get; }

    public EnemyAttackEventArgs(float enemyAttackDamage, float enemyRange)
    {
        EnemyDamage = enemyAttackDamage;
        EnemyRange = enemyRange;
    }
}

 /* End of Enemy Code */

}
