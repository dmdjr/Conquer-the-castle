using UnityEngine;

// 역할: 적과의 충돌 감지, 방어 여부에 따라 피해 또는 방어 처리만 담당
public class PlayerDamageReceiver : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private DefenseSystem defenseSystem;
    [SerializeField] private float invincibleDuration = 1f;

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

        Vector2 hitDir = ((Vector2)(transform.position - collision.transform.position)).normalized;

        // 방어 중이면 피해 없이 넉백만
        if (defenseSystem.TryBlock(hitDir))
        {
            invincibleTimer = invincibleDuration;
            return;
        }

        // 일반 피격: HP만 감소 (넉백 없음)
        float damage = collision.gameObject.GetComponent<EnemyBase>()?.AttackPower ?? 5f;
        playerStats.TakeDamage(damage);
        invincibleTimer = invincibleDuration;
    }
}
