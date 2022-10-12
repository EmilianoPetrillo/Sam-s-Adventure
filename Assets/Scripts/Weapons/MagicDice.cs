using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDice : MonoBehaviour
{

    protected float speed;
    float t = 0;
    public float damage;
    public bool HasExplodingAnimation = false;
    private GameObject player;

    private void Start()
    {
        speed = 12;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        t += Time.deltaTime;
        if (t >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !HasExplodingAnimation)
        {
            collision.gameObject.GetComponent<Character>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player" && HasExplodingAnimation)
        {
            collision.gameObject.GetComponent<Character>().TakeDamage((int)damage);
            //Animator animator = GetComponent<Animator>();
            //animator.SetBool("Hit", true);
            speed = 0;
        }
    }
}
