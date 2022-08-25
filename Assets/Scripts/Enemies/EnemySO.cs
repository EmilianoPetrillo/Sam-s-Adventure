using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy")]
public class EnemySO : CharacterSO
{
    public float coins;
    public float multiplier = 1;
    public float[] attackRange;
    public float moveSpeed;
    public float healthAmountToRecoverInPercent;
    public float healthAmountToTriggerHealPhaseInPercent;
}
