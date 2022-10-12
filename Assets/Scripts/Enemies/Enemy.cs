using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Enemy : Character
{
    protected bool deathCheck = false;
    //Senza il controllo deathCheck (dentro OnDeath()) diversi proiettili potevano invocare la funzione OnDeath
    //facendo avanzare il player di più livelli in un colpo solo.

    protected Animator animator;
    public HealthBar healthBar;

    protected float t = 0;
    protected bool timer;

    public bool canRevive;

    protected EnemySO enemySO;
    public EnemySO EnemySO
    {
        get { return enemySO; }
    }

    protected void Awake()
    {
        enemySO = Object.Instantiate<EnemySO>((EnemySO)characterSO);
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        enemySO.coins *= enemySO.multiplier * 2;
        enemySO.ATK *= enemySO.multiplier;
        enemySO.HP *= enemySO.multiplier;
        healthBar.SetMaxHealth(enemySO.HP);
        //print("Multiplier:" + enemySO.multiplier);
        enemySO.MAXHP = enemySO.HP;
 
    }

    protected virtual void Update()
    {
       
        if (deathCheck == false)
            Move();
        if (timer == true)
            t += Time.deltaTime;
        if (t >= animator.GetCurrentAnimatorStateInfo(0).length && !canRevive)
        {
            UIController.Instance.CoinsUp(enemySO.coins);
            if (gameObject.name == "CultistPriest")
            {
                GameController.Instance.SpawnDevourer();
            }
            else
            {
                GameController.Instance.LevelUp();
            }
                
            Destroy(gameObject);
        }
        else if(t >= animator.GetCurrentAnimatorStateInfo(0).length && canRevive)
        {
            t = 0;
            Respawn();
        }
       
        
    }

    protected virtual void Move()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[0])
        {
            transform.Translate(Vector2.left * enemySO.moveSpeed * Time.deltaTime);
        }
        else
            Attack();
    }

    protected override void OnDeath()
    {
        //the stuff itself is in the update
        if (deathCheck == false)
        {
            timer = true;
            deathCheck = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Dead", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public override void TakeDamage(int damage)
    {
        enemySO.HP -= damage;
        if (enemySO.HP > 0)
            healthBar.SetHealth(enemySO.HP);
        if (enemySO.HP <= 0)
        {
            healthBar.SetHealth(0);
            OnDeath();
        }
    }

    protected virtual void Attack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);
    }

    public void DoDamage()
    {
        Player.Instance.TakeDamage((int)enemySO.ATK);
    }

    public void Respawn()
    {
        enemySO.HP = enemySO.MAXHP;
        healthBar.SetHealth(enemySO.HP);
        animator.SetBool("Dead", false);
        animator.SetBool("Revive", true);
    }

    public void EndRespawn()
    {
        timer = false;
        t = 0;
        animator.SetBool("Revive", false);
        animator.SetBool("Walk", true);
        canRevive = false;
        GetComponent<BoxCollider2D>().enabled = true;
        deathCheck = false;
    }
}