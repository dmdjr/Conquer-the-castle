using UnityEngine;

// 역할: 보스 이동 처리만 담당 (추후 패턴별 이동 확장 가능)
public class BossMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private bool isMoving = true;

    private void Update()
    {
        if (!isMoving) return;
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void SetMoving(bool value)
    {
        isMoving = value;
    }
}
