using UnityEngine;

// 역할: 보스 처치 시 장비 아이템 드롭 위치에 스폰만 담당
public class BossDropper : MonoBehaviour
{
    [SerializeField] private BossSpawner bossSpawner;
    [SerializeField] private StageManager stageManager;

    private void OnEnable()
    {
        bossSpawner.onBossDefeated += SpawnEquipment;
    }

    private void OnDisable()
    {
        bossSpawner.onBossDefeated -= SpawnEquipment;
    }

    private void SpawnEquipment(Vector3 position)
    {
        var prefab = stageManager.CurrentStageData.equipmentDropPrefab;
        if (prefab == null) return;
        Instantiate(prefab, position, Quaternion.identity);
    }
}
