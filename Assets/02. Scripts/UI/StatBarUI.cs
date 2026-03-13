using System;
using UnityEngine;
using UnityEngine.UI;

// 역할: PlayerStats 이벤트를 받아 슬라이더 UI 업데이트만 담당
// HP바/MP바 둘 다 이 스크립트 하나로 처리
public class StatBarUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    public enum StatType { HP, MP }
    [SerializeField] private StatType statType;

    private void Start()
    {
        if (statType == StatType.HP)
        {
            playerStats.onHpChanged += UpdateBar;
            UpdateBar(playerStats.CurrentHp, playerStats.MaxHp);
        }
        else
        {
            playerStats.onMpChanged += UpdateBar;
            UpdateBar(playerStats.CurrentMp, playerStats.MaxMp);
        }
    }

    private void OnDestroy()
    {
        if (statType == StatType.HP)
            playerStats.onHpChanged -= UpdateBar;
        else
            playerStats.onMpChanged -= UpdateBar;
    }

    private void UpdateBar(float current, float max)
    {
        slider.value = current / max;
    }
}
