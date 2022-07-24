using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy")]
public class EnemySO : CharacterSO
{
    public float coins;
    public float multiplier = 1;
}
