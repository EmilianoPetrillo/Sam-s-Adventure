using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class OldGolem : Enemy
{

    public GameObject Projectile;
    public Transform ProjectileSpawnPosition;

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

            if (enemySO.HP <= enemySO.MAXHP * (enemySO.healthAmountToTriggerHealPhaseInPercent / 100) && hasSpited == false)
            {
                Spit();
            }

            if (t >= animator.GetCurrentAnimatorStateInfo(0).length && animator.GetCurrentAnimatorStateInfo(0).IsName("Spit"))
            {
                animator.SetBool("Spit", false);
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

    private bool hasSpited = false;

    protected override void Move()
    {

        Vector2 pos = transform.position;
        transform.position = pos;
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[0])
        {
            transform.Translate(Vector2.left * enemySO.moveSpeed * Time.deltaTime);
        }
        else
            Attack();
    }
    private void Spit()
    {
        hasSpited = true;
        animator.SetBool("Attack", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Spit", true);
        timer = true;
        t = 0;

        GameObject go = Instantiate(Projectile, ProjectileSpawnPosition.position, Quaternion.identity);
        go.GetComponent<MagicDice>().damage = enemySO.ATK * 2;
    }

    protected override void Attack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Spit", false);
    }

    //public void Spit()
    //{
    //    if (enemySO.HP <= enemySO.MAXHP)
    //    {
    //        if (enemySO.HP + enemySO.MAXHP * (enemySO.healthAmountToRecoverInPercent / 100) <= enemySO.MAXHP)
    //        {
    //            GameObject go = Instantiate LavaSpit, LavaSpitSpawnPosition.position, Quaternion.identity);
    //            go.GetComponent<LavaSpit>().damage = enemySO.ATK * 2;
    //        }
    //    }
    //}
}

