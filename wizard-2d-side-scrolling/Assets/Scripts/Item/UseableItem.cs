using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Useable")]
public class UseableItem : ItemSO
{
    public UseableItem()
    {
        itemType = ItemType.Useable;
    }
}
