using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Placeable")]
public class PlaceableItem : ItemSO
{
    public PlaceableItem()
    {
        itemType = ItemType.Placeable;
    }
}
