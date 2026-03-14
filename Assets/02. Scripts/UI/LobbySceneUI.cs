using UnityEngine;
using UnityEngine.UI;

// 역할: 로비에서 스테이지 버튼 해금 상태 표시 및 스테이지 진입 처리만 담당
public class LobbySceneUI : MonoBehaviour
{
    [SerializeField] private Button[] stageButtons;    // 0: Stage1, 1: Stage2, 2: Stage3 버튼
    [SerializeField] private GameObject[] lockIcons;   // 각 버튼 위 잠금 아이콘 (같은 인덱스)

    private void Start()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            bool unlocked = StageUnlockManager.IsUnlocked(i);
            stageButtons[i].interactable = unlocked;
            if (lockIcons[i] != null)
                lockIcons[i].SetActive(!unlocked);
        }

        // 버튼에 클릭 이벤트 등록 (캡처 변수로 인덱스 고정)
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int index = i;
            stageButtons[i].onClick.AddListener(() => StageLoader.LoadStage(index));
        }
    }
}
