using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    protected override void Start()
    {
        HP = 2000 * multiplier;
        coins = 500 * multiplier;
        ATK = 500 * multiplier;
    }
}
