using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    public static PlayerInventoryUI Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public List<ItemSlot> Slots = new List<ItemSlot>();

    public GameObject InventoryContainer;

    private void Start()
    {
        foreach(Transform t in InventoryContainer.transform)
        {
            if (t.GetComponent<ItemSlot>())
            {
                Slots.Add(t.GetComponent<ItemSlot>());
            }
        }

        // AddItemToInventory(ItemController.Instance.Items.GetItem(Item.eItemType.Consumable));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.SecretOfProtector));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.Shotgun));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.PlasmaSniper));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.Desolation));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.Dominion));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.BreakerOfTerror));
        AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.MessegerOfShadow));
    }

    public void AddItemToInventory(Item _item)
    {
        foreach(ItemSlot slot in Slots)
        {
            if(slot.Draggable.Item == null)
            {
                slot.Draggable.Item = _item;
                break;
            }
        }

        UpdateDisplay();
    }

    public void AddWeaponToInventory(WeaponItem _weaponItem)
    {
        foreach (ItemSlot slot in Slots)
        {
            if (slot.Draggable.Item == null)
            {
                slot.Draggable.Item = _weaponItem;
                break;
            }
        }

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        foreach(ItemSlot slot in Slots)
        {
            slot.UpdateItemDisplay();
        }
    }

}
