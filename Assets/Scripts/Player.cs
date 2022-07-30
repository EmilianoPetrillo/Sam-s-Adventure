using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public static Player Instance;
    public HealthBar healthBar;
    public Animator animator;
    private CharacterSO playerSO;

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
        playerSO = Object.Instantiate(characterSO);
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
        healthBar.SetMaxHealth(playerSO.HP);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //characterSO.HP -= 100;
            //healthBar.SetHealth(characterSO.HP);
        }
        if (playerSO.HP <= 0)
        {
            OnDeath();
        }
        healthBar.SetHealth(playerSO.HP);
    }

    public override void TakeDamage(int damage)
    {
        playerSO.HP -= damage;
        if (characterSO.HP <= 0)
        {
            OnDeath();
        }
    }

    protected override void OnDeath()
    {
        UIController.Instance.DeathPanelOn();  
    }

    public void PlayerRespawn()
    {
        playerSO.HP = 800;
        healthBar.SetMaxHealth(playerSO.HP);
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
        CheckCrit(playerSO.ATK, projectile0);
        
    }

    private void ShotgunAttack()
    {
        float shotgunatk = playerSO.ATK / 2;
        for(int i = 0; i < 4; i++)
        {
            projectile1 = Instantiate(projectilePrefab1, transform.position + Vector3.right, new Quaternion(1, Random.Range(-0.2f, 0.2f), 0, 0));
            CheckCrit(shotgunatk, projectile1);
        }
    }

    private void SniperAttack()
    {
        float sniperatk = playerSO.ATK * 1.5f;
        projectile2 = Instantiate(projectilePrefab2, transform.position + Vector3.right, Quaternion.identity);
        CheckCrit(sniperatk, projectile2);
    }

    private void CheckCrit(float dmg, GameObject projectile)
    {
        if (Random.Range(0f, 1f) <= (playerSO.CRITRATE / 100))
            projectile.GetComponent<Projectile>().damage = dmg * (playerSO.CRITDAMAGE / 100);
        else
            projectile.GetComponent<Projectile>().damage = dmg;
    }
    #endregion
    #region STATS UPGRADE METHODS
    public void ATKUpgrade()
    {
        playerSO.ATK += 10;
    }

    public void HPUpgrade()
    {
        playerSO.HP += 10;
    }

    public void CRITRATEUpgrade()
    {
        playerSO.CRITRATE += 0.5f;
    }

    public void CRITDAMAGEUpgrade()
    {
        playerSO.CRITDAMAGE += 0.5f;
    }
    #endregion
}
