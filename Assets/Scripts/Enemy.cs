using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    protected int coins;
    protected UIController UIController;
    protected GameController GameController;
    protected bool deathCheck = false;
    //Senza il controllo deathCheck (dentro OnDeath()) diversi proiettili potevano invocare la funzione OnDeath
    //facendo avanzare il player di più livelli in un colpo solo.
    protected float speed = 1.5f;

    protected void Start()
    {
        hitpoints = 1000;
        coins = 100;
        FindReferences();
    }

    private void Update()
    {
        Move();
    }

    private void FindReferences()
    {
        UIController = FindObjectOfType<UIController>();
        GameController = FindObjectOfType<GameController>();
    }

    protected override void OnDeath()
    {
        if(deathCheck == false)
        {
            deathCheck = true;
            UIController.CoinsUp(coins);
            GameController.LevelUp();
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
