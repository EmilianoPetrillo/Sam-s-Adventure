using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject projectilePrefab0;
    public GameObject projectile0;
    public GameObject projectilePrefab1;
    public GameObject projectile1;
    public GameObject projectilePrefab2;
    public GameObject projectile2;

    int weapon = 0;//0 pistola, 1 pompa, 2 cecchino

    int ATK;

    private void Start()
    {
        ATK = 100;
    }

    public void Attack()
    {
        if (weapon == 0)
            PistolAttack();
        else if (weapon == 1)
            ShotgunAttack();
        else if (weapon == 2)
            SniperAttack();
    }

    public void ChangeWeapon()
    {
        if(weapon < 2)
        {
            weapon++;
        }
        else
        {
            weapon = 0;
        }
        print(weapon);
    }

    private void PistolAttack()
    {
        projectile0 = Instantiate(projectilePrefab0, transform.position + Vector3.right, Quaternion.identity);
        projectile0.GetComponent<Projectile>().damage = ATK;
    }

    private void ShotgunAttack()
    {
       for(int i = 0; i < 4; i++)
       {
            projectile1 = Instantiate(projectilePrefab1, transform.position + Vector3.right, new Quaternion(1, Random.Range(-0.2f, 0.2f), 0, 0));
            projectile1.GetComponent<Projectile>().damage = ATK * 0.5f; 
       }
    }

    private void SniperAttack()
    {
        projectile2 = Instantiate(projectilePrefab2, transform.position + Vector3.right, Quaternion.identity);
        projectile2.GetComponent<Projectile>().damage = ATK;
    }

}
