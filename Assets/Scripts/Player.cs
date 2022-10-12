using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Transform projectileSpawnPosition;
    public GameObject ActiveGun;
    public GameObject Arm;
    public GameObject Shield;
    public GameObject ShieldButton;

    public WeaponItem[] HeldWeapons;
    public ItemSlot[] HeldWeaponsSlots;

    public int weapon = 0;//0 pistola, 1 pompa, 2 cecchino

    private float t = 0;
    private float t1 = 0;
    private float t2 = 0;
    private bool timer = false;
    public bool timer1 = false;
    public bool timer2 = false;
    private Coroutine shootCoroutine = null;

    private void Start()
    {
        healthBar.SetMaxHealth(playerSO.HP);
        playerSO.MAXHP = playerSO.HP;
    }

    private void Update()
    {
        healthBar.SetHealth(playerSO.HP);
        if (playerSO.HP <= 0)
        {
            healthBar.SetHealth(0);
            OnDeath();
        }
       
        if (timer)
        {
            t += Time.deltaTime;
            if (t >= 2)
                ShieldOn();
        }

        if (timer1 && HeldWeapons[weapon] != null)
        {
            t1 += Time.deltaTime;
            if (t1 >= 1 / HeldWeapons[weapon].FireRate)
            {
                StopShooting();
                t1 = 0;
                timer1 = false;
            }
        }

        if (timer2 && HeldWeapons[weapon] != null)
        {
            t2 += Time.deltaTime;
            if (t2 >= 1 / HeldWeapons[weapon].FireRate)
            {
                t2 = 0;
                timer2 = false;
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateHeldWeapons();
    }

    public override void TakeDamage(int damage)
    {
        if (!Shield.activeSelf)
        {
            playerSO.HP -= damage;
            if (characterSO.HP <= 0)
            {
                OnDeath();
            }
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

    public void ShieldOn()
    {
        if(t < PlayerSO.SHIELDTIMEON)
        {
            Shield.SetActive(true);
            timer = true;
        }
        else if (t >= PlayerSO.SHIELDTIMEON)
        {
            Shield.SetActive(false);
            //Shield CDR: 5 sec
            if(t >= PlayerSO.SHIELDCOOLDOWN)
            {
                timer = false;
                t = 0;
            }
        }
    }

    public void UpdateHeldWeapons()
    {
        if (HeldWeaponsSlots[0].GetComponentInChildren<DraggableItem>() != null)
            HeldWeapons[0] = HeldWeaponsSlots[0].GetComponentInChildren<DraggableItem>().Item as WeaponItem;
        else
            HeldWeapons[0] = null;

        if (HeldWeaponsSlots[1].GetComponentInChildren<DraggableItem>() != null)
            HeldWeapons[1] = HeldWeaponsSlots[1].GetComponentInChildren<DraggableItem>().Item as WeaponItem;
        else
            HeldWeapons[1] = null;
        
        if (HeldWeapons[weapon] != null)
            ActiveGun.GetComponent<SpriteRenderer>().sprite = HeldWeapons[weapon].image;
        else
            ActiveGun.GetComponent<SpriteRenderer>().sprite = null;

        if (HeldWeapons[weapon] != null)
            UIController.Instance.ActiveGunImage.sprite = HeldWeapons[weapon].image;
        else
            UIController.Instance.ActiveGunImage.sprite = null;
    }

    public void Attack()
    {
        if(HeldWeapons[weapon] != null && HeldWeapons[weapon].Shooting == false)
        {
            
            if (HeldWeapons[weapon].weaponFireType == WeaponItem.eWeaponFireType.Manual && timer1 == false)
            {
                HeldWeapons[weapon].StartShoot();
                timer1 = true;
                shootCoroutine = StartCoroutine(HeldWeapons[weapon].Shoot(Arm));
            }

            if (HeldWeapons[weapon].weaponFireType == WeaponItem.eWeaponFireType.Automatic && timer2 == false)
            {
                shootCoroutine = StartCoroutine(HeldWeapons[weapon].Shoot(Arm));
                timer2 = true;
            }
        }
        else
            print("You have no weapon in your hands!");

    }

    public void StopAutoShoot()
    {
        timer1 = true;
    }

    public void StopShooting()
    { if (HeldWeapons[weapon] != null)
        HeldWeapons[weapon].StopShoot();
        if(shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
    }

    private bool tutorial = false;

    public void ChangeWeapon()
    {
        if (weapon < 1)
            weapon++;
        else if(weapon == 1)
            weapon = 0;

        if (tutorial)
        {
            UITextController.Instance.NextPhase();
            tutorial = false;
        }
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
        playerSO.ATK = playerSO.ATK + (25 * UIController.Instance.keyATK);
    }

    public void HPUpgrade()
    {
        playerSO.MAXHP = playerSO.MAXHP + (100 * UIController.Instance.keyHP);
        playerSO.HP = playerSO.HP + (100 * UIController.Instance.keyHP);
        healthBar.SetMaxHealth(playerSO.MAXHP);
    }

    public void CRITRATEUpgrade()
    {
        if (playerSO.CRITRATE < 100)
            playerSO.CRITRATE += 1f;
        else
            CRITDAMAGEUpgrade();
    }

    public void CRITDAMAGEUpgrade()
    {
        playerSO.CRITDAMAGE = playerSO.CRITDAMAGE + (4f * UIController.Instance.keyCRITDAMAGE);
    }

    #endregion
}
