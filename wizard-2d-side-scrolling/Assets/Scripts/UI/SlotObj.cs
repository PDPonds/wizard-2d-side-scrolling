using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotObj : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            ItemObj item = eventData.pointerDrag.GetComponent<ItemObj>();
            item.parentAfterDrag = transform;
        }
        else if (transform.childCount > 0)
        {
            ItemObj item = eventData.pointerDrag.GetComponent<ItemObj>();
            ItemObj inSlotItem = transform.GetChild(0).GetComponent<ItemObj>();
            if (item.item == inSlotItem.item)
            {
                int itemAmount = item.amount;
                int inSlotAmount = inSlotItem.amount;
                int maxStack = item.item.maxStack;
                if (itemAmount + inSlotAmount > maxStack)
                {
                    int canAddAmount = maxStack - inSlotAmount;
                    item.RemoveItemInSlot(canAddAmount);
                    inSlotItem.SetItemAmount(maxStack);
                }
                else
                {
                    inSlotItem.AddItemToSlot(itemAmount);
                    item.RemoveItemInSlot(itemAmount);
                }
            }
            else
            {
                item.parentAfterDrag = transform;
                inSlotItem.parentAfterDrag = item.parentAfterDrag;
                inSlotItem.transform.SetParent(inSlotItem.parentAfterDrag);

            }
        }
    }
}
