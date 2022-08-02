using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : Enemy
{
    protected override void Move()
    {
        
    }

    protected override void OnDeath()
    {
        GameController.Instance.FakeEnemyDeath();
    }

}
