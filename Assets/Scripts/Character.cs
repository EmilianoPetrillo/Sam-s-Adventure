using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected int hitpoints;

    public void TakeDamage(int damage)
    {
        hitpoints-= damage;
        if (hitpoints <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        print("i'm dead shit");
        Destroy(gameObject);
    }

}
