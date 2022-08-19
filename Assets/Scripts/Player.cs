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

    public Transform projectileSpawnPosition;
    public GameObject ActiveGun;
    public GameObject Arm;
    public GameObject Shield;

    public WeaponItem[] HeldWeapons;
    public ItemSlot[] HeldWeaponsSlots;

    public int weapon = 0;//0 pistola, 1 pompa, 2 cecchino

    private float t = 0;
    private bool timer = false;

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
        if(t < 2)
        {
            Shield.SetActive(true);
            timer = true;
        }
        else if (t >= 2)
        {
            Shield.SetActive(false);
            if(t >= 10)
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
        Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection.z = 0f;
        Vector3 aimDirection = (mouseDirection - Arm.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -30f, 30f);
        Arm.transform.eulerAngles = new Vector3(0, 0, angle);
        if (HeldWeapons[weapon] != null)
            HeldWeapons[weapon].Shoot(angle);
        else
            print("You have no weapon in your hands!");

    }

    private bool tutorial = true;

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
