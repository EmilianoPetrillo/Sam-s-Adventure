using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaStone : Enemy
{

    public float meleeRange; //this should be in the SO
    private bool laserBeamTimer = false;
    private bool canMove = false;
    private bool shoot = false;
    private float t1;

    public GameObject laserBeam;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector2(transform.position.x ,-4.2f);
    }

    protected override void Move()
    {
        if (canMove)
            Movement();

        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange)
            canMove = true;
        else if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= enemySO.attackRange && Vector2.Distance(transform.position, Player.Instance.transform.position) > meleeRange && !shoot)
            LaserBeamAttack();
        else if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= meleeRange)
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
        shoot = true;
        canMove = false;
        animator.SetBool("Idle", false);
        animator.SetBool("LaserBeam", true);
        laserBeamTimer = true;
    }

    protected override void Attack()
    {
        base.Attack();
        canMove = false;
    }

}
