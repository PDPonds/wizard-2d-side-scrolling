using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    PlayerManager playerManager;

    [Header("===== Time =====")]
    [SerializeField] TextMeshProUGUI hoursText;
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI dayCountText;
    [SerializeField] TextMeshProUGUI timeOfDayText;

    [Header("===== Interact =====")]
    [SerializeField] GameObject interactText;

    [Header("===== Inventory =====")]
    public GameObject mainInventory;
    public Transform itemRestPoint;

    [Header("===== Inventory Bar =====")]
    [SerializeField] GameObject slotBorderParent;
    [SerializeField] GameObject selectBorderPrefab;
    [SerializeField] GameObject slot1;
    [SerializeField] GameObject slot2;
    [SerializeField] GameObject slot3;
    [SerializeField] GameObject slot4;
    [SerializeField] GameObject slot5;
    [SerializeField] GameObject slot6;
    [SerializeField] GameObject slot7;
    [SerializeField] GameObject slot8;
    [SerializeField] GameObject slot9;
    [HideInInspector] public SlotObj curSlotSelected;
    GameObject slotBorder;

    [Header("===== Storage =====")]
    public GameObject storageInventory;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();

        UpdateDay();
        UpdateDayCount();
        UpdateTimeOfDayText();
    }

    private void Update()
    {
        UpdateHours();
    }

    #region Interact
    public void SetInteractText(string text)
    {
        TextMeshProUGUI textM = interactText.GetComponent<TextMeshProUGUI>();
        textM.text = text;
    }

    public void ShowInteractText()
    {
        interactText.SetActive(true);
    }

    public void HideInteractText()
    {
        interactText.SetActive(false);
    }
    #endregion

    #region Time

    public void UpdateHours()
    {
        hoursText.text = $"Hours : {GameManager.Instance.curHours} : {GameManager.Instance.curMinute} : {GameManager.Instance.curSec}";
    }

    public void UpdateDayCount()
    {
        dayCountText.text = $"Day : {GameManager.Instance.curDayCount}";
    }

    public void UpdateDay()
    {
        dayText.text = $"Day : {GameManager.Instance.curDay}";
    }

    public void UpdateTimeOfDayText()
    {
        timeOfDayText.text = $"{GameManager.Instance.curTimeOfTheDay}";
    }

    #endregion

    #region Inventory

    public void ToggleMainInventory()
    {
        if (mainInventory.activeSelf)
        {
            mainInventory.SetActive(false);
            if (playerManager.curSelectStorage != null)
            {
                storageInventory.SetActive(false);
                playerManager.curSelectStorage.anim.Play("Close");
                playerManager.curSelectStorage = null;
                DestroyItemObjInStorage();
            }
            playerManager.SwitchBehavior(PlayerBehavior.Normal);
        }
        else
        {
            mainInventory.SetActive(true);
            playerManager.SwitchBehavior(PlayerBehavior.UIShowing);
        }
    }

    void DestroyItemObjInStorage()
    {
        for (int i = 0; i < storageInventory.transform.childCount; i++)
        {
            Transform slot = storageInventory.transform.GetChild(i);
            if (slot.childCount > 0)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }
    }

    #endregion

    #region InventoryBar

    public void SelectSlotBar(int index)
    {
        switch (index)
        {
            case 1:
                SlotObj slotObj1 = slot1.GetComponent<SlotObj>();
                curSlotSelected = slotObj1;
                break;
            case 2:
                SlotObj slotObj2 = slot2.GetComponent<SlotObj>();
                curSlotSelected = slotObj2;
                break;
            case 3:
                SlotObj slotObj3 = slot3.GetComponent<SlotObj>();
                curSlotSelected = slotObj3;
                break;
            case 4:
                SlotObj slotObj4 = slot4.GetComponent<SlotObj>();
                curSlotSelected = slotObj4;
                break;
            case 5:
                SlotObj slotObj5 = slot5.GetComponent<SlotObj>();
                curSlotSelected = slotObj5;
                break;
            case 6:
                SlotObj slotObj6 = slot6.GetComponent<SlotObj>();
                curSlotSelected = slotObj6;
                break;
            case 7:
                SlotObj slotObj7 = slot7.GetComponent<SlotObj>();
                curSlotSelected = slotObj7;
                break;
            case 8:
                SlotObj slotObj8 = slot8.GetComponent<SlotObj>();
                curSlotSelected = slotObj8;
                break;
            case 9:
                SlotObj slotObj9 = slot9.GetComponent<SlotObj>();
                curSlotSelected = slotObj9;
                break;
            default:
                SlotObj slotObj = slot1.GetComponent<SlotObj>();
                curSlotSelected = slotObj;
                break;
        }

        InitSelectSlotBarBorder();
    }

    void InitSelectSlotBarBorder()
    {
        if(slotBorder != null) Destroy(slotBorder.gameObject);

        GameObject border = Instantiate(selectBorderPrefab);
        border.transform.SetParent(slotBorderParent.transform);

        slotBorder = border;

        RectTransform curSlotRect = curSlotSelected.GetComponent<RectTransform>();

        RectTransform rectTransform = border.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = curSlotRect.localPosition + new Vector3(0, -50, 0);
    }

    #endregion

}
