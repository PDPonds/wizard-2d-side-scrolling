using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class WeaponItem : ItemSO
{
    [Header("- Prefab")]
    public GameObject bulletPrefab;

    [Header("- Speed")]
    public float bulletSpeed;

    [Header("- Delay")]
    public float attackDelay;

    [Header("- Damage")]
    public int minDamage;
    public int maxDamage;

    [Header("- Bullet Duration")]
    public float bulletDuration;

    public WeaponItem()
    {
        itemType = ItemType.Weapon;
    }

    public int GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }

}
