using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavorOfTheMoonProjectile : Projectile
{
    private void Start()
    {
        speed = 10;
        expireTime = 2f;
    }

    protected override void Update()
    {
        base.Update();
        damage += (int)(250 * Time.deltaTime);
    }
}
