using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CataclysmProjectile : Projectile
{
    void Start()
    {
        speed = 10;
        expireTime = 1.5f;
    }
}
