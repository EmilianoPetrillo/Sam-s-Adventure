using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : Projectile
{
    private void Start()
    {
        speed = 8;
        expireTime = 0.8f;
    }
}
