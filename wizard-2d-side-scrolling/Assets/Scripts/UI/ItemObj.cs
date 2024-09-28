using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemObj : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image img;
    [SerializeField] Button btn;

    [HideInInspector] public Transform parentAfterDrag;

    [HideInInspector] public ItemSO item;
    [HideInInspector] public int amount;

    public void SetupItem(ItemSO item, int amount)
    {
        this.item = item;
        this.amount = amount;
        img.sprite = item.itemIcon;
        UpdateAmount();
    }

    void UpdateAmount()
    {
        amountText.text = amount.ToString();
    }

    public void AddItemToSlot(int amount)
    {
        this.amount += amount;
        UpdateAmount();
    }

    public void RemoveItemInSlot(int amount)
    {
        this.amount -= amount;
        UpdateAmount();
        if (this.amount == 0)
        {
            DestroyItem();
        }
    }

    public void SetItemAmount(int amount)
    {
        this.amount = amount;
        UpdateAmount();
        if (this.amount == 0)
        {
            DestroyItem();
        }
    }

    void DestroyItem()
    {
        Destroy(gameObject);
        if (GameManager.Instance.player.curSelectStorage != null)
        {
            GameManager.Instance.player.curSelectStorage.UpdateStorage();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        img.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(GameManager.Instance.playerUI.itemRestPoint.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GameManager.Instance.player.mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        img.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

        if (GameManager.Instance.player.curSelectStorage != null)
        {
            GameManager.Instance.player.curSelectStorage.UpdateStorage();
        }
    }

}
