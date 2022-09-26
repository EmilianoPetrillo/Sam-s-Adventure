using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdditionalShopItemInfo : MonoBehaviour, IPointerClickHandler
{
    public WeaponItem weaponInfo;
    public bool isOutOfStock = false;

    public void Start()
    {
        isOutOfStock = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && !isOutOfStock)
        {
            if (UIController.Instance.Coins >= weaponInfo.WeaponCost)
            {
                isOutOfStock = true;
                transform.GetComponent<Image>().sprite = ShopController.Instance.OutOfStockImage;
                UIController.Instance.CoinsDown(weaponInfo.WeaponCost);
                PlayerInventoryUI.Instance.AddWeaponToInventory(ItemController.Instance.Items.GetWeapon(weaponInfo.WeaponType));
            }
            
        }
    }
}
