using UnityEngine;

// 역할: 게임 상태를 읽어 Animator 파라미터 설정만 담당
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerInputData inputData;
    [SerializeField] private DefenseSystem defenseSystem;
    [SerializeField] private PlayerStats playerStats;

    private Animator animator;

    private static readonly int IsCharging = Animator.StringToHash("IsCharging");
    private static readonly int Attack     = Animator.StringToHash("Attack");
    private static readonly int Defend     = Animator.StringToHash("Defend");
    private static readonly int Hurt       = Animator.StringToHash("Hurt");

    private void Awake() => animator = GetComponent<Animator>();

    private void OnEnable()
    {
        inputData.onAttackRequested  += OnAttack;
        defenseSystem.onBlocked      += OnDefend;
        playerStats.onDamaged        += OnHurt;
    }

    private void OnDisable()
    {
        inputData.onAttackRequested  -= OnAttack;
        defenseSystem.onBlocked      -= OnDefend;
        playerStats.onDamaged        -= OnHurt;
    }

    private void Update()
    {
        animator.SetBool(IsCharging, inputData.IsCharging);
    }

    private void OnAttack()             => animator.SetTrigger(Attack);
    private void OnDefend(Vector2 _)    => animator.SetTrigger(Defend);
    private void OnHurt()               => animator.SetTrigger(Hurt);
}
