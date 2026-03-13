using UnityEngine;
using UnityEngine.EventSystems;

// 역할: 방어 버튼 눌림 감지만 담당
public class DefenseButtonUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private DefenseSystem defenseSystem;

    public void OnPointerDown(PointerEventData eventData)
    {
        defenseSystem.Activate();
    }
}
