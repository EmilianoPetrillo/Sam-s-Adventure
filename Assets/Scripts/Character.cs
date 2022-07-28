using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public CharacterSO characterSO;
    public virtual void TakeDamage(int damage)
    {
        characterSO.HP -= damage;
        Player.Instance.healthBar.SetHealth(characterSO.HP);
        if (characterSO.HP <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

}
