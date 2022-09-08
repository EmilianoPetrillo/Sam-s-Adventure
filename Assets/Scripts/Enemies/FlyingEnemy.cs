using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy   
{
    public float speed;
    private GameObject player;
    float sinCenterY;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector2(transform.position.x, transform.position.y + 4);
        sinCenterY = transform.position.y;
    }

    protected override void Update()
    {
        if (deathCheck == false)
            Move();
        if (timer == true)
            t += Time.deltaTime;
        if (t >= animator.GetCurrentAnimatorStateInfo(0).length)
        {
            UIController.Instance.CoinsUp(enemySO.coins);
            GameController.Instance.LevelUp();
            Destroy(gameObject);
        }
    }


    protected override void Move()
    {

        Vector2 pos = transform.position;

        float sin = Mathf.Sin(pos.x);
        pos.y = sinCenterY + sin;

        transform.position = pos;
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > enemySO.attackRange[0])
        {
            //    transform.Translate(Vector2.left * enemySO.moveSpeed * Time.deltaTime);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
            Attack();
    }
}
