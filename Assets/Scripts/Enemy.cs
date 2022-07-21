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

    protected void Start()
    {
        hitpoints = 1000;
        coins = 100;
        FindReferences();
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
}
