using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    protected float speed;
    float t = 0;
    public float damage;

    protected virtual void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        t += Time.deltaTime;
        if(t >= 1.2f)
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
