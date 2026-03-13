using System;
using UnityEngine;

// 역할: 플레이어 입력 상태값을 저장하는 데이터 컨테이너
[CreateAssetMenu(fileName = "PlayerInputData", menuName = "Conquer/PlayerInputData")]
public class PlayerInputData : ScriptableObject
{
    public float MoveDirection { get; private set; }
    public float FacingDirection { get; private set; } = 1f; // 1 = 오른쪽, -1 = 왼쪽

    public bool IsKnockedBack { get; private set; }

    public event Action onAttackRequested;

    public void SetMoveDirection(float direction)
    {
        MoveDirection = direction;
    }

    public void SetFacingDirection(float direction)
    {
        if (direction != 0f)
            FacingDirection = direction;
    }

    public void SetKnockedBack(bool value)
    {
        IsKnockedBack = value;
    }

    public void RequestAttack()
    {
        onAttackRequested?.Invoke();
    }
}
