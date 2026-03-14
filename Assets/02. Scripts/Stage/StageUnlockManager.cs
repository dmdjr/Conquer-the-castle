using UnityEngine;

// 역할: 스테이지 해금 상태를 PlayerPrefs로 저장/조회만 담당
public static class StageUnlockManager
{
    public static bool IsUnlocked(int stageIndex)
    {
        if (stageIndex == 0) return true; // Stage 1은 항상 해금
        return PlayerPrefs.GetInt($"StageUnlocked_{stageIndex}", 0) == 1;
    }

    public static void Unlock(int stageIndex)
    {
        PlayerPrefs.SetInt($"StageUnlocked_{stageIndex}", 1);
        PlayerPrefs.Save();
    }
}
