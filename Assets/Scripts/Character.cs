using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    protected float HP;
    protected float ATK;
    protected float CRITRATE;
    protected float CRITDAMAGE;

    public void TakeDamage(int damage)
    {
        HP-= damage;
        if (HP <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

}
