using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionProjectile : Projectile
{
    void Start()
    {
        speed = 6;
        expireTime = 1.5f;
    }
}
