using System;
using UnityEngine;

// 역할: 플레이어 HP/MP 데이터 보관 및 변경 이벤트 발생만 담당
public class PlayerStats : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private float maxHp = 100f;
    private float currentHp;

    [Header("MP")]
    [SerializeField] private float maxMp = 100f;
    private float currentMp;

    public event Action<float, float> onHpChanged; // (current, max)
    public event Action<float, float> onMpChanged; // (current, max)

    public float CurrentHp => currentHp;
    public float MaxHp => maxHp;
    public float CurrentMp => currentMp;
    public float MaxMp => maxMp;

    private void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;

        onHpChanged?.Invoke(currentHp, maxHp);
        onMpChanged?.Invoke(currentMp, maxMp);
    }

    public void TakeDamage(float amount)
    {
        currentHp = Mathf.Clamp(currentHp - amount, 0f, maxHp);
        onHpChanged?.Invoke(currentHp, maxHp);
    }

    public bool UseMp(float amount)
    {
        if (currentMp < amount) return false;

        currentMp = Mathf.Clamp(currentMp - amount, 0f, maxMp);
        onMpChanged?.Invoke(currentMp, maxMp);
        return true;
    }

    public void RestoreMp(float amount)
    {
        currentMp = Mathf.Clamp(currentMp + amount, 0f, maxMp);
        onMpChanged?.Invoke(currentMp, maxMp);
    }
}
