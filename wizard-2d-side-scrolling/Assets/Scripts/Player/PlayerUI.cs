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
    [SerializeField] GameObject mainInventory;
    public Transform itemRestPoint;

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
            storageInventory.SetActive(false);
            playerManager.curSelectStorage = null;
            DestroyItemObjInStorage();
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

}
