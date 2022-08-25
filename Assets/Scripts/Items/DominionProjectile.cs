using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominionProjectile : Projectile
{
    void Start()
    {
        speed = 15;
        expireTime = 1;
    }
}
