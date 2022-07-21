using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private new void Start()
    {
        base.Start();
        hitpoints = 2000;
        coins = 500;
    }
}
