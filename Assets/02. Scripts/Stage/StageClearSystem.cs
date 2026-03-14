using UnityEngine;

// 역할: 장비 획득 시 스테이지 해금 처리 및 클리어 팝업 호출만 담당
public class StageClearSystem : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private StageClearPopupUI popupUI;
    [SerializeField] private PlayerStatsPersistence statsPersistence;

    public void OnEquipmentPickedUp(string equipmentName)
    {
        StageData stage = stageManager.CurrentStageData;

        // 다음 스테이지 해금
        if (stage.nextStageIndex >= 0)
            StageUnlockManager.Unlock(stage.nextStageIndex);

        // 스탯 저장
        statsPersistence.SaveAll();

        // 클리어 팝업 표시
        popupUI.Show(stage.stageNumber, equipmentName, stage.nextStageIndex);
    }
}
