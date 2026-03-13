using System;
using UnityEngine;

// 역할: 방어 상태 관리, 쿨타임, 1회 피격 흡수 처리만 담당
public class DefenseSystem : MonoBehaviour
{
    [SerializeField] private float cooldown = 1f;

    public bool IsDefending { get; private set; }
    public event Action<Vector2> onBlocked; // 방어 성공 시 넉백 방향 전달

    private float cooldownTimer = 0f;
    private bool isCooldown => cooldownTimer > 0f;

    private void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    public void Activate()
    {
        if (isCooldown || IsDefending) return;
        IsDefending = true;
    }

    // PlayerDamageReceiver에서 피격 시 호출 → 방어 중이면 흡수
    public float GetCooldownRatio() => cooldownTimer / cooldown;

    public bool TryBlock(Vector2 hitDirection)
    {
        if (!IsDefending) return false;

        IsDefending = false;
        cooldownTimer = cooldown;
        onBlocked?.Invoke(hitDirection);
        return true;
    }
}
