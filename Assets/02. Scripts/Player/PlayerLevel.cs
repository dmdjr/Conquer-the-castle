using System;
using UnityEngine;

// 역할: 레벨, EXP 데이터 관리 및 레벨업 처리만 담당
public class PlayerLevel : MonoBehaviour
{
    private static readonly int[] ExpTable = { 30, 50, 75, 100, 130, 160, 200, 250 };
    private const int MaxLevel = 9;

    private int currentLevel = 1;
    private int currentExp = 0;

    public int CurrentLevel => currentLevel;
    public int CurrentExp => currentExp;
    public int ExpToNextLevel => currentLevel < MaxLevel ? ExpTable[currentLevel - 1] : 0;

    public event Action<int, int> onExpChanged;  // (currentExp, expToNextLevel)
    public event Action<int> onLevelUp;          // (newLevel)

    // 역할: 저장된 레벨/EXP 복원 (PlayerStatsPersistence에서 호출)
    public void LoadLevel(int savedLevel, int savedExp)
    {
        currentLevel = Mathf.Clamp(savedLevel, 1, MaxLevel);
        currentExp = savedExp;
        onExpChanged?.Invoke(currentExp, ExpToNextLevel);
    }

    public void AddExp(int amount)
    {
        if (currentLevel >= MaxLevel) return;

        currentExp += amount;
        onExpChanged?.Invoke(currentExp, ExpToNextLevel);

        while (currentLevel < MaxLevel && currentExp >= ExpTable[currentLevel - 1])
        {
            currentExp -= ExpTable[currentLevel - 1];
            currentLevel++;
            onLevelUp?.Invoke(currentLevel);
            onExpChanged?.Invoke(currentExp, ExpToNextLevel);
        }
    }
}
