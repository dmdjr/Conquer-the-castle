using UnityEngine;
using UnityEngine.EventSystems;

// 역할: 공격 버튼 눌림 감지만 담당
public class AttackButtonUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerInputData inputData;

    public void OnPointerDown(PointerEventData eventData)
    {
        inputData.RequestAttack();
    }
}
