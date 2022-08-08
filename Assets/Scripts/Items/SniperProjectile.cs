using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperProjectile : Projectile
{
    private void Start()
    {
        speed = 8;
    }

    protected override void Update()
    {
        base.Update();
        damage += (int)(250 * Time.deltaTime);
    }

}
