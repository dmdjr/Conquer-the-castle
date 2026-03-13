using UnityEngine;

// 역할: 적과의 충돌 감지, 방어 여부에 따라 피해/방어/적 넉백 처리만 담당
public class PlayerDamageReceiver : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private DefenseSystem defenseSystem;
    [SerializeField] private PlayerInputData inputData;
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
        if (!collision.gameObject.CompareTag("Enemy")) return;

        // 적과 닿으면 무조건 돌진 중단
        inputData.SetCharging(false);

        if (isInvincible) return;

        Vector2 hitDir = ((Vector2)(transform.position - collision.transform.position)).normalized;

        // 방어 중이면 HP 무감소 + 적 앞으로 연쇄 넉백
        if (defenseSystem.TryBlock(hitDir))
        {
            EnemyKnockback enemyKnockback = collision.gameObject.GetComponent<EnemyKnockback>();
            enemyKnockback?.ApplyKnockback(Vector2.right);
            invincibleTimer = invincibleDuration;
            return;
        }

        // 일반 피격: HP 감소
        float damage = collision.gameObject.GetComponent<EnemyBase>()?.AttackPower ?? 5f;
        playerStats.TakeDamage(damage);
        invincibleTimer = invincibleDuration;
    }
}
