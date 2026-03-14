using System;
using UnityEngine;

// 역할: 적의 HP, 피격, 사망 처리만 담당
public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float maxHp = 30f;
    [SerializeField] protected float attackPower = 5f;
    [SerializeField] protected int expReward = 10;
    protected float currentHp;

    public float AttackPower => attackPower;
    public int ExpReward => expReward;

    public event Action<Vector3> onDied;  // EnemyDropper, EnemyAnimator가 구독
    public event Action          onHit;   // EnemyAnimator가 구독 (피격 애니메이션용)

    private EnemyPool pool;
    private Action onDefeatedCallback;
    private bool isDead = false;

    public void Init(EnemyPool pool, Action onDefeatedCallback)
    {
        this.pool = pool;
        this.onDefeatedCallback = onDefeatedCallback;
        currentHp = maxHp;
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHp -= amount;
        if (currentHp <= 0f)
            Die();
        else
            onHit?.Invoke();
    }

    protected virtual void Die()
    {
        isDead = true;
        onDied?.Invoke(transform.position);   // 드롭 위치 전달 + 애니메이션 트리거
        onDefeatedCallback?.Invoke();          // 스테이지 진행도 + EXP 처리
        // 풀 반환은 EnemyAnimator가 사망 애니메이션 후 Deactivate() 호출
    }

    // EnemyAnimator가 사망 애니메이션 끝난 후 호출
    public void Deactivate()
    {
        pool?.ReturnToPool(gameObject);
    }
}
