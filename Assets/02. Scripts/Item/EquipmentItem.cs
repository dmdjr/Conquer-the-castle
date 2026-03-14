using UnityEngine;

// 역할: 플레이어가 장비를 밟았을 때 StageClearSystem에 알림만 담당
public class EquipmentItem : MonoBehaviour
{
    private StageClearSystem stageClearSystem;
    private StageManager stageManager;

    private void Awake()
    {
        // 프리팹은 씬 오브젝트를 직접 참조할 수 없으므로 씬에서 찾아옴
        stageClearSystem = FindFirstObjectByType<StageClearSystem>();
        stageManager = FindFirstObjectByType<StageManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        stageClearSystem.OnEquipmentPickedUp(stageManager.CurrentStageData.equipmentName);
        Destroy(gameObject);
    }
}
