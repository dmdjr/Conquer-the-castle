using UnityEngine;

// 역할: PlayerInputData를 읽어 플레이어 이동 및 방향 처리만 담당
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (inputData.IsKnockedBack) return;
        rb.linearVelocity = new Vector2(inputData.MoveDirection * moveSpeed, rb.linearVelocity.y);
        inputData.SetFacingDirection(inputData.MoveDirection);
    }
}
