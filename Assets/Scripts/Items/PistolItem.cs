using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/Pistol")]
public class PistolItem : WeaponItem
{
    public override void Shoot()
    {
        projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, Quaternion.identity);
        DamageCalculator(Player.Instance.PlayerSO.ATK, projectile);
        Debug.Log("lol");
    }
}
