using UnityEngine;

// 역할: 플레이어 입력 상태값을 저장하는 데이터 컨테이너
// ScriptableObject로 만들어 MoveButtonUI ↔ PlayerMover 간 직접 참조 없이 데이터 공유
[CreateAssetMenu(fileName = "PlayerInputData", menuName = "Conquer/PlayerInputData")]
public class PlayerInputData : ScriptableObject
{
    public float MoveDirection { get; private set; }

    public void SetMoveDirection(float direction)
    {
        MoveDirection = direction;
    }
}
