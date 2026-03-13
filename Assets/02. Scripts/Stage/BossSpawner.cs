using UnityEngine;

// 역할: 잡몹 전멸 시 보스 스폰 및 보스 사망을 StageProgressTracker에 전달만 담당
public class BossSpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private StageProgressTracker progressTracker;

    private BossBase currentBoss;

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
        currentBoss.onBossDefeated -= OnBossDefeated;
        progressTracker.OnBossDefeated();
    }
}
