using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public static Player Instance;
    public HealthBar healthBar;
    public Animator animator;
    private CharacterSO playerSO;

    public CharacterSO PlayerSO
    {
        get { return playerSO; }
    }

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
    public Transform projectileSpawnPosition;
    public GameObject[] Guns;
    public GameObject ActiveGun;
    public GameObject Arm;

    int weapon = 0;//0 pistola, 1 pompa, 2 cecchino

    private void Start()
    {
        healthBar.SetMaxHealth(playerSO.HP);
        playerSO.MAXHP = playerSO.HP;
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
        GameController.Instance.OnPlayerDeath();
        PlayerRespawn();
    }

    public void PlayerRespawn()
    {
        playerSO.HP = playerSO.MAXHP;
        healthBar.SetMaxHealth(playerSO.MAXHP);
    }

    #region WEAPONS STUFF

    public void Attack()
    {
        if (weapon == 0)
            PistolAttack();
        else if (weapon == 1)
            ShotgunAttack();
        //else if (weapon == 2)
          //  SniperAttack();
    }

    private bool tutorial = true;

    public void ChangeWeapon()
    {
        if (weapon < 1)
        {
            ActiveGun.transform.localPosition = new Vector2(-0.056f, 0.007f);
            weapon++;
        }
        else if(weapon == 1)
        {
            weapon = 0;
            ActiveGun.transform.localPosition = new Vector2(0.03600002f, 0.03199995f);
        }

        UIController.Instance.ChangeActiveWeapon();
        ActiveGun.GetComponent<SpriteRenderer>().sprite = Guns[weapon].GetComponent<SpriteRenderer>().sprite;

        if (tutorial)
        {
            UITextController.Instance.NextPhase();
            tutorial = false;
        }
    }

    private void PistolAttack()
    {
        projectile0 = Instantiate(projectilePrefab0, projectileSpawnPosition.position, Quaternion.identity);
        CheckCrit(playerSO.ATK, projectile0);
        
    }

    private void ShotgunAttack()
    {
        float shotgunatk = playerSO.ATK / 2;
        for(int i = 0; i < 4; i++)
        {
            projectile1 = Instantiate(projectilePrefab1, projectileSpawnPosition.position, new Quaternion(1, Random.Range(-0.2f, 0.2f), 0, 0));
            CheckCrit(shotgunatk, projectile1);
        }
    }

    private void SniperAttack()
    {
        float sniperatk = playerSO.ATK * 1.5f;
        projectile2 = Instantiate(projectilePrefab2, projectileSpawnPosition.position, Quaternion.identity);
        CheckCrit(sniperatk, projectile2);
    }

    private void CheckCrit(float dmg, GameObject projectile)
    {
        if (Random.Range(0f, 1f) <= (playerSO.CRITRATE / 100))
            projectile.GetComponent<Projectile>().damage = dmg * (playerSO.CRITDAMAGE / 100);
        else
            projectile.GetComponent<Projectile>().damage = dmg;
    }

    [SerializeField] float gunMovement;

    public void GunMove()
    {
        Arm.transform.localPosition = new Vector2(Arm.transform.localPosition.x - gunMovement, Arm.transform.localPosition.y);
        gunMovement = -gunMovement;
    }

    #endregion

    #region STATS UPGRADE METHODS

    public void ATKUpgrade()
    {
        playerSO.ATK += 25;
    }

    public void HPUpgrade()
    {
        playerSO.MAXHP += 100;
        playerSO.HP += 100;
        healthBar.SetMaxHealth(playerSO.MAXHP);
    }

    public void CRITRATEUpgrade()
    {
        playerSO.CRITRATE += 1f;
    }

    public void CRITDAMAGEUpgrade()
    {
        playerSO.CRITDAMAGE += 1f;
    }

    #endregion
}
