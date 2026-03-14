using UnityEngine;

// 역할: PlayerPrefs에서 스테이지 인덱스를 읽어 각 시스템에 StageData 주입 및 게임 시작 담당
public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData[] stages; // 0: Stage1, 1: Stage2, 2: Stage3
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private BossSpawner bossSpawner;
    [SerializeField] private StageProgressTracker progressTracker;

    public StageData CurrentStageData { get; private set; }

    private void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedStage", 0);
        index = Mathf.Clamp(index, 0, stages.Length - 1);
        CurrentStageData = stages[index];

        enemySpawner.SetStageData(CurrentStageData);
        bossSpawner.SetStageData(CurrentStageData);
        progressTracker.SetStageData(CurrentStageData);
    }

    private void Start()
    {
        enemySpawner.StartSpawning();
    }
}
