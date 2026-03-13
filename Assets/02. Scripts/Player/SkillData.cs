using UnityEngine;

// 역할: 스킬 데이터 보관만 담당
[CreateAssetMenu(fileName = "SkillData", menuName = "Conquer/SkillData")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public float mpCost;
    public float cooldown;
}
