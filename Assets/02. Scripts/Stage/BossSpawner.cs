using System;
using UnityEngine;

// 역할: 잡몹 전멸 시 보스 스폰, 보스 사망 이벤트 전달만 담당
public class BossSpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private Transform bossSpawnPoint;

    public event Action<Vector3> onBossDefeated; // BossDropper가 구독 (드롭 위치 전달)

    private BossBase currentBoss;

    // StageManager가 Awake에서 주입
    public void SetStageData(StageData data) => stageData = data;

    // StageProgressTracker.onAllEnemiesDefeated 에 연결
    public void SpawnBoss()
    {
        if (stageData.bossPrefab == null) return;

        var obj = Instantiate(stageData.bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        currentBoss = obj.GetComponent<BossBase>();
        currentBoss.onBossDefeated += OnBossDefeated;
    }

    private void OnBossDefeated()
    {
        Vector3 dropPosition = currentBoss.transform.position;
        currentBoss.onBossDefeated -= OnBossDefeated;
        onBossDefeated?.Invoke(dropPosition);
    }
}
