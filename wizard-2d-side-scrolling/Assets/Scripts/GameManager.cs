using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Day
{
    Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
}

public enum TimeOfTheDay
{
    Morning, Afternoon, Evening
}

public class GameManager : Singleton<GameManager>
{
    [Header("===== Generate Game Prefab =====")]
    [Header("- Player")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject cameraPrefab;
    [Space(10)]
    [SerializeField] Transform spawnPoin;
    [HideInInspector] public PlayerManager player;
    [HideInInspector] public PlayerUI playerUI;

    [Header("===== Time =====")]
    [SerializeField] float startTimeSpeed;
    float curTimeSpeed;
    public int curDayCount;
    [HideInInspector] public Day curDay;
    [HideInInspector] public TimeOfTheDay curTimeOfTheDay;
    [HideInInspector] public int curHours;
    [HideInInspector] public int curMinute;
    [HideInInspector] public float curSec;

    [Header("===== Item Generator =====")]
    [SerializeField] List<ItemSO> allItems = new List<ItemSO>();
    [SerializeField] GameObject itemObjPrefab;
    [SerializeField] int minDrop;
    [SerializeField] int maxDrop;

    [Header("===== Parrallax =====")]
    [SerializeField] Parallax back;
    [SerializeField] Parallax mid;
    [SerializeField] Parallax Fornt;

    private void Awake()
    {
        InitPrefab();
        ResetTimeSpeed();
    }

    private void Update()
    {
        IncreaseTime();
    }

    void InitPrefab()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoin.position, Quaternion.identity);
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        this.player = playerManager;
        PlayerUI playerUI = player.GetComponent<PlayerUI>();
        this.playerUI = playerUI;
        GameObject camera = Instantiate(cameraPrefab, spawnPoin.position, Quaternion.identity);
        CameraController camCon = camera.GetComponent<CameraController>();
        camCon.SelectTarget(player.transform);
    }

    #region Time

    public void ResetTimeSpeed()
    {
        SetTimeSpeed(startTimeSpeed);
    }

    public void SetTimeSpeed(float speed)
    {
        curTimeSpeed = speed;
    }

    void IncreaseTime()
    {
        curSec += curTimeSpeed * Time.deltaTime;
        if (curSec >= 60)
        {
            curMinute++;
            if (player.IsPhase(PlayerPhase.Normal)) player.Heal(player.healPerMinute);

            if (curMinute >= 60)
            {
                curHours++;
                curMinute = 0;

                if (curHours % 8 == 0) SwitchTimeOfTheDay();

                if (curHours >= 24)
                {
                    curDayCount++;
                    playerUI.UpdateDayCount();
                    SwitchDay();
                    curHours = 0;
                }
            }
            curSec = 0;
        }
    }

    void SwitchDay()
    {
        switch (curDay)
        {
            case Day.Monday:
                curDay = Day.Tuesday;
                break;
            case Day.Tuesday:
                curDay = Day.Wednesday;
                break;
            case Day.Wednesday:
                curDay = Day.Thursday;
                break;
            case Day.Thursday:
                curDay = Day.Friday;
                break;
            case Day.Friday:
                curDay = Day.Saturday;
                break;
            case Day.Saturday:
                curDay = Day.Sunday;
                break;
            case Day.Sunday:
                curDay = Day.Monday;
                break;
        }
        playerUI.UpdateDay();
    }

    void SwitchTimeOfTheDay()
    {
        switch (curTimeOfTheDay)
        {
            case TimeOfTheDay.Morning:
                curTimeOfTheDay = TimeOfTheDay.Afternoon;
                break;
            case TimeOfTheDay.Afternoon:
                curTimeOfTheDay = TimeOfTheDay.Evening;
                break;
            case TimeOfTheDay.Evening:
                curTimeOfTheDay = TimeOfTheDay.Morning;
                break;
        }
        playerUI.UpdateTimeOfDayText();
    }

    #endregion

    #region ItemObj Generator

    public void GenerateItemObj(ItemSO itemSO, int amount, Transform slot)
    {
        GameObject obj = Instantiate(itemObjPrefab, slot);
        ItemObj itemObj = obj.GetComponent<ItemObj>();
        if (amount <= itemSO.maxStack)
            itemObj.SetupItem(itemSO, amount);
        else
            itemObj.SetupItem(itemSO, itemSO.maxStack);
    }

    public ItemSO RandomItem()
    {
        return allItems[Random.Range(0, allItems.Count)];
    }

    public int RandomAmount()
    {
        return Random.Range(minDrop, maxDrop);
    }

    #endregion

}
