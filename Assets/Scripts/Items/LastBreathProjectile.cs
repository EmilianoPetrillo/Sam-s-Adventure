using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBreathProjectile : Projectile
{
    private void Start()
    {
        speed = 7;
        expireTime = 10f;
    }
}
