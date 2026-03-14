using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 역할: Stage Clear 팝업 표시 및 버튼 입력 처리만 담당
public class StageClearPopupUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI clearText;
    [SerializeField] private TextMeshProUGUI equipmentText;
    [SerializeField] private Button lobbyButton;
    [SerializeField] private Button nextStageButton;

    private int nextStageIndex;

    private void Start()
    {
        panel.SetActive(false);
        lobbyButton.onClick.AddListener(OnLobbyClicked);
        nextStageButton.onClick.AddListener(OnNextStageClicked);
    }

    public void Show(int stageNumber, string equipmentName, int nextIdx)
    {
        nextStageIndex = nextIdx;
        clearText.text = $"Stage {stageNumber} Clear!";
        equipmentText.text = $"{equipmentName} 획득!";
        nextStageButton.gameObject.SetActive(nextIdx >= 0);
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnLobbyClicked()
    {
        Time.timeScale = 1f;
        StageLoader.LoadLobby();
    }

    private void OnNextStageClicked()
    {
        Time.timeScale = 1f;
        StageLoader.LoadStage(nextStageIndex);
    }
}
