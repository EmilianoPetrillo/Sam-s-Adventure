using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item.eItemType SlotType;
    public DraggableItem Draggable;
    public int WeaponSlot;

    public void OnDrop(DraggableItem _draggableItem)
    {
        Item tempItem = Draggable.Item;
        Draggable.Item = _draggableItem.Item;
        if(tempItem != null)
        {
            _draggableItem.Item = tempItem;
        }
        else
        {
            _draggableItem.Item = null;
        }

        UpdateItemDisplay();
        _draggableItem.ParentSlot.UpdateItemDisplay();

        if (SlotType == Item.eItemType.Weapon)
        {
            Player.Instance.HeldWeapons[WeaponSlot] = Draggable.Item as WeaponItem;
        }
    }

    public void UpdateItemDisplay()
    {
        if(Draggable.Item != null)
        {
            Draggable.ItemImage.gameObject.SetActive(true);
            Draggable.ItemImage.sprite = Draggable.Item.image;
        }
        else
        {
            Draggable.ItemImage.gameObject.SetActive(false);
        }
    }
}
