using System.Collections;
using UnityEngine;

// 역할: 방어 성공 이벤트를 받아 Rigidbody2D에 큰 넉백 힘 적용만 담당
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private DefenseSystem defenseSystem;
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private float knockbackForce = 15f;  // 방어 넉백은 크게
    [SerializeField] private float knockbackDuration = 0.3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputData.SetKnockedBack(false);
    }

    private void OnEnable()
    {
        defenseSystem.onBlocked += ApplyKnockback;
    }

    private void OnDisable()
    {
        defenseSystem.onBlocked -= ApplyKnockback;
    }

    private void ApplyKnockback(Vector2 direction)
    {
        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(direction));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        inputData.SetKnockedBack(true);
        rb.linearVelocity = direction * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        inputData.SetKnockedBack(false);
    }
}
