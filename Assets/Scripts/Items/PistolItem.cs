using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/Pistol")]
public class PistolItem : WeaponItem
{
    public override void Shoot(float angle)
    {
        projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, Quaternion.Euler(0,0,angle));
        DamageCalculator(Player.Instance.PlayerSO.ATK, projectile);
    }
}
