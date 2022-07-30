using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDice : MonoBehaviour
{

    protected float speed;
    float t = 0;
    public float damage;

    private void Start()
    {
        speed = -5;
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        t += Time.deltaTime;
        if (t >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }

    //public void OnTriggerEnter()
    //{

    //}
}
