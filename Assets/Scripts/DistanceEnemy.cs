using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : Character
{
    protected bool deathCheck = false;
    //Senza il controllo deathCheck (dentro OnDeath()) diversi proiettili potevano invocare la funzione OnDeath
    //facendo avanzare il player di più livelli in un colpo solo.
    protected float speed = 1.5f;

    Animator animator;
    Animation deathAnimation;
    public MagicDice magicDice;
    public HealthBar healthBar;

    private float t = 0;
    private bool timer;


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
        enemySO.coins *= enemySO.multiplier;
        enemySO.ATK *= enemySO.multiplier;
        enemySO.HP *= enemySO.multiplier;
        healthBar.SetMaxHealth(enemySO.HP);
    }

    private void Update()
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

    protected override void OnDeath()
    {
        if (deathCheck == false)
        {
            timer = true;
            deathCheck = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Dead", true);
        }
    }

    protected virtual void Move()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) > 6)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
            Attack();
    }

    public override void TakeDamage(int damage)
    {
        enemySO.HP -= damage;
        if (enemySO.HP >= 0)
            healthBar.SetHealth(enemySO.HP);
        if (enemySO.HP <= 0)
        {
            OnDeath();
        }
    }

    private void Attack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Attack", true);
    }

    public void DoDamage()
    {
        Player.Instance.TakeDamage(100);
    }

    public void Shoot()
    {
        GameObject go = Instantiate(magicDice.gameObject, transform.position, Quaternion.identity);
        MagicDice goMagicDice = go.GetComponent<MagicDice>();
    }
}
