using System.Collections;
using UnityEngine;

// 역할: StageData를 읽어 적 스폰 타이밍과 위치 처리만 담당
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private StageProgressTracker progressTracker;
    [SerializeField] private PlayerLevel playerLevel;

    private int spawnedCount = 0;
    private int activeEnemyCount = 0;

    public void StartSpawning()
    {
        spawnedCount = 0;
        activeEnemyCount = 0;
        StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        while (spawnedCount < stageData.totalEnemyCount)
        {
            if (activeEnemyCount < stageData.maxSimultaneous)
                SpawnEnemy();

            yield return new WaitForSeconds(stageData.spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        var obj = enemyPool.Get(spawnPoint.position);
        var enemy = obj.GetComponent<EnemyBase>();

        enemy.Init(enemyPool, () =>
        {
            activeEnemyCount--;
            progressTracker.OnEnemyDefeated();
            playerLevel.AddExp(enemy.ExpReward);
        });

        spawnedCount++;
        activeEnemyCount++;
    }
}
