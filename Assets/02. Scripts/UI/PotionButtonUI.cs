using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 역할: 포션 수량 표시 및 사용 버튼 처리만 담당
public class PotionButtonUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private DropType potionType; // HpPotion 또는 MpPotion
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button button;

    private void Start()
    {
        if (potionType == DropType.HpPotion)
            inventory.onHpPotionChanged += UpdateCount;
        else
            inventory.onMpPotionChanged += UpdateCount;

        button.onClick.AddListener(UsePotion);
        UpdateCount(0);
    }

    private void OnDestroy()
    {
        if (potionType == DropType.HpPotion)
            inventory.onHpPotionChanged -= UpdateCount;
        else
            inventory.onMpPotionChanged -= UpdateCount;
    }

    private void UpdateCount(int count)
    {
        countText.text = count.ToString();
    }

    private void UsePotion()
    {
        if (potionType == DropType.HpPotion)
            inventory.UseHpPotion();
        else
            inventory.UseMpPotion();
    }
}
