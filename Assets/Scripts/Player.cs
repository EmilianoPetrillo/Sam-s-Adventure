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

    public WeaponItem[] HeldWeapons;
    public ItemSlot[] HeldWeaponsSlots;

    int weapon = 0;//0 pistola, 1 pompa, 2 cecchino

    private void Start()
    {
        healthBar.SetMaxHealth(playerSO.HP);
        playerSO.MAXHP = playerSO.HP;
    }

    private void Update()
    {
        if (playerSO.HP <= 0)
        {
            OnDeath();
        }
        healthBar.SetHealth(playerSO.HP);
    }

    private void FixedUpdate()
    {
        UpdateHeldWeapons();
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
    }

    public void Attack()
    {
        if (HeldWeapons[weapon] != null)
            HeldWeapons[weapon].Shoot();
        else
            print("You have no weapon in your hands!");
    }

    private bool tutorial = true;

    public void ChangeWeapon()
    {
        if (weapon < 1)
        {
            weapon++;
            ActiveGun.transform.localPosition = new Vector2(-0.056f, 0.007f);
        }
        else if(weapon == 1)
        {
            weapon = 0;
            ActiveGun.transform.localPosition = new Vector2(0.03600002f, 0.03199995f);
        }

        UIController.Instance.ChangeActiveWeapon();

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
