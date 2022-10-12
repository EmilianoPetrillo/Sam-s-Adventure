using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/Sniper")]
public class SniperItem : WeaponItem
{
    public override IEnumerator Shoot(GameObject Arm)
    {
        while (true)
        {
            Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDirection.z = 0f;
            Vector3 aimDirection = (mouseDirection - Arm.transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -5f, 30f);
            Arm.transform.eulerAngles = new Vector3(0, 0, angle);
            float sniperatk = Player.Instance.PlayerSO.ATK * 2.5f * DamageMultiplier;
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
            int diceRolling = UnityEngine.Random.Range(0, 100);
            if (diceRolling <= Player.Instance.PlayerSO.CRITRATE)
            {
                sniperatk += Player.Instance.PlayerSO.CRITDAMAGE * DamageMultiplier;
            }
            projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, Quaternion.Euler(0, 0, angle));
            DamageCalculator(sniperatk, projectile);
            yield return new WaitForSeconds(1 / FireRate);
        }
    }
}
