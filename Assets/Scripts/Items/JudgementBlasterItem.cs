using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Create Weapon Item/JudgementBlaster")]
public class JudgementBlaster : WeaponItem
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
            float JudgementBlasteratk = Player.Instance.PlayerSO.ATK * DamageMultiplier;
            projectile = Instantiate(projectilePrefab, Player.Instance.projectileSpawnPosition.position, Quaternion.Euler(0, 0, angle));
            DamageCalculator(JudgementBlasteratk, projectile);
            yield break;
        }
    }
}
