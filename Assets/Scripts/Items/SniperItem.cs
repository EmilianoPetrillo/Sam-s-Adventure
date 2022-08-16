using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/Sniper")]
public class SniperItem : WeaponItem
{
    public override void Shoot(float angle)
    {
        float sniperatk = Player.Instance.PlayerSO.ATK * 1.5f * DamageMultiplier;
        projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, Quaternion.Euler(0, 0, angle));
        DamageCalculator(sniperatk, projectile);
        Debug.Log("lol");
    }
}
