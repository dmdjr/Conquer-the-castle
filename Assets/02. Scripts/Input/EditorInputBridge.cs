#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.InputSystem;

// 역할: 에디터 테스트용 키보드 입력을 기존 시스템에 전달만 담당
// 키 매핑:
//   →        : 돌진
//   F        : 공격
//   Z/X/C    : 스킬 1/2/3
//   Space    : 방어
//   A        : HP 포션
//   D        : MP 포션
public class EditorInputBridge : MonoBehaviour
{
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private DefenseSystem defenseSystem;
    [SerializeField] private PlayerInventory playerInventory;

    private void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        // 돌진 (→ 누르는 동안 유지, 떼면 해제)
        if (kb.rightArrowKey.wasPressedThisFrame) inputData.SetCharging(true);
        if (kb.rightArrowKey.wasReleasedThisFrame) inputData.SetCharging(false);

        // 공격
        if (kb.fKey.wasPressedThisFrame) inputData.RequestAttack();

        // 스킬
        if (kb.zKey.wasPressedThisFrame) skillSystem.UseSkill(0);
        if (kb.xKey.wasPressedThisFrame) skillSystem.UseSkill(1);
        if (kb.cKey.wasPressedThisFrame) skillSystem.UseSkill(2);

        // 방어
        if (kb.spaceKey.wasPressedThisFrame) defenseSystem.Activate();

        // 포션
        if (kb.aKey.wasPressedThisFrame) playerInventory.UseHpPotion();
        if (kb.dKey.wasPressedThisFrame) playerInventory.UseMpPotion();
    }
}
#endif
