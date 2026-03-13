using System;
using UnityEngine;

// 역할: 보스 HP, 피격, 사망 이벤트만 담당
public class BossBase : MonoBehaviour
{
    [SerializeField] protected float maxHp = 500f;
    [SerializeField] protected float attackPower = 15f;
    protected float currentHp;

    public float AttackPower => attackPower;

    public event Action onBossDefeated;

    private void OnEnable()
    {
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
        onBossDefeated?.Invoke();
        gameObject.SetActive(false);
    }
}
