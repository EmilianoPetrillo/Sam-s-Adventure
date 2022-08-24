//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveKnight : Enemy
{
    protected override void Start()
    {
        base.Start();
        animator.GetCurrentAnimatorStateInfo(0);
        timer = true;
        enemySO.MAXHP = enemySO.HP;
    }

    //public override void TakeDamage(int damage)
    //{
    //    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Pray"))
    //        base.TakeDamage(damage);
    //}

    protected override void Update()
    {
        if (deathCheck == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                Move();

            //if (t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("Pray"))
            //{
            //    animator.SetBool("Pray", false);
            //    animator.SetBool("Walk", true);
            //    timer = false;
            //    t = 0;
            //}

            if (enemySO.HP <= enemySO.MAXHP / 3 && hasHealed == false)
            {
                StartHealPhase();
            }

            if (t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("Heal"))
            {
                animator.SetBool("Heal", false);
                animator.SetBool("Walk", true);
                timer = false;
                t = 0;
            }

            //if (t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("AttackfromAir"))
            //{
            //    animator.SetBool("AttackFromAir", false);
            //    animator.SetBool("Walk", true);
            //    timer = false;
            //    t = 0;
            //}
        }

        if (timer == true)
            t += Time.deltaTime;

        if (deathCheck && t >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            UIController.Instance.CoinsUp(enemySO.coins);
            GameController.Instance.LevelUp();
            Destroy(gameObject);
        }
    }

    protected override void Move()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[0])
        {
            transform.Translate(Vector2.left * enemySO.moveSpeed * Time.deltaTime);
        }
        else
            Attack();
    }

    private bool hasHealed = false;

    private void StartHealPhase()
    {
        hasHealed = true;
        animator.SetBool("Attack", false);
        //animator.SetBool("AttackfromAir", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Heal", true);
        timer = true;
        t = 0;
    }

    protected override void Attack()
    {
        //float x = Random.Range(0f, 2f);
        //if (x <= 0.5f)
        animator.SetBool("Attack", true);
        animator.SetBool("Walk", false);
        //else
        //    animator.SetBool("Roll", true);
    }

    public void Heal()
    {
        if (enemySO.HP <= enemySO.MAXHP)
        {
            if (enemySO.HP + enemySO.MAXHP / 20 <= enemySO.MAXHP)
                enemySO.HP += enemySO.MAXHP / 20;
            else
                enemySO.HP = enemySO.MAXHP;
        }
        healthBar.SetHealth(enemySO.HP);
    }

    //public void Jump()
    //{
        
    //}
}
