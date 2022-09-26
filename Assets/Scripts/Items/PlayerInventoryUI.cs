using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private List<ItemSlot> Slots = new List<ItemSlot>();
    private List<ItemSlot> HeldWeapons = new List<ItemSlot>();

    public GameObject InventoryContainer;
    public GameObject EquipmentContainer;

    public void StartInventory()
    {
        foreach(Transform t in InventoryContainer.transform)
        {
            if (t.GetComponent<ItemSlot>())
            {
                Slots.Add(t.GetComponent<ItemSlot>());
            }
        }
        foreach (Transform t in EquipmentContainer.transform)
        {
            if (t.GetComponent<ItemSlot>())
            {
                HeldWeapons.Add(t.GetComponent<ItemSlot>());
            }
        }

        AddWeaponToHand(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.Dominion));
        AddWeaponToHand(ItemController.Instance.Items.GetWeapon(WeaponItem.eWeaponType.Shotgun));
        Debug.Log(HeldWeapons[0].transform.GetChild(0).GetComponent<Image>().sprite);
        UIController.Instance.ActiveGunImage.sprite = HeldWeapons[0].transform.GetChild(0).GetComponent<Image>().sprite;
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
    public void AddWeaponToHand(WeaponItem _weaponItem)
    {
        foreach (ItemSlot slot in HeldWeapons)
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
        foreach (ItemSlot slot in HeldWeapons)
        {
            slot.UpdateItemDisplay();
        }
    }

}
