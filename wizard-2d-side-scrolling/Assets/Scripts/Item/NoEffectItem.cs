using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/NoEffect")]
public class NoEffectItem : ItemSO
{
    public NoEffectItem()
    {
        itemType = ItemType.NoEffect;
    }
}
