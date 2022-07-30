using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : Enemy
{
    public GameObject magicDice;
    public Transform magicDiceSpawnPosition;

    public void Shoot()
    {
        GameObject go = Instantiate(magicDice, magicDiceSpawnPosition.position, Quaternion.identity);
        go.GetComponent<MagicDice>().damage = enemySO.ATK * enemySO.multiplier;
    }
}
