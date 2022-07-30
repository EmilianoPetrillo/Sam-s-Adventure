using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : Enemy
{
    public GameObject magicDice;
    public Transform magicDiceSpawnPosition;
    protected override void Move()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > 6)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
            Attack();
    }

    public void Shoot()
    {
        GameObject go = Instantiate(magicDice, magicDiceSpawnPosition.position, Quaternion.identity);
    }
}
