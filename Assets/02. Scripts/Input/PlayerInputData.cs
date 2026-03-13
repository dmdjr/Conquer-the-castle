using System;
using UnityEngine;

// 역할: 플레이어 입력 상태값을 저장하는 데이터 컨테이너
[CreateAssetMenu(fileName = "PlayerInputData", menuName = "Conquer/PlayerInputData")]
public class PlayerInputData : ScriptableObject
{
    public bool IsCharging { get; private set; }
    public bool IsKnockedBack { get; private set; }

    public event Action onAttackRequested;

    public void SetCharging(bool value) { IsCharging = value; }
    public void SetKnockedBack(bool value) { IsKnockedBack = value; }
    public void RequestAttack() { onAttackRequested?.Invoke(); }
}
