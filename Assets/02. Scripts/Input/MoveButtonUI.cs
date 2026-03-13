using UnityEngine;
using UnityEngine.EventSystems;

// 역할: 버튼 눌림/뗌 감지만 담당, 이동 처리는 하지 않음
public class MoveButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private float direction; // 왼쪽 버튼: -1 / 오른쪽 버튼: 1

    public void OnPointerDown(PointerEventData eventData)
    {
        inputData.SetMoveDirection(direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputData.SetMoveDirection(0f);
    }
}
