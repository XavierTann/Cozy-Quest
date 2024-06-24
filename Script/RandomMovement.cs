using System;
using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeInterval;

    private Vector2 moveDirection;

    private void Start() {
        StartCoroutine(ChangeDirectionRoutine());
    }

    public void HandleUpdate() {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true) {
            yield return new WaitForSeconds(changeInterval);
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        // Get a random direction
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        float radian = randomAngle * Mathf.Deg2Rad;
        moveDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;
    }

    public Vector2 GetMoveDirection
    {
        get { return moveDirection; }
    }

    public float GetMoveSpeed
    {
        get { return moveSpeed; }
    }
}
