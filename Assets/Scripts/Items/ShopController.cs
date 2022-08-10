using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

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

    public List<WeaponItem> WeaponsToSell = new List<WeaponItem>();
    public List<Image> WeaponsToSellImage = new List<Image>();
    public List<Text> WeaponsCostText = new List<Text>();

    private void Start()
    {
        for(int i = 0; i <= WeaponsToSell.Count - 1; i++)
            WeaponsCostText[i].text = WeaponsToSell[i].WeaponCost.ToString();
        for (int i = 0; i <= WeaponsToSell.Count - 1; i++)
            WeaponsToSellImage[i].sprite = WeaponsToSell[i].image;
    }
}
