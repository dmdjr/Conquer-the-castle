using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 역할: PlayerLevel 이벤트를 받아 EXP 바/텍스트 업데이트만 담당
public class ExpBarUI : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI expText;

    private void Start()
    {
        playerLevel.onExpChanged += UpdateExpBar;
        UpdateExpBar(playerLevel.CurrentExp, playerLevel.ExpToNextLevel);
    }

    private void OnDestroy()
    {
        playerLevel.onExpChanged -= UpdateExpBar;
    }

    private void UpdateExpBar(int current, int required)
    {
        if (required <= 0)
        {
            expSlider.value = 1f;
            expText.text = "MAX";
            return;
        }

        expSlider.value = (float)current / required;
        expText.text = $"{current}/{required}";
    }
}
