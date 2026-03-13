using UnityEngine;
using UnityEngine.EventSystems;

// 역할: 스킬 버튼 눌림 감지만 담당
public class SkillButtonUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private int skillIndex; // 0, 1, 2

    public void OnPointerDown(PointerEventData eventData)
    {
        skillSystem.UseSkill(skillIndex);
    }
}
