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

    private EnemyPool pool;
    private Action onDefeatedCallback;

    // 풀에서 꺼낼 때 호출 - 상태 초기화 및 콜백 등록
    public void Init(EnemyPool pool, Action onDefeatedCallback)
    {
        this.pool = pool;
        this.onDefeatedCallback = onDefeatedCallback;
        currentHp = maxHp;
    }

    public void TakeDamage(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0f)
            Die();
    }

    protected virtual void Die()
    {
        onDefeatedCallback?.Invoke();
        pool.ReturnToPool(gameObject);
    }
}
