using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    public PlayerInputActionMap playerInputActionMap;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator animator;

    [SerializeField]
    private LayerMask collisionLayer;

    [SerializeField]
    private LayerMask interactableLayer;

    [SerializeField]
    private LayerMask waterLayer;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private LayerMask collectiblesLayer;

    [SerializeField]
    private GameObject inventoryUI;

    [SerializeField]
    private float pickUpRange = 1f;

    private Vector3 targetPos;
    public Vector3 TargetPos
    {
        get { return targetPos; }
    }
    private Vector3 interactPos;
    public Vector3 InteractPos
    {
        get { return interactPos; }
    }

    private Vector3 faceDirection;
    public Vector3 FaceDirection
    {
        get { return faceDirection; }
    }

    private void Awake()
    {
        // Movement
        playerInputActionMap = new PlayerInputActionMap();
        rb = GetComponent<Rigidbody2D>();

        // Animator
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Enable the input action map
        playerInputActionMap.Enable();

        playerInputActionMap.Player.Attack.performed += _ => OnAttack();
        playerInputActionMap.Player.OpenInventory.performed += _ => OpenInventory();
    }

    private void OnDisable()
    {
        // Disable the input action map
        playerInputActionMap.Disable();
    }

    public void HandleUpdate()
    {
        Move();
        MoveAnimation();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (ItemDetected() != null)
        {
            Collider2D collider = ItemDetected();

            if (collider.CompareTag("Coin"))
            {
                CollectCoin();
            }
            if (collider.CompareTag("Potion"))
            {
                CollectPotion();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (PotionSystem.Instance.potionSO.Count > 0)
            {
                PotionSystem.Instance.OnPotionUsed();
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SkillTreeUI.Instance.OpenSkillTree();
        }
    }

    private void Move()
    {
        movement = playerInputActionMap.Player.Move.ReadValue<Vector2>();
        targetPos = rb.position + movement * moveSpeed * Time.deltaTime;
        if (movement.x != 0 || movement.y != 0)
        {
            faceDirection = new Vector3(movement.x, movement.y);
        }

        if (IsWalkable(targetPos))
        {
            rb.MovePosition(targetPos);
        }
    }

    private void MoveAnimation()
    {
        if (movement != Vector2.zero)
        {
            // Moving
            if (movement.x != 0)
                movement.y = 0;

            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }
        else
        {
            // Not Moving
            animator.SetBool("isMoving", false);
        }
    }

    private void Interact()
    {
        interactPos = transform.position + faceDirection;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    private void OnAttack()
    {
        Attack();
        AttackAnimation();
    }

    private void Attack()
    {
        GameObject attackee = AttackSystem.Instance.DetectAttack(
            gameObject,
            enemyLayer,
            faceDirection
        );
        if (attackee != null)
        {
            AttackSystem.Instance.PerformAttack(gameObject, attackee);
        }
    }

    private void AttackAnimation()
    {
        animator.SetTrigger("isAttacking");
        if (movement != Vector2.zero)
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }
    }

    private void OpenInventory()
    {
        inventoryUI.GetComponent<InventoryUI>().DisplayInventory();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (
            Physics2D.OverlapCircle(
                targetPos,
                0.1f,
                collisionLayer | interactableLayer | waterLayer
            ) != null
        )
        {
            return false;
        }
        return true;
    }

    private Collider2D ItemDetected()
    {
        var collider = Physics2D.OverlapCircle(
            gameObject.transform.position,
            pickUpRange,
            collectiblesLayer
        );
        if (collider != null)
        {
            return collider;
        }
        return null;
    }

    private void CollectCoin()
    {
        // Logic to remove coin prefab
        GameObject coin = ItemDetected().gameObject;
        Destroy(coin);

        // Logic to add coins to player coins system
        CoinSystem.Instance.EarnCoins(1);
    }

    private void CollectPotion()
    {
        GameObject potion = ItemDetected().gameObject;

        Destroy(potion);

        PotionSystem.Instance.OnPotionObtained?.Invoke();
    }
}
