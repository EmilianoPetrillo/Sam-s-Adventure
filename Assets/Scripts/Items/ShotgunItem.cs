using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/Shotgun")]
public class ShotgunItem : WeaponItem
{
    public override void Shoot()
    {
        float shotgunatk = Player.Instance.PlayerSO.ATK / 2;
        for (int i = 0; i < 4; i++)
        {
            projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, new Quaternion(1, Random.Range(-0.2f, 0.2f), 0, 0));
            DamageCalculator(shotgunatk, projectile);
        }
        Debug.Log("lol");
    }
}
