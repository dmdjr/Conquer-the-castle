using UnityEngine;

// 역할: 적 사망 시 확률에 따라 포션 드롭 스폰만 담당 (파편 드롭 제거됨)
public class EnemyDropper : MonoBehaviour
{
    [SerializeField] private GameObject hpPotionPrefab;
    [SerializeField] private GameObject mpPotionPrefab;

    [Range(0f, 1f)] [SerializeField] private float hpPotionChance = 0.15f;
    [Range(0f, 1f)] [SerializeField] private float mpPotionChance = 0.15f;

    private EnemyBase enemyBase;

    private void Awake()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    private void OnEnable()
    {
        enemyBase.onDied += SpawnDrops;
    }

    private void OnDisable()
    {
        enemyBase.onDied -= SpawnDrops;
    }

    private void SpawnDrops(Vector3 position)
    {
        TrySpawn(hpPotionPrefab, hpPotionChance, position);
        TrySpawn(mpPotionPrefab, mpPotionChance, position);
    }

    private void TrySpawn(GameObject prefab, float chance, Vector3 position)
    {
        if (prefab == null) return;
        if (Random.value <= chance)
            Instantiate(prefab, position, Quaternion.identity);
    }
}
