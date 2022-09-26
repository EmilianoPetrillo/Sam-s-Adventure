using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEnemy : Enemy
{
    protected override void Move()
    {
        
    }

    private bool tutorial = false;

    protected override void OnDeath()
    {
        if (tutorial)
        {
            tutorial = false;
            UITextController.Instance.NextPhase();
        }
        Destroy(gameObject);
    }

}
