using System;
using UnityEngine;

// 역할: 포션 수량 저장, 추가, 사용만 담당
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float hpPotionRestoreAmount = 30f;
    [SerializeField] private float mpPotionRestoreAmount = 40f;

    private int hpPotionCount;
    private int mpPotionCount;

    public event Action<int> onHpPotionChanged;
    public event Action<int> onMpPotionChanged;

    public void AddItem(DropType type)
    {
        switch (type)
        {
            case DropType.HpPotion:
                hpPotionCount++;
                onHpPotionChanged?.Invoke(hpPotionCount);
                break;
            case DropType.MpPotion:
                mpPotionCount++;
                onMpPotionChanged?.Invoke(mpPotionCount);
                break;
        }
    }

    public void UseHpPotion()
    {
        if (hpPotionCount <= 0) return;
        hpPotionCount--;
        playerStats.Heal(hpPotionRestoreAmount);
        onHpPotionChanged?.Invoke(hpPotionCount);
    }

    public void UseMpPotion()
    {
        if (mpPotionCount <= 0) return;
        mpPotionCount--;
        playerStats.RestoreMp(mpPotionRestoreAmount);
        onMpPotionChanged?.Invoke(mpPotionCount);
    }
}
