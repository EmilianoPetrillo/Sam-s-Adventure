using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected bool deathCheck = false;
    //Senza il controllo deathCheck (dentro OnDeath()) diversi proiettili potevano invocare la funzione OnDeath
    //facendo avanzare il player di più livelli in un colpo solo.
    protected float speed = 1.5f;

    protected EnemySO enemySO;
    public EnemySO EnemySO
    {
        get { return enemySO; }
    }

    protected void Awake()
    {
        enemySO = Object.Instantiate<EnemySO>((EnemySO)characterSO);
    }

    protected virtual void Start()
    {
        enemySO.coins *= enemySO.multiplier;
        enemySO.ATK *= enemySO.multiplier;
        enemySO.HP *= enemySO.multiplier;
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
            UIController.Instance.CoinsUp(enemySO.coins);
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

    public override void TakeDamage(int damage)
    {
        enemySO.HP -= damage;
        if (enemySO.HP <= 0)
        {
            OnDeath();
        }
    }
}
