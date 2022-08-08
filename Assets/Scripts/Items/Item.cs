using UnityEngine;

public abstract class Item : ScriptableObject
{

    public string Name;
    public Sprite image;

    public enum eItemType
    {
        None,
        Weapon,
        Consumable
    }

    public eItemType ItemType;

}
