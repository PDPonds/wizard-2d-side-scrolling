using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour, IInteractable
{
    public bool isCraftAlready;
    public CraftSlot craftSlot;

    [SerializeField] Transform craftParent;
    [SerializeField] SlotObj slot;
    [SerializeField] Button craftBTN;
    [SerializeField] Image craftIcon;
    [SerializeField] TextMeshProUGUI craftAmount;

    [HideInInspector] public Animator anim;
    SpriteRenderer spriteRen;

    List<StorageSlot> allStorageSlot = new List<StorageSlot>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();

        craftBTN.onClick.AddListener(Craft);

        UpdateChestColor();
    }

    private void Update()
    {
        if (slot.transform.childCount > 0)
        {
            ItemObj itemObj = slot.transform.GetChild(0).GetComponent<ItemObj>();
            craftAmount.text = $"{itemObj.amount} / {craftSlot.amount}";
        }
        else
        {
            craftAmount.text = $"00 / {craftSlot.amount}";
        }
    }

    public void UpdateStorage()
    {
        allStorageSlot.Clear();

        for (int i = 0; i < GameManager.Instance.playerUI.storageInventory.transform.childCount; i++)
        {
            Transform slot = GameManager.Instance.playerUI.storageInventory.transform.GetChild(i);
            if (slot.childCount > 0)
            {
                ItemObj itemObj = slot.GetChild(0).GetComponent<ItemObj>();
                ItemSO item = itemObj.item;
                int amount = itemObj.amount;
                int index = i;

                StorageSlot storageSlot = new StorageSlot();
                storageSlot.item = item;
                storageSlot.amount = amount;
                storageSlot.storageSlotIndex = index;

                allStorageSlot.Add(storageSlot);
            }
        }

    }

    public string GetInteractText()
    {
        if (isCraftAlready)
        {
            return $"[E] to Open";
        }
        else
        {

            return $"[E] to Craft";
        }
    }

    public void Interact()
    {
        if (isCraftAlready)
        {
            if (!GameManager.Instance.playerUI.mainInventory.activeSelf)
            {
                PlayerUI playerUI = GameManager.Instance.playerUI;
                playerUI.ToggleMainInventory();
                playerUI.storageInventory.gameObject.SetActive(true);

                PlayerManager player = GameManager.Instance.player;
                player.curChest = this;

                SpawnItemObj();

                anim.Play("Open");

            }
            else
            {
                GameManager.Instance.playerUI.ToggleMainInventory();
            }
        }
        else
        {
            if (craftParent.gameObject.activeSelf)
            {
                craftParent.gameObject.SetActive(false);
            }
            else
            {
                craftParent.gameObject.SetActive(true);
                craftIcon.sprite = craftSlot.item.itemIcon;
                craftAmount.text = craftSlot.amount.ToString();
            }
        }
    }

    void Craft()
    {
        if (slot.transform.childCount > 0)
        {
            ItemObj itemObj = slot.transform.GetChild(0).GetComponent<ItemObj>();
            if (itemObj.item == craftSlot.item &&
                itemObj.amount >= craftSlot.amount)
            {
                itemObj.RemoveItemInSlot(craftSlot.amount);
                isCraftAlready = true;
                if (slot.transform.childCount == 0)
                {
                    craftParent.gameObject.SetActive(false);
                }
                else
                {
                    craftBTN.gameObject.SetActive(false);
                    craftIcon.gameObject.SetActive(false);
                    craftAmount.gameObject.SetActive(false);
                }
            }
        }
    }

    void SpawnItemObj()
    {
        if (allStorageSlot.Count > 0)
        {
            for (int i = 0; i < allStorageSlot.Count; i++)
            {
                ItemSO item = allStorageSlot[i].item;
                int amount = allStorageSlot[i].amount;
                int index = allStorageSlot[i].storageSlotIndex;
                GameManager.Instance.GenerateItemObj(item, amount, GameManager.Instance.player.GetStorageSlot(index));
            }
        }
    }

    void UpdateChestColor()
    {
        if (isCraftAlready)
        {
            spriteRen.color = Color.white;
        }
        else
        {
            spriteRen.color = Color.cyan;
        }
    }

}

[Serializable]
public class CraftSlot
{
    public ItemSO item;
    public int amount;
}
