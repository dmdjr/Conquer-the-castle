using UnityEngine;
using UnityEngine.UI;

// 역할: 개별 스탯 행의 슬롯 표시 및 강화 버튼 처리만 담당
public class StatUpgradeRowUI : MonoBehaviour
{
    [SerializeField] private StatUpgradeSystem upgradeSystem;
    [SerializeField] private StatUpgradeSystem.StatType statType;
    [SerializeField] private Image[] slots = new Image[3];
    [SerializeField] private Button upgradeButton;

    [SerializeField] private Color filledColor = Color.yellow;
    [SerializeField] private Color emptyColor = Color.gray;

    private void Start()
    {
        upgradeSystem.onUpgradeChanged += Refresh;
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        Refresh();
    }

    private void OnDestroy()
    {
        upgradeSystem.onUpgradeChanged -= Refresh;
    }

    private void OnUpgradeClicked()
    {
        upgradeSystem.Upgrade(statType);
    }

    private void Refresh()
    {
        int count = upgradeSystem.GetUpgradeCount(statType);

        for (int i = 0; i < slots.Length; i++)
            slots[i].color = i < count ? filledColor : emptyColor;

        upgradeButton.interactable = upgradeSystem.CanUpgrade(statType);
    }
}
