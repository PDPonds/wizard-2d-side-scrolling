using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour, IInteractable
{
    [Range(0, 30)]
    public int minSpawnItem;
    [Range(0, 30)]
    public int maxSpawnItem;

    [HideInInspector] public Animator anim;

    bool isFirstTimeOpen = true;
    List<StorageSlot> allStorageSlot = new List<StorageSlot>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void SpawnItem()
    {
        int spawnCount = GetSpawnCount();
        ItemSO item = GameManager.Instance.RandomItem();
        int amount = GameManager.Instance.RandomAmount();
        if (spawnCount > 0)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                StorageSlot slot = new StorageSlot();
                slot.item = item;
                slot.amount = amount;
                slot.storageSlotIndex = i;
                allStorageSlot.Add(slot);
            }
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
        return $"[E] to open storage";
    }

    public void Interact()
    {
        if (!GameManager.Instance.playerUI.mainInventory.activeSelf)
        {
            PlayerUI playerUI = GameManager.Instance.playerUI;
            playerUI.ToggleMainInventory();
            playerUI.storageInventory.gameObject.SetActive(true);

            PlayerManager player = GameManager.Instance.player;
            player.curSelectStorage = this;

            if (isFirstTimeOpen)
            {
                SpawnItem();
                SpawnItemObj();
            }
            else
            {
                SpawnItemObj();
            }

            anim.Play("Open");

            isFirstTimeOpen = false;
        }
        else
        {
            GameManager.Instance.playerUI.ToggleMainInventory();
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

    int GetSpawnCount()
    {
        return UnityEngine.Random.Range(minSpawnItem, maxSpawnItem);
    }

}

[Serializable]
public class StorageSlot
{
    public ItemSO item;
    public int amount;
    public int storageSlotIndex;
}
