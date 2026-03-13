using UnityEngine;
using UnityEngine.UI;

// 역할: 쿨타임 비율을 읽어 버튼 위 오버레이 Image 업데이트만 담당
public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private int skillIndex;
    [SerializeField] private Image cooldownOverlay; // 버튼 위 어두운 오버레이 Image

    private void Update()
    {
        cooldownOverlay.fillAmount = skillSystem.GetCooldownRatio(skillIndex);
    }
}
