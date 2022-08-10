using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess : Enemy
{
    protected override void Start()
    {
        base.Start();
        animator.GetCurrentAnimatorStateInfo(0);
        timer = true;
        enemySO.MAXHP = enemySO.HP;
    }

    public override void TakeDamage(int damage)
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Defend"))
            base.TakeDamage(damage);
    }

    protected override void Update()
    {
        if (deathCheck == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            Move();

        if (timer == true)
            t += Time.deltaTime;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && t >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            UIController.Instance.CoinsUp(enemySO.coins);
            GameController.Instance.LevelUp();
            Destroy(gameObject);
        }
        if(t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("Defend"))
        {
            animator.SetBool("Defend", false);
            animator.SetBool("Walk", true);
            timer = false;
            t = 0;
        }
        if (enemySO.HP <= enemySO.MAXHP / 3)
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
        if (t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
            timer = false;
            t = 0;
        }
        if (t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialAttack"))
        {
            animator.SetBool("SpecialAttack", false);
            animator.SetBool("Walk", true);
            timer = false;
            t = 0;
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

    private void StartHealPhase()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("SpecialAttack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Heal", true);
        timer = true;
        t = 0;
    }

    protected override void Attack()
    {
        timer = true;
        float x = Random.Range(0f, 1f);
        if (x <= 0.5f)
            animator.SetBool("Attack", true);
        else
            animator.SetBool("SpecialAttack", true);
    }

    public void Heal()
    {
        enemySO.MAXHP -= enemySO.MAXHP / 100;
        if (enemySO.HP <= enemySO.MAXHP)
            enemySO.HP += enemySO.HP/20;
    }
}
