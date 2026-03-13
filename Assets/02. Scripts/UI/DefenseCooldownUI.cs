using UnityEngine;
using UnityEngine.UI;

// 역할: 방어 쿨타임 비율을 읽어 버튼 위 오버레이 Image 업데이트만 담당
public class DefenseCooldownUI : MonoBehaviour
{
    [SerializeField] private DefenseSystem defenseSystem;
    [SerializeField] private Image cooldownOverlay;

    private void Update()
    {
        cooldownOverlay.fillAmount = defenseSystem.GetCooldownRatio();
    }
}
