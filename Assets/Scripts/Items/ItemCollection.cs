using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Collection", menuName ="Create Item Collection")]
public class ItemCollection : ScriptableObject
{
    public List<Item> Items = new List<Item>();
    public List<WeaponItem> WeaponItems = new List<WeaponItem>();

    public Item GetItem(Item.eItemType _type)
    {
        List<Item> possibleItems = new List<Item>();

        for(int i = 0; i < Items.Count; i++)
        {
            if(Items[i].ItemType == _type)
                possibleItems.Add(Items[i]);
        }

        if (possibleItems.Count == 0)
            Debug.Log("No Items of Type " + _type);

        Item item = possibleItems[Random.Range(0, possibleItems.Count)];

        return item;
    }

    public WeaponItem GetWeapon(WeaponItem.eWeaponType _weaponType)
    {
        List<WeaponItem> possibleWeaponItems = new List<WeaponItem>();

        for (int i = 0; i < Items.Count; i++)
        {
            if (WeaponItems[i].WeaponType == _weaponType)
                possibleWeaponItems.Add(WeaponItems[i]);
        }

        if (possibleWeaponItems.Count == 0)
            Debug.Log("No Weapons of Type " + _weaponType);

        WeaponItem weaponItem = possibleWeaponItems[Random.Range(0, possibleWeaponItems.Count)];

        return weaponItem;
    }
}
