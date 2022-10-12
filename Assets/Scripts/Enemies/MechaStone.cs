using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaStone : Enemy
{
    private bool laserBeamTimer = false;
    private bool glowingTimer = false;
    private bool canMove = false;
    private bool shoot = false;
    private float t1;
    private float t2;

    public GameObject laserBeam;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector2(transform.position.x ,-4.2f);
        enemySO.coins = enemySO.coins / enemySO.multiplier / 2;
    }

    protected override void Move()
    {
        if (canMove)
            Movement();

        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[0])
            canMove = true;
        else if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= enemySO.attackRange[0] && Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[1] && !shoot)
            LaserBeamAttack();
        else if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= enemySO.attackRange[1])
            Attack();

        if (laserBeamTimer == true)
            t1 += Time.deltaTime;
        if (t1 >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            laserBeamTimer = false;
            t1 = 0;
            laserBeam.SetActive(true);
        }
    }

    public void EndLaserBeam()
    {
        animator.SetBool("LaserBeam", false);
        animator.SetBool("Idle", true);
        canMove = true;
        laserBeam.SetActive(false);
        enemySO.ATK *= 3;
    }

    private void Movement()
    {
        transform.Translate(Vector2.left * enemySO.moveSpeed * Time.deltaTime);
    }

    protected void LaserBeamAttack()
    {
        canMove = false;
        animator.SetBool("Glowing", true);
        glowingTimer = true;
        if (glowingTimer)
            t2 += Time.deltaTime;
        if(t2 >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            glowingTimer = false;
            t2 = 0;
            shoot = true;
            animator.SetBool("Idle", false);
            animator.SetBool("LaserBeam", true);
            laserBeamTimer = true;
        }
    }

    protected override void Attack()
    {
        base.Attack();
        canMove = false;
    }

    protected override void OnDeath()
    {
        //the stuff itself is in the update
        if (deathCheck == false)
        {
            timer = true;
            deathCheck = true;
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
            animator.SetBool("LaserBeam", false);
            animator.SetBool("RangedAttack", false);
            animator.SetBool("Dead", true);
        }
    }

}
