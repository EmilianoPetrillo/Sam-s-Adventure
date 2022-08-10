using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : Enemy
{
    public GameObject Projectile;
    public Transform ProjectileSpawnPosition;

    public void Shoot()
    {
        GameObject go = Instantiate(Projectile, ProjectileSpawnPosition.position, Quaternion.identity);
        go.GetComponent<MagicDice>().damage = enemySO.ATK;
    }
}
