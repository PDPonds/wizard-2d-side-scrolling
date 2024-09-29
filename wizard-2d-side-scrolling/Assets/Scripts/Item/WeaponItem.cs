using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponItem : ItemSO
{

    [Header("- Delay")]
    public float attackDelay;

    [Header("- Damage")]
    public int minDamage;
    public int maxDamage;

    public WeaponItem()
    {
        itemType = ItemType.Weapon;
    }

    public int GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }

}
