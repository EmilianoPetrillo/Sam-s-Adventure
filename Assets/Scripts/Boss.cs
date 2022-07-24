using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        enemySO.coins = 500 * enemySO.multiplier;
        characterSO.ATK = 500 * enemySO.multiplier;
    }
}
