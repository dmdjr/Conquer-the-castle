using UnityEngine;

// 역할: 공격 입력을 받아 히트박스로 적에게 데미지 처리만 담당
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float attackRangeX = 1.5f;  // 히트박스 가로
    [SerializeField] private float attackRangeY = 1f;    // 히트박스 세로
    [SerializeField] private LayerMask enemyLayer;

    private void OnEnable()
    {
        inputData.onAttackRequested += PerformAttack;
    }

    private void OnDisable()
    {
        inputData.onAttackRequested -= PerformAttack;
    }

    private void PerformAttack()
    {
        Vector2 attackCenter = (Vector2)transform.position
            + Vector2.right * inputData.FacingDirection * (attackRangeX / 2f);
        Vector2 attackSize = new Vector2(attackRangeX, attackRangeY);

        var hits = Physics2D.OverlapBoxAll(attackCenter, attackSize, 0f, enemyLayer);
        foreach (var hit in hits)
        {
            hit.GetComponent<EnemyBase>()?.TakeDamage(playerStats.AttackPower);
            hit.GetComponent<BossBase>()?.TakeDamage(playerStats.AttackPower);
        }
    }

    // 씬 뷰에서 히트박스 확인용
    private void OnDrawGizmosSelected()
    {
        if (inputData == null) return;
        Gizmos.color = Color.red;
        Vector2 center = (Vector2)transform.position
            + Vector2.right * inputData.FacingDirection * (attackRangeX / 2f);
        Gizmos.DrawWireCube(center, new Vector3(attackRangeX, attackRangeY, 0f));
    }
}
