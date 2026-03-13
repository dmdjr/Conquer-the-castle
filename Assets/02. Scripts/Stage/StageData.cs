using UnityEngine;

// 역할: 스테이지 구성 데이터 보관만 담당
[CreateAssetMenu(fileName = "StageData", menuName = "Conquer/StageData")]
public class StageData : ScriptableObject
{
    [Header("Stage Info")]
    public int stageNumber;

    [Header("Enemy")]
    public GameObject enemyPrefab;       // 해당 스테이지에서 나오는 적 프리팹
    public int totalEnemyCount;          // 총 처치해야 할 적 수 (20 / 30 / 40)
    public int maxSimultaneous = 5;      // 화면에 동시에 존재할 수 있는 최대 적 수
    public float spawnInterval = 2f;     // 적 스폰 간격 (초)

    [Header("Boss")]
    public GameObject bossPrefab;        // 스테이지 클리어 후 등장할 보스 프리팹
}
