using UnityEngine;

// 역할: 적의 오른쪽 → 왼쪽 이동만 담당
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
