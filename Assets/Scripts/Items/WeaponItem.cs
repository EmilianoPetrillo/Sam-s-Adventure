using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon Item", menuName ="Create Weapon Item")]
public abstract class WeaponItem : Item
{
    public enum eWeaponType
    {
        Pistol,
        Shotgun,
        Sniper
    }

    public eWeaponType WeaponType;
    public GameObject projectilePrefab;
    protected GameObject projectile;
    public int WeaponCost;
    public float DamageMultiplier;
    public float ShootRate;
    protected float time;
    public bool Shooting = false;

    public virtual IEnumerator Shoot(GameObject Arm)
    {
        yield break;    
    }
    public void StopShoot()
    {
        Shooting = false;
    }
    public void StartShoot()
    {
        Shooting = true;
    }
    protected void DamageCalculator(float dmg, GameObject projectile)
    {
        if (Random.Range(0f, 1f) <= (Player.Instance.PlayerSO.CRITRATE / 100))
            projectile.GetComponent<Projectile>().damage = dmg * (Player.Instance.PlayerSO.CRITDAMAGE / 100);
        else
            projectile.GetComponent<Projectile>().damage = dmg;
    }

    public void BuyItem()
    {
        if (UIController.Instance.Coins >= WeaponCost)
        {
            UIController.Instance.CoinsUp(-WeaponCost);
            PlayerInventoryUI.Instance.AddWeaponToInventory(this);
            Debug.Log("You have acquired " + name);
        }
    }

    public virtual void Update()
    {
        
    }
}
