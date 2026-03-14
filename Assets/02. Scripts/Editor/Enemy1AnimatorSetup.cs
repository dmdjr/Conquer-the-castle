#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

// 역할: Stage1_Enemy Animator Controller 파라미터·트랜지션 자동 세팅만 담당
// 사용법: Unity 상단 메뉴 → Tools → Setup Enemy1 Animator
public class Enemy1AnimatorSetup
{
    [MenuItem("Tools/Setup Enemy1 Animator")]
    public static void Setup()
    {
        string path = "Assets/05. Animations/Enemy1/Stage1_Enemy.controller";
        AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);

        if (controller == null)
        {
            Debug.LogError("Stage1_Enemy.controller를 찾을 수 없습니다: " + path);
            return;
        }

        // 파라미터 초기화 후 재등록
        while (controller.parameters.Length > 0)
            controller.RemoveParameter(controller.parameters[0]);

        controller.AddParameter("IsAttacking", AnimatorControllerParameterType.Bool);
        controller.AddParameter("Die",         AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Hit",         AnimatorControllerParameterType.Trigger);

        AnimatorStateMachine sm = controller.layers[0].stateMachine;

        // 상태 참조 수집
        AnimatorState walk = null, attack = null, death = null, hit = null;
        foreach (var child in sm.states)
        {
            switch (child.state.name)
            {
                case "Enemy1_Walk":   walk   = child.state; break;
                case "Enemy1_Attack": attack = child.state; break;
                case "Enemy1_Death":  death  = child.state; break;
                case "Enemy1_Hit":    hit    = child.state; break;
            }
        }

        // 기존 트랜지션 초기화
        sm.anyStateTransitions = new AnimatorStateTransition[0];
        foreach (var child in sm.states)
            child.state.transitions = new AnimatorStateTransition[0];

        // Walk ↔ Attack
        var toAttack = walk.AddTransition(attack);
        toAttack.hasExitTime = false;
        toAttack.duration = 0f;
        toAttack.AddCondition(AnimatorConditionMode.If, 0, "IsAttacking");

        var toWalk = attack.AddTransition(walk);
        toWalk.hasExitTime = false;
        toWalk.duration = 0f;
        toWalk.AddCondition(AnimatorConditionMode.IfNot, 0, "IsAttacking");

        // Any State → Hit (피격, 짧게 재생 후 복귀)
        var toHit = sm.AddAnyStateTransition(hit);
        toHit.hasExitTime = false;
        toHit.duration = 0f;
        toHit.canTransitionToSelf = false;
        toHit.AddCondition(AnimatorConditionMode.If, 0, "Hit");

        // Hit → Walk (클립 끝나면 복귀)
        var hitToWalk = hit.AddTransition(walk);
        hitToWalk.hasExitTime = true;
        hitToWalk.exitTime = 1f;
        hitToWalk.duration = 0f;

        // Any State → Death (사망, 복귀 없음)
        var toDeath = sm.AddAnyStateTransition(death);
        toDeath.hasExitTime = false;
        toDeath.duration = 0f;
        toDeath.canTransitionToSelf = false;
        toDeath.AddCondition(AnimatorConditionMode.If, 0, "Die");

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
        Debug.Log("Enemy1 Animator 세팅 완료!");
    }
}
#endif
