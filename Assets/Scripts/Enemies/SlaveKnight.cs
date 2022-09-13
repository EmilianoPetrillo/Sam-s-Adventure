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
        enemySO.MAXHP = enemySO.HP;
    }

    protected override void Update()
    {
        if (deathCheck == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") || animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                Move();

            if (enemySO.HP <= enemySO.MAXHP * (enemySO.healthAmountToTriggerHealPhaseInPercent / 100) && hasHealed == false)
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

    private bool hasJumped = false;

    protected override void Move()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[0])
        {
            if (!hasJumped)
                Jump();
            transform.Translate(Vector2.left * enemySO.moveSpeed * Time.deltaTime);
        }
        else
            Attack();
    }

    private bool hasHealed = false;
    public float jumpingSpeed;

    private void StartHealPhase()
    {
        hasHealed = true;
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Heal", true);
        timer = true;
        t = 0;
    }

    protected override void Attack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", false);
    }

    public void Heal()
    {
        if (enemySO.HP <= enemySO.MAXHP)
        {
            if (enemySO.HP + enemySO.MAXHP * (enemySO.healthAmountToRecoverInPercent / 100) <= enemySO.MAXHP)
                enemySO.HP += enemySO.MAXHP * (enemySO.healthAmountToRecoverInPercent / 100);
            else
                enemySO.HP = enemySO.MAXHP;
        }
        healthBar.SetHealth(enemySO.HP);
    }

    private bool moveVertically;

    public void Jump()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", true);
        if (moveVertically)
        {
            transform.Translate(Vector2.up * jumpingSpeed * Time.deltaTime);
        }
    }

    public void GoUp()
    {
        moveVertically = true;
    }

    public void GoDown()
    {
        jumpingSpeed = -jumpingSpeed;
    }

    public void Land()
    {
        moveVertically = false;
    }

    public void EndJump()
    {
        hasJumped = true;
        animator.SetBool("Walk", true);
        animator.SetBool("Jump", false);
    }
}
