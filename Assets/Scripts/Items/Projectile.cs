using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    protected float speed;
    protected float t = 0;
    protected float expireTime = 1.2f;
    public float damage;

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        t += Time.deltaTime;
        if(t >= expireTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Character>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }

}
