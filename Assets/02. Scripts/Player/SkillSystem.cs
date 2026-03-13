using UnityEngine;
using UnityEngine.Events;

// 역할: 쿨타임 관리, MP 검증, 스킬 발동만 담당
public class SkillSystem : MonoBehaviour
{
    [SerializeField] private SkillData[] skills = new SkillData[3];
    [SerializeField] private PlayerStats playerStats;

    public UnityEvent<int> onSkillUsed; // 발동된 스킬 인덱스 전달 (추후 스킬 효과 연결용)

    private float[] cooldownTimers = new float[3];

    private void Update()
    {
        for (int i = 0; i < cooldownTimers.Length; i++)
            if (cooldownTimers[i] > 0f)
                cooldownTimers[i] -= Time.deltaTime;
    }

    public void UseSkill(int index)
    {
        if (index < 0 || index >= skills.Length) return;
        if (cooldownTimers[index] > 0f) return;
        if (!playerStats.UseMp(skills[index].mpCost)) return;

        cooldownTimers[index] = skills[index].cooldown;
        onSkillUsed?.Invoke(index);
    }

    // 쿨타임 비율 반환 (0 = 사용 가능, 1 = 방금 사용)
    public float GetCooldownRatio(int index)
    {
        if (index < 0 || index >= skills.Length) return 0f;
        if (skills[index].cooldown <= 0f) return 0f;
        return cooldownTimers[index] / skills[index].cooldown;
    }
}
