using UnityEngine;

// 역할: Rigidbody2D velocity로 왼쪽 이동만 담당
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private bool isMoving = true;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (!isMoving) return;
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

    public void SetMoving(bool value)
    {
        isMoving = value;
        if (!value) rb.linearVelocity = Vector2.zero;
    }
}
