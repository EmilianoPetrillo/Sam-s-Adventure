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


    private void Start()
    {
        
        Transform shopContent = transform.transform.GetChild(2).GetChild(0);
        List<Transform> contentLines = new List<Transform>();
        foreach(Transform item in shopContent)
        {
            contentLines.Add(item);
        }
        for (int i = 0; i < WeaponsToSell.Count; i++)
        {
            //Image
            contentLines[i].GetChild(0).GetComponent<Image>().sprite = WeaponsToSell[i].image;
            //Cost
            int cost = WeaponsToSell[i].WeaponCost;
            string costString = cost.ToString();
            if ( cost / 1000 > 0)
            {
                cost = cost / 1000;
                costString = cost.ToString() + "K";
            }
            contentLines[i].GetChild(1).GetComponent<Text>().text = costString;
            //Weapon Name
            contentLines[i].GetChild(2).GetComponent<Text>().text = WeaponsToSell[i].Name.ToString();
            //Weapon Type
            contentLines[i].GetChild(3).GetComponent<Text>().text = WeaponsToSell[i].WeaponAttackType.ToString();
            //Weapon Fire Rate
            contentLines[i].GetChild(4).GetComponent<Text>().text = WeaponsToSell[i].FireRate.ToString();
            //Weapon Damage
            contentLines[i].GetChild(5).GetComponent<Text>().text = WeaponsToSell[i].WeaponDamage.ToString();
        }
    }
}
