using UnityEngine;

// 역할: IsCharging 상태를 읽어 Rigidbody2D로 돌진 이동만 담당
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharger : MonoBehaviour
{
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private float chargeSpeed = 25f;

    private Rigidbody2D rb;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (inputData.IsKnockedBack) return;

        float vx = inputData.IsCharging ? chargeSpeed : 0f;
        rb.linearVelocity = new Vector2(vx, rb.linearVelocity.y);
    }
}
