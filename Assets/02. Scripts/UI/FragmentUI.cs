using UnityEngine;
using TMPro;

// 역할: 파편 수량 텍스트 업데이트만 담당
public class FragmentUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private TextMeshProUGUI fragmentText;

    private void Start()
    {
        inventory.onFragmentChanged += UpdateText;
        UpdateText(0);
    }

    private void OnDestroy()
    {
        inventory.onFragmentChanged -= UpdateText;
    }

    private void UpdateText(int count)
    {
        fragmentText.text = $"파편: {count}";
    }
}
