using UnityEngine;

// 역할: PlayerPrefs로 스탯/레벨/강화 데이터 저장 및 복원만 담당
public class PlayerStatsPersistence : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private StatUpgradeSystem upgradeSystem;

    private void Start()
    {
        LoadAll();
    }

    public void SaveAll()
    {
        PlayerPrefs.SetFloat("MaxHp", playerStats.MaxHp);
        PlayerPrefs.SetFloat("MaxMp", playerStats.MaxMp);
        PlayerPrefs.SetFloat("AttackPower", playerStats.AttackPower);
        PlayerPrefs.SetInt("Level", playerLevel.CurrentLevel);
        PlayerPrefs.SetInt("Exp", playerLevel.CurrentExp);
        PlayerPrefs.SetInt("UpgradeHP", upgradeSystem.GetUpgradeCount(StatUpgradeSystem.StatType.HP));
        PlayerPrefs.SetInt("UpgradeMP", upgradeSystem.GetUpgradeCount(StatUpgradeSystem.StatType.MP));
        PlayerPrefs.SetInt("UpgradeATK", upgradeSystem.GetUpgradeCount(StatUpgradeSystem.StatType.ATK));
        PlayerPrefs.SetInt("UpgradePoints", upgradeSystem.AvailablePoints);
        PlayerPrefs.Save();
    }

    private void LoadAll()
    {
        if (!PlayerPrefs.HasKey("MaxHp")) return; // 저장 데이터 없으면 기본값 사용

        playerStats.LoadStats(
            PlayerPrefs.GetFloat("MaxHp"),
            PlayerPrefs.GetFloat("MaxMp"),
            PlayerPrefs.GetFloat("AttackPower")
        );

        playerLevel.LoadLevel(
            PlayerPrefs.GetInt("Level", 1),
            PlayerPrefs.GetInt("Exp", 0)
        );

        upgradeSystem.LoadUpgrades(
            PlayerPrefs.GetInt("UpgradeHP", 0),
            PlayerPrefs.GetInt("UpgradeMP", 0),
            PlayerPrefs.GetInt("UpgradeATK", 0),
            PlayerPrefs.GetInt("UpgradePoints", 0)
        );
    }
}
