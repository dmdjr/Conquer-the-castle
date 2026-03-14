using System.Collections;
using UnityEngine;

// 역할: 적 상태 이벤트를 받아 Animator 파라미터 설정만 담당
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private EnemyContactDetector contactDetector;
    [SerializeField] private EnemyBase enemyBase;

    private Animator animator;

    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int Die         = Animator.StringToHash("Die");
    private static readonly int Hit         = Animator.StringToHash("Hit");

    private void Awake() => animator = GetComponent<Animator>();

    private void OnEnable()
    {
        contactDetector.onPlayerContact += OnPlayerContact;
        enemyBase.onDied                += OnDied;
        enemyBase.onHit                 += OnHit;
    }

    private void OnDisable()
    {
        contactDetector.onPlayerContact -= OnPlayerContact;
        enemyBase.onDied                -= OnDied;
        enemyBase.onHit                 -= OnHit;
    }

    private void OnPlayerContact(bool isContact) => animator.SetBool(IsAttacking, isContact);
    private void OnHit()                          => animator.SetTrigger(Hit);

    private void OnDied(Vector3 _)
    {
        animator.SetTrigger(Die);
        StartCoroutine(DeactivateAfterDeath());
    }

    private IEnumerator DeactivateAfterDeath()
    {
        // 한 프레임 대기 후 사망 애니메이션 길이만큼 기다린 뒤 풀 반환
        yield return null;
        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);
        enemyBase.Deactivate();
    }
}
