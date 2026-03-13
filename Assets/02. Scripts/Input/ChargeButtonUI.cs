using UnityEngine;
using UnityEngine.EventSystems;

// 역할: 돌진 버튼 눌림 감지만 담당 (뗄 때는 멈추지 않음 - 적 충돌 시 자동 정지)
public class ChargeButtonUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerInputData inputData;

    public void OnPointerDown(PointerEventData eventData) => inputData.SetCharging(true);
}
