using System;
using UnityEngine;

// 역할: 적과의 충돌 감지 및 무적 시간 적용 후 PlayerStats에 피해 전달만 담당
public class PlayerDamageReceiver : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float invincibleDuration = 1f;

    public event Action<Vector2> onHit; // 넉백 방향 전달

    private float invincibleTimer = 0f;
    private bool isInvincible => invincibleTimer > 0f;

    private void Update()
    {
        if (invincibleTimer > 0f)
            invincibleTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) => HandleCollision(collision);

    private void OnCollisionStay2D(Collision2D collision) => HandleCollision(collision);

    private void HandleCollision(Collision2D collision)
    {
        if (isInvincible) return;
        if (!collision.gameObject.CompareTag("Enemy")) return;

        float damage = collision.gameObject.GetComponent<EnemyBase>()?.AttackPower ?? 5f;
        playerStats.TakeDamage(damage);
        invincibleTimer = invincibleDuration;

        Vector2 knockbackDir = ((Vector2)(transform.position - collision.transform.position)).normalized;
        onHit?.Invoke(knockbackDir);
    }
}
