using System.Collections;
using UnityEngine;

// 역할: 적 넉백 적용 및 다른 적과 충돌/인접 시 연쇄 넉백만 담당
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 12f;
    [SerializeField] private float knockbackDuration = 0.4f;
    [SerializeField] private float chainRadius = 0.8f;  // 인접 적 감지 반경
    [SerializeField] private EnemyMover enemyMover;

    private Rigidbody2D rb;
    private bool isBeingKnockedBack = false;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    public void ApplyKnockback(Vector2 direction)
    {
        if (isBeingKnockedBack) return;

        // 재귀 방지를 위해 먼저 플래그 설정
        isBeingKnockedBack = true;

        // 이미 붙어있는 적들에게 즉시 연쇄 전파 (OnCollisionEnter2D가 안 터지는 경우 대응)
        PropagateToNearbyEnemies();

        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(direction));
    }

    private void PropagateToNearbyEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, chainRadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject) continue;
            if (!hit.CompareTag("Enemy")) continue;
            hit.GetComponent<EnemyKnockback>()?.ApplyKnockback(Vector2.right);
        }
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        enemyMover.SetMoving(false);
        rb.linearVelocity = direction * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;
        isBeingKnockedBack = false;
        enemyMover.SetMoving(true);
    }

    // 넉백 중 날아가다가 아직 안 닿은 뒤쪽 적과 충돌 시 연쇄
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBeingKnockedBack) return;
        if (!collision.gameObject.CompareTag("Enemy")) return;
        collision.gameObject.GetComponent<EnemyKnockback>()?.ApplyKnockback(Vector2.right);
    }
}
