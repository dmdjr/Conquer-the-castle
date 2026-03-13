using System.Collections;
using UnityEngine;

// 역할: 피격 이벤트를 받아 Rigidbody2D에 넉백 힘 적용만 담당
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private PlayerDamageReceiver damageReceiver;
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private float knockbackForce = 8f;
    [SerializeField] private float knockbackDuration = 0.2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputData.SetKnockedBack(false); // ScriptableObject 런타임 상태 초기화
    }

    private void OnEnable()
    {
        damageReceiver.onHit += ApplyKnockback;
    }

    private void OnDisable()
    {
        damageReceiver.onHit -= ApplyKnockback;
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
