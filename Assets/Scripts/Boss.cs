using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        enemySO.coins *= enemySO.multiplier;
        characterSO.ATK *= enemySO.multiplier;
    }
}
