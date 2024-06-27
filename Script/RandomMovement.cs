using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f; // Movement speed
    [SerializeField] private float changeDirectionTime = 2f; // Time to change direction
    [SerializeField] private LayerMask collisionLayer; // Collision layer mask for obstacles

    private Vector2 movementDirection;
    private float changeDirectionTimer;

    private void Start()
    {
        PickRandomDirection();
    }

    private void Update()
    {
        HandleUpdate();
    }

    public void HandleUpdate()
    {
        Move();
        UpdateDirectionTimer();
    }

    public void PickRandomDirection()
    {
        // Pick a random direction in 2D space
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        changeDirectionTimer = changeDirectionTime;
    }

    private void Move()
    {
        if (IsWalkable())
        {
            transform.Translate((Vector3)movementDirection * speed * Time.deltaTime, Space.World);
        }

    }

    private bool IsWalkable()
    {
        Vector2 targetPos = (Vector2)transform.position + movementDirection * speed * Time.deltaTime;
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.1f, collisionLayer);
        return collider == null;
    }

    private void UpdateDirectionTimer()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0f)
        {
            PickRandomDirection();
        }
    }

    public Vector2 MoveDirection
    {
        get { return movementDirection; }
    }

    public float MoveSpeed
    {
        get { return speed; }
    }
}
