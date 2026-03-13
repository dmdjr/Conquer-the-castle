using UnityEngine;
using TMPro;

// 역할: PlayerStats 이벤트를 받아 "현재/최대" 텍스트 업데이트만 담당
public class StatTextUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TextMeshProUGUI statText;

    public enum StatType { HP, MP }
    [SerializeField] private StatType statType;

    private void Start()
    {
        if (statType == StatType.HP)
        {
            playerStats.onHpChanged += UpdateText;
            UpdateText(playerStats.CurrentHp, playerStats.MaxHp);
        }
        else
        {
            playerStats.onMpChanged += UpdateText;
            UpdateText(playerStats.CurrentMp, playerStats.MaxMp);
        }
    }

    private void OnDestroy()
    {
        if (statType == StatType.HP)
            playerStats.onHpChanged -= UpdateText;
        else
            playerStats.onMpChanged -= UpdateText;
    }

    private void UpdateText(float current, float max)
    {
        statText.text = $"{Mathf.CeilToInt(current)}/{Mathf.CeilToInt(max)}";
    }
}
