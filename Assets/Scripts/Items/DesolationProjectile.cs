using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesolationProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 15;
        expireTime = 1;
    }
}
