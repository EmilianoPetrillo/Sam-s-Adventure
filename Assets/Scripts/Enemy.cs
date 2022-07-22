using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    protected float coins;
    protected bool deathCheck = false;
    //Senza il controllo deathCheck (dentro OnDeath()) diversi proiettili potevano invocare la funzione OnDeath
    //facendo avanzare il player di più livelli in un colpo solo.
    protected float speed = 1.5f;

    public float multiplier = 1;

    protected virtual void Start()
    {
        HP = 1000 * multiplier;
        coins = 100 * multiplier;
        ATK = 100 * multiplier;
    }

    private void Update()
    {
        Move();
    }

    protected override void OnDeath()
    {
        if(deathCheck == false)
        {
            deathCheck = true;
            UIController.Instance.CoinsUp(coins);
            GameController.Instance.LevelUp();
            base.OnDeath();
        }
    }

    protected virtual void Move()
    {
        if(Vector2.Distance(transform.position, new Vector2(-6f, 0f)) > 2)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); 
        }
    }
}
