using UnityEngine;

// 역할: 게임 진행 중 MP를 선형적으로 회복시키는 것만 담당
public class MpRegenSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float regenPerSecond = 5f; // 초당 회복량

    private void Update()
    {
        playerStats.RestoreMp(regenPerSecond * Time.deltaTime);
    }
}
