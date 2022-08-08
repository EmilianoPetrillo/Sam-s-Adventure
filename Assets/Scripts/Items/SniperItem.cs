using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/Sniper")]
public class SniperItem : WeaponItem
{
    public override void Shoot()
    {
        float sniperatk = Player.Instance.PlayerSO.ATK * 1.5f;
        projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, Quaternion.identity);
        DamageCalculator(sniperatk, projectile);
        Debug.Log("lol");
    }
}
