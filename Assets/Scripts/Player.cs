using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public static Player Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject projectilePrefab0;
    GameObject projectile0;
    public GameObject projectilePrefab1;
    GameObject projectile1;
    public GameObject projectilePrefab2;
    GameObject projectile2;

    int weapon = 0;//0 pistola, 1 pompa, 2 cecchino

    private void Start()
    {
        ATK = 100;
        HP = 1000;
        CRITRATE = 0;
        CRITDAMAGE = 150;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            HP -= 100;
        if (HP <= 0)
        {
            OnDeath();
        }
    }

    protected override void OnDeath()
    {
        GameController.Instance.ForcedLevelDown();
    }

    public void PlayerRespawn()
    {
        HP = 1000;
    }

    #region ATTACK TYPES
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
        if (weapon < 2)
        {
            weapon++;
        }
        else
        {
            weapon = 0;
        }
    }

    private void PistolAttack()
    {
        projectile0 = Instantiate(projectilePrefab0, transform.position + Vector3.right, Quaternion.identity);
        CheckCrit(ATK, projectile0);
    }

    private void ShotgunAttack()
    {
        float shotgunatk = ATK / 2;
        for(int i = 0; i < 4; i++)
        {
            projectile1 = Instantiate(projectilePrefab1, transform.position + Vector3.right, new Quaternion(1, Random.Range(-0.2f, 0.2f), 0, 0));
            CheckCrit(shotgunatk, projectile1);
        }
    }

    private void SniperAttack()
    {
        float sniperatk = ATK * 1.5f;
        projectile2 = Instantiate(projectilePrefab2, transform.position + Vector3.right, Quaternion.identity);
        CheckCrit(sniperatk, projectile2);
    }

    private void CheckCrit(float dmg, GameObject projectile)
    {
        if (Random.Range(0f, 1f) <= (CRITRATE / 100))
            projectile.GetComponent<Projectile>().damage = dmg * (CRITDAMAGE / 100);
        else
            projectile.GetComponent<Projectile>().damage = dmg;
    }
    #endregion
    #region STATS UPGRADE METHODS
    public void ATKUpgrade()
    {
        ATK += 10;
    }

    public void HPUpgrade()
    {
        HP += 10;
    }

    public void CRITRATEUpgrade()
    {
        CRITRATE += 0.5f;
    }

    public void CRITDAMAGEUpgrade()
    {
        CRITDAMAGE += 0.5f;
    }
    #endregion
}
