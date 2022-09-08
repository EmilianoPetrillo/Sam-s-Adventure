using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessegerOfSadowProjectile : Projectile
{
    void Start()
    {
        speed = 2;
        expireTime = 20f;
    }
}
