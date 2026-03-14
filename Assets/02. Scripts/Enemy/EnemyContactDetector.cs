using System;
using UnityEngine;

// 역할: 플레이어와의 물리 접촉 감지 및 이벤트 발생만 담당
public class EnemyContactDetector : MonoBehaviour
{
    public event Action<bool> onPlayerContact; // true: 접촉, false: 떨어짐

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            onPlayerContact?.Invoke(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            onPlayerContact?.Invoke(false);
    }
}
