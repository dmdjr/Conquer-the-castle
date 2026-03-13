using UnityEngine;

// 역할: 레벨업 팝업 표시/숨김 및 게임 일시정지만 담당
public class LevelUpPopupUI : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private StatUpgradeSystem upgradeSystem;
    [SerializeField] private GameObject popupRoot; // 팝업 + 배경 블로커 전체

    private void Start()
    {
        playerLevel.onLevelUp += OnLevelUp;
        popupRoot.SetActive(false);
    }

    private void OnDestroy()
    {
        playerLevel.onLevelUp -= OnLevelUp;
    }

    private void OnLevelUp(int newLevel)
    {
        upgradeSystem.AddPoint();
        ShowPopup();
    }

    private void ShowPopup()
    {
        popupRoot.SetActive(true);
        Time.timeScale = 0f;
    }

    // X 버튼에 연결
    public void ClosePopup()
    {
        popupRoot.SetActive(false);
        Time.timeScale = 1f;
    }
}
