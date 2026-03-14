#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

// 역할: Player Animator Controller의 파라미터와 트랜지션 자동 세팅만 담당
// 사용법: Unity 상단 메뉴 → Tools → Setup Player Animator
public class PlayerAnimatorSetup
{
    [MenuItem("Tools/Setup Player Animator")]
    public static void Setup()
    {
        string path = "Assets/05. Animations/Player/Player.controller";
        AnimatorController controller = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);

        if (controller == null)
        {
            Debug.LogError("Player.controller를 찾을 수 없습니다: " + path);
            return;
        }

        // 기존 파라미터 초기화 후 재등록
        while (controller.parameters.Length > 0)
            controller.RemoveParameter(controller.parameters[0]);

        controller.AddParameter("IsCharging", AnimatorControllerParameterType.Bool);
        controller.AddParameter("Attack",     AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Defend",     AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Hurt",       AnimatorControllerParameterType.Trigger);

        AnimatorStateMachine sm = controller.layers[0].stateMachine;

        // 상태 참조 수집
        AnimatorState idle = null, run = null, attack = null, defend = null, hurt = null;
        foreach (var child in sm.states)
        {
            switch (child.state.name)
            {
                case "Player_Idle":   idle   = child.state; break;
                case "Player_Run":    run    = child.state; break;
                case "Player_Attack": attack = child.state; break;
                case "Player_Defend": defend = child.state; break;
                case "Player_Hurt":   hurt   = child.state; break;
            }
        }

        // 기존 트랜지션 초기화
        sm.anyStateTransitions = new AnimatorStateTransition[0];
        foreach (var child in sm.states)
            child.state.transitions = new AnimatorStateTransition[0];

        // Idle ↔ Run
        AddTransition(idle, run, hasExit: false, duration: 0f, param: "IsCharging", mode: AnimatorConditionMode.If);
        AddTransition(run, idle, hasExit: false, duration: 0f, param: "IsCharging", mode: AnimatorConditionMode.IfNot);

        // Any State → Attack / Defend / Hurt (trigger)
        AddAnyTransition(sm, attack, "Attack");
        AddAnyTransition(sm, defend, "Defend");
        AddAnyTransition(sm, hurt,   "Hurt");

        // Attack / Defend / Hurt → Idle (클립 끝나면 복귀)
        AddTransition(attack, idle, hasExit: true, duration: 0f);
        AddTransition(defend, idle, hasExit: true, duration: 0f);
        AddTransition(hurt,   idle, hasExit: true, duration: 0f);

        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
        Debug.Log("Player Animator 세팅 완료!");
    }

    private static void AddTransition(AnimatorState from, AnimatorState to,
        bool hasExit, float duration, string param = null, AnimatorConditionMode mode = AnimatorConditionMode.If)
    {
        var t = from.AddTransition(to);
        t.hasExitTime = hasExit;
        t.exitTime = 1f;
        t.duration = duration;
        if (param != null) t.AddCondition(mode, 0, param);
    }

    private static void AddAnyTransition(AnimatorStateMachine sm, AnimatorState to, string trigger)
    {
        var t = sm.AddAnyStateTransition(to);
        t.hasExitTime = false;
        t.duration = 0f;
        t.canTransitionToSelf = false;
        t.AddCondition(AnimatorConditionMode.If, 0, trigger);
    }
}
#endif
