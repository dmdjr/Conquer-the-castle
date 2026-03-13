using System;
using UnityEngine;

// 역할: 강화 포인트 관리 및 스탯 수치 적용만 담당
public class StatUpgradeSystem : MonoBehaviour
{
    public enum StatType { HP, MP, ATK }

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int maxUpgradesPerStat = 3;

    [Header("강화량")]
    [SerializeField] private float hpPerUpgrade = 20f;
    [SerializeField] private float mpPerUpgrade = 20f;
    [SerializeField] private float atkPerUpgrade = 5f;

    private int[] upgradeCounts = new int[3]; // HP, MP, ATK 순서
    private int availablePoints = 0;

    public int AvailablePoints => availablePoints;
    public event Action onUpgradeChanged;

    public void AddPoint()
    {
        availablePoints++;
        onUpgradeChanged?.Invoke();
    }

    public bool CanUpgrade(StatType stat)
    {
        return availablePoints > 0 && upgradeCounts[(int)stat] < maxUpgradesPerStat;
    }

    public int GetUpgradeCount(StatType stat) => upgradeCounts[(int)stat];

    public void Upgrade(StatType stat)
    {
        if (!CanUpgrade(stat)) return;

        upgradeCounts[(int)stat]++;
        availablePoints--;
        ApplyStat(stat);
        onUpgradeChanged?.Invoke();
    }

    private void ApplyStat(StatType stat)
    {
        switch (stat)
        {
            case StatType.HP:  playerStats.IncreaseMaxHp(hpPerUpgrade);          break;
            case StatType.MP:  playerStats.IncreaseMaxMp(mpPerUpgrade);          break;
            case StatType.ATK: playerStats.IncreaseAttackPower(atkPerUpgrade);   break;
        }
    }
}
