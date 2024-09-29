using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponItem : ItemSO
{
    public WeaponItem()
    {
        itemType = ItemType.Weapon;
    }

}
