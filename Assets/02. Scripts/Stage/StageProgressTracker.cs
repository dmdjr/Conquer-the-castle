using UnityEngine;
using UnityEngine.Events;

// 역할: 적 처치 수 카운트 및 보스 등장/스테이지 클리어 트리거만 담당
public class StageProgressTracker : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private EnemySpawner spawner;

    public UnityEvent onAllEnemiesDefeated;
    public UnityEvent onStageClear;

    private int defeatedCount = 0;
    private bool bossSpawned = false;

    public void OnEnemyDefeated()
    {
        defeatedCount++;

        if (!bossSpawned && defeatedCount >= stageData.totalEnemyCount)
        {
            bossSpawned = true;
            spawner.StopSpawning();
            onAllEnemiesDefeated?.Invoke();
        }
    }

    public void OnBossDefeated()
    {
        onStageClear?.Invoke();
    }

    public void ResetStage()
    {
        defeatedCount = 0;
        bossSpawned = false;
    }
}
